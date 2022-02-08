using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alone
{
    class CustomException : Exception
    {
        private string exceptionMessage;

        public CustomException() : base() {}

        public CustomException(string message) : base(message)
        {
            exceptionMessage = message;
        }

        public string ExceptionMessage
        {
            get { return exceptionMessage; }
            set { exceptionMessage = value; }
        }
    }
}
