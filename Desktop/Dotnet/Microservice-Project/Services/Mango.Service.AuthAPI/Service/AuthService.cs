using Mango.Service.AuthAPI.Data;
using Mango.Service.AuthAPI.Models;
using Mango.Service.AuthAPI.Models.Dto;
using Mango.Service.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Service.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
           var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower());
           bool isvalid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
           if( user == null && isvalid == false ) {
            return new LoginResponseDto { User = null, Token = "" };
           }

           // if user is found and password is correct
           UserDto userDto = new()
            {
            Email = user.Email,
            ID = user.Id,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber
              };

              LoginResponseDto loginResponseDto = new LoginResponseDto()
              {
                User = userDto,
                Token = ""
              };

              return loginResponseDto;
            
          

           }
        

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
           ApplicationUser user = new ApplicationUser
           {
            UserName = registrationRequestDto.Email,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
            PhoneNumber = registrationRequestDto.PhoneNumber,
            Name = registrationRequestDto.Name
            
           };
           
           try
           {
            var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
            if(result.Succeeded) {

              //  var userToReturn = _db.ApplicationUsers.FirstOrDefault(u => u.UserName == registrationRequestDto.Email);
                UserDto userDto = new() {
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber
                };
                return "User created successfully.";
            } else {
                return result.Errors.FirstOrDefault()?.Description ?? "User creation failed. error occured.";
            }
           }
           catch (Exception ex )
           {
            Console.WriteLine(ex);
              return "User creation failed. error occured.";
           }
         

        }
    }

}