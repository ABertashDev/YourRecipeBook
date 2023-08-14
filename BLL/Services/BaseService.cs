using AutoMapper;
using DAL.Data;

namespace BLL.Services
{
    public class BaseService
    {
        internal readonly RecipeBookDbContext _context;
        internal readonly IMapper _mapper;

        public BaseService(RecipeBookDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
