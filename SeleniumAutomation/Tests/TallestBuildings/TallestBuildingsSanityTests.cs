using SeleniumAutomation.Pages;
using SeleniumAutomation.Pages.TallestBuildings;

namespace SeleniumAutomation.Tests.TallestBuildings;

[TestFixture(Author = "TS", Category = "Sanity")]
public class TallestBuildingsSanityTests
{
    public TallestBuildingsPage page { get; set; }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        page.Quit();
    }

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        page = new TallestBuildingsPage();
    }

    [SetUp]
    public void SetUp()
    {
        // set
        page.Go("list", "tallest100-construction");
    }

    [Test]
    [TestCase(100)]
    public void VerifyTheNumberOfBuildings(int numberOfBuildings)
    {
        // act
        page.SelectDropdownOption(TallestBuildingsDropdownEnum.TallestCompleted);
        var tableRows = page.GetTableRows();

        // assert
        Assert.AreEqual(numberOfBuildings, tableRows.Count);
    }

    [Test]
    [TestCase("Lotte World Tower", "123")]
    public void VerifyNumberOfFloors(string buildingName, string numberOfFloors)
    {
        // act
        page.SelectDropdownOption(TallestBuildingsDropdownEnum.TallestCompleted);
        var stories = page.GetStoriesByBuildingName(buildingName);
        // assert
        Assert.AreEqual(numberOfFloors, stories);
    }

    [Test]
    [TestCase(163, "Burj Khalifa")]
    public void GetTheBuildingWithHighestNumberOfFloors(int floors, string name)
    {
        // act
        page.SelectDropdownOption(TallestBuildingsDropdownEnum.TallestCompleted);
        var building = page.GetTheBuildingWithHighestNumberOfFloors();

        // assert
        Assert.AreEqual(floors, building.Value);
        Assert.AreEqual(name, building.Key);
    }
}
