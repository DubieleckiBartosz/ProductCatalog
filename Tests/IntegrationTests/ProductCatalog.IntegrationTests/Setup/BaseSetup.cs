using System.Net.Http.Headers; 
using Microsoft.AspNetCore.Mvc.Testing; 
using Newtonsoft.Json;
using ProductCatalog.IntegrationTests.Tools;

namespace ProductCatalog.IntegrationTests.Setup;

public abstract class BaseSetup : IClassFixture<WebApplicationFactory<Program>>
{
    private const string Env = "IntegrationTests";
    protected HttpClient Client; 
    protected BaseSetup(WebApplicationFactory<Program> factory)
    { 
        this.Client = factory.WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment(Env);
        }).CreateClient(); 
    }

    protected JsonSerializerSettings? SerializerSettings() => new JsonSerializerSettings
    {
        ContractResolver = new PrivateResolver(), 
        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        TypeNameHandling = TypeNameHandling.Auto,
        NullValueHandling = NullValueHandling.Ignore
    };

    protected async Task<HttpResponseMessage> ClientCall<TRequest>(TRequest obj, HttpMethod methodType,
        string requestUri)
    {
        var request = new HttpRequestMessage(methodType, requestUri);
        if (obj != null)
        {
            var serializeObject = JsonConvert.SerializeObject(obj);
            request.Content = new StringContent(serializeObject);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }

        return await this.Client.SendAsync(request);
    }


    protected async Task<TResponse?> ReadFromResponse<TResponse>(HttpResponseMessage response)
    {
        var contentString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(contentString, this.SerializerSettings());
    }
}