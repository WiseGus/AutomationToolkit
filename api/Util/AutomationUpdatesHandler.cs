using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Api.Util
{
    internal class AutomationUpdatesHandler : IDisposable
    {
        private Preset _presetObj;
        private AppSettings _settingsObj;
        private Process _process;
        private List<string> _messages;

        public AutomationUpdatesHandler(Preset presetObj, AppSettings settingsObj)
        {
            _presetObj = presetObj;
            _settingsObj = settingsObj;
        }

        internal OperationResult Execute()
        {
            /*
            * Input Arguments
            * 0 = Calling Application (Glx.DevToolkitSetup^1.0.0)
            * 1 = Tfs name
            * 2 = Tfs workspace
            * 3 = Tfs project
            *
            * 4 = Entity
            * 5 = Entity description (spaces will be _)
            * 6 = IsMaster (true/false)
            * 7 = Entity Project Folder
            * 8 = Module name (IS)
            * 9 = Called from Entity/Form/WPF/Form register action/New array-master form action (0,1,2,3,4)
            * 10 = if new action manager file (true/false)
            * 11 = if register browser (true/false)
            * 12 = Form name (when called New array-master form action calledFrom=4)
            */
            _messages = new List<string>();

            _process = new Process();
            _process.EnableRaisingEvents = true;
            _process.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(process_OutputDataReceived);
            _process.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(process_ErrorDataReceived);
            _process.Exited += new System.EventHandler(process_Exited);

            _process.StartInfo.FileName = _settingsObj.TfPath;
            _process.StartInfo.Arguments = "//TODO";
            _process.StartInfo.UseShellExecute = false;
            _process.StartInfo.RedirectStandardError = true;
            _process.StartInfo.RedirectStandardOutput = true;

            _messages.Add("[AutomationToolkitUpdates process]=> ");

            _process.Start();
            _process.BeginErrorReadLine();
            _process.BeginOutputReadLine();

            _process.WaitForExit();

            return new OperationResult
            {
                ResultMessage = string.Join(Environment.NewLine, _messages)
            };
        }

        void process_Exited(object sender, EventArgs e)
        {
            var res = string.Format("process exited with code {0}\n", _process.ExitCode.ToString());
            Console.WriteLine(res);
            _messages.Add(res);
        }

        void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            var res = e.Data + "\n";
            Console.WriteLine(res);
            _messages.Add(res);
        }

        void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
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
