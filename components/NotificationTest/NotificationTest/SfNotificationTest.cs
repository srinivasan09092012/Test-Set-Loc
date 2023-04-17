using sfNotificationService;

namespace NotificationTest
{
    public class SfNotificationTest
    {
        public void sendNotify()
        {
            List<Address> toAddress = new List<Address>();
            Address addressList = new Address()
            {
                EmailAddress = "avinash.k@gainwelltechnologies.com",
                MailType = Address.MailTypeEnum.To
            };
            toAddress.Add(addressList);
            addressList = new Address()
            {
                EmailAddress = "kolisetty.samanth@gainwelltechnologies.com",
                MailType = Address.MailTypeEnum.To
            };
            toAddress.Add(addressList);

            Address notificationAddress = new Address()
            {
                EmailAddress = "appshcproducttd01team@gainwelltechnologies.com"
            };
            string emailBody = File.ReadAllText("emailBody.txt");
            var serviceChannelFactory = ChannelFactoryHelper.InitializeChannelFactory<INotificationService>("https://sf.test.mapshc.com/HPP.NotificationServices/R6.0/?ListenerName=NotificationServiceListener", "BasicHttpsBinding");
            INotificationService NotifyService = ChannelFactoryHelper.CreateChannel<INotificationService>(serviceChannelFactory);
            NotifyService.SendEmail("77b50320-5f06-5740-84f4-18d4a8cda51d", "sf notify", emailBody, toAddress, notificationAddress, sfNotificationService.MailPriority.Normal, new List<Attachments>());
            Console.WriteLine("Sf url Notification Sent");

        }
    }
}
