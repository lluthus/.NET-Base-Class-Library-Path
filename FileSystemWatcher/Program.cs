using System;
using static System.Console;
using System.IO;

namespace TheFileSystemWatcher
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Parsing command line options");
			var directoryToWatch = args[0];
			if (!Directory.Exists(directoryToWatch))
			{
				WriteLine($"ERROR: {directoryToWatch} does not exist");
			}
			else { 
				WriteLine($"Watching directory {directoryToWatch} for changes");
				using (var inputFileWatcher = new FileSystemWatcher(directoryToWatch)) {
					inputFileWatcher.IncludeSubdirectories = false;
					inputFileWatcher.InternalBufferSize = 32768; // 32KB
					inputFileWatcher.Filter = "*.*";
					inputFileWatcher.NotifyFilter = NotifyFilters.LastWrite;

					inputFileWatcher.Created += FileCreated;
					inputFileWatcher.Deleted += FileDeleted;
					inputFileWatcher.Changed += FileChanged;
					inputFileWatcher.Renamed += FileRenamed;
					inputFileWatcher.Error += WatchError;

					inputFileWatcher.EnableRaisingEvents = true;

				}
			}
		}

		private static void WatchError(object sender, ErrorEventArgs e)
		{
			WriteLine($"ERROR: file system watching may no longer be active: {e.GetException()}");

		}

		private static void FileRenamed(object sender, RenamedEventArgs e)
		{
			WriteLine($"File renamed: {e.Name} - type: {e.ChangeType}");

		}

		private static void FileChanged(object sender, FileSystemEventArgs e)
		{
			WriteLine($"File changed: {e.Name} - type: {e.ChangeType}");
		}

		private static void FileDeleted(object sender, FileSystemEventArgs e)
		{
			WriteLine($"File deleted: {e.Name} - type: {e.ChangeType}");

		}

		private static void FileCreated(object sender, FileSystemEventArgs e)
		{
			WriteLine($"File created: {e.Name} - type: {e.ChangeType}");
		}
	}
}
