namespace Recon.Interface
{
    public interface IRecon
    {
        long Id { get; set; }
        string Reference { get; set; }
        // Decimals for money - Always
        decimal Amount { get; set; }
    }
}