namespace TM.PartnerStores.UnitTests.Domain
{
    using Xunit;
    using TM.PartnerStores.Domain.Partner.Entities;
    using System;

    public class DocumentUnitTests
    {
        [Fact]
        public void Document_Creation_Should_Succeed()
        {
            //Given
            var value = "30.617.984/0001-15";

            //When
            var document = Document.NewDocument(value);

            //Then
            Assert.NotNull(document);
        }

        [Fact]
        public void Document_Creation_With_Null_Value_Should_Throws_ArgumentNullException()
        {
            //Given
            var value = null as string;

            //When
            var documentAction = new Action(() => Document.NewDocument(value));

            //Then
            Assert.Throws<ArgumentNullException>(documentAction);
        }

        [Fact]
        public void Document_Implict_String_Should_Succeed()
        {
            //Given
            var value = "30.617.984/0001-15";

            //When
            var document = Document.NewDocument(value);

            //Then
            Assert.Equal(value, document);
        }

        [Fact]
        public void Document_ToString_Should_Succeed()
        {
            //Given
            var value = "30.617.984/0001-15";

            //When
            var document = Document.NewDocument(value);

            //Then
            Assert.Equal(value, document.ToString());
        }

        [Fact]
        public void Document_Equality_Should_Succeed()
        {
            //Given
            var value = "30.617.984/0001-15";

            //When
            var document1 = Document.NewDocument(value);
            var document2 = Document.NewDocument(value);

            //Then
            Assert.True(document1 == document2);
        }

        [Fact]
        public void Document_Difference_Should_Succeed()
        {
            //Given
            var value = "30.617.984/0001-15";
            var value2 = "30.617.984/0001-16";

            //When
            var document1 = Document.NewDocument(value);
            var document2 = Document.NewDocument(value2);

            //Then
            Assert.True(document1 != document2);
        }

        [Fact]
        public void Document_GetHashCode_Should_Succeed()
        {
            //Given
            var value = "30.617.984/0001-15";

            //When
            var document = Document.NewDocument(value);

            //Then
            Assert.True(document.GetHashCode() != default(int));
        }
    }
}