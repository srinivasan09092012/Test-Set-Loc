//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved. 
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using CommandLine;
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.UserMeta;
using HPE.HSP.UA3.Core.API.Logger;
using NISTMongoAtlasExtractPOC.Ctrl;
using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace NISTMongoAtlasExtractPOC.Main
{
    public class BatchProcessProgram
    {
        private enum ExitCode : int
        {
            Success = 0,
            Errror = 1
        }

        public static int Main(string[] args)
        {
            try
            {
                System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
                if (args.Contains("ex"))
                {
                    System.ApplicationException exceptionEx = new System.ApplicationException("testing a thrown exception");
                    throw exceptionEx;
                }

                ////CommandLineOptions myOptions = null;

                var x = Parser.Default.ParseArguments<CommandLineOptions>(args).WithParsed<CommandLineOptions>(options =>
                  {
                  ////myOptions = options;
                  if (Validate(options))
                  {
                      var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                      string processType = "NISTMongoAtlasExtractPOC.Ctrl";
                      string correlationId = Guid.NewGuid().ToString("D");
                      LoggerManager.Logger.LogInformational(CommandLine.Parser.Default.FormatCommandLine(options));
                      Thread.SetData(Thread.GetNamedDataSlot(CoreConstants.Thread.CorrelationId), correlationId);
                      Thread.SetData(Thread.GetNamedDataSlot(CoreConstants.Thread.TenantId), options.TenantId);
                      //// ConfigurationManager.AppSettings["ModuleName"] = options.Module; (obsolete)
                      if (ApplicationConfigurationManager.AppSettings == null)
                      {
                              //// (awaiting Pulse fix for Atlas) 
                        
                           ////   ApplicationConfigurationManager.LoadApplicationConfiguration(options.TenantId.ToString("D"), new CacheManager());                     
                      }

                          BatchExecutorContext context = new BatchExecutorContext(processType);
                          context.AddInitializerParm("CommandLineOptions", options);
                          context.AddInitializerParm(CoreConstants.Thread.CorrelationId, correlationId);
                          //// (awaiting Pulse fix for Atlas) 

                          //// (awaiting Pulse fix for Atlas) 
                          ////     context.AddInitializerParm("ApplicationName", ApplicationConfigurationManager.AppSettings["ApplicationName"]);
                          //// (awaiting Pulse fix for Atlas) 
                          ////     context.AddInitializerParm("ModuleName", ApplicationConfigurationManager.AppSettings["ModuleName"]);
                          //// (awaiting build out of evidence vault functionality) 
                          ////    context.AddInitializerParm("EvidenceVault", ApplicationConfigurationManager.AppSettings["EvidenceVault"]);
                          //// NOT FULLY IMPLEMENTED
                          ////    context.AddInitializerParm("EvidenceVault", ApplicationConfigurationManager.AppSettings["EvidenceVault"]);

                          context.RunJob();
                          LoggerManager.Logger.LogInformational("Overall time taken: " + stopwatch.Elapsed);
                      }
                  });

                return (int)ExitCode.Success;
            }
            catch (Exception ex)
            {
                LoggerManager.Logger.LogInformational("Exception: " + ex.Message);
                LoggerManager.Logger.LogInformational("Stack Trace =>>>\n: " + ex.StackTrace);
                return (int)ExitCode.Errror;
            }
        }

        public static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (LoggerManager.Logger != null)
            {
                LoggerManager.Logger.LogError(e.ExceptionObject.ToString());
            }
        }

        private static bool Validate(CommandLineOptions options)
        {
            bool isValid = false;

            if (options.Locale != "en-US")
            {
                throw new ArgumentOutOfRangeException("LocaleCode: en-US supported");
            }

            isValid = true;

            return isValid;
        }

        private static bool ValidateArgs(string[] args)
        {
            bool isValid = false;
            if (args.Contains("-help", StringComparer.OrdinalIgnoreCase) || args.Length < 1)
            {
                DisplayUsage();
            }
            else
            {
                Guid id;
                if (!Guid.TryParse(args[0], out id))
                {
                    throw new ArgumentOutOfRangeException("tenant id must be a valid guid");
                }

                isValid = true;
            }

            return isValid;
        }

        private static void DisplayUsage()
        {
            LoggerManager.Logger.LogInformational("Usage: ");
            LoggerManager.Logger.LogInformational("NISTMongoAtlasExtractPOC.exe [tenantId] [LocaleCode]");
            LoggerManager.Logger.LogInformational(Environment.NewLine);
            LoggerManager.Logger.LogInformational("Example: ");
            LoggerManager.Logger.LogInformational("NISTMongoAtlasExtractPOC.exe  \"77B50320-5F06-5740-84F4-18D4A8CDA51D\" \"en-us\"");
            LoggerManager.Logger.LogInformational(Environment.NewLine);
            LoggerManager.Logger.LogInformational("Version: " + Environment.NewLine + Assembly.GetExecutingAssembly().GetName().Version);
            System.Console.ReadLine();
        }

        private static string GetLocale(string[] args)
        {
            string locale = "en-us";

            if (args.Length > 1)
            {
                return args[1];
            }

            return locale;
        }
    }
}
