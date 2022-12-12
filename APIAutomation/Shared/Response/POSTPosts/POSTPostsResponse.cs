using RestSharp;

namespace APIAutomation.Shared.Response.POSTPosts;

public class POSTPostsResponse : IBaseResponse
{
    public int id { get; set; }
    public RestResponse? ResponseObject { get; set; }

}
