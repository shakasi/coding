using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityDemoFrmework
{
    public interface IBook
    {
        string Write();
    }

    public class ABook : IBook
    {
        public string Write() => "This is A";
    }

    public class BBook : IBook
    {
        public string Write() => "This is B";
    }

    public class CBook : IBook
    {
        public string Write() => "This is C";
    }
}
