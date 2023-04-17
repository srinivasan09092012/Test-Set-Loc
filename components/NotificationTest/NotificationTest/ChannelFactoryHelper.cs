using System.ServiceModel;
using System.ServiceModel.Channels;

namespace NotificationTest
{
    public static class ChannelFactoryHelper
    {
        public static T CreateChannel<T>(ChannelFactory channelFactory)
        {
            ChannelFactory<T> channel = channelFactory as ChannelFactory<T>;
            return channel.CreateChannel();
        }

        public static Binding ReturnBinding(string bindingType)
        {
            try
            {
                return WCFBinding.ReturnBinding(ParseEnum<WCFBinding.BindingTypes>(bindingType));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static ChannelFactory<T> InitializeChannelFactory<T>(string endpointaddress, string bindingConfigurationName)
        {
            EndpointAddress endpoint = new EndpointAddress(endpointaddress);
            return new ChannelFactory<T>(ReturnBinding(bindingConfigurationName), endpoint);
        }
    }
}
