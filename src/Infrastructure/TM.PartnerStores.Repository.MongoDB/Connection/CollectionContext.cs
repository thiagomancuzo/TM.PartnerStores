namespace TM.PartnerStores.Repository.MongoDB.Connection
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using global::MongoDB.Driver;
    using TM.PartnerStores.Repository.MongoDB.Model;

    public class CollectionContext<T> : ICollectionContext<T> where T : BaseModel
    {
        private readonly MongoDBConnectionSettings _settings;
        private readonly DbContext _dbContext;
        private readonly Lazy<IMongoCollection<T>> _lazyCollection;

        public IMongoCollection<T> GetCollection() => _lazyCollection.Value;

        public CollectionContext(MongoDBConnectionSettings settings, DbContext dbContext)
        {   
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _dbContext= dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            _lazyCollection = new Lazy<IMongoCollection<T>>(() => _dbContext.GetDatabase().GetCollection<T>(_settings.PartnerCollectionName));
        }
    }
}
