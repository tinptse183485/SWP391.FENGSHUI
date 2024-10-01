using FengShuiKoi_BO;
using FengShuiKoi_DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
	public class AccountRepo : IAccountRepo
	{
		public bool AddAccount(Account account) => AccountDAO.Instance.AddAccount(account);

		public bool DeleteAccount(string userId) => AccountDAO.Instance.DeleteAccount(userId);

		public Account GetAccountByEmail(string email) => AccountDAO.Instance.GetAccountByEmail(email);

		public Account GetAccountByUserID(string userid)=> AccountDAO.Instance.GetAccountByUserID(userid);

		public List<Account> GetAllAccounts()=> AccountDAO.Instance.GetAccounts();

		public bool UpdateAccount(string userId) => AccountDAO.Instance.UpdateAccount(userId);
	}
}
