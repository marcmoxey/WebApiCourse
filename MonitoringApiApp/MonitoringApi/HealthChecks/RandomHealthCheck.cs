using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MonitoringApi.HealthChecks;

public class RandomHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        int responseTimeInMs = Random.Shared.Next(300);

        if(responseTimeInMs < 100)
        {
            return Task.FromResult(HealthCheckResult.Healthy($"The respone time is excellent ({responseTimeInMs}ms)"));
        }
        else if (responseTimeInMs < 200)
        {
            return Task.FromResult(HealthCheckResult.Degraded($"The respone time is greater than expected ({responseTimeInMs}ms)"));
        } else
        {
            return Task.FromResult(HealthCheckResult.Unhealthy($"The respone time is unexpecteable ({responseTimeInMs}ms)"));
        }

    }
}
