using APIAutomation.Shared.BasicTestConfiguration;
using APIAutomation.Shared.Request;
using APIAutomation.Shared.Response.GetPostById;
using APIAutomation.Shared.Response.POSTPosts;
using RestSharp;
using System.Net;

namespace APIAutomation.Tests.POSTPosts;

[TestFixture("/posts/", Author = "TS", Category = "Sanity")]
public class PostPostsTests : BaseTest
{
    public readonly string endpoint;

    public PostPostsTests(string endpoint) => this.endpoint = endpoint;

    [Test]
    [TestCase(101)]
    public async Task PostANewEntryTest(int id)
    {
        // set
        RestRequest request = new RequestBuilder(this, endpoint, Method.Post)
            .Build();

        // act
        POSTPostsResponse response = await Client.PostAsync<POSTPostsResponse>(request);

        // assert
        Assert.AreEqual(HttpStatusCode.Created, response.ResponseObject!.StatusCode);
        Assert.AreEqual(id, response.id);
    }
}
