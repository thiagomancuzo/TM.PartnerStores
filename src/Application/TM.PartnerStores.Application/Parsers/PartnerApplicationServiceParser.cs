namespace TM.PartnerStores.Application.Parsers
{
    using System;
    using System.Collections.Generic;

    using TM.PartnerStores.Application.Contracts;
    using TM.PartnerStores.Application.Partner.Models;
    using TM.PartnerStores.Application.Partner.Models.PartnerCreation;
    using TM.PartnerStores.Application.Partner.Models.PartnerList;
    using TM.PartnerStores.Application.Partner.Models.SinglePartnerRetrieve;
    using TM.PartnerStores.Domain.Partner.Entities;

    public class PartnerApplicationServiceParser : IPartnerApplicationServiceParser
    {
        public Partner FromPartnerCreationInput(PartnerCreationInput input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            return ModelToDomain(input);
        }

        public Point FromPartnerSearchInput(PartnerSearchInput input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            return new Point(input.Lat, input.Lng);
        }

        public PartnerListOutput ToPartnerListOutput(IEnumerable<Partner> partners)
        {
            if (partners == null) throw new ArgumentNullException(nameof(partners));

            var partnerListOutput = new PartnerListOutput();
            foreach(var partner in partners)
            {
                partnerListOutput.Pdvs.Add(DomainToModel<PartnerModel>(partner));
            }

            return partnerListOutput;
        }

        public PartnerSearchOutput ToPartnerSearchOutput(Partner partner)
        {
            if(partner == null) return null;

            return DomainToModel<PartnerSearchOutput>(partner);
        }

        public SinglePartnerRetrieveOutput ToSinglePartnerRetrieveOutput(Partner partner)
        {
            if(partner == null) return null;

            return DomainToModel<SinglePartnerRetrieveOutput>(partner);
        }

        private Partner ModelToDomain(PartnerModel model)
        {
            if(model == null) return null;
            
            return new Partner(
                model.Id,
                model.TradingName,
                model.OwnerName,
                Document.NewDocument(model.Document),
                new Multipolygon(model.CoverageArea.Coordinates),
                new Point(model.Address.Coordinates));
        }

        private T DomainToModel<T>(Partner partner) where T : PartnerModel, new()
        {
            return new T
            {
                Id = partner.Id,
                Address = new GeoJson<IEnumerable<double>>
                {
                    Coordinates = partner.Address,
                    Type = GeoJsonType.Point
                },
                Document = partner.Document,
                OwnerName = partner.OwnerName,
                TradingName = partner.TradingName,
                CoverageArea = new GeoJson<IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>>>
                {
                    Type = GeoJsonType.Multipolygon,
                    Coordinates = partner.CoverageArea
                }
            };
        }
    }
}
