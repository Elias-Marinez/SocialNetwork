﻿
namespace SocialNetwork.Core.Application.Dtos.Account.Response
{
    public class AuthenticationResponse : BaseResponse
    {
        public  string? Id { get; set; }
        public  string? UserName { get; set; }
        public  string? Email { get; set; }
        public bool IsVerified { get; set; }
    }
}