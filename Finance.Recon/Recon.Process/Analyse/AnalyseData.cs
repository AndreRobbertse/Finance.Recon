using System.Collections.Generic;
using Recon.Core.Repository;

namespace Recon.Process.Analyse
{
    public class AnalyseData
    {
        private List<Model.ReconFrom> FromRecons { get; set; }
        private List<Model.ReconTo> ToRecons { get; set; }

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