namespace VNext.Entity
{
    public partial class SqlBuilder
    {
        public SqlBuilder AsDelete()
        {
            Method = "delete";
            return this;
        }

    }
}