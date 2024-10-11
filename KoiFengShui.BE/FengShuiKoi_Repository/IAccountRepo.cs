using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Repository
{
	public interface IAccountRepo
	{
		public Account GetAccountByEmail(string email);
		public Account GetAccountByUserID(string userid);

		public List<Account> GetAllAccounts();
		public bool AddAccount(Account account);
		public bool UpdateAccountByUser(Account newAccountData);
        public bool UpdateAccountByAdmin(Account newAccountData);
		public bool DeleteAccount(string userId);
	}
}
