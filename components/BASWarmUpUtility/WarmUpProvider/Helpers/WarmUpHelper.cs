//-----------------------------------------------------------------------------------------
// This code is the property of Gainwell Technologies, Copyright (c) 2021. All rights reserved.
//
// Any unauthorized use in whole or in part without written consent is strictly prohibited.
// Violators may be punished to the full extent of the law.
//-----------------------------------------------------------------------------------------
using HP.HSP.UA3.Core.BAS.CQRS.Base;
using HP.HSP.UA3.Core.BAS.CQRS.Caching;
using HP.HSP.UA3.Core.BAS.CQRS.Helpers;
using HP.HSP.UA3.Core.BAS.CQRS.Interfaces;
using HP.HSP.UA3.Core.BAS.CQRS.UserMeta;
using HPE.HSP.UA3.Core.API.Logger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WarmUpProvider.Domain;
using WarmUpProvider.NotificationService;

namespace WarmUpProvider.Helpers
{
    public class WarmUpHelper
    {
        private ConcurrentBag<ExceptionNotificationModel> businessExceptionMessages = new ConcurrentBag<ExceptionNotificationModel>();

        public void StartUp()
        {
            this.WarmUpEndpoints();
        }

        private void WarmUpEndpoints()
        {
            try
            {
                FileHelper fileHelper = new FileHelper();
                List<TenantWarmUpModel> tenantWarmUpModels = new List<TenantWarmUpModel>();

                tenantWarmUpModels = fileHelper.ReadEndpointDataFromJSON();

                List<Task> tasks = new List<Task>();

                foreach (var tenant in tenantWarmUpModels)
                {
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    this.LoggerHelper("-----------Starting to Warm up Provider Endpoints for Tenant : " + tenant.TenantId + "-----------");
                    foreach (var moduleEndpoint in tenant.Endpoints)
                    {
                        string absoluteURL = tenant.RootURL + moduleEndpoint.EndPoint;
                        tasks.Add(Task.Run(() =>
                        {
                            this.WarmUpServices(moduleEndpoint, absoluteURL, tenant.TenantId);
                        }));

                        if (tasks.Count == 15)
                        {
                            Task.WaitAll(tasks.ToArray());
                            tasks.Clear();
                        }
                    }

                    Task.WaitAll(tasks.ToArray());
                    tasks.Clear();

                    if (this.businessExceptionMessages.Count > 0)
                    {
                        this.SendMail(tenant.TenantId);
                        this.businessExceptionMessages = null;
                    }

                    stopWatch.Stop();
                    this.LoggerHelper("Total Time Took To Warm Up Provider Endpoints of Tenant : " + tenant.TenantId + " " + stopWatch.Elapsed.Hours + ":" + stopWatch.Elapsed.Minutes + ":" + stopWatch.Elapsed.Seconds);
                }

                tenantWarmUpModels.Clear();
            }
            catch (Exception ex)
            {
                this.LoggerHelper("Error warming up Provider Endpoints!", WarmUpEnums.LogType.Error, ex);
                throw ex;
            }

            this.LoggerHelper("-----------Warming up Endpoints Completed-----------");
        }

        private void WarmUpServices(ModuleEndpointModel moduleEndpoint, string absoluteURL, string tenantId)
        {
            try
            {
                IServiceChannelFactory serviceChannelFactory = new ServiceChannelFactory(string.IsNullOrEmpty(moduleEndpoint.Binding) ? "BasicHttpBinding" : moduleEndpoint.Binding, absoluteURL);

                serviceChannelFactory.Invoke<IServiceAvailability>(
                    proxy => proxy.IsServiceAvailable());

                this.LoggerHelper("Service Available. Module Name = " + moduleEndpoint.ModuleName + ", Application Name = " + moduleEndpoint.ApplicationName + " and Enpoint = " + absoluteURL, WarmUpEnums.LogType.Information);

                if (moduleEndpoint.CheckHealthStatus)
                {
                    serviceChannelFactory.Invoke<IServiceAvailability>(
                                proxy => proxy.IsServiceHealthy(tenantId));

                    this.LoggerHelper("Service Health Check Successfull. Module Name = " + moduleEndpoint.ModuleName + ", Application Name = " + moduleEndpoint.ApplicationName + " and Enpoint = " + absoluteURL, WarmUpEnums.LogType.Information);
                }
            }
            catch (FaultException<BusinessValidationException> ex)
            {
                this.ProcessBusinessException(moduleEndpoint, absoluteURL, ex);
            }
            catch (FaultException<HP.HSP.UA3.Core.BAS.CQRS.Base.ServiceException> ex)
            {
                this.LoggerHelper("Service warm up failed. Module Name = " + moduleEndpoint.ModuleName + ", Application Name = " + moduleEndpoint.ApplicationName + " and Enpoint = " + absoluteURL, WarmUpEnums.LogType.Warning, ex);
                this.businessExceptionMessages.Add(new ExceptionNotificationModel()
                {
                    BusinessExceptionMessage = new List<BusinessExceptionMessage>()
                    {
                        new BusinessExceptionMessage("FaultException<ServiceException>", "FaultException<ServiceException>", ex.Message)
                    },
                    Endpoint = absoluteURL
                });
            }
            catch (ActionNotSupportedException ex)
            {
                this.LoggerHelper("Service warm up failed. Module Name = " + moduleEndpoint.ModuleName + ", Application Name = " + moduleEndpoint.ApplicationName + " and Enpoint = " + absoluteURL, WarmUpEnums.LogType.Warning, ex);
                this.businessExceptionMessages.Add(new ExceptionNotificationModel()
                {
                    BusinessExceptionMessage = new List<BusinessExceptionMessage>()
                    {
                        new BusinessExceptionMessage("ActionNotSupportedException", "ActionNotSupportedException", ex.Message)
                    },
                    Endpoint = absoluteURL
                });
            }
            catch (Exception ex)
            {
                this.LoggerHelper("Service warm up failed. Module Name = " + moduleEndpoint.ModuleName + ", Application Name = " + moduleEndpoint.ApplicationName + " and Enpoint = " + absoluteURL, WarmUpEnums.LogType.Warning, ex);
                this.businessExceptionMessages.Add(new ExceptionNotificationModel()
                {
                    BusinessExceptionMessage = new List<BusinessExceptionMessage>()
                    {
                        new BusinessExceptionMessage("Exception", "Exception", ex.Message)
                    },
                    Endpoint = absoluteURL
                });
            }
        }

        private void ProcessBusinessException(ModuleEndpointModel moduleEndpoint, string absoluteURL, FaultException<BusinessValidationException> ex)
        {
            List<BusinessExceptionMessage> businessMessages = new List<BusinessExceptionMessage>();
            string inRuleNotInUse = " does not exist in the catalog.";

            foreach (var item in ex.Detail.BusinessMessages)
            {
                if (item.FieldName == CoreConstants.WarmUpUtility.BusinessExceptionKeys.InRule)
                {
                    if (item.MessageDefault.EndsWith(inRuleNotInUse))
                    {
                        this.LoggerHelper("Service Health Check Successfull. Module Name = " + moduleEndpoint.ModuleName + ", Application Name = " + moduleEndpoint.ApplicationName + " and Enpoint = " + absoluteURL, WarmUpEnums.LogType.Information);
                    }
                    else
                    {
                        this.LoggerHelper("Service warm up failed. Module Name = " + moduleEndpoint.ModuleName + ", Application Name = " + moduleEndpoint.ApplicationName + " and Enpoint = " + absoluteURL, WarmUpEnums.LogType.Warning, ex);
                        businessMessages.Add(item);
                    }
                }
                else
                {
                    this.LoggerHelper("Service warm up failed. Module Name = " + moduleEndpoint.ModuleName + ", Application Name = " + moduleEndpoint.ApplicationName + " and Enpoint = " + absoluteURL, WarmUpEnums.LogType.Warning, ex);
                    businessMessages.Add(item);
                }
            }

            if (businessMessages.Count > 0)
            {
                this.businessExceptionMessages.Add(new ExceptionNotificationModel()
                {
                    BusinessExceptionMessage = new List<BusinessExceptionMessage>(businessMessages),
                    Endpoint = absoluteURL
                });
            }
        }

        private void SendMail(string tenantID)
        {
            this.LoggerHelper("Sending Exception Notification Mail.....", WarmUpEnums.LogType.Information);
            string notificationBinding = ConfigurationManager.AppSettings["NotificationBinding"];
            string notificationEndpoint = ConfigurationManager.AppSettings["NotificationEndpoint"];
            string fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"];
            string toEmailAddress = ConfigurationManager.AppSettings["ToEmailAddress"];
            string appName = ConfigurationManager.AppSettings["ApplicationName"];
            string exceptionMessages = string.Empty;
            string emailSubject = "Warm Up Exception Notification_" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString();
            string machineName = Environment.MachineName;
            string emailBody = "Exception messages which were observed while warming up services are attached in this mail.\r\nServer Name : " + machineName + "\r\nApplication Name : " + appName + "\r\n"; //// Thank you,\rBAS Warm Up Utility.";
            string emailBodyNote = "Please note: This email was sent by an automated process from an unmonitored address. Please do not reply to this email.";
            List<Address> toAddress = new List<Address>();
            List<Attachments> attachment = new List<Attachments>();
            emailBody += emailBodyNote;

            Address fromAddress = new Address()
            {
                DisplayName = "BASWarmUpUtility",
                EmailAddress = fromEmailAddress
            };

            foreach (string item in toEmailAddress.Split(','))
            {
                Address toEmailIds = new Address()
                {
                    EmailAddress = item.Trim(),
                    MailType = Address.MailTypeEnum.To
                };
                toAddress.Add(toEmailIds);
            }

            exceptionMessages += "Endpoint,";
            exceptionMessages += "Key,";
            exceptionMessages += "Exception Details,";
            exceptionMessages += "\r\n";

            foreach (var item in this.businessExceptionMessages)
            {
                foreach (var item2 in item.BusinessExceptionMessage)
                {
                    exceptionMessages += item.Endpoint + ",";
                    exceptionMessages += item2.FieldName + ",";
                    exceptionMessages += item2.MessageDefault.Replace("\r\n", "").Replace(",", "|");
                    exceptionMessages += "\r\n";
                }
            }

            attachment.Add(new Attachments()
            {
                AttachmentType = Attachments.MediaTypes.PlainText,
                Contents = Encoding.ASCII.GetBytes(exceptionMessages),
                ContentId = Guid.NewGuid().ToString(),
                Name = "ExceptionDetails.csv"
            });

            try
            {
                IServiceChannelFactory serviceChannelFactory = new ServiceChannelFactory(notificationBinding, notificationEndpoint);

                serviceChannelFactory.Invoke<INotificationService>(
                        proxy => proxy.SendEmail(tenantID, emailSubject, emailBody, toAddress, fromAddress, MailPriority.Normal, attachment));

                this.LoggerHelper("Exception Notifications Sent.", WarmUpEnums.LogType.Information);
            }
            catch (Exception ex)
            {
                this.LoggerHelper("Failed to Send Exception Notification Mail.", WarmUpEnums.LogType.Warning, ex);
            }
        }

        private void LoggerHelper(string message, WarmUpEnums.LogType logType = WarmUpEnums.LogType.Information, Exception ex = null)
        {
            switch (logType)
            {
                case WarmUpEnums.LogType.Information:
                    LoggerManager.Logger.LogInformational(message);
                    break;

                case WarmUpEnums.LogType.Warning:
                    if (ex == null)
                    {
                        LoggerManager.Logger.LogWarning(message);
                    }
                    else
                    {
                        LoggerManager.Logger.LogWarning(message + " " + ex.Message);
                    }
                    break;

                case WarmUpEnums.LogType.Error:
                    if (ex == null)
                    {
                        LoggerManager.Logger.LogError(message);
                    }
                    else
                    {
                        LoggerManager.Logger.LogError(message + " " + ex.Message);
                    }
                    break;

                default:
                    LoggerManager.Logger.LogInformational(message);
                    break;
            }
        }

    }
}