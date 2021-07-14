using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FP.DAL
{
    public class ProductCategory : ServiceBase
    {


        public int SaveProductCategory(ProductCategoryModal objData)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = 0;

                    string query = "";

                    if (objData.ProductCategoryId == 0)
                    {
                        query = "Select Count(ProductCategoryId) from ProductCategory where Name = @Name and IsDeleted = 0";

                        output = _dbDapperContext.Query<int>(query, new
                        {
                            objData.Name
                        }).FirstOrDefault();

                        if (output < 1)
                        {
                            query = "Insert into ProductCategory(Name, CreatedBy, CreatedDate) values(@Name, @CreatedBy, GetUtcDate())";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Name,
                                CreatedBy = objData.ManagedBy
                            });
                        }
                        else
                        {
                            output = 0;
                        }
                    }
                    else
                    {
                        query = "Select Count(ProductCategoryId) from ProductCategory where Name = @Name and IsDeleted = 0 and ProductCategoryId <> @ProductCategoryId";

                        output = _dbDapperContext.Query<int>(query, new
                        {
                            objData.Name,
                            objData.ProductCategoryId
                        }).FirstOrDefault();

                        if (output < 1)
                        {
                            query = "Update ProductCategory Set Name = @Name, ModifiedBy = @ModifiedBy, ModifiedDate = GetUtcDate() where ProductCategoryId = @ProductCategoryId";
                            output = _dbDapperContext.Execute(query, new
                            {
                                objData.Name,
                                ModifiedBy = objData.ManagedBy,
                                objData.ProductCategoryId
                            });
                        }
                        else
                        {
                            output = 0;
                        }
                    }
                    return output;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return -1;
            }
        }

        public List<ProductCategoryModal> GetProductCategoryList()
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    string query = "Select ProductCategoryId, Name, FORMAT(CreatedDate, 'dd/MM/yyyy ') as CreatedDate from ProductCategory order by ProductCategoryId desc";
                    List<ProductCategoryModal> _objData = _dbDapperContext.Query<ProductCategoryModal>(query).ToList();

                    return _objData;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public ProductCategoryModal GetProductCategoryById(int productCategoryId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {

                    string query = "Select ProductCategoryId, Name from ProductCategory where ProductCategoryId = @ProductCategoryId";
                    ProductCategoryModal _objData = _dbDapperContext.Query<ProductCategoryModal>(query, new
                    {
                        ProductCategoryId = productCategoryId
                    }).FirstOrDefault();

                    return _objData;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return null;
            }
        }

        public int RemoveProductCategory(int productCategoryId)
        {
            try
            {
                using (IDbConnection _dbDapperContext = GetDefaultConnection())
                {
                    int output = _dbDapperContext.Execute("Delete from ProductCategory where ProductCategoryId = @ProductCategoryId", new
                    {
                        ProductCategoryId = productCategoryId
                    });
                    return output;
                }
            }
            catch (Exception ex)
            {
                //Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                return -1;
            }
        }

    }
}
