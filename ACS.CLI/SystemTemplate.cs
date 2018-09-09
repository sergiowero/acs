using System;
using System.IO;
using System.Text;

namespace ACS.CLI
{
	public class SystemTemplate : Template
	{
		private const string TEXT = @"using ACS;

namespace {namespace}
{
	public class {class} : BaseSystem
	{
		protected override void OnUpdate()
		{
			
		}
	}
}";

		protected override string TemplateText => TEXT;
	}
}
