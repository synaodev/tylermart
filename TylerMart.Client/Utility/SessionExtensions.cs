using System;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace TylerMart.Client.Utility {
	public static class SessionExtensions {
		public static bool SetAsJson<T>(this ISession session, string key, T value) {
			bool success = true;
			try {
				session.SetString(key, JsonSerializer.Serialize(value));
			} catch (NotSupportedException ex) {
				System.Console.WriteLine(ex);
				success = false;
			}
			return success;
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
