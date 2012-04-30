using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ResultStore.Processing.Import
{
    public class ImportFormatNotSupportedException : Exception
    {
        public ImportFormatNotSupportedException(string message) : base(message) { }

        //---------------------------------------------------------------------------------------------------

        public ImportFormatNotSupportedException(string message, Exception inner_exception) : base(message, inner_exception) { }
    }
}
