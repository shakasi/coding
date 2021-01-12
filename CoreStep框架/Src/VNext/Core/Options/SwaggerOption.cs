using System.Collections.Generic;

namespace VNext.Options
{
    /// <summary>
    /// Swagger选项
    /// </summary>
    public class SwaggerOption
    {
        public ICollection<SwaggerEndpoint> Endpoints { get; set; } = new List<SwaggerEndpoint>();

        public string RoutePrefix { get; set; }

        public bool MiniProfiler { get; set; } = true;

        public bool Enabled { get; set; }
    }

    public class SwaggerEndpoint
    {
        public string Title { get; set; }

        public string Version { get; set; }

        public string Url { get; set; }
    }
}