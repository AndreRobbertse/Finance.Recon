namespace Recon.Interface
{
    public interface IRecon
    {
        string Id { get; set; }
        // Decimals for money - Always
        decimal Amount { get; set; }
    }
}