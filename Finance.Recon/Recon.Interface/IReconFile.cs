using System.IO;

namespace Recon.Interface
{
    public interface IReconFile
    {
        FileInfo FileInfo { get; set; }
        string ColumnId { get; set; }
        string ColumnValue { get; set; }
        int HearderRow { get; set; }
        int ColumnIdIndex { get; set; }
        int ColumnValueIndex { get; set; }
    }
}