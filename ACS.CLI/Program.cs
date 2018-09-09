using CommandLine;
using System.Collections.Generic;
using System;
using System.IO;

namespace ACS.CLI
{
	class BaseOptions
	{
		[Value(0, HelpText = "File path of the new element", Required = true)]
		public string FilePath { get; set; }

		[Option('c', "classname", HelpText = "Class name of the component. Defaults to file name")]
		public string ClassName { get; set; }

		[Option('n', "namespace", HelpText = "Namespace name of the component. Defaults to 'Default'")]
		public string Namespace { get; set; }
	}

	[Verb("createcomp", HelpText = "Create a new empty component")]
	class CreateComponentOptions : BaseOptions
	{

	}

	[Verb("createsystem", HelpText = "Create a new empty system")]
	class CreateSystemOptions : BaseOptions
	{

	}

	class Program
	{
		static int Main(string[] args)
		{
			var result = Parser.Default.ParseArguments<CreateComponentOptions, CreateSystemOptions>(args);

			int res = result.MapResult(
				(CreateComponentOptions opts) => { return CreateFile(new ComponentTemplate(), opts); },
				(CreateSystemOptions opts) => { return CreateFile(new SystemTemplate(), opts); },
				errs => 1);

			//Console.ReadKey();
			return res;
		}

		static int CreateFile(Template template, BaseOptions opts)
		{
			try
			{
				template.CreateFile(opts.FilePath, opts.ClassName, opts.Namespace);
				return 0;
			}
			catch (IOException)
			{
				Console.WriteLine("File already exist");
				return 1;
			}
		}
	}
}
