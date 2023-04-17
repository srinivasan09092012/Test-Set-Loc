using HPP.Core.API.AddressValidation.Data;
using HPP.Core.API.AddressValidation.Providers;
using Microsoft.Extensions.Configuration;

namespace AddressDoctorTest
{
    public class Program
    {
        public static IConfiguration configuration;
        public static void Main(string[] args)
        {
            File.AppendAllText("testLog.txt", " step1 : " + Environment.NewLine);

            configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", false)
            .Build();

            AddressModel address = configuration.GetSection("Address").Get<AddressModel>();

            AddressInteractiveServiceDataProvider addressInteractiveServiceDataProvider = new AddressInteractiveServiceDataProvider("", "securedhttpbinding", "securedhttpbinding", configuration.GetSection("appSettings")["Addr.Interactive"]);
            ResultModel response = addressInteractiveServiceDataProvider.ValidateAddress(address);
            File.AppendAllText("testLog.txt", "Interactive- response - ." + response.Message + Environment.NewLine);
            File.AppendAllText("testLog.txt", "Interactive- PostalCode - ." + response.ResultData?.PostalCode + Environment.NewLine);
            File.AppendAllText("testLog.txt", "Interactive- Longitude - ." + response.ResultData?.Longitude + Environment.NewLine);
            File.AppendAllText("testLog.txt", "Interactive- Latitude - ." + response.ResultData?.Latitude + Environment.NewLine);
            File.AppendAllText("testLog.txt", "Interactive- OutputXML - ." + response.ResultData?.OutputXML + Environment.NewLine);

            AddressServiceDataProvider addressServiceDataProvider = new AddressServiceDataProvider("", "securedhttpbinding", "securedhttpbinding", configuration.GetSection("appSettings")["Addr.Services"]);
            response = addressServiceDataProvider.ValidateAddress(address);
            File.AppendAllText("testLog.txt", "Services- response - ." + response.Message + Environment.NewLine);
            File.AppendAllText("testLog.txt", "Services- PostalCode - ." + response.ResultData?.PostalCode + Environment.NewLine);
            File.AppendAllText("testLog.txt", "Services- Longitude - ." + response.ResultData?.Longitude + Environment.NewLine);
            File.AppendAllText("testLog.txt", "Services- Latitude - ." + response.ResultData?.Latitude + Environment.NewLine);
            File.AppendAllText("testLog.txt", "Services- OutputXML - ." + response.ResultData?.OutputXML + Environment.NewLine);
            File.AppendAllText("testLog.txt", "Services- ended." + Environment.NewLine);
            Console.Write(File.ReadAllText("testLog.txt"));
            Console.Read();
        }
    }
}