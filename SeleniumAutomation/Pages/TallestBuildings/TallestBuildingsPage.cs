using OpenQA.Selenium.DevTools.V106.Debugger;
using SeleniumAutomation.Shared.BasicTestConfiguration;
using System.Reflection.Metadata.Ecma335;

namespace SeleniumAutomation.Pages.TallestBuildings;

public class TallestBuildingsPage : BaseTest, IBasePage
{
    private readonly string pagePath = "/buildings";
    public string listsDropdownLocator = "#lists-pages-select-container";
    public string listsDropdownOptionLocator = "#lists-pages-select-container option";
    public string tableBodyRowsLocator = "#buildingsTable tbody tr";
    public string tableStoryColumnLocator = "td.hidden.forget";
    public string tableBuildingNameLocator = ".building-hover";

    public TallestBuildingsPage() { }

    public List<IWebElement> GetTableRows()
    {
        return Test.GetMultipleElements(tableBodyRowsLocator);
    }

    public string? GetStoriesByBuildingName(string buildingName)
    {
        var row = GetRowByBuildingName(buildingName);
        var cell = row?.FindElement(By.CssSelector(tableStoryColumnLocator));
        var value = cell?.FindElement(By.CssSelector("p")).Text;

        return value;
    }

    public IWebElement? GetRowByBuildingName(string buildingName)
    {
        List<IWebElement> rows = GetTableRows();
        IWebElement? row = rows.Find(row =>
            {
                var query = row.FindElements(By.CssSelector($"[data-order='{buildingName}']"));
                if (query.Any())
                {
                    return true;
                }
                return false;
            }
        );

        if (row is null)
        {
            throw new Exception("The row you were looking for has not been found");
        };

        return row;
    }

    public KeyValuePair<string, int> GetTheBuildingWithHighestNumberOfFloors()
    {
        var rows = GetTableRows();
        IWebElement? row = rows.MaxBy(row =>
        {
            var stories = row.FindElement(By.CssSelector(tableStoryColumnLocator));
            return Convert.ToInt32(stories.FindElement(By.CssSelector("p")).Text);
        });

        if (row is null)
        {
            throw new Exception("GetTheBuildingWithHighestNumberOfFloors() -> Could not find the row");
        }

        string buildingName = row.FindElement(By.CssSelector(tableBuildingNameLocator)).GetAttribute("data-order");
        int buildingStories = Convert.ToInt32(row.FindElement(By.CssSelector($"{tableStoryColumnLocator} p")).Text);
        var result = new KeyValuePair<string, int>(buildingName, buildingStories);

        return result;
    }

    public void Go()
    {
        Test.GoTo(pagePath);
    }

    public void Go(string queryParam, string queryParamValue)
    {
        Test.GoTo(pagePath + $"?{queryParam}={queryParamValue}");
    }

    public void Quit()
    {
        Test.QuitDriver();
    }

    public void SelectDropdownOption(TallestBuildingsDropdownEnum option)
    {
        IWebElement dropdown = Test.GetElement(listsDropdownLocator);
        dropdown.Click();

        string optionLocator = listsDropdownOptionLocator;

        switch (option)
        {
            case TallestBuildingsDropdownEnum.TallestCompleted:
                optionLocator += "[value='tallest100-completed']";
                break;
            case TallestBuildingsDropdownEnum.TallestUnderConstruction:
                optionLocator += "[value='tallest100-construction']";
                break;
            default:
                break;
        }

        Test.GetElement(optionLocator).Click();
        Test.Wait(Shared.Selenium.WaitEnum.StalenessOf, dropdown);
    }


}
