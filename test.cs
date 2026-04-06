using System.Diagnostics;

var code = "var i=0; while(true) i++;";
//var code = "Console.WriteLine(\"Hello, World!\");";

var containerName = $"code-sandbox-{Guid.NewGuid().ToString("N").Substring(0, 8)}";

var runPsi = new ProcessStartInfo
{
    FileName = "docker",
    Arguments = $"run --rm -i --name {containerName} code-sandbox",
    RedirectStandardInput = true,
    RedirectStandardOutput = true,
    RedirectStandardError = true,
    UseShellExecute = false,
};

using var process = Process.Start(runPsi);

await process.StandardInput.WriteAsync(code);
process.StandardInput.Close();

var exitTask = process.WaitForExitAsync();
var timeoutTask = Task.Delay(TimeSpan.FromSeconds(10));

var stdOutTask = process.StandardOutput.ReadToEndAsync();
var stdErrTask = process.StandardError.ReadToEndAsync();

var finishedTask = await Task.WhenAny(exitTask, timeoutTask);

if (finishedTask == timeoutTask)
{
    Console.WriteLine("timeout gg");
    
    var stopPsi = new ProcessStartInfo
    {
        FileName = "docker",
        Arguments = $"stop {containerName}",
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
    };
    var stopProcess = Process.Start(stopPsi);
    await stopProcess.WaitForExitAsync();

    return;
}

var stdOut = await stdOutTask;
var stdErr = await stdErrTask;

Console.WriteLine(stdOut);
Console.WriteLine(stdErr);