﻿using Microsoft.AspNetCore.Identity;
using Nancy;
using ShopTARge22.Core.Domain;
using ShopTARge22.Core.Dto.AccountsDtos;
using ShopTARge22.Core.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge22.ApplicationServices.Services
{
    public class AccountsServices : IAccountsServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountsServices
            (
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUser> Register(ApplicationUserDto dto)
        {
            var user = new ApplicationUser
            {
                //username ja email on IdentityUseri seest
                UserName = dto.Email,
                Email = dto.Email,
                City = dto.City

            };

            var result = await _userManager.CreateAsync( user, dto.Password );

            if ( result.Succeeded )
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //var confirmationLink = Url.Action
            }
            return user;
        }

        public async Task<ApplicationUser> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync( userId );
            if ( user == null )
            {
                string errorMessage = $"The provided User ID {userId} is not valid.";
            }

            var result = await _userManager.ConfirmEmailAsync( user, token );
            return user;
        }

        public async Task<ApplicationUser> Login(LoginDto dto)
        {
            dto.ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            var user = await _userManager.FindByEmailAsync(dto.Email);

            return user;
        }
        
        
    }
}
