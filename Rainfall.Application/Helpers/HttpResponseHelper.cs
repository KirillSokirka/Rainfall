using System.Net;
using Newtonsoft.Json;
using Rainfall.Application.Shared;

namespace Rainfall.Application.Helpers;

public class HttpResponseHelper
{
    public static async Task<Result<T>> DeserializeHttpResponse<T>(HttpResponseMessage response)
    {
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<T>(content);

        return Result<T>.Success(result);
    }
}