using Microsoft.Extensions.Configuration;
using SeleniumAutomation.Shared.Selenium;

namespace SeleniumAutomation.Shared.BasicTestConfiguration;

public class BaseTest
{
    public Configuration Configuration { get; set; }
    public TestExecutor Test { get; set; }

    public BaseTest()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json").Build();

        var section = config.GetSection(nameof(BasicTestConfiguration.Configuration));
        Configuration = section.Get<Configuration>()!;

        Test = new TestExecutor(Configuration);
    }


}
