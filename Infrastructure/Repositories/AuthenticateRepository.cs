using Application.DTO;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly CinemaContext _context;
        private readonly JwtConfiguration? _jwtConfiguration;
        public AuthenticateRepository(CinemaContext context, IOptions<JwtConfiguration>? jwtOptions) {
            _context = context;
            _jwtConfiguration = jwtOptions?.Value;
        }

        public async Task<ResponseManager<TokenDTO>> DoLogin(LoginDTO model)
        {
            var response = new ResponseManager<TokenDTO>();
            try
            {

                var users = await _context.UsersViews.Where(x=> x.Email.ToLower().Trim() == model.Email.ToLower().Trim())
                    .OrderByDescending(o=>o.CreatedAt).ToListAsync();

                if (users.Count > 1)
                {
                    throw new CustomException($"Se ha encontrado más de un usuario correspondiente a las credenciales introducidas. Favor consultar con un administrador.",null,HttpStatusCode.BadRequest);
                }

                var user = users.SingleOrDefault();

                if (user == null)
                {
                    throw new CustomException($"El usuario no ha sido encontrado.", null, HttpStatusCode.BadRequest);
                }

                if (AreEqual(model.Password, user?.PasswordHash, user?.PasswordSalt) == false)
                {
                    throw new CustomException($"Contraseña incorrecta.", null, HttpStatusCode.BadRequest);
                }

                if (user?.IsRecordActive == false)
                {
                    throw new CustomException($"Tu cuenta está deshabilitada. Para más información puede consultar con un administrador.", null, HttpStatusCode.BadRequest);
                }

                var token = new TokenDTO();
                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.RoleName),
                        new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString())
                    };

                var tokenString = GenerateToken(claims);

                token.Token = tokenString;

                response.SingleData = token;

            }
            catch(CustomException e)
            {
                throw new CustomException(e.Message, e.InnerException, e.StatusCode, e.ClassName,e.MethodName,e.CreationUserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        public async Task<ResponseManager> DoSignUp(SignUpDTO model)
        {
            var response = new ResponseManager();
            try
            {
                model.Email = model.Email.ToLower();

                var users = await _context.UsersViews.Where(x => x.Email.ToLower().Trim() == model.Email.ToLower().Trim())
                   .OrderByDescending(o => o.CreatedAt).ToListAsync();

                if (users.Count > 0)
                {
                    throw new CustomException($"Actualmente existe un usuario registrado con el email seleccionado, por favor introduzca otro.", null, HttpStatusCode.BadRequest);
                }

                string salt = CreateSalt(10);
                string hash = CreateSHA256Hash(model.Password, salt);

                var newUser = new Domain.Entities.User
                {
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    Email = model.Email.ToLower(),
                    RoleId = model.RoleId,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    CreatedAt = DateTime.Now
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                
            }
            catch (CustomException e)
            {
                throw new CustomException(e.Message, e.InnerException, e.StatusCode, e.ClassName, e.MethodName, e.CreationUserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        private string GenerateToken(List<Claim> claims)
        {
            string? secret = _jwtConfiguration?.Secret;

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: _jwtConfiguration?.ValidIssuer,
                audience: _jwtConfiguration?.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddHours(72),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }

        private static bool AreEqual(string plainTextInput, string? hash, string? salt)
        {
            try
            {
                string newHashed = CreateSHA256Hash(plainTextInput, salt);
                return newHashed.Equals(hash);
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        private static string CreateSalt(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        private static string CreateSHA256Hash(string plainTextInput, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(plainTextInput + salt);
            SHA256Managed sha256ManagedString = new SHA256Managed();
            byte[] hash = sha256ManagedString.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

    }
}
