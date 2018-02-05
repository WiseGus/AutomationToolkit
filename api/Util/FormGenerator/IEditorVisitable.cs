namespace Api.Util.FormGenerator
{
  public interface IEditorVisitable
  {
    void Accept(IEditorVisitor visitor);
  }
}
