using System;

namespace Aster.Framework.Common.Exceptions
{
    public class MyException : Exception
    {
        public MyException(string errMsg) : this(string.Empty, errMsg) { }

        public MyException(string errCode, string errMsg) : base(errMsg)
        {
            ErrCode = errCode;
            ErrMsg = errMsg;
        }

        public string ErrCode { get; set; }

        public string ErrMsg { get; set; }
    }
}
