using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Recon.Interface
{
    public interface IReconResult
    {
        //long Id { get; set; }
        [Description("ReferenceFrom")]
        string ReferenceFrom { get; set; }
        // Decimals for money - Always
        [Description("AmountFrom")]
        decimal? AmountFrom { get; set; }
        [Description("ReferenceTo")]
        string ReferenceTo { get; set; }
        // Decimals for money - Always
        [Description("AmountTo")]
        decimal? AmountTo { get; set; }
        [Description("Match")]
        string Match { get; set; }
        [Description("Difference")]
        decimal? Difference { get; set; }
    }
}