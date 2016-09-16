using Recon.Interface;

namespace Recon.Model
{
    public class Recon : IRecon
    {
        public Recon()
        {
        }

        public Recon(string id, decimal amount)
        {
            Id = id;
            Amount = amount;
        }

        public string Id { get; set; }
        public decimal Amount { get; set; }
    }
}