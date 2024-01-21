namespace Rainfall.Application.Contracts.DTOs;

public class RainfallResponse
{
    public RainfallResponse(RainfallExternalApiResponse externalApiResponse)
    {
        Readings = externalApiResponse.Items.Select(i => new RainfallData(i.DateTime, i.Value)).ToList();
    }

    public List<RainfallData> Readings { get; set; } = new();
}

public record RainfallData(DateTime DateMeasured, decimal AmountMeasured);