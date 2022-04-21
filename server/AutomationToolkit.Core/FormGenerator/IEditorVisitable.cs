namespace Core.FormGenerator
{
  public interface IEditorVisitable
  {
    void Accept(IEditorVisitor visitor);
  }
}
