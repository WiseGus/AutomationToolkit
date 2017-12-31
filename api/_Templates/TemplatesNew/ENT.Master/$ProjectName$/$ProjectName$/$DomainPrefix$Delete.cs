using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLnet.Base.Attributes;
using SLnet.Base.Workflow;
using SLnet.Base.Workflow.Activities;
using $DomainName$.Core.Base;

namespace $ProjectName$ {

    [slRegisterWorkflow($DomainPrefix$ObjRegName.$DomainNameFull$, $DomainPrefix$ObjRegName$ModuleName$.$EntityName$ + ".Delete")]
	public class $DomainPrefix$Delete : slWorkflow {
		public override void BuildWorkflow(slWorkflowContext Context) {
			this.Name = "$DomainPrefix$Delete";
			var SetPathInstance=new SetPath();
			SetPathInstance.Name = "SetPath";
			SetPathInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_Delete_SetPath, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			SetPathInstance.ResultBinding = "SetPathValue";
			SetPathInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "fetchPath",Path="SetPathValue"}
				};
			this.Child = SetPathInstance;
			var GetInstance=new Get();
			GetInstance.Name = "Get";
			GetInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_Delete_Get, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			GetInstance.ResultBinding = "DataCollection";
			GetInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "fetchPath",Path="SetPathValue"}
				};
			SetPathInstance.Child = GetInstance;
			var VerifyDeletePermissionInstance=new VerifyDeletePermission();
			VerifyDeletePermissionInstance.Name = "VerifyDeletePermission";
			VerifyDeletePermissionInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_VerifyDeletePermission, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			VerifyDeletePermissionInstance.ResultBinding = "Ok";
			VerifyDeletePermissionInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"},
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			GetInstance.Child = VerifyDeletePermissionInstance;
			var VerifyPermission_WhenInstance=new VerifyPermission_When();
			VerifyPermission_WhenInstance.Name = "VerifyPermission_When";
			VerifyDeletePermissionInstance.Child = VerifyPermission_WhenInstance;
			var ValidateForDeleteInstance=new ValidateForDelete();
			ValidateForDeleteInstance.Name = "ValidateForDelete";
			ValidateForDeleteInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_ValidateForDelete, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			ValidateForDeleteInstance.ResultBinding = "Ok";
			ValidateForDeleteInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"},
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			VerifyPermission_WhenInstance.TrueChild = ValidateForDeleteInstance;
			var Validate_WhenInstance=new Validate_When();
			Validate_WhenInstance.Name = "Validate_When";
			ValidateForDeleteInstance.Child = Validate_WhenInstance;
			var DeleteWarningsInstance=new DeleteWarnings();
			DeleteWarningsInstance.Name = "DeleteWarnings";
			DeleteWarningsInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_DeleteWarnings, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			DeleteWarningsInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"},
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			Validate_WhenInstance.TrueChild = DeleteWarningsInstance;
			var PreDeleteProcessingInstance=new PreDeleteProcessing();
			PreDeleteProcessingInstance.Name = "PreDeleteProcessing";
			PreDeleteProcessingInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_PreDeleteProcessing, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			PreDeleteProcessingInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"},
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			DeleteWarningsInstance.Child = PreDeleteProcessingInstance;
			var DeleteDataInstance=new DeleteData();
			DeleteDataInstance.Name = "DeleteData";
			DeleteDataInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_DeleteData, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			DeleteDataInstance.ResultBinding = "DataCollection";
			DeleteDataInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"},
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			PreDeleteProcessingInstance.Child = DeleteDataInstance;
			var SetPostPathInstance = new SetPostPath();
            SetPostPathInstance.Name = "SetPostPath";
            SetPostPathInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_Delete_SetPostPath, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            SetPostPathInstance.ResultBinding = "SetPostPathValue";
            SetPostPathInstance.ParamBindings = new slWorkflowParamBinding[]{
                new slWorkflowParamBinding() {Name = "postPath", Path = "SetPostPathValue"}
                };
            DeleteDataInstance.Child = SetPostPathInstance;
            var ApplyDeleteDataInstance = new ApplyDeleteData();
            ApplyDeleteDataInstance.Name = "ApplyDeleteData";
            ApplyDeleteDataInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_ApplyDeleteData, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            ApplyDeleteDataInstance.ParamBindings = new slWorkflowParamBinding[]{
                new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"},
                new slWorkflowParamBinding(){Name = "postPath",Path="SetPostPathValue"},
                new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
                };
			SetPostPathInstance.Child = ApplyDeleteDataInstance;
			var UpdateAuditHistoryInstance=new UpdateAuditHistory();
			UpdateAuditHistoryInstance.Name = "UpdateAuditHistory";
			UpdateAuditHistoryInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_Delete_UpdateAuditHistory, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			UpdateAuditHistoryInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"}
				};
			ApplyDeleteDataInstance.Child = UpdateAuditHistoryInstance;
			var PostDeleteProcessingInstance=new PostDeleteProcessing();
			PostDeleteProcessingInstance.Name = "PostDeleteProcessing";
			PostDeleteProcessingInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_PostDeleteProcessing, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			PostDeleteProcessingInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "collection",Path="DataCollection"},
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			UpdateAuditHistoryInstance.Child = PostDeleteProcessingInstance;
			var AddToErrorLog_ValidateInstance=new AddToErrorLog_Validate();
			AddToErrorLog_ValidateInstance.Name = "AddToErrorLog_Validate";
			AddToErrorLog_ValidateInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_Delete_AddToErrorLog, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			AddToErrorLog_ValidateInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			Validate_WhenInstance.FalseChild = AddToErrorLog_ValidateInstance;
			var AddToErrorLog_PermissionInstance=new AddToErrorLog_Permission();
			AddToErrorLog_PermissionInstance.Name = "AddToErrorLog_Permission";
			AddToErrorLog_PermissionInstance.OperationType = Type.GetType("$ProjectName$.$DomainPrefix$$EntityName$+opServer_Delete_AddToErrorLog, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
			AddToErrorLog_PermissionInstance.ParamBindings = new slWorkflowParamBinding[]{
				new slWorkflowParamBinding(){Name = "messageLogger",Path="MessageLogger"}
				};
			VerifyPermission_WhenInstance.FalseChild = AddToErrorLog_PermissionInstance;
		}
		public class SetPath : slOperationActivity {
		}
		public class Get : slOperationActivity {
		}
		public class VerifyDeletePermission : slOperationActivity {
		}
		public class VerifyPermission_When : slIfElseActivity {
			public override bool EvalCondition(slWorkflowContext Context) {
				return (bool)Context.Parameters["Ok"];
			}
		}
		public class ValidateForDelete : slOperationActivity {
		}
		public class Validate_When : slIfElseActivity {
			public override bool EvalCondition(slWorkflowContext Context) {
				return (bool)Context.Parameters["Ok"];
			}
		}
		public class DeleteWarnings : slOperationActivity {
		}
		public class PreDeleteProcessing : slOperationActivity {
		}
		public class DeleteData : slOperationActivity {
		}
		public class SetPostPath : slOperationActivity {
                //Execute$ProjectName$.$DomainPrefix$$EntityName$+opServer_Delete_SetPostPath, $ProjectName$, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
                //Param:(00)postPath,Path=SetPathValue
                //Param:(ReturnValue),Path=SetPathValue
        }
        public class ApplyDeleteData : slOperationActivity {
		}
		public class UpdateAuditHistory : slOperationActivity {
		}
		public class PostDeleteProcessing : slOperationActivity {
		}
		public class AddToErrorLog_Validate : slOperationActivity {
		}
		public class AddToErrorLog_Permission : slOperationActivity {
		}
	}

}
