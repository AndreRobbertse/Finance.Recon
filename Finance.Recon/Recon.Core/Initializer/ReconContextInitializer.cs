using System.Data.Entity;
using Recon.Core.Contexts;

namespace Recon.Core.Initializer
{
    public class ReconContextInitializer : DropCreateDatabaseAlways<ReconContext>
    {
        
    }
}