using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLnet.Base.Interfaces;
using $DomainName$.Core.Base;
using $DomainName$.Core.Base.Trace;
using $DomainName$.Data.DataObjects;
using $DomainName$.$ModuleName$.Implementor;

namespace $ProjectName$.Implementors {

    public class $DomainPrefix$$EntityName$ImplClient : $DomainPrefix$$ModuleName$ArrayImplClient {

        new protected $DomainPrefix$$EntityName$Impl Impl {
            get { return ($DomainPrefix$$EntityName$Impl)base.Impl; }
            set { base.Impl = value; }
        }

        public $DomainPrefix$$EntityName$ImplClient(IslAppContext appContext, $DomainPrefix$$EntityName$Impl impl)
            : base(appContext, impl) {
        }              

    }
}
