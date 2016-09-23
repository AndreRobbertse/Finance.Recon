using System.Collections.Generic;
using System.Linq;
using Recon.Core.Contexts;

namespace Recon.Core.Repository
{
    public class MatchToViewRepository
    {
        private ReconContext _context;
        public MatchToViewRepository()
        {
            _context = new ReconContext();
        }

        public List<Model.MatchToView> Get()
        {
            return _context.MatchToViews.ToList();
        }
    }
}