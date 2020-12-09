using Microsoft.Extensions.DependencyInjection;
using RazorTemplates.Net;
using System;
using TemplateLibrary.Models;

namespace ConsoleApp
{
    public class Program
    {
        public static async System.Threading.Tasks.Task Main()
        {
            var serviceProvider = ContainerConfiguration.Configure();
            var templateParser = serviceProvider.GetService<ITemplateParser>();

            var helloWorldModel = new HelloWorldModel { Name = "RazorTemplates.Net" };
            string result = await templateParser.Render(helloWorldModel.TemplateName, helloWorldModel);

            Console.WriteLine(result);

            result = await templateParser.Render("/Templates/NoModel.cshtml");

            Console.WriteLine(result);

            Console.ReadKey();
        }
    }
}