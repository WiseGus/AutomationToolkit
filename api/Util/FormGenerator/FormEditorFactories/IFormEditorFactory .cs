using Api.Util.FormGenerator.FormEditors;
using Api.Util.FormGenerator.Model;

namespace Api.Util.FormGenerator.FormEditorFactories {
  public interface IFormEditorFactory {
    IApplyFormEditor Create(FormEditorInfo propInfo);
  }
}
