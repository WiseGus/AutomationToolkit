export interface Preset {
  alias: string;
  aliasCategory: string;
  projectName: string;
  templateOrigin: string;
  outputFolderPath: string;
  fileKeywordTypesExtensions: string;
  keywords: Keyword[];
  useAutomationUpdates: boolean;
  automationUpdatesMode?: 'IsEntity' | 'IsWinForm' | 'IsWpfForm';
}

export interface Keyword {
  keywordName: string;
  replacement: string;
  keywordType: string;
  showInGenerate: boolean;
}
