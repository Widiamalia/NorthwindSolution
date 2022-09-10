using AutoMapper;
using Northwind.Contracts.Dto.Category;
using Northwind.Domain.Base;
using Northwind.Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northwind.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public CategoryService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }


        public void Edit(CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategory(bool trackChanges)
        {
            var categoryModel = await _repositoryManager.CategoryRepository.GetAllCategory(trackChanges);
            // source = categoryModel, target categoryDto
            var categoryDto = _mapper.Map<IEnumerable<CategoryDto>>(categoryModel);
            return categoryDto;
        }

        public Task<CategoryDto> GetCategoryById(int cstegoryId, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public void Insert(CategoryForCreateDto categoryForCreateDto)
        {
            throw new NotImplementedException();
        }

        public void Remove(CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
