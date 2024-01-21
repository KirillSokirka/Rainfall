namespace Rainfall.Application.Contracts.DTOs;

public record RainfallQuery( string StationId, int Count = 10);