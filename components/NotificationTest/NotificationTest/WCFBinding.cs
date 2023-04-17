using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace NotificationTest
{
    public static class WCFBinding
    {
        private static Binding binding;

        public enum BindingTypes
        {
            BasicHttpBinding,
            BasicHttpsBinding,
            WS2007FederationHttpBinding,
            WSHttpBinding,
            NetTcpBinding
        }

        private enum DefaultSettingInts
        {
            BasicHttpBinding_MaxBufferSize,
            BasicHttpBinding_MaxReceivedMessageSize,
            BasicHttpBinding_MaxBufferPoolSize,
            WSHttpBinding_MaxReceivedMessageSize,
            WSHttpBinding_MaxBufferPoolSize,
            NetTcpBinding_MaxBufferSize,
            NetTcpBinding_MaxReceivedMessageSize,
            NetTcpBinding_MaxBufferPoolSize
        }

        private enum DefaultTimeouts
        {
            BasicHttpBinding_CloseTimeout,
            BasicHttpBinding_ReceiveTimeout,
            BasicHttpBinding_SendTimeout,
            BasicHttpBinding_OpenTimeout,
            WSHttpBinding_CloseTimeout,
            WSHttpBinding_ReceiveTimeout,
            WSHttpBinding_SendTimeout,
            WSHttpBinding_OpenTimeout,
            NetTcpBinding_CloseTimeout,
            NetTcpBinding_ReceiveTimeout,
            NetTcpBinding_SendTimeout,
            NetTcpBinding_OpenTimeout
        }

        public static Binding ReturnBinding(BindingTypes bindingType)
        {
            switch (bindingType)
            {
                case BindingTypes.BasicHttpBinding:
                    binding = ReturnBasicHttpBinding();
                    break;
                case BindingTypes.BasicHttpsBinding:
                    binding = ReturnBasicHttpBinding(true);
                    break;
                default:
                    throw new ArgumentNullException("ModuleBinding Application Setting is not defined");
            }

            return binding;
        }

        private static BasicHttpBinding ReturnBasicHttpBinding(bool sslFlag = false)
        {
            BasicHttpBinding result = new BasicHttpBinding();

            if (sslFlag)
            {
                result.Security.Mode = BasicHttpSecurityMode.Transport;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //if (ApplicationConfigurationManager.AppSettings != null && !string.IsNullOrEmpty(ApplicationConfigurationManager.AppSettings["CertificateWarningCheck"]) && ApplicationConfigurationManager.AppSettings["CertificateWarningCheck"] == "true")
                //{
                //    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                //}
            }

            result.MaxBufferSize = int.MaxValue;
            result.MaxReceivedMessageSize = int.MaxValue;
            result.MaxBufferPoolSize = int.MaxValue;
            TimeSpan sendTimeout = ParseTimeSpan(DefaultTimeouts.BasicHttpBinding_SendTimeout);
            result.SendTimeout = sendTimeout == TimeSpan.MinValue ? result.SendTimeout : sendTimeout;
            return result;
        }

        private static TimeSpan ParseTimeSpan(DefaultTimeouts applicationSettingName)
        {
            var value = "00:05:00";

            TimeSpan result;

            if (!TimeSpan.TryParse(value, out result))
            {
                TimeSpan.TryParse("00:10:00", out result);
            }

            return result;
        }
    }
}
