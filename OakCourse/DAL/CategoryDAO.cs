using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoryDAO : PostContext
    {
        public int AddCategory(Category category)
        {
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return category.ID;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<CategoryDTO> GetCategories()
        {
            List<Category> list = db.Categories.Where(x => x.isDeleted == false)
                .OrderByDescending(x => x.ID)
                .ToList();

            List<CategoryDTO> dtolist = new List<CategoryDTO>();

            foreach (var item in list)
            {
                CategoryDTO dto = new CategoryDTO
                {
                    CategoryName = item.CategoryName,
                    ID = item.ID
                };

                dtolist.Add(dto);
            }

            return dtolist;
        }

        public void UpdateCategory(CategoryDTO model)
        {
            try
            {
                Category category = db.Categories.First(x => x.ID == model.ID);

                category.CategoryName = model.CategoryName;
                category.LastUpdateDate = DateTime.Now;
                category.LastUpdateUserID = UserStatic.UserID;

                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public CategoryDTO GetCategoryWithID(int ID)
        {
            try
            {
                Category category = db.Categories.First(x => x.ID == ID);
                
                CategoryDTO dto = new CategoryDTO 
                {
                    ID= category.ID,
                    CategoryName= category.CategoryName
                };
                
                return dto;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
