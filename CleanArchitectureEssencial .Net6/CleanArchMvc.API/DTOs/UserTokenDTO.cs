using System;

namespace CleanArchMvc.API.DTOs
{
    public class UserTokenDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

}
