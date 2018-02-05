using Api.Util.FormGenerator.Visitors;
using System.Collections.Generic;

namespace Api.Util.FormGenerator.FormEditors
{
  public abstract class BaseEditor : IApplyFormEditor, IEditorVisitable
  {
    public List<string> Instantiations => _instantiations;

    public List<string> ISupportInitializeBegin => _iSupportInitializeBegin;

    public List<string> PropsSetup => _propsSetup;

    public List<string> ISupportInitializeEnd => _iSupportInitializeEnd;

    public List<string> Declarations => _declarations;

    public abstract string Name { get; }

    public abstract string ControlName { get; }

    public virtual string LayoutName { get; }

    private List<string> _iSupportInitializeBegin;
    private List<string> _propsSetup;
    private List<string> _iSupportInitializeEnd;
    private List<string> _instantiations;
    private List<string> _declarations;

    public BaseEditor()
    {
      _iSupportInitializeBegin = new List<string>();
      _propsSetup = new List<string>();
      _iSupportInitializeEnd = new List<string>();
      _instantiations = new List<string>();
      _declarations = new List<string>();
    }

    public void Apply()
    {
      Declarations.Add(AddDeclaration());
      ISupportInitializeBegin.Add(AddISupportInitializeBegin());
      PropsSetup.Add(AddPropsSetup());
      ISupportInitializeEnd.Add(AddISupportInitializeEnd());
      Instantiations.Add(AddInstantiation());
    }

    protected abstract string AddInstantiation();
    protected abstract string AddISupportInitializeEnd();
    protected abstract string AddPropsSetup();
    protected abstract string AddISupportInitializeBegin();
    protected abstract string AddDeclaration();

    public void Accept(IEditorVisitor visitor)
    {
      visitor.Visit(this);
    }
  }
}
