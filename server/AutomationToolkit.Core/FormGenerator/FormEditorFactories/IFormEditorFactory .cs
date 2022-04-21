using Core.FormGenerator.Model;

namespace Core.FormGenerator.FormEditorFactories
{
  public interface IFormEditorFactory
  {
    IApplyFormEditor Create(FormEditorInfo propInfo);
  }
}
