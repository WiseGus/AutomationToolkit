using System.Collections.Generic;

namespace Api.Util.FormGenerator
{
    public class DesignerInfo
    {
        public string Namespace { get; set; }
        public string ClassName { get; set; }
        public List<string> Instantiations { get; set; }
        public List<string> ISupportInitializeBegin { get; set; }
        public List<string> PropsSetup { get; set; }
        public List<string> ISupportInitializeEnd { get; set; }
        public List<string> Declarations { get; set; }
    }

}
