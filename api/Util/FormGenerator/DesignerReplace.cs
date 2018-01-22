using System;

namespace Api.Util.FormGenerator
{
    public class DesignerReplace
    {
        const string DESIGNER_TEMPLATE =
@"namespace $Namespace$
{
  partial class $ClassName$
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name=""disposing"">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing(components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        $Instantiations$

        $ISupportInitializeBegin$

        $PropsSetup$

        $ISupportInitializeEnd$
    }

    #endregion

    $Declarations$
  }
}";
        public string GenerateDesigner(DesignerInfo data)
        {
            var designerString = DESIGNER_TEMPLATE
              .Replace("$Namespace$", data.Namespace)
              .Replace("$ClassName$", data.ClassName)
              .Replace("$Instantiations$", string.Join(Environment.NewLine, data.Instantiations))
              .Replace("$ISupportInitializeBegin$", string.Join(Environment.NewLine, data.ISupportInitializeBegin))
              .Replace("$PropsSetup$", string.Join(Environment.NewLine, data.PropsSetup))
              .Replace("$ISupportInitializeEnd$", string.Join(Environment.NewLine, data.ISupportInitializeEnd))
              .Replace("$Declarations$", string.Join(Environment.NewLine, data.Declarations));
            return designerString;
        }

    }
}
