using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
    public class BlogRepo : IBlogRepo
    {
        public bool AddBlog(Blog Blog) => BlogDAO.Instance.AddBlog(Blog);

        public bool DeleteBlog(string BlogID) => BlogDAO.Instance.DeleteBlog(BlogID);



        public Blog GetAccountByUserID(string BlogID) => BlogDAO.Instance.GetBlogByBlogID(BlogID);

        public List<Blog> GetBlogs() => BlogDAO.Instance.GetBlogs();

        public bool UpdateBlog(string BlogID) => BlogDAO.Instance.UpdateBlog(BlogID);
    }
}
