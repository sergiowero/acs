using System;
using System.IO;
using System.Text;

namespace ACS.CLI
{
	public abstract class Template
	{
		protected abstract string TemplateText { get; }

		public void CreateFile(string filePath, string className = null, string @namespace = null)
		{
			if (string.IsNullOrWhiteSpace(filePath))
			{
				throw new ArgumentNullException("fileName");
			}

			string fullPath = Path.GetFullPath(filePath);
			if (!Path.HasExtension(fullPath))
			{
				fullPath = fullPath.TrimEnd('.') + ".cs";
			}
			string fileName = Path.GetFileName(fullPath);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);



			className = string.IsNullOrWhiteSpace(className) ? fileNameWithoutExtension : className;
			@namespace = string.IsNullOrWhiteSpace(@namespace) ? "Default" : @namespace;

			using (StreamWriter stream = new StreamWriter(fullPath))
			{
				StringBuilder builder = new StringBuilder(TemplateText);
				builder.Replace("{namespace}", @namespace);
				builder.Replace("{class}", fileNameWithoutExtension);
				stream.Write(builder.ToString());
			}
		}
	}
}
