using System.Text;

namespace VNext.Options
{
    /// <summary>
    /// Sql语句配置
    /// </summary>
    public class SqlOption
    {
        public string Id { get; set; }

        public string From { get; set; }

        public string[] Columns { get; set; }

        public string Where { get; set; }

        public override string ToString()
        {
            var sbSql = new StringBuilder("SELECT ");
            if (Columns != null)
            {
                sbSql.Append(string.Join(',', Columns));
            }
            else
            {
                sbSql.Append("*");
            }
            sbSql.Append($" FROM {From}");

            if (!string.IsNullOrWhiteSpace(Where))
            {
                sbSql.Append($" WHERE {Where}");
            }
            return sbSql.ToString();
        }
    }
}
