namespace TM.PartnerStores.UnitTests.Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using TM.PartnerStores.Domain.Exceptions;
    using TM.PartnerStores.Domain.Partner.Entities;
    using Xunit;


    public class PolygonUnitTests
    {
        [Fact]
        public void Polygon_From_Enumerable_Creation_Should_Succeed()
        {
            //Given
            var points = JsonConvert.DeserializeObject<IEnumerable<IEnumerable<IEnumerable<double>>>>("[[[-43.36556,-22.99669],[-43.36539,-23.01928],[-43.26583,-23.01802],[-43.25724,-23.00649],[-43.23355,-23.00127],[-43.2381,-22.99716],[-43.23866,-22.99649],[-43.24063,-22.99756],[-43.24634,-22.99736],[-43.24677,-22.99606],[-43.24067,-22.99381],[-43.24886,-22.99121],[-43.25617,-22.99456],[-43.25625,-22.99203],[-43.25346,-22.99065],[-43.29599,-22.98283],[-43.3262,-22.96481],[-43.33427,-22.96402],[-43.33616,-22.96829],[-43.342,-22.98157],[-43.34817,-22.97967],[-43.35142,-22.98062],[-43.3573,-22.98084],[-43.36522,-22.98032],[-43.36696,-22.98422],[-43.36717,-22.98855],[-43.36636,-22.99351],[-43.36556,-22.99669]]]");

            //When
            var polygon = new Polygon(points);

            //Then
            Assert.NotNull(polygon);
        }

        [Fact]
        public void Polygon_From_Points_Creation_Should_Succeed()
        {
            //Given
            var points = JsonConvert.DeserializeObject<IEnumerable<IEnumerable<IEnumerable<double>>>>("[[[-43.36556,-22.99669],[-43.36539,-23.01928],[-43.26583,-23.01802],[-43.25724,-23.00649],[-43.23355,-23.00127],[-43.2381,-22.99716],[-43.23866,-22.99649],[-43.24063,-22.99756],[-43.24634,-22.99736],[-43.24677,-22.99606],[-43.24067,-22.99381],[-43.24886,-22.99121],[-43.25617,-22.99456],[-43.25625,-22.99203],[-43.25346,-22.99065],[-43.29599,-22.98283],[-43.3262,-22.96481],[-43.33427,-22.96402],[-43.33616,-22.96829],[-43.342,-22.98157],[-43.34817,-22.97967],[-43.35142,-22.98062],[-43.3573,-22.98084],[-43.36522,-22.98032],[-43.36696,-22.98422],[-43.36717,-22.98855],[-43.36636,-22.99351],[-43.36556,-22.99669]]]");
            var parsedPoints = points.First().Select(p => new Point(p));

            //When
            var polygon = new Polygon(parsedPoints);

            //Then
            Assert.NotNull(polygon);
        }

        [Fact]
        public void Invalid_Polygon_Should_Throws_InvalidPolygonException()
        {
            //Given
            var points = JsonConvert.DeserializeObject<IEnumerable<IEnumerable<IEnumerable<double>>>>("[[[-43.36556,-22.99669],[-43.36539,-23.01928],[-43.26583,-23.01802],[-43.25724,-23.00649],[-43.23355,-23.00127],[-43.2381,-22.99716],[-43.23866,-22.99649],[-43.24063,-22.99756],[-43.24634,-22.99736],[-43.24677,-22.99606],[-43.24067,-22.99381],[-43.24886,-22.99121],[-43.25617,-22.99456],[-43.25625,-22.99203],[-43.25346,-22.99065],[-43.29599,-22.98283],[-43.3262,-22.96481],[-43.33427,-22.96402],[-43.33616,-22.96829],[-43.342,-22.98157],[-43.34817,-22.97967],[-43.35142,-22.98062],[-43.3573,-22.98084],[-43.36522,-22.98032],[-43.36696,-22.98422],[-43.36717,-22.98855],[-43.36636,-22.99351],[-53.36556,-33.99669]]]");

            //When
            var polygonAction = new Action(() => new Polygon(points));

            //Then
            Assert.Throws<InvalidPolygonException>(polygonAction);
        }

        [Fact]
        public void Invalid_Polygon_Length_Should_Throws_InvalidPolygonException()
        {
            //Given
            var points = JsonConvert.DeserializeObject<IEnumerable<IEnumerable<IEnumerable<double>>>>("[[[-43.36556,-22.99669],[-43.36636,-22.99351],[-53.36556,-33.99669]]]");

            //When
            var polygonAction = new Action(() => new Polygon(points));

            //Then
            Assert.Throws<InvalidPolygonException>(polygonAction);
        }

        [Fact]
        public void Polygon_Creation_With_Null_Point_Parameter_Should_Throw_ArgumentNullException()
        {
            var polygonAction = new Action(() => new Polygon(null as IEnumerable<Point>));
            Assert.Throws<ArgumentNullException>(polygonAction);
        }

        [Fact]
        public void Polygon_Creation_With_Null_Enumerable_Parameter_Should_Throw_ArgumentNullException()
        {
            var polygonAction = new Action(() => new Polygon(null as IEnumerable<IEnumerable<IEnumerable<double>>>));
            Assert.Throws<ArgumentNullException>(polygonAction);
        }

        [Fact]
        public void Polygon_GetEnumerator_Should_Succeed()
        {
            //Given
            var points = JsonConvert.DeserializeObject<IEnumerable<IEnumerable<IEnumerable<double>>>>("[[[-43.36556,-22.99669],[-43.36539,-23.01928],[-43.26583,-23.01802],[-43.25724,-23.00649],[-43.23355,-23.00127],[-43.2381,-22.99716],[-43.23866,-22.99649],[-43.24063,-22.99756],[-43.24634,-22.99736],[-43.24677,-22.99606],[-43.24067,-22.99381],[-43.24886,-22.99121],[-43.25617,-22.99456],[-43.25625,-22.99203],[-43.25346,-22.99065],[-43.29599,-22.98283],[-43.3262,-22.96481],[-43.33427,-22.96402],[-43.33616,-22.96829],[-43.342,-22.98157],[-43.34817,-22.97967],[-43.35142,-22.98062],[-43.3573,-22.98084],[-43.36522,-22.98032],[-43.36696,-22.98422],[-43.36717,-22.98855],[-43.36636,-22.99351],[-43.36556,-22.99669]]]");
            var parsedPoints = points.First().Select(p => new Point(p));
            var polygon = new Polygon(parsedPoints);

            //When
            var enumerator = (IEnumerator<List<Point>>)polygon.GetEnumerator();

            //Then
            Assert.NotNull(enumerator);
        }
    }
}