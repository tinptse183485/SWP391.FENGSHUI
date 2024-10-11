using FengShuiKoi_BO;
using Microsoft.EntityFrameworkCore;
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
        public bool UpdateAccountByAdmin(Account newAccountData)
        {
            try
            {
                var existingAccount = this.GetAccountByUserID(newAccountData.UserId);
                if (existingAccount == null)
                {
                    return false;
                }

                // Update account properties
                existingAccount.Status = newAccountData.Status;
                existingAccount.Password = newAccountData.Password;
                

                dbContext.Entry(existingAccount).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateAccountByUser(Account newAccountData)
        {
            try
            {
                var existingAccount = this.GetAccountByUserID(newAccountData.UserId);
                if (existingAccount == null)
                {
                    return false;
                }

             
                existingAccount.Email = newAccountData.Email;
                existingAccount.Password = newAccountData.Password;


                dbContext.Entry(existingAccount).State = EntityState.Modified;
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
