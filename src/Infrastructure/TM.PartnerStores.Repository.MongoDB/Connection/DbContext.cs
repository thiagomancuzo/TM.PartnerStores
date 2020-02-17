namespace TM.PartnerStores.Repository.MongoDB.Connection
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using global::MongoDB.Driver;

    public class DbContext
    {
        private readonly MongoDBConnectionSettings _settings;

        private readonly MongoClient _client;
        private readonly Lazy<IMongoDatabase> _lazyDatabase;

        public DbContext(MongoDBConnectionSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            _client = new MongoClient(settings.ConnectionString);
            _lazyDatabase = new Lazy<IMongoDatabase>(() => _client.GetDatabase(settings.DatabaseName));
        }

        public async Task DropDatabase()
        {
            await _client.DropDatabaseAsync(_settings.DatabaseName).ConfigureAwait(false);
        }

        public IMongoDatabase GetDatabase() => _lazyDatabase.Value;


        public async Task<bool> DatabaseExistsAsync()
        {
            using (var cursor = _client.ListDatabaseNames())
            {
                return (await cursor.ToListAsync().ConfigureAwait(false)).Any(n => n.Equals(_settings.DatabaseName, StringComparison.InvariantCultureIgnoreCase));
            }
        }
    }
}
