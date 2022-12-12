using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace SeleniumAutomation.Shared.Selenium;

public class DriverFactory
{
    public static IWebDriver GetDriver(DriverEnum type)
    {
        switch (type) {
            case DriverEnum.Chrome:
                return new ChromeDriver();
            case DriverEnum.Firefox:
                return new FirefoxDriver();
            default:
                throw new NotImplementedException("Other driver types are not implemented yet");
        }
    }
}
