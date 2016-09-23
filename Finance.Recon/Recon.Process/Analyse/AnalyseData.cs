using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Recon.Core.Repository;
using Recon.Excel;
using Recon.Interface;
using Recon.Shared.Common;
using Recon.Shared.Config;

namespace Recon.Process.Analyse
{
    public class AnalyseData
    {
        private List<Model.MatchToView> MatchToViews { get; set; }

        private List<Model.MatchFromView> MatchFromViews { get; set; }

        private MatchToViewRepository _toViewRepository;
        private MatchFromViewRepository _fromViewRepository;

        public AnalyseData()
        {
            SpinAnimation.Start(50);

            _toViewRepository = new MatchToViewRepository();

            MatchToViews = _toViewRepository.Get();

            Console.WriteLine("Matching {0} records", MatchToViews.Count);

            var writerTo = new Writer(MatchToViews.ToList<IReconResult>());
            var resultTo = writerTo.SaveData(ConfigHelper.Get<string>(Lookup.ResultFile1ConfigKey));
            writerTo = null;

            _fromViewRepository = new MatchFromViewRepository();

            MatchFromViews = _fromViewRepository.Get();

            Console.WriteLine("Doing reverse match on {0} records", MatchFromViews.Count);

            var writerFrom = new Writer(MatchFromViews.ToList<IReconResult>());
            var resultFrom = writerFrom.SaveData(ConfigHelper.Get<string>(Lookup.ResultFile2ConfigKey));
            writerFrom = null;

            SpinAnimation.Stop();
            GC.Collect();
        }

    }
}