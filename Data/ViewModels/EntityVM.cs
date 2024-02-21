using System.Text.Json.Serialization;

namespace TestProjJSON.Data.ViewModels
{
	public class EntityVM
	{
		[JsonIgnore]//Excluding Guid from API VM Schema
		public Guid Guid { get; set; }

		public string Title { get; set; }
		public string Description { get; set; }
		public Guid TypeGuid { get; set; }
	}
}