using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLnet.Base;
using SLnet.Base.Attributes;
using SLnet.Base.DataObjects;
using $ProjectName$.Implementors;

namespace $ProjectName$ {

    partial class $DomainPrefix$$EntityName$ {
        
        [slWorkflowNonVisible()]
        public class GetFetchPath : slBaseObjectOperation {
            [slOperationMethod]
            public slDataObjectProviderFetchPath Execute() {                
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                return impl.Common.GetFetchPath();
            }
        }

        [slWorkflowNonVisible()]
        public class GetPostPath : slBaseObjectOperation {
            [slOperationMethod]
            public slDataObjectProviderPostPath Execute() {                
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                return impl.Common.GetPostPath();
            }
        }
            
    }
}
