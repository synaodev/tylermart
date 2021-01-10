using System;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TylerMart.Client.Utility {
	/// <summary>
	/// Extensions for <see cref="Microsoft.AspNetCore.Http.ISession"/>
	/// </summary>
	public static class SessionExtensions {
		/// <summary>
		/// Serializes value to JSON string and adds to session
		/// </summary>
		/// <param name="session">This session</param>
		/// <param name="key">Key</param>
		/// <param name="value">Value</param>
		/// <returns>
		/// Returns 'true' if successfully serialized
		/// </returns>
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
		/// <summary>
		/// Serializes value to JSON string and adds to session
		/// </summary>
		/// <param name="session">This session</param>
		/// <param name="key">Key</param>
		/// <param name="value">Value</param>
		/// <param name="logger">Logger</param>
		/// <returns>
		/// Returns 'true' if successfully serialized
		/// </returns>
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
		/// <summary>
		/// Gets from session and deserializes value from JSON string
		/// </summary>
		/// <param name="session">This session</param>
		/// <param name="key">Key</param>
		/// <returns>
		/// Returns value at key or default (probably null for objects)
		/// </returns>
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
