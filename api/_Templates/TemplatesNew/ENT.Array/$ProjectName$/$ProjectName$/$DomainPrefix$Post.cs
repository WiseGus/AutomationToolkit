using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLnet.Base.Attributes;
using SLnet.Base.Workflow;
using SLnet.Base.Workflow.Activities;
using $DomainName$.Core.Base;

namespace $ProjectName$ {

    [slRegisterWorkflow($DomainPrefix$ObjRegName.$DomainNameFull$, $DomainPrefix$ObjRegName$ModuleName$.$EntityName$ + ".Post")]
	public class $DomainPrefix$Post : slWorkflow {
		public override void BuildWorkflow(slWorkflowContext Context) {
			this.Name = "$DomainPrefix$Post";
			var VerifyPostPermissionInstance=new VerifyPostPermission();
			VerifyPostPermissionInstance.Name = "VerifyPostPermission";
			VerifyPostPermissionInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_VerifyPostPermission, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			VerifyPostPermissionInstance.ResultBinding = "Ok";
			VerifyPostPermissionInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"},
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			this.Child = VerifyPostPermissionInstance;
			var VerifyPermission_WhenInstance=new VerifyPermission_When();
			VerifyPermission_WhenInstance.Name = "VerifyPermission_When";
			VerifyPostPermissionInstance.Child = VerifyPermission_WhenInstance;
			var PreValidateProcessingInstance=new PreValidateProcessing();
			PreValidateProcessingInstance.Name = "PreValidateProcessing";
			PreValidateProcessingInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_PreValidateProcessing, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			PreValidateProcessingInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"},
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			VerifyPermission_WhenInstance.TrueChild = PreValidateProcessingInstance;
			var ValidateForPostInstance=new ValidateForPost();
			ValidateForPostInstance.Name = "ValidateForPost";
			ValidateForPostInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_ValidateForPost, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			ValidateForPostInstance.ResultBinding = "Ok";
			ValidateForPostInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"},
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			PreValidateProcessingInstance.Child = ValidateForPostInstance;
			var ValidateForPost_WhenInstance=new ValidateForPost_When();
			ValidateForPost_WhenInstance.Name = "ValidateForPost_When";
			ValidateForPostInstance.Child = ValidateForPost_WhenInstance;
			var SaveWarningsInstance=new SaveWarnings();
			SaveWarningsInstance.Name = "SaveWarnings";
			SaveWarningsInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_SaveWarnings, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			SaveWarningsInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"},
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			ValidateForPost_WhenInstance.TrueChild = SaveWarningsInstance;
			var PreSaveProcessingInstance=new PreSaveProcessing();
			PreSaveProcessingInstance.Name = "PreSaveProcessing";
			PreSaveProcessingInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_PreSaveProcessing, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			PreSaveProcessingInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"},
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			SaveWarningsInstance.Child = PreSaveProcessingInstance;
			var SetPathInstance=new SetPath();
			SetPathInstance.Name = "SetPath";
			SetPathInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_Post_SetPath, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			SetPathInstance.ResultBinding = "SetPathValue";
            SetPathInstance.ParamBindings = new slWorkflowParamBinding[]{
                new slWorkflowParamBinding(){Name = "postPath",Path="SetPathValue"}
                };
			PreSaveProcessingInstance.Child = SetPathInstance;
			var SaveDataInstance=new SaveData();
			SaveDataInstance.Name = "SaveData";
			SaveDataInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_SaveData, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			SaveDataInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"},
				new slWorkflowParamBinding(){Name = "postPath",Path="SetPathValue"},
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			SetPathInstance.Child = SaveDataInstance;
			var UpdateAuditHistoryInstance=new UpdateAuditHistory();
			UpdateAuditHistoryInstance.Name = "UpdateAuditHistory";
			UpdateAuditHistoryInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_UpdateAuditHistory, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			UpdateAuditHistoryInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"}
				};
			SaveDataInstance.Child = UpdateAuditHistoryInstance;
			var AfterSaveProcessingInstance=new AfterSaveProcessing();
			AfterSaveProcessingInstance.Name = "AfterSaveProcessing";
			AfterSaveProcessingInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_AfterSaveProcessing, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			AfterSaveProcessingInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"},
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			UpdateAuditHistoryInstance.Child = AfterSaveProcessingInstance;
			var AddToErrorLog_ValidationInstance=new AddToErrorLog_Validation();
			AddToErrorLog_ValidationInstance.Name = "AddToErrorLog_Validation";
			AddToErrorLog_ValidationInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_Post_AddToErrorLog, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			AddToErrorLog_ValidationInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			ValidateForPost_WhenInstance.FalseChild = AddToErrorLog_ValidationInstance;
			var AddToErrorLog_PermissionInstance=new AddToErrorLog_Permission();
			AddToErrorLog_PermissionInstance.Name = "AddToErrorLog_Permission";
			AddToErrorLog_PermissionInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_Post_AddToErrorLog, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			AddToErrorLog_PermissionInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			VerifyPermission_WhenInstance.FalseChild = AddToErrorLog_PermissionInstance;
		}
		public class VerifyPostPermission : slOperationActivity {
		}
		public class VerifyPermission_When : slIfElseActivity {
			public override bool EvalCondition(slWorkflowContext Context) {
				return (bool)Context.Parameters["Ok"];
			}
		}
		public class PreValidateProcessing : slOperationActivity {
		}
		public class ValidateForPost : slOperationActivity {
		}
		public class ValidateForPost_When : slIfElseActivity {
			public override bool EvalCondition(slWorkflowContext Context) {
				return (bool)Context.Parameters["Ok"];
			}
		}
		public class SaveWarnings : slOperationActivity {
		}
		public class PreSaveProcessing : slOperationActivity {
		}
		public class SetPath : slOperationActivity {
		}
		public class SaveData : slOperationActivity {
		}
		public class UpdateAuditHistory : slOperationActivity {
		}
		public class AfterSaveProcessing : slOperationActivity {
		}
		public class AddToErrorLog_Validation : slOperationActivity {
		}
		public class AddToErrorLog_Permission : slOperationActivity {
		}
	}

}
