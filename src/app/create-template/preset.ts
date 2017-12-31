export interface Preset {
  alias: string;
  projectName: string;
  templateOrigin: string;
  outputFolderPath: string;
  fileKeywordTypesExtensions: string;
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
  keywordName: string;
  replacement: string;
  keywordType: string;
  showInGenerate: boolean;
}
