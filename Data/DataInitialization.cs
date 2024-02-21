using TestProjJSON.Data.Models;
using JsonFlatFileDataStore;

namespace TestProjJSON.Data
{
	public class DataInitialization
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly string _filePath;

		public DataInitialization(IServiceProvider serviceProvider, string filePath)
		{
			_serviceProvider = serviceProvider;
			_filePath = filePath;
		}

		public void InitializeData()
		{
			if (FileNotExistsOrEmpty(_filePath))
			{
				using (var serviceScope = _serviceProvider.CreateScope())
				{
					var dataStore = serviceScope.ServiceProvider.GetRequiredService<IDataStore>();

					var classifier1 = new Classifier { Guid = Guid.NewGuid(), Title = "imobil" };
					var classifier2 = new Classifier { Guid = Guid.NewGuid(), Title = "transport" };

					var entities = new[]
					{
						new Entity
						{
							Guid = Guid.NewGuid(),
							Title = "Apartament, cu 3 camere",
							TypeGuid = classifier1.Guid,
							Description = "mun.Chisinau, sec.Centru, bloc nou, etajul 3",
						},
						new Entity
						{
							Guid = Guid.NewGuid(),
							Title = "Casa de locuit",
							TypeGuid = classifier1.Guid,
							Description = "mun.Chisinau, sat.Truseni, 2 nivele",
						},
						new Entity
						{
							Guid = Guid.NewGuid(),
							Title = "Toyota RAV4",
							TypeGuid = classifier2.Guid,
							Description = "anul:2019, combustibil:motorina, locuri:5",
						}
					};

					var classifierCollection = dataStore.GetCollection<Classifier>();
					classifierCollection.InsertOne(classifier1);
					classifierCollection.InsertOne(classifier2);

					var entityCollection = dataStore.GetCollection<Entity>();
					entityCollection.InsertMany(entities);
				}
			}
		}

		private bool FileNotExistsOrEmpty(string filePath)
		{
			return !File.Exists(filePath) || new FileInfo(filePath).Length == 0;
		}
	}
}
