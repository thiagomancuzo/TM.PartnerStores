using System;
using System.Collections.Generic;
using System.Text;
using TM.PartnerStores.Application.Operations.IO;

namespace TM.PartnerStores.Application.Operations
{
    public class ExceptionOutput : IOutput
    {
        public Exception Exception { get; }

        public ExceptionOutput(Exception exception)
        {
            Exception = exception;
        }
    }
}
