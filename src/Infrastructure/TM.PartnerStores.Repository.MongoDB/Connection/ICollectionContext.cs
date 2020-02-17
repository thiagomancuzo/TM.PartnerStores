
namespace TM.PartnerStores.Repository.MongoDB.Connection
{
    using System.Threading.Tasks;
    using global::MongoDB.Driver;
    using TM.PartnerStores.Repository.MongoDB.Model;

    public interface ICollectionContext<T> where T : BaseModel
    {
        IMongoCollection<T> GetCollection();
    }
}
