namespace TM.PartnerStores.Repository.MongoDB.Migration
{
    using System;
    using System.Threading.Tasks;
    using global::MongoDB.Driver;
    using TM.PartnerStores.Repository.MongoDB.Connection;
    using TM.PartnerStores.Repository.MongoDB.Model;


    public class DatabaseCreator
    {
        private readonly DbContext _dbContext;
        private readonly ICollectionContext<PartnerModel> _collectionContext;

        public DatabaseCreator(DbContext dbContext, ICollectionContext<PartnerModel> collectionContext)
        {
            _dbContext = dbContext;
            _collectionContext = collectionContext;
        }

        public async Task CreateIfNotExistsAsync() => await CreateIfNotExistsAsync(null);

        public async Task CreateIfNotExistsAsync(Func<Task> runItIfCreate)
        {
            if(! await _dbContext.DatabaseExistsAsync().ConfigureAwait(false))
            {
                try
                {
                    SetUniqueDocumentIndex();
                    SetUniqueApplicationIdIndex();
                    SetGeoJsonIndexes();

                    if (runItIfCreate != null)
                    {
                        await runItIfCreate.Invoke().ConfigureAwait(false);
                    }
                }
                catch
                {
                    await _dbContext.DropDatabase().ConfigureAwait(false);
                    throw;
                }
            }
        }

        private void SetUniqueApplicationIdIndex()
        {
            var options = new CreateIndexOptions() { Unique = true };
            var definition = new IndexKeysDefinitionBuilder<PartnerModel>().Ascending(p => p.ApplicationId);
            var model = new CreateIndexModel<PartnerModel>(definition, options);

            _collectionContext.GetCollection().Indexes.CreateOne(model);
        }

        private void SetUniqueDocumentIndex()
        {
            var options = new CreateIndexOptions() { Unique = true };
            var definition = new IndexKeysDefinitionBuilder<PartnerModel>().Ascending(p => p.Document);
            var model = new CreateIndexModel<PartnerModel>(definition, options);

            _collectionContext.GetCollection().Indexes.CreateOne(model);
        }

        private void SetGeoJsonIndexes()
        {
            var options = new CreateIndexOptions();
            var definition = new IndexKeysDefinitionBuilder<PartnerModel>()
                .Geo2DSphere(p => p.Address)
                ;

            var model = new CreateIndexModel<PartnerModel>(definition, options);

            _collectionContext.GetCollection().Indexes.CreateOne(model);
        }

    }
}
