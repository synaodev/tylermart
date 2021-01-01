using System;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TylerMart.Client.Utility {
	public static class SessionExtensions {
		public static bool SetAsJson<T>(this ISession session, string key, T value) {
			bool success = true;
			try {
				session.SetString(key, JsonSerializer.Serialize(value));
			} catch (NotSupportedException) { // Expected
				success = false;
			} catch (Exception) { // Unexpected
				success = false;
				throw;
			}
			return success;
		}
		public static bool SetAsJson<T, C>(this ISession session, string key, T value, ILogger<C> logger) {
			if (!session.SetAsJson(key, value)) {
				logger.LogError(
					"Failed to serialize \"{0} to \"{1}\" for session data!",
					key.ToString(),
					value
				);
				return false;
			}
			return true;
		}
		public static T GetFromJson<T>(this ISession session, string key) {
			var result = default(T);
			if (!string.IsNullOrEmpty(key)) {
				string data = session.GetString(key);
				if (!string.IsNullOrEmpty(data)) {
					result = JsonSerializer.Deserialize<T>(data);
				}
			}
			return result;
		}
	}
}
