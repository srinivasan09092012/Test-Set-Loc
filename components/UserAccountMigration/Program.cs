//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//--------------------------------------------------------------------------------------------------
using FileHelpers;
using HPE.HSP.UA3.Core.API.IdentityManagement.Providers;
using HPE.HSP.UA3.Core.API.Logger.Loggers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using UserAccountMigration.Domain;
using UserAccountMigration.Providers;
using UserAccountMigration.Utilities;
using api = HPE.HSP.UA3.Core.API.IdentityManagement.Interfaces.Domain;

namespace UserAccountMigration
{
    public class Program
    {
        private static ProcessConfig currentProcess = null;
        private static int processCount = 0;
        private static CoreLogger logger = new CoreLogger();
        private static MigrationConfig migrationConfig = null;
        private static ActiveDirectoryProvider adProvider;
        private static ActiveDirectoryQueryProvider adQueryProvider;
        private static UserServiceProvider userServiceProvider;
        private static UserQueryServiceProvider userQueryServiceProvider;
        private static List<UserAccountError> userAccountErrors = null;
        private static List<UserXrefError> userXrefErrors = null;
        private static List<DelegateUserError> delegateUserErrors = null;
        private static List<UserProfile> userProfiles = new List<UserProfile>();
        private static List<UserProfile> delegateProfiles = new List<UserProfile>();
        private static Stopwatch timer = new Stopwatch();

        static void Main(string[] args)
        {
            try
            {
                Thread.CurrentThread.Name = "000";
                timer.Start();
                LogMessage(0, "Process started");
                LoadConfigration();
                InitializeADProviders();
                InitializeServiceProviders();
                ProcessMigration();
                LogMessage(0, "Process complete");
            }
            catch (Exception ex)
            {
                LogMessage(0, "Process halted with fatal exception: ", ex);
            }
            finally
            {
                timer.Stop();
                ReportCounts();
                Console.Write("Press any key to close");
                Console.ReadKey();
            }
        }

        private static void AddUserAccount(UserAccount userAccount, bool processAD, bool processProfile)
        {
            userAccount.Validate();
            LogMessage(1, string.Format("Adding user '{0}'", userAccount.UserName));

            if (processAD)
            {
                api.UserAccount adUserAccount = ConvertAdUserAccount(userAccount);
                adProvider.AddUser(adUserAccount);
            }

            if (processProfile)
            {
                ProcessUserProfile(userAccount);
            }
        }

        private static api.UserAccount ConvertAdUserAccount(UserAccount userAccount)
        {
            List<string> groups = new List<string>(userAccount.Groups.Split('|'));
            api.UserAccount adUserAccount = new api.UserAccount()
            {
                Identity = new api.UserIdentity()
                {
                    DisplayName = userAccount.FirstName + " " + userAccount.LastName,
                    EmailAddress = userAccount.EmailAddress,
                    FirstName = userAccount.FirstName,
                    LastName = userAccount.LastName,
                    MiddleName = userAccount.MiddleName,
                    PhoneNumber = userAccount.PhoneNumber,
                    UserName = userAccount.UserName
                },
                Groups = groups,
                IsEnabled = true,
                RegistrationQualifiers = new List<api.RegistrationQualifier>()
                    {
                        new api.RegistrationQualifier("BirthDate", userAccount.BirthDate.Value.ToString("yyyyMMdd")),
                        new api.RegistrationQualifier("Last4SSN", userAccount.Last4SSN)
                    },
                Password = userAccount.Password
            };

            return adUserAccount;
        }

        private static UserAccount ConvertUserAccount(api.UserAccount adUserAccount)
        {
            UserAccount userAccount = new UserAccount();
            if (adUserAccount != null && adUserAccount.Identity != null)
            {
                DateTime birthDate = DateTime.MinValue;
                string last4SSN = string.Empty;
                if (adUserAccount.RegistrationQualifiers != null && adUserAccount.RegistrationQualifiers.Count > 0)
                {
                    api.RegistrationQualifier qualifier = adUserAccount.RegistrationQualifiers.Find(rq => rq.Key == "BirthDate");
                    if (qualifier != null)
                    {
                        if (qualifier.Value.Length == 8)
                        {
                            birthDate = new DateTime(
                                Convert.ToInt32(qualifier.Value.Substring(0, 4)),
                                Convert.ToInt32(qualifier.Value.Substring(4, 2)),
                                Convert.ToInt32(qualifier.Value.Substring(6, 2)));
                        }
                    }

                    qualifier = adUserAccount.RegistrationQualifiers.Find(rq => rq.Key == "Last4SSN");
                    if (qualifier != null)
                    {
                        last4SSN = qualifier.Value;
                    }
                }

                string groups = string.Empty;
                foreach (string adGroup in adUserAccount.Groups)
                {
                    groups += adGroup + "|";
                }
                if (groups.Length > 0)
                {
                    groups = groups.Substring(0, groups.Length - 1);
                }

                userAccount = new UserAccount()
                {
                    BirthDate = birthDate,
                    EmailAddress = adUserAccount.Identity.EmailAddress,
                    FirstName = adUserAccount.Identity.FirstName,
                    Groups = groups,
                    Last4SSN = last4SSN,
                    LastName = adUserAccount.Identity.LastName,
                    MiddleName = adUserAccount.Identity.MiddleName,
                    Password = adUserAccount.Password,
                    PhoneNumber = adUserAccount.Identity.PhoneNumber,
                    UserName = adUserAccount.Identity.UserName
                };
            }

            return userAccount;
        }

        private static string FormatExportFileName(ProcessConfig process)
        {
            string fileName = string.Empty;

            string filePath = process.FilePath;
            if (!filePath.EndsWith("\\"))
            {
                filePath += "\\";
            }

            fileName = process.FilePath + process.FileName;
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            if (File.Exists(fileName))
            {
                throw new ApplicationException(string.Format("File '{0}' already exists. Delete file and try again.", fileName));
            }

            return fileName;
        }

        private static string FormatImportErrorFileName(ProcessConfig process)
        {
            string fileName = string.Empty;

            string filePath = process.FilePath;
            if (!filePath.EndsWith("\\"))
            {
                filePath += "\\";
            }

            fileName = process.FilePath +
                process.FileName.Substring(0, process.FileName.LastIndexOfAny(new char[] { '.' })) + "-Errors" + process.FileName.Substring(process.FileName.LastIndexOfAny(new char[] { '.' })) +
                string.Format("-{0}", DateTime.Now.ToString("yyyyMMddhhmmss"));
            if (!Directory.Exists(filePath))
            {
                throw new ApplicationException(string.Format("File path '{0}' does not exist. Correct file path and try again.", filePath));
            }

            if (File.Exists(fileName))
            {
                throw new ApplicationException(string.Format("File name '{0}' already exists. Delete file and try again.", fileName));
            }

            return fileName;
        }

        private static string FormatImportDelegateErrorFileName(ProcessConfig process)
        {
            string fileName = string.Empty;

            if (!string.IsNullOrEmpty(process.DelegateFileName))
            {
                string filePath = process.FilePath;
                if (!filePath.EndsWith("\\"))
                {
                    filePath += "\\";
                }

                fileName = process.FilePath +
                    process.DelegateFileName.Substring(0, process.FileName.LastIndexOfAny(new char[] { '.' })) + "-Errors" + process.DelegateFileName.Substring(process.DelegateFileName.LastIndexOfAny(new char[] { '.' })) +
                    string.Format("-{0}", DateTime.Now.ToString("yyyyMMddhhmmss"));
                if (!Directory.Exists(filePath))
                {
                    throw new ApplicationException(string.Format("File path '{0}' does not exist. Correct file path and try again.", filePath));
                }

                if (File.Exists(fileName))
                {
                    throw new ApplicationException(string.Format("File name '{0}' already exists. Delete file and try again.", fileName));
                }
            }

            return fileName;
        }

        private static string FormatImportFileName(ProcessConfig process)
        {
            string fileName = string.Empty;

            string filePath = process.FilePath;
            if (!filePath.EndsWith("\\"))
            {
                filePath += "\\";
            }

            fileName = process.FilePath + process.FileName;
            if (!Directory.Exists(filePath))
            {
                throw new ApplicationException(string.Format("File path '{0}' does not exist. Correct file path and try again.", filePath));
            }

            if (!File.Exists(fileName))
            {
                throw new ApplicationException(string.Format("File name '{0}' does not exist. Correct file name and try again.", fileName));
            }

            return fileName;
        }


        private static string FormatImportDelegateFileName(ProcessConfig process)
        {
            string fileName = string.Empty;

            if (!string.IsNullOrEmpty(process.DelegateFileName))
            {
                string filePath = process.FilePath;
                if (!filePath.EndsWith("\\"))
                {
                    filePath += "\\";
                }

                fileName = process.FilePath + process.DelegateFileName;
                if (!Directory.Exists(filePath))
                {
                    throw new ApplicationException(string.Format("Delegate file path '{0}' does not exist. Correct delegate file path and try again.", filePath));
                }

                if (!File.Exists(fileName))
                {
                    throw new ApplicationException(string.Format("Delegate file name '{0}' does not exist. Correct delegate file name and try again.", fileName));
                }
            }

            return fileName;
        }
        private static void ImportAccount(object threadAccountData)
        {
            List<UserAccount> userAccounts = new List<UserAccount>(threadAccountData as IEnumerable<UserAccount>);
            foreach (UserAccount userAccount in userAccounts)
            {
                try
                {
                    api.UserAccount adUserAccount = adQueryProvider.GetUser(userAccount.UserName);
                    if (adUserAccount != null)
                    {
                        UpdateUserAccount(userAccount, adUserAccount.AccountExpirationDate, currentProcess.ProcessUserProfile);
                    }
                    else
                    {
                        AddUserAccount(userAccount, currentProcess.ProcessAD, currentProcess.ProcessUserProfile);
                    }

                    lock (currentProcess)
                    {
                        currentProcess.ProcessedCount++;
                    }
                }
                catch (Exception ex)
                {
                    UserAccountError userAccountError = new UserAccountError(userAccount, ex.Message);
                    LogMessage(1, string.Format("Error processing user '{0}' with error: '{1}'", userAccount.UserName, ex.Message));
                    lock (userAccountErrors)
                    {
                        userAccountErrors.Add(userAccountError);
                    }

                    lock (currentProcess)
                    {
                        currentProcess.ErroredCount++;
                    }
                }
            }
        }

        private static void ImportXref(object threadAccountData)
        {
            List<UserXref> userXrefs = new List<UserXref>(threadAccountData as IEnumerable<UserXref>);
            foreach (UserXref userXref in userXrefs)
            {
                try
                {
                    userXref.Validate(delegateProfiles.Count());
                    List<UserProfile> delegateList = new List<UserProfile>();
                    UserProfile primaryUser = GetUserPofileId(userXref.PrimaryUserName, userXref);
                    if (string.IsNullOrEmpty(userXref.SecondaryUserName))
                    {
                        delegateList = delegateProfiles;
                    }
                    else
                    {
                        UserProfile secondaryUser = GetUserPofileId(userXref.SecondaryUserName, userXref);
                        if (secondaryUser != null)
                        {
                            delegateList.Add(secondaryUser);
                        }
                    }

                    if (primaryUser != null && delegateList.Any())
                    {
                        foreach (UserProfile del in delegateList)
                        {
                            try
                            {
                                userXref.SecondaryUserName = del.UserName;
                                string userXrefId = string.Empty;
                                string userXrefAssocId = string.Empty;
                                bool identicalXrefExists = false;

                                userQueryServiceProvider.GetUserXref(userXref, ref userXrefId, ref userXrefAssocId, ref identicalXrefExists);

                                if (identicalXrefExists)
                                {
                                    lock (currentProcess)
                                    {
                                        currentProcess.DuplicateCount++;
                                    }
                                }
                                else if (string.IsNullOrEmpty(userXrefId))
                                {
                                    userServiceProvider.AddProfileXref(userXref, primaryUser.ProfileId.ToString(), del.ProfileId.ToString(), del.RelationshipCode);
                                    lock (currentProcess)
                                    {
                                        currentProcess.ProcessedCount++;
                                    }
                                }
                                else
                                {
                                    userServiceProvider.UpdateProfileXref(userXref, userXrefId, userXrefAssocId);
                                    lock (currentProcess)
                                    {
                                        currentProcess.UpdatedCount++;
                                    }
                                }
                            }
                            catch (FaultException<UserService.BusinessValidationException> ex)
                            {
                                string msg = ex.Detail.BusinessMessages.Count > 0 ? ex.Detail.BusinessMessages[0].MessageDefault : ex.Message;
                                UserXrefError userXrefError = new UserXrefError(userXref, msg);
                                LogMessage(1, string.Format("Error processing primary user '{0}' secondary user '{1}' association '{2}' with error: '{3}'", userXref.PrimaryUserName, del.UserName, userXref.AssociationId, msg));
                                lock (userXrefErrors)
                                {
                                    userXrefErrors.Add(userXrefError);
                                }

                                lock (currentProcess)
                                {
                                    currentProcess.ErroredCount++;
                                }
                            }
                            catch (Exception ex)
                            {
                                UserXrefError userXrefError = new UserXrefError(userXref, ex.Message);
                                LogMessage(1, string.Format("Error processing primary user '{0}' secondary user '{1}' association '{2}' with error: '{3}'", userXref.PrimaryUserName, del.UserName, userXref.AssociationId, ex.Message));
                                lock (userXrefErrors)
                                {
                                    userXrefErrors.Add(userXrefError);
                                }

                                lock (currentProcess)
                                {
                                    currentProcess.ErroredCount++;
                                }
                            }
                        }
                    }
                }
                catch (FaultException<UserService.ServiceException> ex)
                {
                    UserXrefError userXrefError = new UserXrefError(userXref, ex.Message);
                    LogMessage(1, string.Format("Error processing primary user '{0}' secondary user '{1}' association '{2}' with error: '{3}'", userXref.PrimaryUserName, userXref.SecondaryUserName, userXref.AssociationId, ex.Message));
                    lock (userXrefErrors)
                    {
                        userXrefErrors.Add(userXrefError);
                    }

                    lock (currentProcess)
                    {
                        currentProcess.ErroredCount++;
                    }
                }
                catch (FaultException<UserService.BusinessValidationException> ex)
                {
                    UserXrefError userXrefError = new UserXrefError(userXref, ex.Message);
                    LogMessage(1, string.Format("Error processing primary user '{0}' secondary user '{1}' association '{2}' with error: '{3}'", userXref.PrimaryUserName, userXref.SecondaryUserName, userXref.AssociationId, ex.Message));
                    lock (userXrefErrors)
                    {
                        userXrefErrors.Add(userXrefError);
                    }

                    lock (currentProcess)
                    {
                        currentProcess.ErroredCount++;
                    }
                }
                catch (Exception ex)
                {
                    UserXrefError userXrefError = new UserXrefError(userXref, ex.Message);
                    LogMessage(1, string.Format("Error processing primary user '{0}' secondary user '{1}' association '{2}' with error: '{3}'", userXref.PrimaryUserName, userXref.SecondaryUserName, userXref.AssociationId, ex.Message));
                    lock (userXrefErrors)
                    {
                        userXrefErrors.Add(userXrefError);
                    }

                    lock (currentProcess)
                    {
                        currentProcess.ErroredCount++;
                    }
                }
            }
        }

        private static void InitializeADProviders()
        {
            LogMessage(0, "Initializing AD providers");
            adProvider = new ActiveDirectoryProvider(
                migrationConfig.Environment.ADServer,
                migrationConfig.Environment.ADUser,
                migrationConfig.Environment.ADPassword,
                migrationConfig.Environment.ADContainer);

            adQueryProvider = new ActiveDirectoryQueryProvider(
                migrationConfig.Environment.ADServer,
                migrationConfig.Environment.ADUser,
                migrationConfig.Environment.ADPassword,
                migrationConfig.Environment.ADContainer);
        }

        private static void InitializeServiceProviders()
        {
            LogMessage(0, "Initializing service providers");
            userServiceProvider = new UserServiceProvider(migrationConfig.Environment);
            userQueryServiceProvider = new UserQueryServiceProvider(migrationConfig.Environment);
        }

        private static void LoadDelegates(List<DelegateUser> delegateUsers)
        {
            foreach (DelegateUser del in delegateUsers)
            {
                try
                {
                    del.Validate();
                    if (delegateProfiles.Any<UserProfile>(d => d.UserName.ToUpper() == del.UserName.ToUpper()))
                    {
                        string msg = string.Format("Error processing delegate '{0}': delegate already in the list.", del.UserName);
                        DelegateUserError delegateUserError = new DelegateUserError(del, msg);
                        LogMessage(1, msg);
                        lock (delegateUserErrors)
                        {
                            delegateUserErrors.Add(delegateUserError);
                        }

                        lock (currentProcess)
                        {
                            currentProcess.ErroredCount++;
                        }
                    }
                    else
                    { 
                        UserProfile profile = userQueryServiceProvider.LoadUserProfile(del.UserName);
                        if (profile != null)
                        {
                            delegateProfiles.Add(profile);
                        }
                        else
                        {
                            string msg = string.Format("Error processing delegate '{0}': user profile not found", del.UserName);
                            DelegateUserError delegateUserError = new DelegateUserError(del, msg);
                            LogMessage(1, msg);
                            lock (delegateUserErrors)
                            {
                                delegateUserErrors.Add(delegateUserError);
                            }

                            lock (currentProcess)
                            {
                                currentProcess.ErroredCount++;
                            }
                        }
                    }
                }
                catch (FaultException<UserService.ServiceException> ex)
                {
                    DelegateUserError delegateUserError = new DelegateUserError(del, ex.Message);
                    LogMessage(1, string.Format("Error processing delegate user '{0}' with error: '{1}'", del.UserName, ex.Message));
                    lock (delegateUserErrors)
                    {
                        delegateUserErrors.Add(delegateUserError);
                    }

                    lock (currentProcess)
                    {
                        currentProcess.ErroredCount++;
                    }
                }
                catch (FaultException<UserService.BusinessValidationException> ex)
                {
                    DelegateUserError delegateUserError = new DelegateUserError(del, ex.Message);
                    LogMessage(1, string.Format("Error processing delegate user '{0}' with error: '{1}'", del.UserName, ex.Message));
                    lock (delegateUserErrors)
                    {
                        delegateUserErrors.Add(delegateUserError);
                    }

                    lock (currentProcess)
                    {
                        currentProcess.ErroredCount++;
                    }
                }
                catch (Exception ex)
                {
                    DelegateUserError delegateUserError = new DelegateUserError(del, ex.Message);
                    LogMessage(1, string.Format("Error processing delegate user '{0}' with error: '{1}'", del.UserName, ex.Message));
                    lock (delegateUserErrors)
                    {
                        delegateUserErrors.Add(delegateUserError);
                    }

                    lock (currentProcess)
                    {
                        currentProcess.ErroredCount++;
                    }
                }
            }
        }

        private static void LoadConfigration()
        {
            LogMessage(0, "Loading configuration");
            string filePath = AppDomain.CurrentDomain.BaseDirectory + Constants.ConfigFile;
            if (File.Exists(filePath))
            {
                string fileContents = File.ReadAllText(filePath);
                migrationConfig = Serializer.XmlDeserialize<MigrationConfig>(fileContents, string.Empty);
                LogMessage(0, "Validating configuration");
                migrationConfig.Validate();
            }
            else
            {
                throw new ApplicationException(string.Format("Configuration file '{0}' does not exist", filePath));
            }
        }

        private static void LogBreak()
        {
            Console.WriteLine("");
        }

        private static void LogMessage(int level, string msg)
        {
            if (!string.IsNullOrWhiteSpace(msg))
            {
                string dateTimestampThreadName = DateTime.Now.ToString("hh:mm:ss:ff") + "|" + Thread.CurrentThread.Name + "|";
                string tabs = new string(' ', level * 2);
                logger.LogInformational(dateTimestampThreadName + tabs + msg);
                Console.WriteLine(dateTimestampThreadName + tabs + msg);
            }
        }

        private static void LogMessage(int level, string msg, Exception ex)
        {
            if (!string.IsNullOrEmpty(msg) || ex != null)
            {
                string dateTimestampThreadName = DateTime.Now.ToString("hh:mm:ss:ff") + "|" + Thread.CurrentThread.Name + "|";
                string tabs = new string(' ', level * 2);
                logger.LogFatal(dateTimestampThreadName + tabs + msg, ex);
                Console.WriteLine(dateTimestampThreadName + tabs + msg + ex.Message);
            }
        }

        private static void ProcessExport(Domain.Environment environment)
        {
            string exportFile = FormatExportFileName(currentProcess);

            try
            {
                LogMessage(1, string.Format("Querying users in AD matching Group '{0}'", currentProcess.UserGroupFilter));
                List<api.UserAccount> adUserAccounts = adQueryProvider.GetActiveUsersInGroup(currentProcess.UserGroupFilter);

                LogMessage(1, "Processing user accounts for export");
                List<UserAccount> userAccounts = new List<UserAccount>();
                foreach (api.UserAccount adUserAccount in adUserAccounts)
                {
                    LogMessage(2, string.Format("Processing user ID '{0}'", adUserAccount.Identity.UserName));
                    UserAccount userAccount = ConvertUserAccount(adUserAccount);
                    if (currentProcess.ProcessUserProfile)
                    {
                        LogMessage(2, string.Format("Querying profile for user ID '{0}'", adUserAccount.Identity.UserName));
                        UserProfile userProfile = userQueryServiceProvider.LoadUserProfile(adUserAccount.Identity.UserName);
                        if (userProfile != null)
                        {
                            userAccount.GeneralId = userProfile.GeneralId;
                        }
                    }

                    userAccounts.Add(userAccount);
                    currentProcess.ProcessedCount++;
                }

                LogMessage(1, string.Format("Creating export file '{0}'", exportFile));
                FileHelperEngine<UserAccount> fileEngine = new FileHelperEngine<UserAccount>();
                fileEngine.HeaderText = fileEngine.GetFileHeader();
                fileEngine.WriteFile(exportFile, userAccounts);
                currentProcess.ProcessedStatus = ProcessConfig.ProcessStatusType.Complete;
            }
            catch(Exception)
            {
                currentProcess.ProcessedStatus = ProcessConfig.ProcessStatusType.Error;
                throw;
            }
        }

        private static void ProcessImport(Domain.Environment environment)
        {
            string importFile = FormatImportFileName(currentProcess);
            string errorFile = FormatImportErrorFileName(currentProcess);

            try
            {
                LogMessage(1, string.Format("Loading users from import file '{0}'", importFile));
                FileHelperEngine<UserAccount> fileEngine = new FileHelperEngine<UserAccount>();
                List<UserAccount> userAccounts = new List<UserAccount>(fileEngine.ReadFile(importFile));
                LogMessage(1, string.Format("{0} user accounts loaded", userAccounts.Count.ToString("#,##0")));
                if (userAccounts != null && userAccounts.Count > 0)
                {
                    StartImportThreads(userAccounts, currentProcess.MaxThreads);
                }
                else
                {
                    LogMessage(1, string.Format("No records to process."));
                }

                currentProcess.ProcessedStatus = ProcessConfig.ProcessStatusType.Complete;

                if (userAccountErrors != null & userAccountErrors.Count > 0)
                {
                    userAccountErrors.Sort(delegate (UserAccountError a1, UserAccountError a2) { return a1.UserName.CompareTo(a2.UserName); });
                    FileHelperEngine<UserAccountError> fileErrorEngine = new FileHelperEngine<UserAccountError>();
                    fileErrorEngine.HeaderText = fileErrorEngine.GetFileHeader();
                    fileErrorEngine.WriteFile(errorFile, userAccountErrors);
                }
            }
            catch (Exception)
            {
                currentProcess.ProcessedStatus = ProcessConfig.ProcessStatusType.Error;
                throw;
            }
        }

        private static void ProcessImportXref(Domain.Environment environment)
        {
            string importFile = FormatImportFileName(currentProcess);
            string importDelegateFile = FormatImportDelegateFileName(currentProcess);
            string errorFile = FormatImportErrorFileName(currentProcess);
            string errorDelegateFile = FormatImportDelegateErrorFileName(currentProcess);
            delegateProfiles = new List<UserProfile>();

            try
            {
                LogMessage(1, string.Format("Loading user xrefs from import file '{0}'", importFile));
                FileHelperEngine<UserXref> fileEngine = new FileHelperEngine<UserXref>();
                List<UserXref> userXrefs = new List<UserXref>(fileEngine.ReadFile(importFile));
                LogMessage(1, string.Format("{0} user xrefs loaded", userXrefs.Count.ToString("#,##0")));
                List<DelegateUser> delegates = null;
                if (!string.IsNullOrEmpty(importDelegateFile))
                {
                    LogMessage(1, string.Format("Loading delegates from import file '{0}'", importDelegateFile));
                    FileHelperEngine<DelegateUser> delFileEngine = new FileHelperEngine<DelegateUser>();
                    delegates = new List<DelegateUser>(delFileEngine.ReadFile(importDelegateFile));
                    LogMessage(1, string.Format("{0} delegates loaded", delegates.Count.ToString("#,##0")));
                    LoadDelegates(delegates);
                }

                if (delegateUserErrors != null & delegateUserErrors.Count > 0)
                {
                    delegateUserErrors.Sort(delegate (DelegateUserError a1, DelegateUserError a2) { return a1.UserName.CompareTo(a2.UserName); });
                    FileHelperEngine<DelegateUserError> fileErrorEngine = new FileHelperEngine<DelegateUserError>();
                    fileErrorEngine.HeaderText = fileErrorEngine.GetFileHeader();
                    fileErrorEngine.WriteFile(errorDelegateFile, delegateUserErrors);
                }
                else if (userXrefs != null && userXrefs.Count > 0)
                {
                    StartImportThreads(userXrefs, currentProcess.MaxThreads);
                }
                else
                {
                    LogMessage(1, string.Format("No records to process."));
                }

                currentProcess.ProcessedStatus = ProcessConfig.ProcessStatusType.Complete;

                if (userXrefErrors != null & userXrefErrors.Count > 0)
                {
                    userXrefErrors.OrderBy(e => e.PrimaryUserName).ThenBy(e => e.SecondaryUserName);
                    FileHelperEngine<UserXrefError> fileErrorEngine = new FileHelperEngine<UserXrefError>();
                    fileErrorEngine.HeaderText = fileErrorEngine.GetFileHeader();
                    fileErrorEngine.WriteFile(errorFile, userXrefErrors);
                }
            }
            catch (Exception e)
            {
                currentProcess.ProcessedStatus = ProcessConfig.ProcessStatusType.Error;
                throw;
            }
        }

        private static void ProcessMigration()
        {
            foreach (ProcessConfig process in migrationConfig.Processes)
            {
                currentProcess = process;
                userAccountErrors = new List<UserAccountError>();
                userXrefErrors = new List<UserXrefError>();
                delegateUserErrors = new List<DelegateUserError>();
                try
                {
                    LogMessage(0, string.Format("Migration started for process '{0}'", process.Name));
                    switch (process.ProcessType)
                    {
                        case ProcessConfig.ProcessTypes.Export:
                            ProcessExport(migrationConfig.Environment);
                            break;

                        case ProcessConfig.ProcessTypes.Import:
                            ProcessImport(migrationConfig.Environment);
                            break;

                        case ProcessConfig.ProcessTypes.ImportXref:
                            ProcessImportXref(migrationConfig.Environment);
                            break;

                        default:
                            throw new ApplicationException(string.Format("Unsupported process type '{0}'. Process aborted.", process.ProcessType.ToString()));
                    }
                }
                finally
                {
                    processCount++;
                    LogMessage(0, string.Format("Migration ended for process '{0}'", process.Name));
                }
            }
        }

        private static void ProcessUserProfile(UserAccount userAccount)
        {
            UserProfile userProfile = userQueryServiceProvider.LoadUserProfile(userAccount.UserName);
            if (userProfile == null)
            {
                userProfile = new UserProfile()
                {
                    EmailAddress = userAccount.EmailAddress,
                    FirstName = userAccount.FirstName,
                    GeneralId = userAccount.GeneralId,
                    LastName = userAccount.LastName,
                    LocaleId = migrationConfig.Environment.DefaultLocale,
                    PhoneNumber = userAccount.PhoneNumber,
                    RegQualifiers = new List<RegistrationQualifier>()
                    {
                        new RegistrationQualifier("BirthDate", userAccount.BirthDate.Value.ToString("yyyyMMdd")),
                        new RegistrationQualifier("Last4SSN", userAccount.Last4SSN)
                    },
                    UserName = userAccount.UserName,
                    TenantId = Guid.Parse(migrationConfig.Environment.TenantId)
                };
                userServiceProvider.AddProfile(userProfile);
            }
            else
            {
                userProfile.PhoneNumber = userAccount.PhoneNumber;
                userProfile.RegQualifiers.Clear();
                userProfile.RegQualifiers.Add(new RegistrationQualifier("BirthDate", userAccount.BirthDate.Value.ToString("yyyyMMdd")));
                userProfile.RegQualifiers.Add(new RegistrationQualifier("Last4SSN", userAccount.Last4SSN));
                userProfile.EmailAddress = userAccount.EmailAddress;
                userProfile.FirstName = userAccount.FirstName;
                userProfile.GeneralId = userAccount.GeneralId;
                userProfile.LastName = userAccount.LastName;

                userServiceProvider.UpdateProfile(userProfile);
            }
        }

        private static UserProfile GetUserPofileId(string userName, UserXref userXref)
        {
            UserProfile profile = null;
            string id = string.Empty;
            profile = userProfiles.FirstOrDefault<UserProfile>(u => u.UserName == userName);
            if (profile == null)
            {
                profile = userQueryServiceProvider.LoadUserProfile(userName);
                if (profile != null)
                {
                    userProfiles.Add(profile);
                }
                else
                {
                    string msg = string.Format("Error processing user '{0}': user profile not found", userName);
                    UserXrefError userXrefError = new UserXrefError(userXref, msg);
                    LogMessage(1, msg);
                    lock (userXrefErrors)
                    {
                        userXrefErrors.Add(userXrefError);
                    }

                    lock (currentProcess)
                    {
                        currentProcess.ErroredCount++;
                    }
                }
            }


            return profile;

        }
        private static void ReportCounts()
        {
            LogBreak();
            LogMessage(0, "===========================================================================");
            LogMessage(0, "Processing Summary");
            LogMessage(0, "===========================================================================");
            LogMessage(0, string.Format("Processes Run:  {0}", processCount.ToString("#,##0")));
            LogMessage(0, string.Format("Execution Time: {0} seconds", timer.Elapsed.Seconds.ToString("#,##0")));
            LogBreak();

            foreach (ProcessConfig process in migrationConfig.Processes)
            {
                LogMessage(0, string.Format("Process: {0}", process.Name));
                LogMessage(0, "---------------------------------------------------------------------------");
                if (process.ProcessType == ProcessConfig.ProcessTypes.Export)
                {
                    LogMessage(1, string.Format("Account Exports: {0}", process.ProcessedCount.ToString("#,##0")));
                    LogMessage(1, string.Format("Account Errors:  {0}", process.ErroredCount.ToString("#,##0")));
                }
                else if (process.ProcessType == ProcessConfig.ProcessTypes.Import)
                {
                    LogMessage(1, string.Format("Account Imports: {0}", process.ProcessedCount.ToString("#,##0")));
                    LogMessage(1, string.Format("Account Errors:  {0}", process.ErroredCount.ToString("#,##0")));
                }
                else
                {
                    LogMessage(1, string.Format("Xref Imports: {0}", process.ProcessedCount.ToString("#,##0")));
                    LogMessage(1, string.Format("Xref Updates: {0}", process.UpdatedCount.ToString("#,##0")));
                    LogMessage(1, string.Format("Xref Duplicates:  {0}", process.DuplicateCount.ToString("#,##0")));
                    LogMessage(1, string.Format("Xref Errors:  {0}", process.ErroredCount.ToString("#,##0")));
                }
                LogBreak();
            }
        }

        private static void StartImportThreads(List<UserAccount> userAccounts, int maxThreads)
        {
            List<Thread> threads = new List<Thread>();
            int threadCount = maxThreads;
            if (maxThreads > userAccounts.Count)
            {
                threadCount = userAccounts.Count;
            }

            int recordsPerThread = userAccounts.Count / threadCount;

            LogMessage(1, string.Format("Starting {0} threads processing {1}+ accounts each", threadCount.ToString("#,##0"), recordsPerThread.ToString("#,##0")));
            for (int i = 0; i < threadCount; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(Program.ImportAccount));
                t.Name = (i + 1).ToString("000");
                threads.Add(t);
                if(i < threadCount - 1)
                {
                    t.Start(userAccounts.Skip(i * recordsPerThread).Take(recordsPerThread));
                }
                else
                {
                    t.Start(userAccounts.Skip(i * recordsPerThread));
                }
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }
        }


        private static void StartImportThreads(List<UserXref> userXrefs, int maxThreads)
        {
            List<Thread> threads = new List<Thread>();
            int threadCount = maxThreads;
            if (maxThreads > userXrefs.Count)
            {
                threadCount = userXrefs.Count;
            }

            int recordsPerThread = userXrefs.Count / threadCount;

            LogMessage(1, string.Format("Starting {0} threads processing {1} xrefs each", threadCount.ToString("#,##0"), recordsPerThread.ToString("#,##0")));
            for (int i = 0; i < threadCount; i++)
            {
                Thread t = new Thread(new ParameterizedThreadStart(Program.ImportXref));
                t.Name = (i + 1).ToString("000");
                threads.Add(t);
                if (i < threadCount - 1)
                {
                    t.Start(userXrefs.Skip(i * recordsPerThread).Take(recordsPerThread));
                }
                else
                {
                    t.Start(userXrefs.Skip(i * recordsPerThread));
                }
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }
        }

        private static void UpdateUserAccount(UserAccount userAccount, DateTime? accountExpirationDate, bool processProfile)
        {
            userAccount.Validate();
            LogMessage(1, string.Format("Updating user '{0}'", userAccount.UserName));
            api.UserAccount adUserAccount = ConvertAdUserAccount(userAccount);
            adProvider.UpdateUser(adUserAccount.Identity, adUserAccount.RegistrationQualifiers, accountExpirationDate);
            if (!string.IsNullOrWhiteSpace(userAccount.Password))
            {
                adProvider.ResetUserPassword(userAccount.UserName, userAccount.Password);
            }

            if (processProfile)
            {
                ProcessUserProfile(userAccount);
            }
        }
    }
}
