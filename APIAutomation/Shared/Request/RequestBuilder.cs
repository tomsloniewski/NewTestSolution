using APIAutomation.Shared.BasicTestConfiguration;
using RestSharp;

namespace APIAutomation.Shared.Request;

public class RequestBuilder
{
    private RestRequest _request;

    public RequestBuilder(BaseTest test, string endpoint, Method method)
    {
        string resource = test.Configuration.BaseUrl + endpoint;
        _request = new RestRequest(resource, method);
    }

    public RequestBuilder AddPathParameter(string pathParameterValue, string pathParameterName = null)
    {
        if (pathParameterName is not null)
        {
            _request.Resource = $"{_request.Resource}/{pathParameterName}";
        }
        _request.Resource = $"{_request.Resource}/{pathParameterValue}";
        return this;
    }
    public RequestBuilder AddBody(object body)
    {
        _request.AddBody(body);
        return this;
    }

    public RequestBuilder AddHeader(string name, string value)
    {
        _request.AddHeader(name, value);
        return this;
    }

    public RestRequest Build()
    {
        return _request;
    }
}
