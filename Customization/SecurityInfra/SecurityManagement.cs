using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Backend.Customization.SecurityInfra
{
    public class SecurityManagement
    {
        UserManager<IdentityUser> UserManager;
        SignInManager<IdentityUser> SignInManager;
        RoleManager<IdentityRole> RoleManager;
        IConfiguration config;

        public SecurityManagement(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
            config = configuration;
        }


        public async Task<bool> RegisterUserAsync(AppUser user)
        {
            bool isUserCreated = false;

            try
            {
                var userExist = await UserManager.FindByEmailAsync(user.Email);

                if (userExist != null)
                {
                    throw new Exception($"User with Email {user.Email} already exists");
                }

                IdentityUser newUser = new IdentityUser()
                {
                    Email = user.Email,
                    UserName = user.Email,
                };

                if (user.Password == user.ConfirmPassword && IsValidEmail(user.Email))
                {
                    var result = await UserManager.CreateAsync(newUser, user.Password);

                    if (result.Succeeded)
                    {
                        isUserCreated = true;

                        if (!string.IsNullOrEmpty(user.Role))
                        {
                            // Check if the provided Role is valid
                            if (user.Role != "College" && user.Role != "Student")
                            {
                                throw new Exception("Invalid Role. Valid Roles are 'College' and 'Student'.");
                            }

                            // Create the Role if it doesn't exist
                            if (!await RoleManager.RoleExistsAsync(user.Role))
                            {
                                await RoleManager.CreateAsync(new IdentityRole(user.Role));
                            }

                            // Assign the user to the specified Role
                            await UserManager.AddToRoleAsync(newUser,user.Role);
                        }
                    }
                    else
                    {
                        throw new Exception($"Failed to create user. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                    }
                }
                else
                {
                    throw new Exception("Password does not match or invalid email format");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isUserCreated;
        }



        public async Task<SecurityResponse> AuthenticateUserAsync(LoginUser user)
        {
            SecurityResponse response = new SecurityResponse();

            try
            {
                var userExist = await UserManager.FindByEmailAsync(user.Email);
                if (userExist == null)
                    throw new Exception($"User with Email {user.Email} is not found");

                var RoleListForUser = await UserManager.GetRolesAsync(userExist);
                if (RoleListForUser.Count == 0)
                {
                    throw new Exception($"User with Email {user.Email} is not assigned to any Role, please assign Role for authentication");
                }

                var result = await SignInManager.PasswordSignInAsync(user.Email, user.Password, false, lockoutOnFailure: true);
                response.IsLoggedIn = true;
                if (!result.Succeeded)
                {
                    throw new Exception($"Login failed for User with Email {user.Email}, check the password");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }

      

        public async Task<bool> LogoutAsync()
        {
            await SignInManager.SignOutAsync();
            return true;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
