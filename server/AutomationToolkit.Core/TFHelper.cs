using System;
using System.Diagnostics;

namespace Core
{
  public class TFImpl
  {
    private string _TFPath;

    public TFImpl(string tfPath)
    {
      this._TFPath = tfPath;
    }

    public void RunTFCommand(string workingDirectory, string[] args)
    {
      if (string.IsNullOrEmpty(_TFPath))
      {
        throw new ArgumentNullException(_TFPath, "You must set the TF.exe path");
      }

      var process = new Process();
      process.StartInfo.WorkingDirectory = workingDirectory;
      process.StartInfo.FileName = _TFPath;
      process.StartInfo.Arguments = string.Join(' ', args);
      process.StartInfo.UseShellExecute = false;

      process.Start();
      process.WaitForExit();
    }
  }
}
