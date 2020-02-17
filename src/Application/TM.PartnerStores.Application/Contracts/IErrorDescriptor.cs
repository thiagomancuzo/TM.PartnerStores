using TM.PartnerStores.Application.Operations;

namespace TM.PartnerStores.Application.Contracts
{
    public interface IErrorDescriptor
    {
        OperationErrorType OperationErrorType { get; }

        string Message { get; }

        ExceptionOutput ExceptionOutput { get; }
    }
}