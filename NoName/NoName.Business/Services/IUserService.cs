using NoName.Business.Dtos;
using NoName.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoName.Business.Services
{
	public interface IUserService
	{
		ServiceMessage AddUser(AddUserDto addUserDto);

		UserDto Login(LoginDto loginDto);
	}
}
