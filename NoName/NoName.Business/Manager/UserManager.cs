using Microsoft.AspNetCore.DataProtection;
using NoName.Business.Dtos;
using NoName.Business.Services;
using NoName.Business.Types;
using NoName.Data.Entities;
using NoName.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoName.Business.Manager
{
	public class UserManager : IUserService
	{
		private readonly IRepository<UserEntity> _userRepository;
		private readonly IDataProtector _dataProtector;

		public UserManager(IRepository<UserEntity> userRepository, IDataProtectionProvider dataProtectionProvider)
		{
			_userRepository = userRepository;	
			_dataProtector = dataProtectionProvider.CreateProtector("Security");
		}

		public ServiceMessage AddUser(AddUserDto addUserDto)
		{
			var hasMail = _userRepository.GetAll(x => x.Email.ToLower() == addUserDto.Email.ToLower()).ToList();
			 

			if (hasMail.Any()) 
			{

				return new ServiceMessage()
				{
					IsSucceed = false,
					Message = "Bu eposta adresli bir kullanıcı zaten mevcut."
				};
			}

			var userEntity = new UserEntity()
			{
				FirstName = addUserDto.FirstName,
				LastName = addUserDto.LastName,
				Email = addUserDto.Email,
				UserType = Data.Enums.UserTypeEnum.User,
				Password = _dataProtector.Protect(addUserDto.Password)
			};


			_userRepository.Add(userEntity);

			return new ServiceMessage
			{
				IsSucceed = true
			};

		}

		public UserDto Login(LoginDto loginDto)
		{

			var userEntity = _userRepository.Get(x => x.Email == loginDto.Email);

			if (userEntity is null)
			{
				return null;
			}

			var rawPassword = _dataProtector.Unprotect(userEntity.Password);

			if (loginDto.Password == rawPassword)
			{
				return new UserDto
				{
					Id = userEntity.Id,
					FirstName = userEntity.FirstName,
					LastName = userEntity.LastName,
					Email = userEntity.Email,
					UserType = userEntity.UserType
				};
			}
			else
			{
				return null;
			}
		}

	}
}
