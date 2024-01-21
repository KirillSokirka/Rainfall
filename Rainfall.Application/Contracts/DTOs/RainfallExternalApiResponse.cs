using Newtonsoft.Json;

namespace Rainfall.Application.Contracts.DTOs;

public class RainfallExternalApiResponse
{
    [JsonProperty("items")]
    public List<RainfallExternalData> Items { get; set; } = new();
}

public class RainfallExternalData
{
    [JsonProperty("dateTime")] public DateTime DateTime { get; set; }
    [JsonProperty("value")] public decimal Value { get; set; }
}