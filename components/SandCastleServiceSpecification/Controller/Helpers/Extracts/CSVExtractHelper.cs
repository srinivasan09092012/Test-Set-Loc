using Common.Interfaces;
using Common.ReportModels;
using FileHelpers;
using System.Collections.Generic;

namespace Controller.Helpers.Extracts
{
    public class CSVExtractHelper : IExtractProvider
    {
        public PublishedEventExtractModel[] GetExtract(string ExtractFile)
        {
            var fileHelperReadEngine = new FileHelperEngine<PublishedEventExtractModel>();
            fileHelperReadEngine.ErrorManager.ErrorMode = ErrorMode.IgnoreAndContinue; // will ignore any issue with the file content and read/parse all posible lines

            var eventsExtractFile = fileHelperReadEngine.ReadFile(ExtractFile);

            return eventsExtractFile;
        }
    }
}
