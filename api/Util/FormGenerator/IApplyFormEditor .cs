using System.Collections.Generic;

namespace Api.Util.FormGenerator {

  public interface IApplyFormEditor {
    string Name { get; }
    string ControlName { get; }
    string LayoutName { get; }
    List<string> Instantiations { get; }
    List<string> ISupportInitializeBegin { get; }
    List<string> PropsSetup { get; }
    List<string> ISupportInitializeEnd { get; }
    List<string> Declarations { get; }
    void Apply();
  }
}
