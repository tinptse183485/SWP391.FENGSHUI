using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
	public interface IMemberRepo
	{
		public Member GetMemberByUserID(string userid);
		public List<Member> GetMembers();
		public bool AddMember(Member member);
		public bool DeleteAccount(string id);
		public bool UpdateMember(string id);
	}
}
