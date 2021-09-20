using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitweb.API.Filters
{
    public class DefaultValueSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema.Properties is null)
            {
                return;
            }

            foreach (var property in schema.Properties)
            {
                if (property.Value.Default is not null && property.Value.Example is null)
                {
                    property.Value.Example = property.Value.Default;
                }
            }
        }
    }
}
