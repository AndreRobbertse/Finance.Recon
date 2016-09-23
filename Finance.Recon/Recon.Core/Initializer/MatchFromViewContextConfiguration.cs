using System.Data.Entity.ModelConfiguration;
using Recon.Model;

namespace Recon.Core.Initializer
{
    public class MatchFromViewContextConfiguration : EntityTypeConfiguration<MatchFromView>
    {
        internal MatchFromViewContextConfiguration()
        {
            this.ToTable("MatchFromView"); ;
            this.Property(x => x.ReferenceFrom).HasColumnName("ReferenceFrom");
            this.Property(x => x.AmountFrom).HasColumnName("AmountFrom").HasColumnType("numeric");
            this.Property(x => x.ReferenceTo).HasColumnName("ReferenceTo");
            this.Property(x => x.AmountTo).HasColumnName("AmountTo").HasColumnType("numeric");
            this.Property(x => x.Match).HasColumnName("Match");
            this.Property(x => x.Difference).HasColumnName("Difference").HasColumnType("numeric");
        }
    }
}