using System;
using System.Diagnostics;
using System.Linq;
using AutomationToolkit.Core.Model;
using AutomationToolkit.Core.Services;
using Microsoft.Extensions.Logging;

namespace AutomationToolkit.Infrastructure;

public class AutomationUpdatesHandler : IDisposable
{
private readonly ILogger _logger;
private Preset _presetObj;
private Process _process;
private IKeywordReplace _keyReplace;

public AutomationUpdatesHandler(ILogger logger, Preset presetObj, IKeywordReplace keyReplace)
{
  _logger = logger;
  _presetObj = presetObj;
  _keyReplace = keyReplace;
}

public void Execute()
{
  /* Replace keywords */
  _presetObj.AutomationUpdates.AutomationUpdatesPath = _keyReplace.Replace(_presetObj.AutomationUpdates.AutomationUpdatesPath);
  if (string.IsNullOrEmpty(_presetObj.AutomationUpdates.AutomationUpdatesPath)) return;

  for (int i = 0; i < _presetObj.AutomationUpdates.AutomationUpdatesArgs.Count; i++)
  {
    var arg = _presetObj.AutomationUpdates.AutomationUpdatesArgs.ElementAt(i);
    _presetObj.AutomationUpdates.AutomationUpdatesArgs[arg.Key] = _keyReplace.Replace(arg.Value).Replace(' ', '_');
  }

  _logger.LogDebug("AutomationToolkit path: " + _presetObj.AutomationUpdates.AutomationUpdatesPath);
  _logger.LogDebug("AutomationToolkit args: " + string.Join(' ', _presetObj.AutomationUpdates.AutomationUpdatesArgs.Select(p => p.Value).ToArray()));

  /* Run AutomationToolkitUpdates */
  try
  {
    _process = new Process();
    _process.EnableRaisingEvents = true;
    _process.OutputDataReceived += process_OutputDataReceived;
    _process.ErrorDataReceived += process_ErrorDataReceived;
    _process.Exited += process_Exited;

    _process.StartInfo.FileName = _presetObj.AutomationUpdates.AutomationUpdatesPath;
    _process.StartInfo.Arguments = string.Join(' ', _presetObj.AutomationUpdates.AutomationUpdatesArgs.Select(p => p.Value).ToArray());
    _process.StartInfo.UseShellExecute = false;
    _process.StartInfo.RedirectStandardError = true;
    _process.StartInfo.RedirectStandardOutput = true;

    _logger.LogDebug("[AutomationToolkitUpdates process]=> ");

    _process.Start();
    _process.BeginErrorReadLine();
    _process.BeginOutputReadLine();

    _process.WaitForExit();
  }
  catch (Exception ex)
  {
    //_messages.Add("AutomationToolkit path: " + _presetObj.AutomationUpdates.AutomationUpdatesPath);
    //_messages.Add("AutomationToolkit args: " + string.Join(' ', _presetObj.AutomationUpdates.AutomationUpdatesArgs.Select(p => p.Value).ToArray()));
    //_messages.Add("AutomationToolkit error:");
    _logger.LogError(ex.ToString());
  }
}

private void process_Exited(object sender, EventArgs e)
{
  var res = string.Format("process exited with code {0}\n", _process.ExitCode.ToString());
  Console.WriteLine(res);
  _logger.LogDebug(res);
}

private void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
{
  var res = e.Data + "\n";
  Console.WriteLine(res);
  _logger.LogDebug(res);
}

private void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
{
  var res = e.Data + "\n";
  Console.WriteLine(res);
  _logger.LogDebug(res);
}

#region IDisposable Support
private bool disposedValue = false; // To detect redundant calls

protected virtual void Dispose(bool disposing)
{
  if (!disposedValue)
  {
    if (disposing)
    {
      _process.Dispose();
    }

    // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
    // TODO: set large fields to null.

    disposedValue = true;
  }
}

// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
// ~AutomationUpdatesHandler() {
//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
//   Dispose(false);
// }

// This code added to correctly implement the disposable pattern.
void IDisposable.Dispose()
{
  // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
  Dispose(true);
  // TODO: uncomment the following line if the finalizer is overridden above.
  // GC.SuppressFinalize(this);
}
#endregion

}
