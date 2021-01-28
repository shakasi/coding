using System;

namespace VNext.Entity
{
    /// <summary>
    /// This class is used as metadata on a property to determine if it is a primary key
    /// </summary>
    public class KeyAttribute : ColumnAttribute
    {
        public KeyAttribute([System.Runtime.CompilerServices.CallerMemberName] string name = "")
        : base(name)
        {

        }

    }
}
