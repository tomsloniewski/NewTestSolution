using APIAutomation.Client;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace APIAutomation.Shared.BasicTestConfiguration;

public class BaseTest
{
    public Configuration Configuration { get; set; }
    public RESTClient Client { get; set; }

    public BaseTest()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json").Build();

        var section = config.GetSection(nameof(BasicTestConfiguration.Configuration));
        Configuration = section.Get<Configuration>()!;

        Client = new RESTClient(Configuration);
    }


}
