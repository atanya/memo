using System;
using System.Web.Http;
using DTO;
using SuperMemo.BL;
using SuperMemo.Models;

namespace SuperMemo.Controllers
{
    public class UserInfoController : ApiController
    {
        public ResponseObject Post([FromBody]UserDto userInfo)
        {
            if (string.IsNullOrEmpty(userInfo.OldPassword) || string.IsNullOrEmpty(userInfo.NewPassword))
            {
                ResponseObject.Failure("Old and new passwords are required");
            }

            var userService = new UserService();
            var user = userService.FindByNameAndPassword(User.Identity.Name, userInfo.OldPassword);
            if (user == null)
                return ResponseObject.Failure("Old password is incorrect");

            userService.Update(user.Id, userInfo.NewPassword);
            return ResponseObject.Success("");
        }
    }
}
