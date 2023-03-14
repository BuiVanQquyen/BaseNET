using ExCore.RFS.Data;
using ExCore.RFS.Utils.Repository;
using Microsoft.Extensions.Logging;

namespace ExCore.RFS.Repository
{
    public class RFSBaseRepository : Repository<SystemContext>
    {
        public RFSBaseRepository(SystemContext dbContext) : base(dbContext)
        {
        }
    }
}
