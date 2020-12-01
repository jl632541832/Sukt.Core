﻿using Microsoft.AspNetCore.Identity;
using Sukt.Core.Domain.Models;
using Sukt.Core.Dtos.LoginIdentity;
using Sukt.Core.Shared.Enums;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.OperationResult;
using Sukt.Core.Shared.ResultMessageConst;
using Sukt.Core.Shared.Security.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sukt.Core.Application.LoginIdentity
{
    public class IdentityContract : IIdentityContract
    {
        private readonly SignInManager<UserEntity> _signInManager = null;
        private readonly UserManager<UserEntity> _userManager = null;
        private readonly IJwtBearerService _jwtBearerService = null;

        public IdentityContract(SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager, IJwtBearerService jwtBearerService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtBearerService = jwtBearerService;
        }

        public async Task<(OperationResponse item, Claim[] cliams)> Login(LoginInputDto input)
        {
            input.NotNull(nameof(input));
            var user = await _userManager.FindByNameAsync(input.UserName);
            if (user == null)
                return (new OperationResponse(ResultMessage.UserNameNotFind, OperationEnumType.Error), new Claim[] { });
            var result = await _userManager.CheckPasswordAsync(user, input.Password);
            if (!result)
                return (new OperationResponse(ResultMessage.PassWordNotCheck, OperationEnumType.Error), new Claim[] { });
            var jwtToken = _jwtBearerService.CreateToken(user.Id, user.UserName);

            return (new OperationResponse(ResultMessage.LoginSusscess, new
            {
                AccessToken = jwtToken.AccessToken,
                NickName = user.NickName,
                UserId = user.Id.ToString(),
                AccessExpires = jwtToken.AccessExpires
            }, OperationEnumType.Error), new Claim[] { });
        }
    }
}