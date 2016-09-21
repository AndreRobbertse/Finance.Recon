using System.Collections.Generic;
using System.Linq;
using Recon.Core.Contexts;

namespace Recon.Core.Repository
{
    public class ReconToRepository
    {
        private ReconContext _context;
        public ReconToRepository()
        {
            _context = new ReconContext();
        }

        public List<Model.ReconTo> Get()
        {
            return _context.ReconTos.ToList();
        }
    }
}