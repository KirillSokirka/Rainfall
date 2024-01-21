using Rainfall.Application.Contracts.DTOs;
using Rainfall.Application.Shared;

namespace Rainfall.Application.Contracts.Services;

public interface IRainfallDataService
{
    Task<Result<RainfallResponse>> GetRainfallDataAsync(RainfallQuery query);
}