using Core.FormGenerator.Model;
using System;

namespace Core.FormGenerator
{
  public class CsDesignerTemplate
  {
    const string DESIGNER_TEMPLATE =
@"
namespace /* CHANGE ME! */
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
        if (disposing && (components != null))
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
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof($ClassName$));
        $Instantiations$
        this.SuspendLayout();
        $ISupportInitializeBegin$
        $PropsSetup$
        $ISupportInitializeEnd$
        // 
        // $ClassName$
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(890, 568);
        this.Name = ""$ClassName$"";
        this.Text = ""$ClassName$"";
        this.ResumeLayout(false);
    }

    #endregion

    $Declarations$
  }
}";
    public string GenerateDesigner(DesignerInfo data)
    {
      var designerString = DESIGNER_TEMPLATE
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
