using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Linq;
using System.Reflection;

namespace RazorTemplates.Net.Engine
{
    internal sealed class RazorEngineBuilder
    {
        private Assembly _viewAssembly;

        public RazorEngine Build() => new RazorEngine(_viewAssembly);

        public RazorEngineBuilder UseEmbeddedResourcesProject(Assembly viewAssembly)
        {
            Assembly relatedAssembly = RelatedAssemblyAttribute.GetRelatedAssemblies(viewAssembly, false).SingleOrDefault();

            if (relatedAssembly == null)
            {
                _viewAssembly = viewAssembly;

                return this;
            }

            _viewAssembly = relatedAssembly;

            return this;
        }
    }
}