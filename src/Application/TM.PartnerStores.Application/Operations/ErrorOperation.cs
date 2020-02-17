namespace TM.PartnerStores.Application.Operations
{
    using System;
    using TM.PartnerStores.Application.Contracts;
    using TM.PartnerStores.Application.Operations.IO;

    public class ErrorOperation<TResult> : IApplicationOperation<TResult>, IErrorDescriptor where TResult : IOutput
    {

        public ErrorOperation(Exception exception, OperationErrorType operationErrorType)
        {
            this.ExceptionOutput = new ExceptionOutput(exception);
            this.OperationErrorType = operationErrorType;
        }

        public TResult Result => default;

        public ExceptionOutput ExceptionOutput { get; }

        public string Message => $"An {OperationErrorType} error has ocurred while operating over {typeof(TResult).Name}: {ExceptionOutput.Exception.Message}";

        public bool Successful => false;

        public OperationErrorType OperationErrorType { get; }
    }
}
