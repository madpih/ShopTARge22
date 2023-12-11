using Microsoft.AspNetCore.Authentication;
using Org.BouncyCastle.Asn1.Mozilla;

namespace ShopTARge22.Core.Dto.AccountsDtos
{
    public class LoginDto
    {
        public string Email { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
