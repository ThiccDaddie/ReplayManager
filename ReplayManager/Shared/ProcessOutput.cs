using System;

using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ThiccDaddie.ReplayManager.Shared
{
	public class ProcessOutput
	{
		private Process process;
		private readonly string fileName;
		private readonly string arguments;
		private bool isFinished;
		public bool IsFaulted { get; set; }
		public string OutputData { get; set; } = string.Empty;
		public string ErrorData { get; set; } = string.Empty;

		public ProcessOutput(string fileName, string arguments = "")
		{
			if (string.IsNullOrWhiteSpace(fileName))
			{
				throw new ArgumentException($"'{nameof(fileName)}' cannot be null or whitespace", nameof(fileName));
			}

			this.arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
			this.fileName = fileName;
		}

		public async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			process = new Process();

			LaunchProcess();

			while (!isFinished)
			{
				if (cancellationToken.IsCancellationRequested)
				{
					isFinished = true;
					process.Kill();
					IsFaulted = true;
				}
				await Task.Delay(50);
			}
		}

		void LaunchProcess()
		{
			process.EnableRaisingEvents = true;
			process.OutputDataReceived += new DataReceivedEventHandler(Process_OutputDataReceived);
			process.ErrorDataReceived += new DataReceivedEventHandler(Process_ErrorDataReceived);
			process.Exited += new EventHandler(Process_Exited);

			process.StartInfo.FileName = fileName;
			process.StartInfo.Arguments = arguments;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardError = true;
			process.StartInfo.RedirectStandardOutput = true;

			process.Start();
			process.BeginErrorReadLine();
			process.BeginOutputReadLine();
		}

		private void Process_Exited(object sender, EventArgs e)
		{
			IsFaulted = process.ExitCode != 0;
			isFinished = true;
		}

		private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
		{
			ErrorData += e.Data;
		}

		private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
		{
			OutputData += e.Data;
		}
	}
}
