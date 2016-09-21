using System.Collections.Generic;
using System.Linq;
using Recon.Core.Contexts;

namespace Recon.Core.Repository
{
    public class ReconFromRepository
    {
        private ReconContext _context;
        public ReconFromRepository()
        {
            _context = new ReconContext();
        }

        public List<Model.ReconFrom> Get()
        {
            return _context.ReconFroms.ToList();
        }
    }
}