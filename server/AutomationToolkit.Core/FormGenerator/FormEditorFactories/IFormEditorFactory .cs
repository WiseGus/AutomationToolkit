using AutomationToolkit.Core.FormGenerator.Model;

namespace AutomationToolkit.Core.FormGenerator.FormEditorFactories;

public interface IFormEditorFactory
{
    IApplyFormEditor Create(FormEditorInfo propInfo);
}
