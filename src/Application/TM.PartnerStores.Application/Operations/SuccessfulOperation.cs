using TM.PartnerStores.Application.Contracts;
using TM.PartnerStores.Application.Operations.IO;

namespace TM.PartnerStores.Application.Operations
{
    public class SuccessfulOperation<TResult> : IApplicationOperation<TResult> where TResult : IOutput
    {
        private readonly TResult result;

        public SuccessfulOperation(TResult result)
        {
            this.result = result;
        }

        public TResult Result => result;

        public bool Successful => true;
    }
}
