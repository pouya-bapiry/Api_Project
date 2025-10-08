using Data.Repositories;
using Entities.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using WebFramework.Api;
using WebFramework.Filters;

namespace MyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController : ControllerBase
    {
        #region fields and ctor

        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        #endregion

        #region Methods

        [HttpGet]
       [ApiResultFilter]
        public async Task<ActionResult<List<User>>> Get()
        {
            var users = await userRepository.TableNoTracking.ToListAsync();
            return BadRequest(users);
            //return new ApiResult<List<User>>
            //{
            //    IsSuccess = true,
            //    StatusCode = 0,
            //    Message = "عملیت با موفقیت انجام شد",
            //    Data = users
            //};
        }

        [HttpGet("{id:int}")]
        public async Task<ApiResult<User>> Get(int id, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(cancellationToken, id);
            if (user == null)
            {
                return NotFound();
            }

            return user;


            //return new ApiResult<User>
            //{
            //    IsSuccess = true,
            //    StatusCode = 0,
            //    Message = "عملیت با موفقیت انجام شد",
            //    Data = user
            //};
        }

        [HttpPost]
        public async Task<ApiResult<User>> Create(User user, CancellationToken cancellationToken)
        {
            await userRepository.AddAsync(user, cancellationToken);

            return Ok(user);
        }

        [HttpPut]
        public async Task<ApiResult> Update(int id, User user, CancellationToken cancellationToken)
        {
            var updateUser = await userRepository.GetByIdAsync(cancellationToken, id);

            updateUser.UserName = user.UserName;
            updateUser.PasswordHash = user.PasswordHash;
            updateUser.FullName = user.FullName;
            updateUser.Age = user.Age;
            updateUser.Gender = user.Gender;
            updateUser.IsActive = user.IsActive;
            updateUser.LastLoginDate = user.LastLoginDate;

            await userRepository.UpdateAsync(updateUser, cancellationToken);
            return new ApiResult(true, ApiResultStatusCode.Success);
            //return new ApiResult
            //{
            //    IsSuccess = true,
            //    StatusCode = 0,
            //    Message = "عملیت با موفقیت انجام شد",
            //};
        }

        [HttpDelete]
        public async Task<ApiResult> Delete(int id, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(cancellationToken, id);
            await userRepository.DeleteAsync(user, cancellationToken);
            return new ApiResult(true, ApiResultStatusCode.Success);
            //return new ApiResult
            //{
            //    IsSuccess = true,
            //    StatusCode = 0,
            //    Message = "عملیت با موفقیت انجام شد",
            //};
        }
        #endregion

    }
}
