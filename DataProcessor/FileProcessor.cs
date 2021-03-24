using static System.Console;
using System.IO;
using System;

namespace DataProcessor
{
	internal class FileProcessor
	{
		private static readonly string BackupDirectoryName = "backup";
		private static readonly string InProgressDirectoryName = "processing";
		private static readonly string CompleteDirectoryName = "complete";

		public string InputFilePath;

		public FileProcessor(string filePath)
		{
			InputFilePath = filePath;
		}	

		public void Process()
		{
			WriteLine($"Begin process of {InputFilePath}");

			if (!File.Exists(InputFilePath)) { 
				WriteLine($"ERROR: file {InputFilePath} does not exists.");
			}

			string rootDirectoryPath = new DirectoryInfo(InputFilePath).Parent.Parent.FullName;
			WriteLine($"Root data path is {rootDirectoryPath}");

			string inputFileDirectoryPath = Path.GetDirectoryName(InputFilePath);
			string backupDirectoryPath = Path.Combine(rootDirectoryPath, BackupDirectoryName);
			Directory.CreateDirectory(backupDirectoryPath);

			string inputFileName = Path.GetFileName(InputFilePath);
			string backupFilePath = Path.Combine(backupDirectoryPath, inputFileName);
			WriteLine($"Copying {InputFilePath} to {backupFilePath}");
			File.Copy(InputFilePath, backupFilePath, true);

			Directory.CreateDirectory(Path.Combine(rootDirectoryPath, InProgressDirectoryName));
			string inProgessFilePath = Path.Combine(rootDirectoryPath, InProgressDirectoryName, inputFileName);

			if (File.Exists(inProgessFilePath)) { 
				WriteLine($"ERROR: a file with the same name {inProgessFilePath} is already being processed.");
				return;
			}

			WriteLine($"Moving {InputFilePath} to {inProgessFilePath}");
			File.Move(InputFilePath, inProgessFilePath);

			string extension = Path.GetExtension(InputFilePath);
			switch (extension) {
				case ".txt":
					ProcessTxtFile(inProgessFilePath);
					break;
				default:
					WriteLine($"{extension} is an unsopported file type.");
					break;
			}

			string completeDirectoryPath = Path.Combine(rootDirectoryPath, CompleteDirectoryName);
			Directory.CreateDirectory(completeDirectoryPath);
			WriteLine($"Moving {inProgessFilePath} to {completeDirectoryPath}");
			var completedFileName = $"{ Path.GetFileNameWithoutExtension(InputFilePath)}-{Guid.NewGuid()}{extension}";
			completedFileName = Path.ChangeExtension(completedFileName, ".complete");
			File.Move(inProgessFilePath, Path.Combine(completeDirectoryPath, completedFileName));

			String inProgressDirectoryPath = Path.GetDirectoryName(inProgessFilePath);
			Directory.Delete(inProgressDirectoryPath, true);

		}

		private void ProcessTxtFile(string inProgessFilePath)
		{
			WriteLine($"Processing text file {inProgessFilePath}");
		}
	}
}