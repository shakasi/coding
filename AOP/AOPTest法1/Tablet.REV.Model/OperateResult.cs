using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tablet.REV.Model
{
    public class OperateResult
    {
        private bool _result = false;
        private string _message = string.Empty;
        public OperateResult()
        {
        }
        public OperateResult(bool result, string message)
        {
            _result = result;
            _message = message;
        }
        public bool Result { get { return _result; } }
        public string Message { get { return _message; } }
    }
}
