export interface Preset {
  name: string;
  projectName: string;
  templateOrigin: string;
  outputFolderPath: string;
  fileTypesExtensions: string;
  keywords: Keyword[];
  autoUpdates: AutoUpdates;
}

export interface AutoUpdates {
  updateEntities: boolean;
  updatePresenters: boolean;
  updateWinforms: boolean;
  updateWpfforms: boolean;
  updateCoreBase: boolean;
  updateCoreMessages: boolean;
  updateActionManager: boolean;
  updateFormsCustomization: boolean;
  updateObjectsCustomization: boolean;
  updateWorkflowsCustomization: boolean;
  isCrmProject: boolean;
}

export interface Keyword {
  keyword: string;
  replacement: string;
  type: string;
}
