using static System.Console;
using System.IO;

namespace DataProcessor
{
	internal class FileProcessor
	{
		public string InputFilePath;

		public FileProcessor(string filePath)
		{
			InputFilePath = filePath;
		}	

		public void Process()
		{
			WriteLine($"Begin process of {InputFilePath}");

			//if(File.Exists())
		}

	}
}