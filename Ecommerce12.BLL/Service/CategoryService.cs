using Ecommerce12.DAL.DTO_s.Request.Category;
using Ecommerce12.DAL.DTO_s.Response.AuthenticationResponse;
using Ecommerce12.DAL.DTO_s.Response.CategoryResponse;
using Ecommerce12.DAL.Models;
using Ecommerce12.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryResponse> CreateCategory(CategoryRequest Request)
        {
            var CategoryReq = Request.Adapt<Category>();
            var categoryDB =  await  _categoryRepository.CreateCategoryRepo(CategoryReq);
            var categoryRes = categoryDB.Adapt<CategoryResponse>();
            return categoryRes;
        }
        public async Task<List<CategoryResponse>> GetAllCategories()
        {
            var CategoriesDB = await _categoryRepository.GetAll();
            var categoriesRes = CategoriesDB.Adapt<List<CategoryResponse>>();
            return categoriesRes;
        }
        public async Task<List<CategoryResponseForUser>> GetAllCategoriesForUser(string Language = "en")
        {
            var CategoriesDB = await _categoryRepository.GetAll();
            var categoriesRes = CategoriesDB.BuildAdapter().AddParameters("Language", Language).AdaptToType<List<CategoryResponseForUser>>();
            return categoriesRes;
        }
        public async Task <BaseResponse> UpdateCategoryAsync(int id , CategoryRequest request )
        {
           try 
            {
                var category = await _categoryRepository.FindByIdAsync(id);
                if(category is null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Category Not Found"
                    };
                }
                if(request.Translation != null)
                {
                    foreach (var translation in request.Translation) 
                    {
                        var existing = category.Translation.FirstOrDefault(t =>t.Lang == translation.Lang);

                        if(existing != null)
                        {
                            existing.Name = translation.Name;
                        }else
                        {
                            return new BaseResponse
                            {
                                Success=false,
                                Message = $"Language {translation.Lang} is not supported"
                            };
                        }
                    }
                }
                await _categoryRepository.UpdateCategoryAsync(category);
                return new BaseResponse
                {
                    Success = true,
                    Message ="category Updated Successfully "
                };
            }
            catch (Exception ex) 
            {
                return new BaseResponse
                {
                    Success = false,
                    Message = "Can't Updated Category",
                    Errors = new List<string> { ex.Message }
                };

            }


        } 
        public async Task<BaseResponse> ToggleStatus(int id )
        {
            try
            {
                var category = await _categoryRepository.FindByIdAsync(id);
                if (category is null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message = "Category Not Found",
                    };
                }
                category.status = category.status == Status.Active ? Status.InActive : Status.Active;
                await _categoryRepository.UpdateCategoryAsync(category);

                return new BaseResponse
                {
                    Success = true,
                    Message = "Category Status Updated Successfully"
                };
            }
            catch (Exception ex)
            {

                return new BaseResponse
                {
                    Success = false,
                    Message = "Can't Delete Category",
                    Errors = new List<string> { ex.Message }
                };

            }

        }
        public async Task<BaseResponse> DeleteAsync(int id)
        {
            try 
            {
                var category = await _categoryRepository.FindByIdAsync(id);
                if (category is null)
                {
                    return new BaseResponse
                    {
                        Success = false,
                        Message ="Category Not Found",
                    };
                }
                await _categoryRepository.DeleteCategoryAsync(category);
                return new BaseResponse
                {
                    Success = true,
                    Message = "Category Deleted Successfully"
                };
            }
            catch(Exception ex) 
            {

                return new BaseResponse
                {
                    Success = false,
                    Message = "Can't Delete Category",
                    Errors = new List<string> {  ex.Message }
                };

            }
        }
    }
}
