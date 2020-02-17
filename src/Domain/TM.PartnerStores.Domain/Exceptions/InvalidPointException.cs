namespace TM.PartnerStores.Domain.Exceptions
{
    using System;

    public class InvalidPointException : DomainException
    {
        public InvalidPointException(string message) : base(message)
        { }

        public InvalidPointException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
