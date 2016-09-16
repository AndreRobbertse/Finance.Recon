using System.Collections.Generic;
using System.Data.Entity;
using Recon.Core.Contexts;
using Recon.Core.Repository;

namespace Recon.Process.Analyse
{
    public class AnalyseData
    {
        private List<Model.Recon> FromRecons { get; set; }
        private List<Model.Recon> ToRecons { get; set; }

        private ReconFromRepository _reconFromRepository;
        private ReconToRepository _reconToRepository;

        public AnalyseData()
        {
            _reconFromRepository = new ReconFromRepository();
            _reconToRepository = new ReconToRepository();

            FromRecons = _reconFromRepository.Get();
            ToRecons = _reconToRepository.Get();
        }

    }
}