using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using Microsoft.EntityFrameworkCore;
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
		public Dictionary<string, int> GetUsersByAgeGroup()
		{
			try
			{
				var now = DateTime.Now;
				var members = dbContext.Members.AsNoTracking().ToList();

				var result = new Dictionary<string, int>
				{
					{"Dưới 18", 0},
					{"18-29", 0},
					{"30-49", 0},
					{"50+", 0}
				};

				foreach (var member in members)
				{
					if (member.Birthday == null) continue;

					int age = now.Year - member.Birthday.Year;
					if (now.Month < member.Birthday.Month || (now.Month == member.Birthday.Month && now.Day < member.Birthday.Day))
					{
						age--;
					}

					if (age < 18) result["Dưới 18"]++;
					else if (age < 30) result["18-29"]++;
					else if (age < 50) result["30-49"]++;
					else result["50+"]++;
				}
				return result;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Lỗi trong GetUsersByAgeGroup: {ex.Message}");
				return new Dictionary<string, int>();
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
		public bool UpdateMember(Member updatedMember)
		{
			try
			{
				var existingMember = this.GetMemberByUserID(updatedMember.UserId);
				if (existingMember == null)
				{
					return false;
				}

				existingMember.Name = updatedMember.Name;
				existingMember.Birthday = updatedMember.Birthday;

				dbContext.Entry(existingMember).State = EntityState.Modified;
				dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{

				return false;
			}
		}
	}
}
