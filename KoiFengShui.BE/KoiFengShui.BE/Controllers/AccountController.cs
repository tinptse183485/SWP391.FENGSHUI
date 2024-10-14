using FengShuiKoi_BO;
using FengShuiKoi_Services;
using KoiFengShui.BE.Model;
using KoiFengShui.BE.TokenService;
using Microsoft.AspNetCore.Mvc;
using System;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiFengShui.BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMemberService _memberService;
        private readonly IToken _tokenService;

        public AccountController(IAccountService accountService, IMemberService memberService, IToken tokenService)
        {
            _memberService = memberService;
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginAccount)
        {
            var account = await _accountService.GetAccountByUserID(loginAccount.UserId);

            if (account == null || account.Password != loginAccount.Password)
            {
                return Unauthorized("Thông tin đăng nhập không hợp lệ");
            }

            if (account.Status != "Active")
            {
                return Unauthorized("Tài khoản không hoạt động");
            }

            var token = _tokenService.CreateToken(account);

            return Ok(new
            {
                Token = token,
                Role = account.Role,
                UserId = account.UserId
            });
        }

        [HttpPut("UpdateAccountUser")]
        public async Task<IActionResult> UpdateAccountUser(RegisterDTO Account)
        {
            try
            {
                var existingAccount = await _accountService.GetAccountByUserID(Account.UserID);
                if (existingAccount == null)
                {
                    return BadRequest("Tài khoản không tồn tại");
                }

                if (string.IsNullOrWhiteSpace(Account.Password))
                {
                    return BadRequest("Mật khẩu không được để trống");
                }
                if (string.IsNullOrWhiteSpace(Account.Email))
                {
                    return BadRequest("Email không được để trống");
                }
                if (string.IsNullOrWhiteSpace(Account.Name))
                {
                    return BadRequest("Tên không được để trống");
                }

                var accountWithSameEmail = await _accountService.GetAccountByEmail(Account.Email);
                if (accountWithSameEmail != null && accountWithSameEmail.UserId != Account.UserID)
                {
                    return BadRequest("Email đã tồn tại");
                }
                existingAccount.Password = Account.Password;
                existingAccount.Email = Account.Email;
                var existingMember = await _memberService.GetMemberByUserID(Account.UserID);
                if (existingMember != null)
                {
                    existingMember.Name = Account.Name;
                    existingMember.Birthday = Account.Birthday;
                    await _memberService.UpdateMember(existingMember);
                }

                bool result = await _accountService.UpdateAccountByUser(existingAccount);

                if (result)
                {
                    return Ok("Cập nhật thành công");
                }
                else
                {
                    return BadRequest("Cập nhật thất bại");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpPut("UpdateAccountAdmin")]
        public async Task<IActionResult> UpdateAccountAdmin(AccountDTO Account)
        {
            try
            {
                if (await _accountService.GetAccountByUserID(Account.UserID) == null)
                {
                    return BadRequest("Tài khoản không tồn tại");
                }

                if (string.IsNullOrWhiteSpace(Account.status))
                {
                    return BadRequest("Trạng thái không được để trống");
                }

                var acc = new Account
                {
                    UserId = Account.UserID,
                    Password = Account.Password,
                    Status = Account.status
                };

                bool result = await _accountService.UpdateAccountByAdmin(acc);

                if (result)
                {
                    return Ok("Cập nhật thành công");
                }
                else
                {
                    return BadRequest("Cập nhật thất bại");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("GetAllAccountMemberInfo")]
        public async Task<IActionResult> GetAllAccountMemberInfo()
        {
            var accounts = await _accountService.GetAllAccounts();
            var accountInfoList = new List<RegisterDTO>();

            foreach (var account in accounts)
            {
                var member = await _memberService.GetMemberByUserID(account.UserId);
                var accountInfo = new RegisterDTO
                {
                    UserID = account.UserId,
                    Email = account.Email,
                    Password = account.Password,
                    Role = account.Role,
                    status = account.Status,
                    Name = member?.Name,
                    Birthday = (DateTime)(member?.Birthday)
                };
                accountInfoList.Add(accountInfo);
            }

            return Ok(accountInfoList);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO newAccount)
        {
            if (await _accountService.GetAccountByUserID(newAccount.UserID) != null)
            {
                return BadRequest("Tài khoản đã tồn tại");
            }
            if (await _accountService.GetAccountByEmail(newAccount.Email) != null)
            {
                return BadRequest("Email đã tồn tại");
            }
            var acc = new Account
            {
                UserId = newAccount.UserID,
                Password = newAccount.Password,
                Role = newAccount.Role,
                Email = newAccount.Email,
                Status = newAccount.status
            };
            bool result = await _accountService.AddAccount(acc);

            var mem = new Member
            {
                Name = newAccount.Name,
                Birthday = newAccount.Birthday,
                UserId = newAccount.UserID
            };
            bool result2 = await _memberService.AddMember(mem);

            if (result && result2)
            {
                return Ok("Đăng ký thành công");
            }
            else
            {
                return BadRequest("Đăng ký thất bại");
            }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleUserDTO googleUser)
        {
            var account = await _accountService.GetAccountByEmail(googleUser.Email);

            if (account == null)
            {
                account = new Account
                {
                    UserId = googleUser.GoogleId,
                    Email = googleUser.Email,
                    Role = "Member",
                    Status = "Active",
                    Password = Guid.NewGuid().ToString()
                };
                await _accountService.AddAccount(account);

                var member = new Member
                {
                    UserId = googleUser.GoogleId,
                    Name = googleUser.Name,
                    Birthday = DateTime.Now
                };
                await _memberService.AddMember(member);
            }

            var token = _tokenService.CreateToken(account);

            return Ok(new
            {
                Token = token,
                Role = account.Role,
                UserId = account.UserId
            });
        }
    }
    }
