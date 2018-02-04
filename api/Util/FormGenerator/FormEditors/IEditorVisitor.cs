namespace Api.Util.FormGenerator.FormEditors
{
  public interface IEditorVisitor
  {
    void Visit(IApplyFormEditor editor);
  }
}
