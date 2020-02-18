namespace TM.PartnerStores.UnitTests.Domain
{
    using System;
    using TM.PartnerStores.Domain.Exceptions;
    using TM.PartnerStores.Domain.Partner.Entities;
    using Xunit;

    public class PointUnitTests
    {
        [Fact]
        public void Point_Creation_Should_Succeed()
        {
            //Given
            double lng = -49.1656;
            double lat = -25.66874;

            var coordinates = new double[] { lng, lat };

            //When
            var pointByCoordinatesList = new Point(coordinates);
            var pointsByCoordinates = new Point(lat, lng);

            //Then
            Assert.NotNull(pointByCoordinatesList);
            Assert.NotNull(pointsByCoordinates);

            Assert.True(pointsByCoordinates.Lng == lng);
            Assert.True(pointsByCoordinates.Lat == lat);

            Assert.True(pointByCoordinatesList.Lng == lng);
            Assert.True(pointByCoordinatesList.Lat == lat);
        }

        [Fact]
        public void Point_ToString_Should_Be_Formatted()
        {
            //Given
            double lng = -49.1656;
            double lat = -25.66874;

            //When
            var pointsByCoordinates = new Point(lat, lng);

            //Then
            Assert.True(pointsByCoordinates.ToString().Equals($"Lat={lat},Lng={lng}"));
        }

        [Fact]
        public void Invalid_Coordinates_Should_Throw_InvalidPointException()
        {
            //Given
            var coordinates = new double[] { -49.1656 };
            //When
            var pointAction = new Action(() => new Point(coordinates));

            //Then
            Assert.Throws<InvalidPointException>(pointAction);
        }

        [Fact]
        public void Point_Equality_Should_Succeed()
        {
            //Given
            var points1 = new Point(-49.1656, -25.66874);
            var points2 = new Point(-49.1656, -25.66874);

            //When
            var trueResult = points1 == points2;
            var falseResult = points1 != points2;

            //Then
            Assert.True(trueResult);
            Assert.False(falseResult);
        }
    }
}