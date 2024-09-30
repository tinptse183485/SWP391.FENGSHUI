using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace FengShuiKoi_Services
{
    public class BlogService : IBlogService
    {
        private IBlogRepo blogRepo;
        public BlogService()
        {
            blogRepo = new BlogRepo();
        }
        public bool AddBlog(Blog Blog)
        {
           return blogRepo.AddBlog(Blog);
        }

        public bool DeleteBlog(string BlogID)
        {
            return blogRepo.DeleteBlog(BlogID);
        }

        public Blog GetAccountByUserID(string BlogID)
        {
            return blogRepo.GetAccountByUserID(BlogID);
        }

        public List<Blog> GetBlogs()
        {
           return blogRepo.GetBlogs();
        
    }

        public bool UpdateBlog(string BlogID)
        {
            return blogRepo.UpdateBlog(BlogID);
        }
    }
}
