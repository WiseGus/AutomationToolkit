namespace Api.Util.FormGenerator.FormEditors
{
  public interface IEditorVisitable
  {
    void Accept(IEditorVisitor visitor);
  }
}
