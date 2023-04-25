using System.Text.Json;
using System.Text.Json.Serialization;
using Refit;

namespace CIS.TestDataApp;

public class RefitFixture<TRefitApi> : IDisposable
{
    public TRefitApi GetRestClient(string baseAddress) 
    {
        var refitSettings = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                Converters =
                {
                    new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            })
        };

        return RestService.For<TRefitApi>(baseAddress, refitSettings);
    }

    public void Dispose()
    {
    }
}