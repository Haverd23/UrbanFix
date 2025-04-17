using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UrbanFix.WebApi.Services
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.RequestBody != null)
                return;

            var hasFileUploadParam = context.MethodInfo
                .GetParameters()
                .Any(p => p.ParameterType == typeof(IFormFile) ||
                          (p.ParameterType.IsClass && p.ParameterType
                              .GetProperties().Any(prop => prop.PropertyType == typeof(IFormFile))));

            if (!hasFileUploadParam)
                return;

            operation.RequestBody = new OpenApiRequestBody
            {
                Content =
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties =
                            {
                                ["imagem"] = new OpenApiSchema
                                {
                                    Type = "string",
                                    Format = "binary"
                                }
                            },
                            Required = new HashSet<string> { "imagem" }
                        }
                    }
                }
            };
        }
    }
}