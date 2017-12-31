using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLnet.Base.Attributes;
using SLnet.Base.Interfaces;
using $DomainName$.Entities;
using $DomainName$.Core.Base;

namespace $ProjectName$ {
    //Client Operations
    [slRegisterOperation("Get", typeof(Client_Get), slRegisterOperationTarget.Client)]
    [slRegisterOperation("GetWithoutWorkflow", typeof(Client_GetWithoutWorkflow), slRegisterOperationTarget.Client)]
    [slRegisterOperation("Post", typeof(Client_Post), slRegisterOperationTarget.Client)]    
    [slRegisterOperation("SaveWarnings", typeof(Client_SaveWarnings), slRegisterOperationTarget.Client)]
    [slRegisterOperation("SaveErrors", typeof(Client_SaveErrors), slRegisterOperationTarget.Client)]

    //Server Operations
    [slRegisterOperation("Get", typeof(Server_Get), slRegisterOperationTarget.Server)]
    [slRegisterOperation("GetWithoutWorkflow", typeof(Server_GetWithoutWorkflow), slRegisterOperationTarget.Server)]
    [slRegisterOperation("Post", typeof(Server_Post), slRegisterOperationTarget.Server)]    
    [slRegisterOperation("SaveWarnings", typeof(Server_SaveWarnings), slRegisterOperationTarget.Server)]
    
    //Both 
    [slRegisterOperation("GetFetchPath", typeof(GetFetchPath), slRegisterOperationTarget.Both)]
    [slRegisterOperation("GetPostPath", typeof(GetPostPath), slRegisterOperationTarget.Both)]    
    
    [slRegisterObject($DomainPrefix$ObjRegName.$DomainNameFull$, $DomainPrefix$ObjRegName$ModuleName$.$EntityName$)]
    public partial class $DomainPrefix$$EntityName$ : $DomainPrefix$$EntityName$EntityBase {
        public $DomainPrefix$$EntityName$(IslAppContext appContext) : base(appContext) {
        }
        
	}
}
