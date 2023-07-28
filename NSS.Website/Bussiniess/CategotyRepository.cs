using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using NSS.Website.Models;

namespace NSS.Website.Bussiniess
{
    public class CategotyRepository : CommonRepository
    {
        internal List<category> GetAll()
        {
            List<category> merchant_list = new List<category>();
            try
            {
                string sql = @"  
                  SELECT TOP (1000) [merchant]
                                  ,[category_name]
                                  ,[category_name_show]
                                  ,[category_no]
                                  ,[image]
                                  ,[slug]
                              FROM [NSS].[dbo].[categories]
                    ";
                merchant_list = new SqlConnection(_sqlConnection).Query<category>(sql
                    , commandType: CommandType.Text).ToList();
                return merchant_list.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}