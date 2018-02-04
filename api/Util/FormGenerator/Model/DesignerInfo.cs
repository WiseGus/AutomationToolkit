using System.Collections.Generic;

namespace Api.Util.FormGenerator.Model {
  public class DesignerInfo {
    public string Todo { get; set; }
    public string Namespace { get; set; }
    public string ClassName { get; set; }
    public List<string> Instantiations { get; set; }
    public List<string> ISupportInitializeBegin { get; set; }
    public List<string> PropsSetup { get; set; }
    public List<string> ISupportInitializeEnd { get; set; }
    public List<string> Declarations { get; set; }

    public DesignerInfo() {
      Instantiations = new List<string>();
      ISupportInitializeBegin = new List<string>();
      PropsSetup = new List<string>();
      ISupportInitializeEnd = new List<string>();
      Declarations = new List<string>();
    }
  }
}
