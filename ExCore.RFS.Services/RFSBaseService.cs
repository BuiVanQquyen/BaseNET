using AutoMapper;
using ExCore.RFS.Repository;
using ExCore.RFS.Utils.Service;
namespace ExCore.RFS.Services
{
    public class RFSBaseService : BaseService
    {
        public RFSBaseService(RFSBaseRepository repository, IMapper mapper) : base(repository, mapper)
        {

        }
    }
}
