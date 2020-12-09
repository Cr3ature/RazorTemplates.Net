namespace TemplateLibrary.Models
{
    public class HelloWorldModel
    {
        public string Name { get; set; }

        public string TemplateName
            => "/Templates/HelloWorld.cshtml";
    }
}