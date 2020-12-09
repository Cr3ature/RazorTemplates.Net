using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace RazorTemplates.Net.Engine
{
    internal sealed class RazorEngine
    {
        private readonly Assembly _viewAssembly;

        public RazorEngine(Assembly assembly)
        {
            _viewAssembly = assembly;
        }

        public async Task<string> Render<TModel>(string viewName, TModel viewModel)
        {
            RazorCompiledItem razorView = FindRazorView(viewName);

            return await GetOutput(_viewAssembly, razorView, viewModel);
        }

        private static void AddViewData<TModel>(RazorPage<TModel> razorPage, TModel model)
        {
            razorPage.ViewData = new ViewDataDictionary<TModel>(
              new EmptyModelMetadataProvider(),
              new ModelStateDictionary())
            {
                Model = model,
            };
        }

        private static async Task<string> GetOutput<TModel>(Assembly viewAssembly, RazorCompiledItem razorCompiledItem, TModel model)
        {
            using var output = new StringWriter();

            Type compiledTemplate = viewAssembly.GetType(razorCompiledItem.Type.FullName);
            var razorPage = (RazorPage)Activator.CreateInstance(compiledTemplate);

            if (razorPage is RazorPage<TModel> page)
            {
                AddViewData(page, model);
            }

            razorPage.ViewContext = new Microsoft.AspNetCore.Mvc.Rendering.ViewContext
            {
                Writer = output,
            };

            razorPage.DiagnosticSource = new DiagnosticListener("GetOutput");
            razorPage.HtmlEncoder = HtmlEncoder.Default;

            await razorPage.ExecuteAsync();

            return output.ToString();
        }

        private RazorCompiledItem FindRazorView(string viewName)
        {
            var list = new RazorCompiledItemLoader().LoadItems(_viewAssembly).ToList();
            RazorCompiledItem razorCompiledItem = new RazorCompiledItemLoader().LoadItems(_viewAssembly).FirstOrDefault(item => item.Identifier == viewName);

            if (razorCompiledItem != null)
                return razorCompiledItem;

            throw new InvalidOperationException($"Unable to find view with name '{viewName}'.");
        }
    }
}