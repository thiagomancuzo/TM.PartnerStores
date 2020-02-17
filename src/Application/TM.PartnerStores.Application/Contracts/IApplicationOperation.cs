namespace TM.PartnerStores.Application.Contracts
{
    using TM.PartnerStores.Application.Operations.IO;

    public interface IApplicationOperation<TResult> where TResult : IOutput
    {
        TResult Result { get; }

        bool Successful { get; }
    }
}
