using TestProjJSON.Data.Models;
using TestProjJSON.Data.ViewModels;
using JsonFlatFileDataStore;
using TestProjJSON.Exceptions;

namespace TestProjJSON.Data.Services
{
    public class ClassifierService
    {
        private readonly IDataStore _dataStore;

        public ClassifierService(IDataStore dataStore)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
        }

        public void AddClassifier(ClassifierVM classifier)
        {
            if (classifier == null)
            {
                throw new ArgumentNullException(nameof(classifier));
            }

            var newClassifier = new Classifier
            {
                Guid = Guid.NewGuid(),
                Title = classifier.Title,
            };

            _dataStore.GetCollection<Classifier>().InsertOne(newClassifier);
        }

        public List<Classifier> GetAllClassifiers()
        {
            return _dataStore.GetCollection<Classifier>().AsQueryable().ToList();
        }

        public Classifier GetClassifierById(Guid guid)
        {
            var classifier = _dataStore.GetCollection<Classifier>().AsQueryable().SingleOrDefault(c => c.Guid == guid);

            if (classifier == null)
            {
                throw new GuidNotFoundException($"Classifier with id: {guid} not found");
            }

            return classifier;
        }

        public void UpdateClassifierById(Guid classifierId, ClassifierVM classifier)
        {
            if (classifier == null)
            {
                throw new ArgumentNullException(nameof(classifier));
            }

            var classifierCollection = _dataStore.GetCollection<Classifier>();
            var existingClassifier = classifierCollection.AsQueryable().SingleOrDefault(c => c.Guid == classifierId);

            if (existingClassifier == null)
            {
                throw new GuidNotFoundException($"Classifier with id: {classifierId} not found");
            }

            existingClassifier.Title = classifier.Title;
            classifierCollection.UpdateOne(c => c.Guid == classifierId, existingClassifier);
        }

        public void DeleteClassifierById(Guid classifierId)
        {
            var classifierCollection = _dataStore.GetCollection<Classifier>();
            var entityCollection = _dataStore.GetCollection<Entity>();

            var classifier = classifierCollection.AsQueryable().SingleOrDefault(c => c.Guid == classifierId);

            if (classifier == null)
            {
                throw new GuidNotFoundException($"Classifier with id: {classifierId} not found");
            }

            // Check if the classifier is associated with any entity
            bool isAssociatedWithEntity = entityCollection.AsQueryable().Any(e => e.TypeGuid == classifierId);

            if (isAssociatedWithEntity)
            {
                throw new InvalidOperationException("Cannot delete the classifier as it is associated with an entity");
            }

            // If not associated with any entity, proceed with deletion
            classifierCollection.DeleteOne(c => c.Guid == classifierId);
        }
    }

}