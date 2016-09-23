using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Recon.Interface;

namespace Recon.Model
{
    public class MatchToView : IReconResult
    {
        public MatchToView()
        {
        }

        public MatchToView(string referenceFrom, decimal amountFrom, string referenceTo, decimal amountTo, string match, decimal difference)
        {
            this.ReferenceFrom = referenceFrom;
            this.ReferenceTo = referenceTo;
            this.AmountFrom = amountFrom;
            this.AmountTo = amountTo;
            this.Match = match;
            this.Difference = difference;
        }

        [Key]
        public string ReferenceFrom { get; set; }
        public decimal? AmountFrom { get; set; }
        public string ReferenceTo { get; set; }
        public decimal? AmountTo { get; set; }
        public string Match { get; set; }
        public decimal? Difference { get; set; }
    }
}