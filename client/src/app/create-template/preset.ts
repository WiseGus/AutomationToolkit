export interface Preset {
  alias: string;
  aliasCategory: string;
  tooltip: string;
  projectName: string;
  templateOrigin: string;
  outputFolderPath: string;
  fileKeywordTypesExtensions: string;
  keywords: Keyword[];
  addToSourceControl: boolean;
  useAutomationUpdates: boolean;
}

export interface Keyword {
  keywordName: string;
  replacement: string;
  keywordType: string;
  showInGenerate: boolean;
}
