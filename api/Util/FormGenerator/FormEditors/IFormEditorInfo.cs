using System;

namespace Api.Util.FormGenerator.FormEditors
{
  [Flags]
  public enum AssignType {
    String = 2,
    Guid = 4,
    Memo = 16,
    Int32 = 32,
    Int16 = 64,
    Boolean = 128,
    DateTime = 256
  }

  public interface IFormEditorInfo
  {
    AssignType AssignType { get; }
    bool IsDefaultForAssignType { get; }
    string EditorName { get; }
    string Category { get; }
  }
}
