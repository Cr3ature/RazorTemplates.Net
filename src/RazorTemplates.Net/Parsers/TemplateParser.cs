using RazorTemplates.Net.Engine;
using System.Threading.Tasks;

namespace RazorTemplates.Net
{
    internal sealed class TemplateParser : ITemplateParser
    {
        public async Task<string> Render(string templateName)
            => await Render<object>(templateName, null);

        public async Task<string> Render<TModel>(string templateName, TModel model)
        {
            RazorEngine engine = new RazorEngineBuilder()
                 .UseEmbeddedResourcesProject(typeof(TModel).Assembly)
                 .Build();

            return await engine.Render(templateName, model);
        }
    }
}