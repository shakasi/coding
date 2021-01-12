using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Reflection;
using VNext.Extensions;

namespace VNext.AspNetCore.Swagger
{
    /// <summary>
    /// Swagger隐藏过滤器
    /// </summary>
    public class HiddenApiFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (ApiDescription apiDescription in context.ApiDescriptions)
            {
                if (apiDescription.TryGetMethodInfo(out MethodInfo method))
                {
                    if (method.ReflectedType.HasAttribute<HiddenApiAttribute>() || method.HasAttribute<HiddenApiAttribute>())
                    {
                        string key = $"/{apiDescription.RelativePath}";
                        if (key.Contains("?"))
                        {
                            int index = key.IndexOf("?", StringComparison.Ordinal);
                            key = key.Substring(0, index);
                        }

                        swaggerDoc.Paths.Remove(key);
                    }
                }
            }
        }
    }
}
