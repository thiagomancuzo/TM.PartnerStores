namespace TM.PartnerStores.Repository.MongoDB.Partner
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using global::MongoDB.Driver;
    using TM.PartnerStores.Domain.Partner.Entities;
    using TM.PartnerStores.Domain.Repositories;
    using TM.PartnerStores.Repository.MongoDB.Connection;
    using TM.PartnerStores.Repository.MongoDB.Model;
    using TM.PartnerStores.Repository.MongoDB.Parsers;

    public class PartnerRepository : IPartnerRepository
    {
        private readonly ICollectionContext<PartnerModel> _database;
        private readonly IPartnerParser _partnerParser;
        private readonly ICoordinateParser _coordinateParser;

        public PartnerRepository(ICollectionContext<PartnerModel> database, IPartnerParser partnerParser, ICoordinateParser coordinateParser)
        {
            _database = database;
            _partnerParser = partnerParser;
            _coordinateParser = coordinateParser;
        }

        public async Task CreateAsync(Partner partner)
        {
            await _database.GetCollection().InsertOneAsync(_partnerParser.ToModel(partner)).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Partner>> GetAsync()
        {
            return (await _database.GetCollection().FindAsync(FilterDefinition<PartnerModel>.Empty)
                .ConfigureAwait(false))
                .ToList()
                .Select(pm => _partnerParser.ToDomain(pm));
        }

        public async Task<Partner> GetAsync(int id)
        {
            var filter = Builders<PartnerModel>.Filter.Eq("applicationId", id);

            using (var cursor = await _database.GetCollection().FindAsync(filter).ConfigureAwait(false))
            {
                var dbPartner = await cursor.FirstOrDefaultAsync().ConfigureAwait(false);
                return _partnerParser.ToDomain(dbPartner);
            }
        }

        public async Task<Partner> GetNearstAsync(Point location)
        {
            var point = _coordinateParser.ToModel(location);
            var builder = Builders<PartnerModel>.Filter;
            var filter = builder.GeoIntersects(p => p.CoverageArea, point) & builder.Near(p => p.Address, point);

            using (var cursor = await _database.GetCollection().FindAsync(filter).ConfigureAwait(false))
            {
                return _partnerParser.ToDomain(await cursor.FirstOrDefaultAsync().ConfigureAwait(false));
            }
        }
    }
}
