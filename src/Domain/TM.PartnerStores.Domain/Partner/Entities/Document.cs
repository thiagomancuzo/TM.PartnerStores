namespace TM.PartnerStores.Domain.Partner.Entities
{
    using System;

    public struct Document
    {
        private readonly string value;

        private Document(string value)
        {
            this.value = value ?? throw new ArgumentNullException(nameof(value)); ;

            ThrowIfInvalidDocument(value);
        }

        private void ThrowIfInvalidDocument(string value)
        {
            // some validations may to be here, but the initial load have invalid documents
        }

        public static Document NewDocument(string document)
        {
            return new Document(document);
        }

        public override string ToString()
        {
            return value;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public static implicit operator string(Document d) => d.ToString();

        public static bool operator ==(Document left, Document right) => left.Equals(right);

        public static bool operator !=(Document left, Document right) => !(left == right);
    }
}