using FengShuiKoi_BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FengShuiKoi_DAO
{
	public class AccountDAO
	{
		private SWP391_FengShuiKoiConsulting_DBContext dbContext;
		private static AccountDAO instance = null;
		public static AccountDAO Instance
		{
			get
			{
				//singleton design pattern
				if (instance == null)
				{
					instance = new AccountDAO();
				}
				return instance;
			}
		}
		public AccountDAO()
		{
			dbContext = new SWP391_FengShuiKoiConsulting_DBContext();
		}
		public Account GetAccountByEmail(string email)
		{
			return dbContext.Accounts.SingleOrDefault(m => m.Email.Equals(email));
		}
		public Account GetAccountByUserID(string userid)
		{
			return dbContext.Accounts.SingleOrDefault(m => m.UserId.Equals(userid));
		}

		public List<Account> GetAccounts()
		{
			return dbContext.Accounts.ToList();
		}
		public bool AddAccount(Account account) {
			bool isSuccess = false;
			Account acc = this.GetAccountByUserID(account.UserId);
			try
			{
				if (acc == null)
				{
					dbContext.Accounts.Add(account);
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
			Account acc = this.GetAccountByUserID(id);
			try
			{
				if (acc != null)
				{
					dbContext.Accounts.Remove(acc);
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
		public bool UpdateAccount(string id)
		{
			bool isSuccess = false;
			Account acc = this.GetAccountByUserID(id);
			try
			{
				if (acc != null)
				{
					dbContext.Entry<Account>(acc).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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
