using FengShuiKoi_BO;
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
            Blog  _blog = this.GetBlogByBlogID(blog.BlogId);
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
        public bool UpdateBlog(string BlogID)
        {
            bool isSuccess = false;
            Blog blog = this.GetBlogByBlogID(BlogID);
            try
            {
                if (blog != null)
                {
                    dbContext.Entry<Blog>(blog).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
    }
}
