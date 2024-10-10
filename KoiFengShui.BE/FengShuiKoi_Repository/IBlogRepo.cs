using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
    public interface IBlogRepo
    {
        public bool AddBlog(Blog Blog);

        public bool DeleteBlog(string BlogID) ;

       

        public Blog GetAccountByUserID(string BlogID) ;

        public List<Blog> GetBlogs() ;

        public bool UpdateBlog(string BlogID);
    }
}
