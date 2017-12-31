using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLnet.Base.Trace;
using SLnet.Base.Interfaces;
using $DomainName$.Core.Base;
using $DomainName$.Core.Base.Trace;
using $DomainName$.Core.Messages;
using $DomainName$.Data.Structure;
using $DomainName$.$ModuleName$.Implementor;

namespace $ProjectName$.Implementors {
 
    public class $DomainPrefix$$EntityName$ImplServer : $DomainPrefix$$ModuleName$ArrayImplServer {

        new protected $DomainPrefix$$EntityName$Impl Impl {
            get { return ($DomainPrefix$$EntityName$Impl)base.Impl; }
            set { base.Impl = value; }
        }

        public $DomainPrefix$$EntityName$ImplServer(IslAppContext appContext, $DomainPrefix$$EntityName$Impl impl)
            : base(appContext, impl) {
        }        
        
        //TODO: override ValidateForPostSpecific, SaveWarningsSpecific if needed
    }

}
