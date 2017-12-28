export interface Preset {
  name: string;
  templateOrigin: string;
  fileTypesExtensions: string[];
  keywords: Map<string, string>;
  autoUpdates: AutoUpdates;
}

export interface AutoUpdates {
  updateEntities?: boolean;
  updatePresenters?: boolean;
  updateWinforms?: boolean;
  updateWpfforms?: boolean;
  updateCoreBase?: boolean;
  updateCoreMessages?: boolean;
  updateActionManager?: boolean;
  updateFormsCustomization?: boolean;
  updateObjectsCustomization?: boolean;
  updateWorkflowsCustomization?: boolean;
  isCrmProject?: boolean;
}
