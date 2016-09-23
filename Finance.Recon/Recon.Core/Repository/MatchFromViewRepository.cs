using System.Collections.Generic;
using System.Linq;
using Recon.Core.Contexts;

namespace Recon.Core.Repository
{
    public class MatchFromViewRepository
    {
        private ReconContext _context;
        public MatchFromViewRepository()
        {
            _context = new ReconContext();
        }

        public List<Model.MatchFromView> Get()
        {
            return _context.MatchFromViews.ToList();
        }
    }
}