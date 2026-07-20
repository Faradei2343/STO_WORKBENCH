namespace STO_WORKBENCH.ZayavkoDrochitel3000
{
    public class GenerationOptions
    {
        public string ExcelFilePath { get; set; }
        public int StartCount { get; set; }
        public int EndCount { get; set; }
    }

    public class SendOptions
    {
        public string FilesDir { get; set; }      // папка с готовыми PDF/XLSX
        public int StartCount { get; set; }
        public int EndCount { get; set; }
        public string RecipientEmail { get; set; }
        public string CcEmail { get; set; }
    }
}
