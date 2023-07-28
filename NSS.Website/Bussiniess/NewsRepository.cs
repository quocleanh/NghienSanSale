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
    public class NewsRepository : CommonRepository
    {
        internal News GetBySlug(string slug)
        {
            List<News> news = new List<News>();
            try
            {
                string sql = @"  
                    SELECT n.ID,
                           n.CategoryID,
                           nc.CateforyName CategoryName,
	                       nc.Link AS CategoryLink,
                           n.Title,
                           n.Description,
                           n.Content,
                           n.Images,
                           n.MetaTitle,
                           n.MetaKeyword,
                           n.MetaDescription,
                           n.Slug,
                           n.LinkAffiliate
                    FROM dbo.News AS n
                        JOIN dbo.NewsCategory AS nc
                            ON nc.ID = n.CategoryID
                    WHERE n.Slug = @Slug
                    ";
                var param = new
                {
                    Slug = slug
                };
                news = new SqlConnection(_sqlConnection).Query<News>(sql
                    , param
                    , commandType: CommandType.Text).ToList();
                return news.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal List<NewsCategory> GetAllCategories()
        {
            List<NewsCategory> NewsCategory = new List<NewsCategory>();
            try
            {
                string sql = @"  
                    SELECT nc.ID,
                           nc.CateforyName CategoryName,
                           nc.Description,
                           nc.Content,
                           nc.Images,
                           nc.MetaTitle,
                           nc.MetaKeyword,
                           nc.MetaDescription,
                           nc.Link,
                           nc.LinkAffiliate
                    FROM dbo.NewsCategory AS nc 
                    ";
                var param = new
                {
                    //Slug = slug
                };
                NewsCategory = new SqlConnection(_sqlConnection).Query<NewsCategory>(sql
                    , param
                    , commandType: CommandType.Text).ToList();
                return NewsCategory.ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        internal List<News> GetAll()
        {
            List<News> news = new List<News>();
            try
            {
                string sql = @"  
                    SELECT n.ID,
                           n.CategoryID,
                           nc.CateforyName CategoryName,
	                       nc.Link AS CategoryLink,
                           n.Title,
                           n.Description,
                           n.Content,
                           n.Images,
                           n.MetaTitle,
                           n.MetaKeyword,
                           n.MetaDescription,
                           n.Slug,
                           n.LinkAffiliate
                    FROM dbo.News AS n
                        JOIN dbo.NewsCategory AS nc
                            ON nc.ID = n.CategoryID

                    ";
                var param = new
                {
                    //Slug = slug
                };
                news = new SqlConnection(_sqlConnection).Query<News>(sql
                    , param
                    , commandType: CommandType.Text).ToList();
                return news.ToList();
            }
            catch (Exception )
            {
                return null;
            }
        }
    }
}