using APIAutomation.Shared.BasicTestConfiguration;
using APIAutomation.Shared.Request;
using APIAutomation.Shared.Response.GetPostById;
using RestSharp;
using System.Net;

namespace APIAutomation.Tests.GETPostsById;

[TestFixture("/posts/", Author = "TS", Category = "Sanity")]
public class GetPostsByIdTests : BaseTest
{
    readonly string endpoint;

    public GetPostsByIdTests(string endpoint) => this.endpoint = endpoint;

    [Test]
    [TestCase(1, 10, "optio molestias id quia eum", "quo et expedita modi cum officia vel magni\ndoloribus qui repudiandae\nvero nisi sit\nquos veniam quod sed accusamus veritatis error")]
    [TestCase(5, 50, "repellendus qui recusandae incidunt voluptates tenetur qui omnis exercitationem", "error suscipit maxime adipisci consequuntur recusandae\nvoluptas eligendi et est et voluptates\nquia distinctio ab amet quaerat molestiae et vitae\nadipisci impedit sequi nesciunt quis consectetur")]
    public async Task VerifyExistingEntriesPositiveTest(int userId, int id, string title, string body)
    {
        // set
        RestRequest request = new RequestBuilder(this, endpoint, Method.Get)
            .AddPathParameter(id.ToString())
            .Build();

        // act
        GETPostsByIdResponse response = await Client.GetAsync<GETPostsByIdResponse>(request);

        // assert
        Assert.AreEqual(HttpStatusCode.OK, response.ResponseObject!.StatusCode);
        Assert.AreEqual(userId, response.userId);
        Assert.AreEqual(id, response.id);
        Assert.AreEqual(title, response.title);
        Assert.AreEqual(body, response.body);
    }

    [Test]
    [TestCase("123")]
    [TestCase("-5")]
    [TestCase("abcde")]
    public async Task VerifyNonExistingEntriesTest(string id)
    {
        // set
        RestRequest request = new RequestBuilder(this, endpoint, Method.Get)
            .AddPathParameter(id)
            .Build();

        // act
        GETPostsByIdResponse response = await Client.GetAsync<GETPostsByIdResponse>(request);

        // assert
        Assert.AreEqual(HttpStatusCode.NotFound, response.ResponseObject!.StatusCode);
        Assert.AreEqual("{}", response.ResponseObject!.Content);
    }
}
