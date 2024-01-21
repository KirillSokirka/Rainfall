using Newtonsoft.Json;
using Rainfall.Application.Contracts.DTOs;
using Rainfall.Application.Contracts.Services;
using Rainfall.Application.Helpers;
using Rainfall.Application.Shared;

namespace Rainfall.Application.Services;

public class RainfallDataService : IRainfallDataService
{
    private readonly HttpClient _httpClient;

    public RainfallDataService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("RainfallDataService");
    }

    public async Task<Result<RainfallResponse>> GetRainfallDataAsync(RainfallQuery query)
    {
        var stationCheckUrl = $"stations?parameter=rainfall&stationReference={query.StationId}";

        var stationResponse = await _httpClient.GetAsync(stationCheckUrl);

        var stationResult = await HttpResponseHelper.DeserializeHttpResponse<StationResponse>(stationResponse);

        if (stationResult.IsFailure)
        {
            return stationResult.Error;
        }

        if (stationResult.Value.Items.Any() is false)
        {
            return Error.NotFound($"Station not with Id = {query.StationId} found.");
        }

        var queryString = $"stations/{query.StationId}/readings?_sorted&_limit={query.Count}";

        var rainfallResponse = await _httpClient.GetAsync(queryString);

        var dataResult =
            await HttpResponseHelper.DeserializeHttpResponse<RainfallExternalApiResponse>(rainfallResponse);

        if (dataResult.IsFailure)
        {
            return stationResult.Error;
        }

        return new RainfallResponse(dataResult.Value);
    }
}