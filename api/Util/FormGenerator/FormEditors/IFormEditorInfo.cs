using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Api.Util.FormGenerator.FormEditors {

  public enum AssignType { Text, Guid, Memo, Number, Date }

  public interface IFormEditorInfo {
    AssignType AssignType { get; }
    bool IsDefaultForAssignType { get; }
    string EditorName { get; }
  }
}
