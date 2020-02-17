namespace TM.PartnerStores.Application.Partner.Models.PartnerCreation
{
    using TM.PartnerStores.Application.Operations.IO;

    public class PartnerCreationOutput : IOutput
    {
        public string Message => "Partner successful created!";
    }
}
