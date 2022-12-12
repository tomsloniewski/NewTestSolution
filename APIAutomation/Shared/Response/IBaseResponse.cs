using RestSharp;

namespace APIAutomation.Shared.Response;

public interface IBaseResponse
{
    RestResponse ResponseObject { get; set; }
}
