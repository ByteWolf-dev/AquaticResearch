using Base.Tools.CsvImport;

namespace ImportConsoleApp.CSVEntities;

public class CsvObservation
{
    public string Notes { get; set; }
    [CsvImportFormat(Format = "yyyy-MM-ddTHH:mm:ss")]
    public DateTime ObservationDateTime { get; set; }
    public string Researchers { get; set; }
    public string Equipment { get; set; }
    public string Location { get; set; }
    public string ResearchProject { get; set; }
}