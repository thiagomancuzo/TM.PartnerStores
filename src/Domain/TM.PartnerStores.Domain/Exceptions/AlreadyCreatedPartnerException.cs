namespace TM.PartnerStores.Domain.Exceptions
{
    using System;
    using Partner.Entities;

    public class AlreadyCreatedPartnerException : DomainException
    {
        public AlreadyCreatedPartnerException(Partner partner) : base($"There is an already created partner with Id {partner.Id} and Document {partner.Document}")
        {
        }

        public AlreadyCreatedPartnerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}