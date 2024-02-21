using TestProjJSON.Data.Models;
using TestProjJSON.Data.ViewModels;
using JsonFlatFileDataStore;
using TestProjJSON.Exceptions;

namespace TestProjJSON.Data.Services
{
    public class EntityService
    {
        private readonly IDataStore _dataStore;

        public EntityService(IDataStore dataStore)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
        }

        public void AddEntity(EntityVM entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (!_dataStore.GetCollection<Classifier>().AsQueryable().Any(c => c.Guid == entity.TypeGuid))
            {
                throw new GuidNotFoundException($"Classifier with id: {entity.TypeGuid} not found.");
            }

            var newEntity = new Entity
            {
                Guid = Guid.NewGuid(),
                Title = entity.Title,
                Description = entity.Description,
                TypeGuid = entity.TypeGuid,
            };

            _dataStore.GetCollection<Entity>().InsertOne(newEntity);
        }

        public List<Entity> GetAllEntities()
        {
            return _dataStore.GetCollection<Entity>().AsQueryable().ToList();
        }

        public Entity GetEntityById(Guid guid)
        {
            var entity = _dataStore.GetCollection<Entity>().AsQueryable().SingleOrDefault(e => e.Guid == guid);

            if (entity == null)
            {
                throw new GuidNotFoundException($"Entity with id: {guid} not found");
            }

            return entity;
        }

        public void UpdateEntityById(Guid entityId, EntityVM entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (!_dataStore.GetCollection<Entity>().AsQueryable().Any(e => e.Guid == entityId))
            {
                throw new GuidNotFoundException($"Entity with id: {entityId} not found");
            }

            if (!_dataStore.GetCollection<Classifier>().AsQueryable().Any(c => c.Guid == entity.TypeGuid))
            {
                throw new GuidNotFoundException($"Classifier with id: {entity.TypeGuid} not found.");
            }

            var entityCollection = _dataStore.GetCollection<Entity>();
            var existingEntity = entityCollection.AsQueryable().SingleOrDefault(e => e.Guid == entityId);

            existingEntity.Title = entity.Title;
            existingEntity.Description = entity.Description;
            existingEntity.TypeGuid = entity.TypeGuid;

            entityCollection.UpdateOne(e => e.Guid == entityId, existingEntity);
        }

        public void DeleteEntityById(Guid entityId)
        {
            if (!_dataStore.GetCollection<Entity>().AsQueryable().Any(e => e.Guid == entityId))
            {
                throw new GuidNotFoundException($"Entity with id: {entityId} not found");
            }

            var entityCollection = _dataStore.GetCollection<Entity>();
            entityCollection.DeleteOne(e => e.Guid == entityId);
        }
    }

}