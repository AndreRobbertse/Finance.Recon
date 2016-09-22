using System.ComponentModel.DataAnnotations.Schema;
using Recon.Interface;

namespace Recon.Model
{
    public class ReconTo : IRecon
    {
        public ReconTo()
        {
        }

        public ReconTo(string reference, decimal amount)
        {
            Reference = reference;
            Amount = amount;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Reference { get; set; }
        public decimal Amount { get; set; }
    }
}
