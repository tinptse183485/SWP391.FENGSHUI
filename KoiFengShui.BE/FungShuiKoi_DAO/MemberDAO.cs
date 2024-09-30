using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FungShuiKoi_DAO
{
	public class MemberDAO
	{
		private SWP391_FengShuiKoiConsulting_DBContext dbContext;

		private static MemberDAO instance = null;

		public static MemberDAO Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new MemberDAO();
				}
				return instance;
			}
		}
		public MemberDAO()
		{
			dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
		}
		public Member GetMemberByUserID(string userid)
		{
			return dbContext.Members.SingleOrDefault(m => m.UserId.Equals(userid));
		}

		public List<Member> GetMembers()
		{
			return dbContext.Members.ToList();
		}
		public bool AddMember(Member member)
		{
			bool isSuccess = false;
			Member mem = this.GetMemberByUserID(member.UserId);
			try
			{
				if (mem == null)
				{
					dbContext.Members.Add(member);
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
		public bool DeleteAccount(string id)
		{
			bool isSuccess = false;
			Member mem = this.GetMemberByUserID(id);
			try
			{
				if (mem != null)
				{
					dbContext.Members.Remove(mem);
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
		public bool UpdateMember(string id)
		{
			bool isSuccess = false;
			Member mem = this.GetMemberByUserID(id);
			try
			{
				if (mem != null)
				{
					dbContext.Entry<Member>(mem).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
