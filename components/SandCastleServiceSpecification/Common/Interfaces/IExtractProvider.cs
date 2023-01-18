using Common.ReportModels;

namespace Common.Interfaces
{
    public interface IExtractProvider
    {
        PublishedEventExtractModel[] GetExtract(string ExtractFile);           
    }
}
