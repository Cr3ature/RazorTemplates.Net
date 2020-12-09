using Microsoft.Extensions.DependencyInjection;

namespace RazorTemplates.Net
{
    public static class RazorTemplatesServiceCollectionExtensions
    {
        public static IServiceCollection AddRazorTemplatesDotNet(this IServiceCollection services)
        {
            services.AddSingleton<ITemplateParser, TemplateParser>();

            return services;
        }
    }
}