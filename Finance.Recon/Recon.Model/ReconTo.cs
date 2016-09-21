using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recon.Interface;

namespace Recon.Model
{
    public class ReconTo : IRecon
    {
        public ReconTo()
        {
        }

        public ReconTo(string id, decimal amount)
        {
            Id = id;
            Amount = amount;
        }

        public string Id { get; set; }
        public decimal Amount { get; set; }
    }
}
