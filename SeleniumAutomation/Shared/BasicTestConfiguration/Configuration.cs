namespace SeleniumAutomation.Shared.BasicTestConfiguration;

public class Configuration
{
    public string? BaseUrl { get; set; }
    public string? Browser { get; set; }
    public bool MaximizeWindow { get; set; }
    public int TimeoutMiliseconds { get; set; }
}
