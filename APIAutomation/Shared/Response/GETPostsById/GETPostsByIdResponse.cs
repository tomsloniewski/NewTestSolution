using RestSharp;

namespace APIAutomation.Shared.Response.GetPostById;

public class GETPostsByIdResponse : IBaseResponse
{
    public int userId { get; set; }
    public int id { get; set; }
    public string? title { get; set; }
    public string? body { get; set; }
    public RestResponse? ResponseObject { get; set; }
}
