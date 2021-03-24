using System;
using System.IO;
using static System.Console;

namespace DataProcessor
{
	class Program
	{
		/*
			Args type:
			1) --file C:\temp\git\.NET-Base-Class-Library-Path\DataProcessor\in\sample-text-file.txt
			2) --dir C:\temp\git\.NET-Base-Class-Library-Path\DataProcessor\in TEXT
		*/
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			var command = args[0];

			if (command == "--file")
			{
				var filePath = args[1];
				WriteLine($"Single file {filePath} selected");
				ProcessSingleFile(filePath);
			}
			else if (command == "--dir")
			{
				var directoryPath = args[1];
				var fileType = args[2];
				WriteLine($"Directory {directoryPath} selected for {fileType} files");
				ProcessDirectory(directoryPath, fileType);
			}
			else {
				WriteLine("Invalid command line options");
			}
			WriteLine("Press enter to quit.");
			ReadLine();
		}
		private static void ProcessSingleFile(string filePath)
		{
			var fileProcessor = new FileProcessor(filePath);
			fileProcessor.Process();
		}

		private static void ProcessDirectory(string directoryPath, string fileType)
		{
			//var allFiles = Directory.GetFiles(directoryPath);
			switch (fileType) {
				case "TEXT":
					string[] textFiles = Directory.GetFiles(directoryPath, "*.txt");
					foreach (var textFilePath in textFiles) {
						var fileProcessor = new FileProcessor(textFilePath);
						fileProcessor.Process();
					}
					break;
				default:
					WriteLine($"ERROR: {fileType} is not supported");
					return;
			}
		}

	}
}
