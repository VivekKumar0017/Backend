using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
                // Check if user exists
                var userExist = await UserManager.FindByEmailAsync(user.Email);
                if (userExist == null)
                    throw new Exception($"User with Email {user.Email} is not found");

                // Authenticate the user
                var result = await SignInManager.PasswordSignInAsync(user.Email, user.Password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    // Generate the token
                    var secretKey = Encoding.ASCII.GetBytes(config["JWTCoreSettings:SecretKey"]);
                    var expiry = Convert.ToInt32(config["JWTCoreSettings:ExpiryInMinutes"]);

                    // Create Claims for the user
                    var claims = new List<Claim>
                    {
                        new Claim("username", userExist.Email),
                    };

                    // Check if the user is a college or a student
                    var isCollege = await UserManager.IsInRoleAsync(userExist, "college");
                    if (isCollege)
                    {
                        claims.Add(new Claim("role", "college"));
                    }
                    else
                    {
                        claims.Add(new Claim("role", "student"));
                    }

                    // Create a Token Descriptor
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(claims),
                        Expires = DateTime.UtcNow.AddMinutes(expiry),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
                    };

                    // Generate the JWT token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    // Set response properties
                    response.IsLoggedIn = true;
                    response.Token = tokenString;
                }
                else
                {
                    // Authentication failed
                    response.IsLoggedIn = false;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
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
