namespace TM.PartnerStores.Application.Operations
{
    using TM.PartnerStores.Application.Contracts;
    using TM.PartnerStores.Application.Operations.IO;

    public class NullResultOperation<TResult> : IApplicationOperation<TResult>, IErrorDescriptor where TResult : IOutput
    {

        public NullResultOperation()
        {
            this.ExceptionOutput = new ExceptionOutput(null);
            this.OperationErrorType = OperationErrorType.NullResult;
        }

        public TResult Result => default;

        public ExceptionOutput ExceptionOutput { get; }

        public string Message => $"There are no {typeof(TResult).Name} resources for current criteria.";

        public bool Successful => false;

        public OperationErrorType OperationErrorType { get; }
    }
}
