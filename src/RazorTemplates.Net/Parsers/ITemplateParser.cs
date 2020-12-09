using System.Threading.Tasks;

namespace RazorTemplates.Net
{
    public interface ITemplateParser
    {
        Task<string> Render(string name);

        Task<string> Render<TModel>(string name, TModel model);
    }
}