namespace TM.PartnerStores.UnitTests.Domain
{
    using System;
    using TM.PartnerStores.Domain.Exceptions;
    using TM.PartnerStores.Domain.Partner.Entities;
    using Xunit;

    public class PointUnitTest
    {
        [Fact]
        public void Point_Creation_Should_Succeed()
        {
            //Given
            var coordinates = new double[] { -49.1656, 25.66874 };

            //When
            var point = new Point(coordinates);

            //Then
            Assert.NotNull(point);
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
    }
}