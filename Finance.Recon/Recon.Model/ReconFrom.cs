using Recon.Interface;

namespace Recon.Model
{
    public class ReconFrom : IRecon
    {
        public ReconFrom()
        {
        }

        public ReconFrom(string id, decimal amount)
        {
            Id = id;
            Amount = amount;
        }

        public string Id { get; set; }
        public decimal Amount { get; set; }
    }
}