using FengShuiKoi_BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_DAO
{
    public class BlogDAO
    {
        private SWP391_FengShuiKoiConsulting_DBContext dbContext;
        private static BlogDAO instance = null;
        public static BlogDAO Instance
        {
            get
            {
                //singleton design pattern
                if (instance == null)
                {
                    instance = new BlogDAO();
                }
                return instance;
            }
        }
        public BlogDAO()
        {
            dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
        }
        public Blog GetBlogByBlogID(string BlogID)
        {
            return dbContext.Blogs.SingleOrDefault(m => m.BlogId.Equals(BlogID));
        }
       

        public List<Blog> GetBlogs()
        {
            return dbContext.Blogs.ToList();
        }
        public bool AddBlog(Blog blog)
        {
            bool isSuccess = false;
            Blog _blog = this.GetBlogByBlogID(blog.BlogId);
            try
            {
                if (_blog == null)
                {
                    dbContext.Blogs.Add(blog);
                    dbContext.SaveChanges();
                    isSuccess = true;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }
        public string GetLastBlogId()
        {
            try
            {
             
                var lastBlogId = dbContext.Blogs
                    .OrderByDescending(b => b.BlogId)
                    .Select(b => b.BlogId)
                    .FirstOrDefault();

                return lastBlogId ?? string.Empty;
            }
            catch (Exception ex)
            {
              
                return string.Empty;
            }
        }
        public bool DeleteBlog(string BlogID)
        {
            bool isSuccess = false;
            Blog blog = this.GetBlogByBlogID(BlogID);
            try
            {
                if (blog != null)
                {
                    dbContext.Blogs.Remove(blog);
                    dbContext.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isSuccess;
        }
        public bool UpdateBlog(Blog updatedBlog)
        {
            if (updatedBlog == null)
            {
                return false; 
            }

            try
            {
                var existingBlog = dbContext.Blogs.Find(updatedBlog.BlogId);
                if (existingBlog == null)
                {
                    return false; 
                }

                dbContext.Entry(existingBlog).CurrentValues.SetValues(updatedBlog);

               
                int affectedRows = dbContext.SaveChanges();

                return affectedRows > 0;
            }
            catch (DbUpdateException ex)
            {
               
                return false;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }
    }
}
