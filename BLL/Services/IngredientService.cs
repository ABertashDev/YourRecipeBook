using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Interfaces;
using DAL.Entities;

namespace BLL.Services
{
    internal class IngredientService : BaseService, IIngredientService
    {

        public IngredientService(IUnitOfWork unitOfWork, IMapper mapper)
            : base(unitOfWork, mapper)
        {
               
        }

        public async Task AddAsync(IngredientModel model)
        {
            
            if (model is null)
            {
                throw new CommonException("Error! IngredientModel is null!");
            }

            if (!model.IsValid(out string message))
            {
                throw new CommonException($"Error! {message}");
            }

            await _unitOfWork.IngredientRepository.AddAsync(_mapper.Map<Ingredient>(model));

            await _unitOfWork.SaveAsync();

        }

        public async Task DeleteAsync(IngredientModel model)
        {

            if (model is null)
            {
                throw new CommonException("Error! IngredientModel is null!");
            }

            await _unitOfWork.IngredientRepository.DeleteByIdAsync(model.Id);

            await _unitOfWork.SaveAsync();

        }

        public async Task<IEnumerable<IngredientModel>> GetAllAsync()
        {

            return _mapper.Map<IEnumerable<IngredientModel>>(await _unitOfWork.IngredientRepository.GetAllAsync());
            
        }

        public async Task<IngredientModel> GetByIdAsync(int id)
        {

            Ingredient ingr = await _unitOfWork.IngredientRepository.GetByIdAsync(id);

            if (ingr is null)
            {
                throw new CommonException($"Error! Ingredient with id={id} doesn't exist!");
            }

            return _mapper.Map<IngredientModel>(ingr);

        }

        public async Task UpdateAsync(IngredientModel model)
        {
            
            if (model is null)
            {
                throw new CommonException($"Error! Ingredient is null!");
            }

            if (!model.IsValid(out string message))
            {
                throw new CommonException($"Error! {message}");
            }

            _unitOfWork.IngredientRepository.Update(_mapper.Map<Ingredient>(model));

            await _unitOfWork.SaveAsync();

        }
    }
}
