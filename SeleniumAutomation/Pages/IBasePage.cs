namespace SeleniumAutomation.Pages;

public interface IBasePage
{
    public void Quit();
    public void Go();
    public void Go(string queryParam, string queryParamValue);
}
