
namespace ACS.CLI
{
	public class ComponentTemplate : Template
	{
		public const string TEXT = @"using ACS;

namespace {namespace}
{
	public class {class} : IComponent
	{
		public void Reset()
		{
			
		}
	}
}";

		protected override string TemplateText => TEXT;
	}
}
