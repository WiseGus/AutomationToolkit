export interface Preset {
  name: string;
  templateOrigin: string;
  fileTypesExtensions: string;
  keywords: Keyword[];
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

export interface Keyword {
  keyword: string;
  replacement?: string;
  type: string;
}
