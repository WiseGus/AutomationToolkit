using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLnet.Base;
using SLnet.Base.Attributes;
using SLnet.Base.DataObjects;
using SLnet.Base.Interfaces;
using $DomainName$.Core.Base;
using $DomainName$.Core.Base.Trace;
using $DomainName$.Data.DataObjects;
using $DomainName$.Data.Structure;
using $ProjectName$.Implementors;

namespace $ProjectName$ {

    partial class $DomainPrefix$$EntityName$ {
                
        [slWorkflowNonVisible()]
        public class Client_Get : slBaseObjectOperation {
            [slOperationMethod]
            public $DomainPrefix$$EntityName$Collection Execute(slDataObjectProviderFetchPath fetchPath) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);                
                return ($DomainPrefix$$EntityName$Collection)impl.Client.Get(impl.entityRegName, "Get", fetchPath);
            }
        }

        [slWorkflowNonVisible()]
        public class Client_GetWithoutWorkflow : slBaseObjectOperation {
            [slOperationMethod]
            public $DomainPrefix$$EntityName$Collection Execute(slDataObjectProviderFetchPath fetchPath) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                return ($DomainPrefix$$EntityName$Collection)impl.Client.GetWithoutWorkflow(impl.entityRegName, "GetWithoutWorkflow", fetchPath);
            }
        }

        [slWorkflowNonVisible()]
        public class Client_Post : slBaseObjectOperation {
            [slOperationMethod]
            public $DomainPrefix$MessageLogger Execute($DomainPrefix$$EntityName$Collection collection) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);                
                return impl.Client.Post(impl.entityRegName, "Post", collection);
            }
        }        

        [slWorkflowNonVisible()]
        public class Client_SaveWarnings : slBaseObjectOperation {
            [slOperationMethod]
            public $DomainPrefix$MessageLogger Execute($DomainPrefix$$EntityName$Collection collection) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);                           
                return impl.Client.SaveWarnings(impl.entityRegName, "SaveWarnings", collection);       
            }
        }

        [slWorkflowNonVisible()]
        public class Client_SaveErrors : slBaseObjectOperation {
            [slOperationMethod]
            public $DomainPrefix$MessageLogger Execute($DomainPrefix$$EntityName$Collection collection) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                return impl.Client.SaveErrors(collection);
            }
        }
        
    }

}

