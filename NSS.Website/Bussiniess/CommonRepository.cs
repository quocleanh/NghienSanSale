using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using NSS.Website.Extension;
using NSS.Website.Models;
namespace NSS.Website.Bussiniess
{
    public class CommonRepository
    {
        public static string _sqlConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static int IsDBNullInt(object data, int val)
        {
            if (data == System.DBNull.Value || data == null)
            {
                return val;
            }
            else
            {
                return Convert.ToInt32(data);
            }
        }
        public List<merchant_db> GetListMerchant()
        {
            List<merchant_db> merchant_list = new List<merchant_db>();
            try
            {
                string sql = @"  
                  SELECT TOP (1000) [id]
                                  ,[display_name]
                                  ,[login_name]
                                  ,[logo]
                                  ,[total_offer]
                              FROM [NSS].[dbo].[merchant_list]
                    ";
                merchant_list = new SqlConnection(_sqlConnection).Query<merchant_db>(sql
                    , commandType: CommandType.Text).ToList();
                return merchant_list.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        internal int InsertCategories(List<category> categories, string merchant)
        {
            try
            {
                if (categories != null && categories.Count > 1)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append("INSERT INTO [dbo].[categories]([merchant],[category_name],[category_name_show],[category_no],[slug]) VALUES ");
                    foreach (var item in categories)
                    {
                        string slug = UrlSlugger.ToUrlSlug(item.category_name_show);
                        builder.AppendFormat("(N'{0}', N'{1}', N'{2}', N'{3}', N'{4}'),",
                            merchant, 
                            item.category_name, 
                            item.category_name_show, 
                            item.category_no, 
                            slug);
                    }
                    string sql = builder.ToString().TrimEnd(',');
                    int RS = new SqlConnection(_sqlConnection).Execute(sql
                        , commandType: CommandType.Text);
                    return RS;
                }
            }
            catch (Exception ex)
            {
            }
            return 0;
        }
    }
}