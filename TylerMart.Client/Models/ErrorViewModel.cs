namespace TylerMart.Client.Models {
	/// <summary>
	/// Error View Model
	/// </summary>
	public class ErrorViewModel {
		/// <summary>
		/// Request ID
		/// </summary>
		public string RequestId { get; set; }
		/// <summary>
		/// Show Request ID?
		/// </summary>
		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
}
