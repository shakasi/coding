namespace VNext.Entity
{
    public class Include
    {
        public string Name { get; set; }
        public SqlBuilder Query { get; set; }
        public string ForeignKey { get; set; }
        public string LocalKey { get; set; }
        public bool IsMany { get; set; }
    }
}