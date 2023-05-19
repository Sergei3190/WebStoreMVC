using Polly;
using Polly.Extensions.Http;

namespace WebStoreMVC.Infrastructure.Extensions
{
	public static class AddPollyExtension
	{
		public static void AddPolicyHandlers(this IHttpClientBuilder clientBuilder)
		{
			ArgumentNullException.ThrowIfNull(nameof(clientBuilder));

			clientBuilder
				.AddPolicyHandler(GetRetryPolicy())
				.AddPolicyHandler(GetCircuitBreaker());
		}

		private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int maxRetryCount = 5, int maxJitterTime = 1000)
		{
			var jitter = new Random();

			return HttpPolicyExtensions
				.HandleTransientHttpError()
				.WaitAndRetryAsync(maxRetryCount, retryAttemp => TimeSpan.FromSeconds(Math.Pow(retryAttemp, 2))
					+ TimeSpan.FromMilliseconds(jitter.Next(0, maxJitterTime)));
		}

		private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreaker() => HttpPolicyExtensions
			.HandleTransientHttpError()
			.CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 5,
								 durationOfBreak: TimeSpan.FromSeconds(30));
	}
}
