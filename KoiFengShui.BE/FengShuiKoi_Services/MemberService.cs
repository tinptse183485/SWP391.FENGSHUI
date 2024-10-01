using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Services
{
	public class MemberService : IMemberService
	{
		private IMemberRepo iMemberRepo;
		public MemberService()
		{
			iMemberRepo = new MemberRepo();
		}
		public bool AddMember(Member member)
		{
			return iMemberRepo.AddMember(member);
		}

		public bool DeleteAccount(string id)
		{
			return iMemberRepo.DeleteAccount(id);
		}

		public Member GetMemberByUserID(string userid)
		{
			return iMemberRepo.GetMemberByUserID(userid);
		}

		public List<Member> GetMembers()
		{
			return iMemberRepo.GetMembers();
		}

		public bool UpdateMember(string id)
		{
			return iMemberRepo.UpdateMember(id);
		}
	}
}
