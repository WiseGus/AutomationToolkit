using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SLnet.Base;
using SLnet.Base.Attributes;
using SLnet.Base.DataObjects;
using SLnet.Base.Trace;
using $DomainName$.Core.Base;
using $DomainName$.Core.Base.SandExtensions;
using $DomainName$.Core.Base.Trace;
using $DomainName$.Core.Implementor;
using $DomainName$.Data.DataObjects;
using $DomainName$.Data.Structure;
using $DomainName$.Core.Messages;
using $ProjectName$.Implementors;

namespace $ProjectName$ {

    partial class $DomainPrefix$$EntityName$ {

        #region Exposed Operations

        [slWorkflowNonVisible()]
        public class Server_Get : slBaseObjectOperation {
            [slOperationMethod]
            public $DomainPrefix$$EntityName$DataContext Execute(slDataObjectProviderFetchPath fetchPath) {                                
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);                
                return impl.Server.Get<$DomainPrefix$$EntityName$DataContext>(this.BaseObject, impl.GetWorkflowRegName("Get"), fetchPath);
            }
        }

        [slWorkflowNonVisible()]
        public class Server_GetByID : slBaseObjectOperation {
            [slOperationMethod]
            public $DomainPrefix$$EntityName$DataContext Execute(string ID) {                
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);      
                return impl.Server.GetByID<$DomainPrefix$$EntityName$DataContext>(this.BaseObject, impl.GetWorkflowRegName("Get"), ID);
            }
        }

        [slWorkflowNonVisible()]
        public class Server_GetWithoutWorkflow : slBaseObjectOperation {
            [slOperationMethod]
            public $DomainPrefix$$EntityName$DataContext Execute(slDataObjectProviderFetchPath fetchPath) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);                
                return impl.Server.GetWithoutWorkflow<$DomainPrefix$$EntityName$DataContext>(fetchPath);
            }
        }

        [slWorkflowNonVisible()]
        public class Server_Delete : slTransactionalOperation {
            [slOperationMethod]
            public $DomainPrefix$MessageLogger Execute(slDataObjectProviderFetchPath fetchPath) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);   
                return impl.Server.Delete(this.BaseObject, impl.GetWorkflowRegName("Delete"), fetchPath);
            }
        }

        [slWorkflowNonVisible()]
        public class Server_DeleteByIDs : slTransactionalOperation {
            [slOperationMethod]
            public $DomainPrefix$MessageLogger Execute(List<string> IDs) {                
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                return impl.Server.DeleteByIDs(this.BaseObject, impl.GetWorkflowRegName("Delete"), IDs);                
            }            
        }

        [slWorkflowNonVisible()]
        public class Server_DeleteByIDsIndividually : slBaseObjectOperation {
            [slOperationMethod]
            public $DomainPrefix$MessageLogger Execute(List<string> IDs) {                
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                return impl.Server.DeleteByIDsIndividually(this.BaseObject, IDs);                
            }            
        }

        [slWorkflowNonVisible()]
        public class Server_Post : slTransactionalOperation {
            [slOperationMethod]
            public $DomainPrefix$MessageLogger Execute($DomainPrefix$$EntityName$DataContext dataContext) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);   
                return impl.Server.Post(this.BaseObject, impl.GetWorkflowRegName("Post"), dataContext);                
            }
        }                             

        [slWorkflowNonVisible()]
        public class Server_SaveWarnings : slBaseObjectOperation {
            [slOperationMethod]
            public $DomainPrefix$MessageLogger Execute($DomainPrefix$$EntityName$DataContext dataContext) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);                            
                return impl.Server.SaveWarnings(dataContext); 
            }
        }   

        [slWorkflowNonVisible()]
        public class Server_DeleteWarnings : slBaseObjectOperation {
            [slOperationMethod]
            public $DomainPrefix$MessageLogger Execute($DomainPrefix$$EntityName$DataContext dataContext) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);                            
                return impl.Server.DeleteWarnings(dataContext);
            }
        }  
        #endregion

        #region Activity Operations

        #region Get Workflow Activity Operations

        public class opServer_VerifyGetPermission : slBaseObjectOperation {
            [slOperationMethod]
            public bool Execute($DomainPrefix$$EntityName$Collection collection, $DomainPrefix$MessageLogger messageLogger) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);   
                return impl.Server.VerifyGetPermission(collection, messageLogger);
            }
        }

        public class opServer_Get_SetPath : slBaseObjectOperation {
            [slOperationMethod]
            public slDataObjectProviderFetchPath Execute(slDataObjectProviderFetchPath fetchPath) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);   
                return impl.Server.GetSetPath(fetchPath);
            }
        }

        public class opServer_FetchData : slBaseObjectOperation {
            [slOperationMethod]
            public $DomainPrefix$$EntityName$Collection Execute(slDataObjectProviderFetchPath fetchPath, $DomainPrefix$$EntityName$Collection collection) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                return ($DomainPrefix$$EntityName$Collection)impl.Server.GetCollection(fetchPath, collection);
            }
        }

        public class opServer_Get_AddToErrorLog : slBaseObjectOperation {
            [slOperationMethod]
            public void Execute($DomainPrefix$MessageLogger messageLogger) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                impl.Common.ReportErrors(messageLogger);
            }
        }

        #endregion

        #region Delete Workflow Activity Operations

        public class opServer_Delete_SetPath : slBaseObjectOperation {
            [slOperationMethod]
            public slDataObjectProviderFetchPath Execute(slDataObjectProviderFetchPath fetchPath) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);   
                return impl.Server.DeleteSetPath(fetchPath);
            }
        }

        public class opServer_Delete_Get : slBaseObjectOperation {
            [slOperationMethod]
            public $DomainPrefix$$EntityName$Collection Execute(slDataObjectProviderFetchPath fetchPath) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                $DomainPrefix$$EntityName$DataContext dc = impl.Server.Get<$DomainPrefix$$EntityName$DataContext>(this.BaseObject, impl.GetWorkflowRegName("Get"), fetchPath);                
                return $DomainPrefix$$EntityName$CollectionFactory.Create(AppContext, dc);
            }
        }

        public class opServer_VerifyDeletePermission : slBaseObjectOperation {
            [slOperationMethod]
            public bool Execute($DomainPrefix$$EntityName$Collection collection, $DomainPrefix$MessageLogger messageLogger) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                return impl.Server.VerifyDeletePermission(collection, messageLogger);
           }
        }

        public class opServer_ValidateForDelete : slBaseObjectOperation {            
            [slOperationMethod]
            public bool Execute($DomainPrefix$$EntityName$Collection collection, $DomainPrefix$MessageLogger messageLogger) {                
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);                                
                return impl.Server.ValidateForDelete(collection, messageLogger);                 
            }
        }

        public class opServer_DeleteWarnings : slBaseObjectOperation {
            [slOperationMethod]
            public void Execute($DomainPrefix$$EntityName$Collection collection, $DomainPrefix$MessageLogger messageLogger) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);                                
                messageLogger.Merge(impl.Server.DeleteWarningsCol(collection));
            }
        }

        public class opServer_PreDeleteProcessing : slBaseObjectOperation {
            [slOperationMethod]
            public void Execute($DomainPrefix$$EntityName$Collection collection, $DomainPrefix$MessageLogger messageLogger) {
                 $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);                                
                 impl.Server.PreDeleteProcessing(collection, messageLogger);
            }
        }

        public class opServer_DeleteData : slBaseObjectOperation {
            [slOperationMethod]
            public $DomainPrefix$$EntityName$Collection Execute($DomainPrefix$$EntityName$Collection collection, $DomainPrefix$MessageLogger messageLogger) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                return ($DomainPrefix$$EntityName$Collection)impl.Server.DeleteData(collection);                
            }
        }

        public class opServer_Delete_SetPostPath : slBaseObjectOperation {
            [slOperationMethod]
            public slDataObjectProviderPostPath Execute(slDataObjectProviderPostPath postPath) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                return postPath;
            }
        }

        public class opServer_ApplyDeleteData : slBaseObjectOperation {
            [slOperationMethod]
            public void Execute($DomainPrefix$$EntityName$Collection collection, slDataObjectProviderPostPath postPath, $DomainPrefix$MessageLogger messageLogger) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                impl.Server.ApplyDeleteData(collection, postPath);
            }
        }


        public class opServer_Delete_UpdateAuditHistory : slBaseObjectOperation {
            [slOperationMethod]
            public void Execute($DomainPrefix$$EntityName$Collection collection) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                impl.Server.UpdateAuditHistory(this.BaseObject, collection, $DomainPrefix$ObjRegName$ModuleName$.$EntityName$, $DomainPrefix$BaseImplServer.$DomainPrefix$AuditHistoryAction.Delete);
            }
        }

        public class opServer_PostDeleteProcessing : slBaseObjectOperation {
            [slOperationMethod]
            public void Execute($DomainPrefix$$EntityName$Collection collection, $DomainPrefix$MessageLogger messageLogger) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                impl.Server.PostDeleteProcessing(collection, messageLogger);
            }
        }

        public class opServer_Delete_AddToErrorLog : slBaseObjectOperation {
            [slOperationMethod]
            public void Execute($DomainPrefix$MessageLogger messageLogger) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                impl.Common.ReportErrors(messageLogger);
            }
        }

        #endregion

        #region Post Workflow Activity Operations

        public class opServer_VerifyPostPermission : slBaseObjectOperation {
            [slOperationMethod]
            public bool Execute($DomainPrefix$$EntityName$Collection collection, $DomainPrefix$MessageLogger messageLogger) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                return impl.Server.VerifyPostPermission(collection, messageLogger);
            }
        }        

        public class opServer_PreValidateProcessing : slBaseObjectOperation {
            [slOperationMethod]
            public void Execute($DomainPrefix$$EntityName$Collection collection, $DomainPrefix$MessageLogger messageLogger) {    
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);                
                impl.Server.PreValidateProcessing(collection, messageLogger);
            }
        }

        public class opServer_ValidateForPost : slBaseObjectOperation {
            [slOperationMethod]
            public bool Execute($DomainPrefix$$EntityName$Collection collection, $DomainPrefix$MessageLogger messageLogger) {                
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);                
                return impl.Server.ValidateForPost(collection, messageLogger);
            }
        }

        public class opServer_SaveWarnings : slBaseObjectOperation {
            [slOperationMethod]
            public void Execute($DomainPrefix$$EntityName$Collection collection, $DomainPrefix$MessageLogger messageLogger) {                
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                messageLogger.Merge(impl.Server.SaveWarningsCol(collection));                
            }
        }

        public class opServer_PreSaveProcessing : slBaseObjectOperation {
            [slOperationMethod]
            public void Execute($DomainPrefix$$EntityName$Collection collection, $DomainPrefix$MessageLogger messageLogger) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                impl.Server.PreSaveProcessing(collection, messageLogger);
            }
        }

        public class opServer_Post_SetPath : slBaseObjectOperation {
            [slOperationMethod]
            public slDataObjectProviderPostPath Execute(slDataObjectProviderPostPath postPath) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                return postPath;
            }
        }


        public class opServer_SaveData : slBaseObjectOperation {
            [slOperationMethod]
            public void Execute($DomainPrefix$$EntityName$Collection collection, slDataObjectProviderPostPath postPath, $DomainPrefix$MessageLogger messageLogger) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                impl.Server.SaveData(collection, postPath); 
            }
        }

        public class opServer_UpdateAuditHistory : slBaseObjectOperation {
            [slOperationMethod]
            public void Execute($DomainPrefix$$EntityName$Collection collection) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                impl.Server.UpdateAuditHistory(this.BaseObject, collection, $DomainPrefix$ObjRegName$ModuleName$.$EntityName$, $DomainPrefix$BaseImplServer.$DomainPrefix$AuditHistoryAction.Post);
            }
        }

        public class opServer_AfterSaveProcessing : slBaseObjectOperation {
            [slOperationMethod]
            public void Execute($DomainPrefix$$EntityName$Collection collection, $DomainPrefix$MessageLogger messageLogger) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                impl.Server.AfterSaveProcessing(collection, messageLogger);
            }
        }

        public class opServer_Post_AddToErrorLog : slBaseObjectOperation {
            [slOperationMethod]
            public void Execute($DomainPrefix$MessageLogger messageLogger) {
                $DomainPrefix$$EntityName$Impl impl = new $DomainPrefix$$EntityName$Impl(AppContext);
                impl.Common.ReportErrors(messageLogger);            
            }
        }

        #endregion
        
        #endregion

    }
}
