using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLnet.Base.DataObjects;
using SLnet.Base.Interfaces;
using $DomainName$.$ModuleName$.Implementor;

namespace $ProjectName$.Implementors {
   
    public class $DomainPrefix$$EntityName$ImplCommon : $DomainPrefix$$ModuleName$MasterImplCommon {
        
        new protected $DomainPrefix$$EntityName$Impl Impl {
            get { return ($DomainPrefix$$EntityName$Impl)base.Impl; }
            set { base.Impl = value; }
        }

        public $DomainPrefix$$EntityName$ImplCommon(IslAppContext appContext, $DomainPrefix$$EntityName$Impl impl)
            : base(appContext, impl) {
        }

        //TODO: override GetFetchPath, GetPostPath if needed
    }
}
