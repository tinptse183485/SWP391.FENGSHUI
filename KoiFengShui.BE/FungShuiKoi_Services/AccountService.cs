using FengShuiKoi_BO;
using FengShuiKoi_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_Services
{
	public class AccountService : IAccountService
	{
		private IAccountRepo iAccountRepo;
		public AccountService()
		{
			iAccountRepo = new AccountRepo();
		}
		public bool AddAccount(Account account)
		{
			return iAccountRepo.AddAccount(account);
		}

		public bool DeleteAccount(string userId)
		{
			return iAccountRepo.DeleteAccount(userId);
		}

		public Account GetAccountByEmail(string email)
		{
			return iAccountRepo.GetAccountByEmail(email);
		}

		public Account GetAccountByUserID(string userid)
		{
			return iAccountRepo.GetAccountByUserID(userid);
		}

		public List<Account> GetAllAccounts()
		{
			return iAccountRepo.GetAllAccounts();
		}

		public bool UpdateAccount(string userId)
		{
			return (iAccountRepo.UpdateAccount(userId));
		}
	}
}
