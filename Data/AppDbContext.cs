using JsonFlatFileDataStore;
using TestProjJSON.Data.Models;

namespace TestProjJSON.Data
{
	public class AppDataContext
	{
		private readonly IDataStore _dataStore;

		public AppDataContext(IDataStore dataStore)
		{
			_dataStore = dataStore;
		}

		public IDocumentCollection<Entity> Entities => _dataStore.GetCollection<Entity>();
		public IDocumentCollection<Classifier> Classifiers => _dataStore.GetCollection<Classifier>();

	}
}