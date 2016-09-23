using System.Threading;
using System.Threading.Tasks;
using Recon.Core;
using Recon.Excel;
using Recon.File;
using Recon.Interface;
using Recon.Model;
using Recon.Process.Analyse;
using Recon.Shared.Common;
using Recon.Shared.Config;

namespace Recon.Process.Files
{
    public class ProcessFiles
    {
        public ProcessFiles()
        {
            // Read Data from files
            var reconFromFile = getReconFile(ReconType.From);
            var reconToFile = getReconFile(ReconType.To);
            Excel.Reader excelReader = new Reader(reconFromFile, reconToFile);

            // Start DB and Import Data
            new Setup(excelReader.FromRecon, excelReader.ToRecon);

            // Compare Recons
            var analyse = new AnalyseData();
        }

        #region private functions

        private IReconFile getReconFile(ReconType recon)
        {
            var reconfile = new ReconFile();
            if (recon == ReconType.From)
            {
                reconfile.FileInfo = FileHelper.GetFileInfo(ConfigHelper.Get<string>(Lookup.ReconFile1ConfigKey));
                reconfile.ColumnId = ConfigHelper.Get<string>(Lookup.ReconFile1Column1ConfigKey);
                reconfile.ColumnValue = ConfigHelper.Get<string>(Lookup.ReconFile1Column2ConfigKey);
            }
            else if (recon == ReconType.To)
            {
                reconfile.FileInfo = FileHelper.GetFileInfo(ConfigHelper.Get<string>(Lookup.ReconFile2ConfigKey));
                reconfile.ColumnId = ConfigHelper.Get<string>(Lookup.ReconFile2Column1ConfigKey);
                reconfile.ColumnValue = ConfigHelper.Get<string>(Lookup.ReconFile2Column2ConfigKey);
            }
            return reconfile;
        }

        #endregion private functions
    }
}