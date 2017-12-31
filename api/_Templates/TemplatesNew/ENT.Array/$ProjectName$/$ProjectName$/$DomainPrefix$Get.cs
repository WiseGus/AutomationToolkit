using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLnet.Base.Attributes;
using SLnet.Base.Workflow;
using SLnet.Base.Workflow.Activities;
using $DomainName$.Core.Base;

namespace $ProjectName$ {

    [slRegisterWorkflow($DomainPrefix$ObjRegName.$DomainNameFull$, $DomainPrefix$ObjRegName$ModuleName$.$EntityName$ + ".Get")]
	public class $DomainPrefix$Get : slWorkflow {
		public override void BuildWorkflow(slWorkflowContext Context) {
			this.Name = "$DomainPrefix$Get";
			var VerifyGetPermissionInstance=new VerifyGetPermission();
			VerifyGetPermissionInstance.Name = "VerifyGetPermission";
			VerifyGetPermissionInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_VerifyGetPermission, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			VerifyGetPermissionInstance.ResultBinding = "Ok";
			VerifyGetPermissionInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"},
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			this.Child = VerifyGetPermissionInstance;
			var VerifyPermission_WhenInstance=new VerifyPermission_When();
			VerifyPermission_WhenInstance.Name = "VerifyPermission_When";
			VerifyGetPermissionInstance.Child = VerifyPermission_WhenInstance;
			var SetPathInstance=new SetPath();
			SetPathInstance.Name = "SetPath";
			SetPathInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_Get_SetPath, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			SetPathInstance.ResultBinding = "SetPathValue";
			SetPathInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "fetchPath",Path="SetPathValue"}
				};
			VerifyPermission_WhenInstance.TrueChild = SetPathInstance;
			var FetchDataInstance=new FetchData();
			FetchDataInstance.Name = "FetchData";
			FetchDataInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_FetchData, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			FetchDataInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "fetchPath",Path="SetPathValue"},
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"}
				};
			SetPathInstance.Child = FetchDataInstance;
			var AddToErrorLogInstance=new AddToErrorLog();
			AddToErrorLogInstance.Name = "AddToErrorLog";
			AddToErrorLogInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_Get_AddToErrorLog, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			AddToErrorLogInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			VerifyPermission_WhenInstance.FalseChild = AddToErrorLogInstance;
		}
		public class VerifyGetPermission : slOperationActivity {
		}
		public class VerifyPermission_When : slIfElseActivity {
			public override bool EvalCondition(slWorkflowContext Context) {
				return (bool)Context.Parameters["Ok"];
			}
		}
		public class SetPath : slOperationActivity {
		}
		public class FetchData : slOperationActivity {
		}
		public class AddToErrorLog : slOperationActivity {
		}
	}

}
