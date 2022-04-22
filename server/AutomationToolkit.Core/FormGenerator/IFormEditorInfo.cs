using System;

namespace AutomationToolkit.Core.FormGenerator;

[Flags]
public enum AssignType
{
    String = 2,
    Guid = 4,
    Memo = 16,
    Int32 = 32,
    Int16 = 64,
    Boolean = 128,
    DateTime = 256,
    Float = 512,
    Double = 1024,
    Decimal = 2048,
    Special = 4096
}

public interface IFormEditorInfo
{
    AssignType AssignType { get; }
    bool IsDefaultForAssignType { get; }
    string EditorName { get; }
    string Category { get; }
}
