using Microsoft.Extensions.DependencyInjection;
using RazorTemplates.Net;
using System;

namespace ConsoleApp
{
    internal static class ContainerConfiguration
    {
        internal static IServiceProvider Configure()
            => new ServiceCollection()
                        .AddRazorTemplatesDotNet()
                        .BuildServiceProvider();
    }
}