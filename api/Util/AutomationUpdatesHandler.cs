using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Api.Util
{
  internal class AutomationUpdatesHandler : IDisposable
  {
    private Preset _presetObj;
    private AppSettings _settingsObj;
    private Process _process;
    private List<string> _messages;
    private KeywordReplace _keyReplace;

    public AutomationUpdatesHandler(Preset presetObj, AppSettings settingsObj, KeywordReplace keyReplace)
    {
      _presetObj = presetObj;
      _settingsObj = settingsObj;
      _keyReplace = keyReplace;
    }

    internal OperationResult Execute()
    {
      _messages = new List<string>();

      /* Replace keywords */
      for (int i = 0; i < _presetObj.AutomationUpdates.AutomationUpdatesArgs.Count; i++)
      {
        var arg = _presetObj.AutomationUpdates.AutomationUpdatesArgs.ElementAt(i);
        _presetObj.AutomationUpdates.AutomationUpdatesArgs[arg.Key] = _keyReplace.Replace(arg.Value).Replace(' ', '_');
      }

      _messages.Add("AutomationToolkit path: " + _presetObj.AutomationUpdates.AutomationUpdatesPath);
      _messages.Add("AutomationToolkit args: " + string.Join(' ', _presetObj.AutomationUpdates.AutomationUpdatesArgs.Select(p => p.Value).ToArray()));

      /* Run AutomationToolkitUpdates */
      try
      {
        _process = new Process();
        _process.EnableRaisingEvents = true;
        _process.OutputDataReceived += process_OutputDataReceived;
        _process.ErrorDataReceived += process_ErrorDataReceived;
        _process.Exited += new System.EventHandler(process_Exited);

        _process.StartInfo.FileName = _presetObj.AutomationUpdates.AutomationUpdatesPath;
        _process.StartInfo.Arguments = string.Join(' ', _presetObj.AutomationUpdates.AutomationUpdatesArgs.Select(p => p.Value).ToArray());
        _process.StartInfo.UseShellExecute = false;
        _process.StartInfo.RedirectStandardError = true;
        _process.StartInfo.RedirectStandardOutput = true;

        _messages.Add("[AutomationToolkitUpdates process]=> ");

        _process.Start();
        _process.BeginErrorReadLine();
        _process.BeginOutputReadLine();

        _process.WaitForExit();
      }
      catch (Exception ex)
      {
        _messages.Add("AutomationToolkit error:");
        _messages.Add(ex.ToString());
      }

      return new OperationResult
      {
        ResultMessage = string.Join(Environment.NewLine, _messages)
      };
    }

    private void process_Exited(object sender, EventArgs e)
    {
      var res = string.Format("process exited with code {0}\n", _process.ExitCode.ToString());
      Console.WriteLine(res);
      _messages.Add(res);
    }

    private void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
      var res = e.Data + "\n";
      Console.WriteLine(res);
      _messages.Add(res);
    }

    private void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
      var res = e.Data + "\n";
      Console.WriteLine(res);
      _messages.Add(res);
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
}
