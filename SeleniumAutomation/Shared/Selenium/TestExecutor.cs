using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumAutomation.Shared.BasicTestConfiguration;
using SeleniumExtras.WaitHelpers;

namespace SeleniumAutomation.Shared.Selenium;

public class TestExecutor
{
    private Configuration configuration;
    private IWebDriver driver;


    public TestExecutor(Configuration config)
    {
        configuration = config;
        driver = DriverFactory.GetDriver((DriverEnum) Enum.Parse(typeof(DriverEnum), configuration.Browser!, true));

        // set implicit wait
        driver.Manage().Timeouts().ImplicitWait = new TimeSpan(0, 0, 0, 0, configuration.TimeoutMiliseconds);
    }


    public IWebElement GetElement(By locator)
    {
        return GetElement(locator, configuration.TimeoutMiliseconds);
    }

    public IWebElement GetElement(string CSSlocator)
    {
        return GetElement(By.CssSelector(CSSlocator), configuration.TimeoutMiliseconds);
    }

    public IWebElement GetElement(By locator, int customTimeoutMilisec)
    {
        var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 0, 0, customTimeoutMilisec));
        wait.Until(ExpectedConditions.ElementExists(locator));
        return driver.FindElement(locator);
    }

    public IWebElement GetElement(string CSSlocator, int customTimeoutMilisec)
    {
        return GetElement(By.CssSelector(CSSlocator), customTimeoutMilisec);
    }

    public List<IWebElement> GetMultipleElements(By locator)
    {
        var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 0, 0, configuration.TimeoutMiliseconds));
        wait.Until(ExpectedConditions.ElementExists(locator));
        return driver.FindElements(locator).ToList();
    }

    public List<IWebElement> GetMultipleElements(By locator, int customTimeoutMilisec)
    {
        var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 0, 0, customTimeoutMilisec));
        wait.Until(ExpectedConditions.ElementExists(locator));
        return driver.FindElements(locator).ToList();
    }

    public List<IWebElement> GetMultipleElements(string CSSlocator)
    {
        var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 0, 0, configuration.TimeoutMiliseconds));
        wait.Until(ExpectedConditions.ElementExists(By.CssSelector(CSSlocator)));
        return driver.FindElements(By.CssSelector(CSSlocator)).ToList();
    }

    public List<IWebElement> GetMultipleElements(string CSSlocator, int customTimeoutMilisec)
    {
        var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 0, 0, customTimeoutMilisec));
        wait.Until(ExpectedConditions.ElementExists(By.CssSelector(CSSlocator)));
        return driver.FindElements(By.CssSelector(CSSlocator)).ToList();
    }

    public void Wait (WaitEnum waitType, By locator)
    {
        Wait(waitType, configuration.TimeoutMiliseconds, locator);
    }

    public void Wait(WaitEnum waitType, IWebElement element)
    {
        Wait(waitType, configuration.TimeoutMiliseconds, element);
    }

    public void Wait(WaitEnum waitType, int timeoutMilisecond, By locator)
    {
        var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 0, 0, timeoutMilisecond));
        switch (waitType)
        {
            case WaitEnum.ElementExist:
                wait.Until(ExpectedConditions.ElementExists(locator));
                break;
            case WaitEnum.ElementIsVisible:
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
                break;
            case WaitEnum.InvisibilityOfElementLocated:
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(locator));
                break;
            default:
                break;
        }
    }

    public void Wait(WaitEnum waitType, int timeoutMilisecond, IWebElement element)
    {
        var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 0, 0, timeoutMilisecond));
        switch (waitType)
        {
            case WaitEnum.StalenessOf:
                wait.Until(ExpectedConditions.StalenessOf(element));
                break;
            default:
                break;
        }
    }

    public void GoTo(string path = "")
    {
        driver.Navigate().GoToUrl(configuration.BaseUrl + path);
    }

    public void QuitDriver()
    {
        driver.Close();
        driver.Quit();
    }
}
