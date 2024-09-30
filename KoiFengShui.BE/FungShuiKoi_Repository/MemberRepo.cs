using FengShuiKoi_BO;
using FungShuiKoi_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
	public class MemberRepo : IMemberRepo
	{
		public bool AddMember(Member member)=> MemberDAO.Instance.AddMember(member);

		public bool DeleteAccount(string id)=> MemberDAO.Instance.DeleteAccount(id);

		public Member GetMemberByUserID(string userid)=> MemberDAO.Instance.GetMemberByUserID(userid);

		public List<Member> GetMembers()=> MemberDAO.Instance.GetMembers();

		public bool UpdateMember(string id)=> MemberDAO.Instance.UpdateMember(id);
	}
}
