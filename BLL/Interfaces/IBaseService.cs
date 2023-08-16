
namespace BLL.Interfaces
{
    public interface IBaseService<TModel> where TModel : class
    {
        bool IsValid();
    }
}
