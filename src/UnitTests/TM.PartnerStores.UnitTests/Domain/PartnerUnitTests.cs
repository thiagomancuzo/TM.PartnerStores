namespace TM.PartnerStores.UnitTests.Domain
{
    using Xunit;
    using TM.PartnerStores.Domain.Partner.Entities;
    using System;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class PartnerUnitTests
    {

        private Partner GetValidPartner()
        {
            return new Partner
            (
                1,
                "Bar da esquina",
                "Donizeti",
                Document.NewDocument("30.617.984/0001-15"),
                new Multipolygon(JsonConvert.DeserializeObject<IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>>>("[[[[-43.36556,-22.99669],[-43.36539,-23.01928],[-43.26583,-23.01802],[-43.25724,-23.00649],[-43.23355,-23.00127],[-43.2381,-22.99716],[-43.23866,-22.99649],[-43.24063,-22.99756],[-43.24634,-22.99736],[-43.24677,-22.99606],[-43.24067,-22.99381],[-43.24886,-22.99121],[-43.25617,-22.99456],[-43.25625,-22.99203],[-43.25346,-22.99065],[-43.29599,-22.98283],[-43.3262,-22.96481],[-43.33427,-22.96402],[-43.33616,-22.96829],[-43.342,-22.98157],[-43.34817,-22.97967],[-43.35142,-22.98062],[-43.3573,-22.98084],[-43.36522,-22.98032],[-43.36696,-22.98422],[-43.36717,-22.98855],[-43.36636,-22.99351],[-43.36556,-22.99669]]]]")),
                new Point(-49.1656, -25.66874)
            );
        }

        private Partner GetInvalidPartner(string nullPropName)
        {
            return new Partner
            (
                nullPropName.Equals("Id") ? default(int) : 1,
                nullPropName.Equals("TradingName") ? null : "Bar da esquina",
                nullPropName.Equals("OwnerName") ? null : "Donizeti",
                Document.NewDocument("30.617.984/0001-15"),
                nullPropName.Equals("CoverageArea") ? null : new Multipolygon(JsonConvert.DeserializeObject<IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>>>("[[[[-43.36556,-22.99669],[-43.36539,-23.01928],[-43.26583,-23.01802],[-43.25724,-23.00649],[-43.23355,-23.00127],[-43.2381,-22.99716],[-43.23866,-22.99649],[-43.24063,-22.99756],[-43.24634,-22.99736],[-43.24677,-22.99606],[-43.24067,-22.99381],[-43.24886,-22.99121],[-43.25617,-22.99456],[-43.25625,-22.99203],[-43.25346,-22.99065],[-43.29599,-22.98283],[-43.3262,-22.96481],[-43.33427,-22.96402],[-43.33616,-22.96829],[-43.342,-22.98157],[-43.34817,-22.97967],[-43.35142,-22.98062],[-43.3573,-22.98084],[-43.36522,-22.98032],[-43.36696,-22.98422],[-43.36717,-22.98855],[-43.36636,-22.99351],[-43.36556,-22.99669]]]]")),
                nullPropName.Equals("Address") ? null : new Point(-49.1656, -25.66874)
            );
        }

        [Fact]
        public void Partner_Id_Should_Have_Value()
        {
            //Given
            var partner = GetValidPartner();

            //When
            var id = partner.Id;

            //Then
            Assert.NotEqual(id, default(int));
        }

        [Fact]
        public void Partner_OwnerName_Should_Have_Value()
        {
            //Given
            var partner = GetValidPartner();

            //When
            var ownerName = partner.OwnerName;

            //Then
            Assert.NotNull(ownerName);
        }

        [Fact]
        public void Partner_TradingName_Should_Have_Value()
        {
            //Given
            var partner = GetValidPartner();

            //When
            var tradingName = partner.TradingName;

            //Then
            Assert.NotNull(tradingName);
        }

        [Fact]
        public void Partner_Document_Should_Have_Value()
        {
            //Given
            var partner = GetValidPartner();

            //When
            var document = partner.Document;

            //Then
            Assert.NotNull(document);
        }

        [Fact]
        public void Partner_CoverageArea_Should_Have_Value()
        {
            //Given
            var partner = GetValidPartner();

            //When
            var coverageArea = partner.CoverageArea;

            //Then
            Assert.NotNull(coverageArea);
        }

        [Fact]
        public void Partner_Address_Should_Have_Value()
        {
            //Given
            var partner = GetValidPartner();

            //When
            var address = partner.Address;

            //Then
            Assert.NotNull(address);
        }

        [Fact]
        public void Partner_Creation_With_Invalid_Id_Should_Throws_ArgumentException()
        {
            //When
            var partnerAction = new Action(() => GetInvalidPartner("Id"));

            //Then
            Assert.Throws<ArgumentException>(partnerAction);
        }

        [Fact]
        public void Partner_Creation_With_Invalid_TradingName_Should_Throws_ArgumentException()
        {
            //When
            var partnerAction = new Action(() => GetInvalidPartner("TradingName"));

            //Then
            Assert.Throws<ArgumentNullException>(partnerAction);
        }

        [Fact]
        public void Partner_Creation_With_Invalid_OwnerName_Should_Throws_ArgumentException()
        {
            //When
            var partnerAction = new Action(() => GetInvalidPartner("OwnerName"));

            //Then
            Assert.Throws<ArgumentNullException>(partnerAction);
        }

        [Fact]
        public void Partner_Creation_With_Invalid_CoverageArea_Should_Throws_ArgumentException()
        {
            //When
            var partnerAction = new Action(() => GetInvalidPartner("CoverageArea"));

            //Then
            Assert.Throws<ArgumentNullException>(partnerAction);
        }

        [Fact]
        public void Partner_Creation_With_Invalid_Address_Should_Throws_ArgumentException()
        {
            //When
            var partnerAction = new Action(() => GetInvalidPartner("Address"));

            //Then
            Assert.Throws<ArgumentNullException>(partnerAction);
        }
    }
}