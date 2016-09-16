using System.IO;
using Recon.Interface;

namespace Recon.Model
{
    public class ReconFile : IReconFile
    {
        public ReconFile()
        {
            HearderRow = -1;
            ColumnIdIndex = -1;
            ColumnValueIndex = -1;
        }

        public FileInfo FileInfo { get; set; }
        public string ColumnId { get; set; }
        public string ColumnValue { get; set; }
        public int HearderRow { get; set; }
        public int ColumnIdIndex { get; set; }
        public int ColumnValueIndex { get; set; }
    }
}