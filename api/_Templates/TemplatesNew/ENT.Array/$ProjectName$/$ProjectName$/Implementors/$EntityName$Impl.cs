using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLnet.Base.Interfaces;
using $DomainName$.Core.Base;
using $DomainName$.Core.Messages;
using $DomainName$.$ModuleName$.Implementor;

namespace $ProjectName$.Implementors {

    public class $DomainPrefix$$EntityName$Impl : $DomainPrefix$$ModuleName$ArrayImpl {

        new public $DomainPrefix$$EntityName$ImplClient Client {
            get { return ($DomainPrefix$$EntityName$ImplClient)base.Client; }
            set { base.Client = value; }
        }
        new public $DomainPrefix$$EntityName$ImplServer Server {
            get { return ($DomainPrefix$$EntityName$ImplServer)base.Server; }
            set { base.Server = value; }
        }
        new public $DomainPrefix$$EntityName$ImplCommon Common {
            get { return ($DomainPrefix$$EntityName$ImplCommon)base.Common; }
            set { base.Common = value; }
        }                  

        public $DomainPrefix$$EntityName$Impl(IslAppContext appContext)
            : base(appContext) {
            
            entityText = $DomainPrefix$ObjectNames.sObj$EntityName$;
            entityRegName = GetEntityRegName();            
        }

        protected override void InitInterfaces() {
            if (AppContext.IsServerContext) {
                Server = new $DomainPrefix$$EntityName$ImplServer(AppContext, this);
            }
            else {
                Client = new $DomainPrefix$$EntityName$ImplClient(AppContext, this);
            }
            Common = new $DomainPrefix$$EntityName$ImplCommon(AppContext, this);
        }        
       
        public static string GetEntityRegName() {
            return $DomainPrefix$Sys.GetRegName($DomainPrefix$ObjRegName$ModuleName$.$EntityName$);
        }
    }
}
