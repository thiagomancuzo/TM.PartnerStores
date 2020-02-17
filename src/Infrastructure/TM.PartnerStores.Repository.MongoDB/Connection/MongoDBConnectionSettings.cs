using MongoDB.Driver;

namespace TM.PartnerStores.Repository.MongoDB.Connection
{
    public class MongoDBConnectionSettings
    {
        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }

        public string PartnerCollectionName { get; set; }
    }
}