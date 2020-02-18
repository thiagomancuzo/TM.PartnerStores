namespace TM.PartnerStores.UnitTests.Domain
{
    using Xunit;
    using TM.PartnerStores.Domain.Partner.Entities;
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.Linq;

    public class MultiPolygonUnitTests
    {
        [Fact]
        public void MultiPolygon_From_Enumerable_Creation_Should_Succeed()
        {
            //Given
            var points = JsonConvert.DeserializeObject<IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>>>("[[[[-43.36556,-22.99669],[-43.36539,-23.01928],[-43.26583,-23.01802],[-43.25724,-23.00649],[-43.23355,-23.00127],[-43.2381,-22.99716],[-43.23866,-22.99649],[-43.24063,-22.99756],[-43.24634,-22.99736],[-43.24677,-22.99606],[-43.24067,-22.99381],[-43.24886,-22.99121],[-43.25617,-22.99456],[-43.25625,-22.99203],[-43.25346,-22.99065],[-43.29599,-22.98283],[-43.3262,-22.96481],[-43.33427,-22.96402],[-43.33616,-22.96829],[-43.342,-22.98157],[-43.34817,-22.97967],[-43.35142,-22.98062],[-43.3573,-22.98084],[-43.36522,-22.98032],[-43.36696,-22.98422],[-43.36717,-22.98855],[-43.36636,-22.99351],[-43.36556,-22.99669]]]]");

            //When
            var polygon = new Multipolygon(points);

            //Then
            Assert.NotNull(polygon);
        }

        [Fact]
        public void MultiPolygon_From_Polygons_Creation_Should_Succeed()
        {
            //Given
            var points = JsonConvert.DeserializeObject<IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>>>("[[[[-43.36556,-22.99669],[-43.36539,-23.01928],[-43.26583,-23.01802],[-43.25724,-23.00649],[-43.23355,-23.00127],[-43.2381,-22.99716],[-43.23866,-22.99649],[-43.24063,-22.99756],[-43.24634,-22.99736],[-43.24677,-22.99606],[-43.24067,-22.99381],[-43.24886,-22.99121],[-43.25617,-22.99456],[-43.25625,-22.99203],[-43.25346,-22.99065],[-43.29599,-22.98283],[-43.3262,-22.96481],[-43.33427,-22.96402],[-43.33616,-22.96829],[-43.342,-22.98157],[-43.34817,-22.97967],[-43.35142,-22.98062],[-43.3573,-22.98084],[-43.36522,-22.98032],[-43.36696,-22.98422],[-43.36717,-22.98855],[-43.36636,-22.99351],[-43.36556,-22.99669]]]]");
            var polygons = points.Select(p => new Polygon(p));

            //When
            var polygon = new Multipolygon(polygons);

            //Then
            Assert.NotNull(polygon);
        }

        [Fact]
        public void MultiPolygon_Creation_With_Null_Polygons_Parameter_Should_Throw_ArgumentNullException()
        {
            var multiPolygonAction = new Action(() => new Multipolygon(null as IEnumerable<Polygon>));
            Assert.Throws<ArgumentNullException>(multiPolygonAction);
        }

        [Fact]
        public void MultiPolygon_Creation_With_Null_Enumerable_Parameter_Should_Throw_ArgumentNullException()
        {
            var multiPolygonAction = new Action(() => new Multipolygon(null as IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>>));
            Assert.Throws<ArgumentNullException>(multiPolygonAction);
        }

        [Fact]
        public void MultiPolygon_GetEnumerator_Should_Succeed()
        {
            //Given
            var points = JsonConvert.DeserializeObject<IEnumerable<IEnumerable<IEnumerable<IEnumerable<double>>>>>("[[[[-43.36556,-22.99669],[-43.36539,-23.01928],[-43.26583,-23.01802],[-43.25724,-23.00649],[-43.23355,-23.00127],[-43.2381,-22.99716],[-43.23866,-22.99649],[-43.24063,-22.99756],[-43.24634,-22.99736],[-43.24677,-22.99606],[-43.24067,-22.99381],[-43.24886,-22.99121],[-43.25617,-22.99456],[-43.25625,-22.99203],[-43.25346,-22.99065],[-43.29599,-22.98283],[-43.3262,-22.96481],[-43.33427,-22.96402],[-43.33616,-22.96829],[-43.342,-22.98157],[-43.34817,-22.97967],[-43.35142,-22.98062],[-43.3573,-22.98084],[-43.36522,-22.98032],[-43.36696,-22.98422],[-43.36717,-22.98855],[-43.36636,-22.99351],[-43.36556,-22.99669]]]]");

            //When
            var multipolygon = new Multipolygon(points);

            //Then
            Assert.NotNull(multipolygon.GetEnumerator());
        }
    }
}