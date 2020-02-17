namespace TM.PartnerStores.Repository.MongoDB.Parsers
{
    using System;
    using Repository.MongoDB.Model;
    using TM.PartnerStores.Domain.Partner.Entities;

    public class PartnerParser : IPartnerParser
    {

        private readonly ICoordinateParser _coordinateParser;

        public PartnerParser(ICoordinateParser coordinateParser)
        {
            _coordinateParser = coordinateParser ?? throw new ArgumentNullException(nameof(coordinateParser));
        }

        public Partner ToDomain(PartnerModel model)
        {
            if (model == null) return null;

            return new Partner(
                model.ApplicationId,
                model.TradingName,
                model.OwnerName,
                Document.NewDocument(model.Document),
                _coordinateParser.ToDomain(model.CoverageArea),
                _coordinateParser.ToDomain(model.Address));
        }

        public PartnerModel ToModel(Partner partner)
        {
            if (partner == null) return null;

            return new PartnerModel
            {
                ApplicationId = partner.Id,
                TradingName = partner.TradingName,
                Document = partner.Document,
                OwnerName = partner.OwnerName,
                CoverageArea = _coordinateParser.ToModel(partner.CoverageArea),
                Address = _coordinateParser.ToModel(partner.Address)
            };
        }
    }
}
