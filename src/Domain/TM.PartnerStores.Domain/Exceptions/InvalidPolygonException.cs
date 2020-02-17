namespace TM.PartnerStores.Domain.Exceptions
{
    using System;

    public class InvalidPolygonException : DomainException
    {
        public InvalidPolygonException(string message) : base(message)
        { }

        public InvalidPolygonException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
