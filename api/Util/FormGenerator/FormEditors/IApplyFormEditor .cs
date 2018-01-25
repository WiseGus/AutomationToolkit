namespace Api.Util.FormGenerator.FormEditors {

  public interface IApplyFormEditor {
    string ControlName { get; }
    string LayoutName { get; }
    string AddInstantiation();
    string AddISupportInitializeBegin();
    string AddPropsSetup();
    string AddISupportInitializeEnd();
    string AddDeclaration();
  }
}
