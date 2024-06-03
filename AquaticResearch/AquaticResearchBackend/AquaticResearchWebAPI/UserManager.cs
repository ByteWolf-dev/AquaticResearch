using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

public class UserManager
{
    private IConfiguration _configuration;

    /// <summary>
    /// Constructor for LoginController.
    /// </summary>
    /// <param name="configuration"></param>
    public UserManager(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Get the JWT.
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    public string GenerateJwt(UserInfo userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub,   userInfo.Username),
            new Claim(JwtRegisteredClaimNames.Email, userInfo.EmailAddress),
            new Claim("DateOfJoin",                 userInfo.DateOfJoin.ToString("yyyy-MM-dd")),
            new Claim(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString())
        };

        if (!string.IsNullOrEmpty(userInfo.Roles))
        {
            claims.Add(new Claim(ClaimTypes.Role, userInfo.Roles));
        }

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
            _configuration["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// User authentication.
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    public UserInfo? AuthenticateUser(User login)
    {
        var userinfo = _users.FirstOrDefault(user => user.Username == login.Username);
        if (userinfo is not null && login.Password == "Guest1234")
        {
            return userinfo;
        }

        return null;
    }

    private static UserInfo[] _users = new[]
    {
        new UserInfo { Username = "Maxi", EmailAddress  = "maxi@htl.at", DateOfJoin  = new DateOnly(2024, 1, 1), Roles  = "Admin"},
        new UserInfo { Username = "Seppi", EmailAddress = "seppi@htl.at", DateOfJoin = new DateOnly(2000, 1, 1) },
        new UserInfo { Username = "Seppi", EmailAddress = "seppi@htl.at", DateOfJoin = new DateOnly(2000, 1, 1) },
        new UserInfo { Username = "Anna", EmailAddress = "anna@htl.at", DateOfJoin = new DateOnly(2023, 5, 15), Roles = "Scientist" },
        new UserInfo { Username = "Bernd", EmailAddress = "bernd@htl.at", DateOfJoin = new DateOnly(2021, 3, 10), Roles = "Scientist" },
    };
}