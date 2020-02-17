namespace TM.PartnerStores.Domain.Partner.Entities
{
    public struct Document
    {
        private readonly string value;

        private Document(string value)
        {
            this.value = value;
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