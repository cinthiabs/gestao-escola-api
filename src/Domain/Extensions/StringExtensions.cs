using System.Security.Cryptography;
using System.Text;

namespace Domain.Extensions;

public static class StringExtensions
{
    public static Dictionary<string, string> GerarHashESalt(string senha)
    {
        using var hmac = new HMACSHA512();
        var saltBytes = hmac.Key;
        var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));

        var senhaHash = Convert.ToBase64String(hashBytes);
        var senhaSalt = Convert.ToBase64String(saltBytes);

        return new Dictionary<string, string>
        {
            { "senhaHash", senhaHash },
            { "senhaSalt", senhaSalt }
        };
    }

}
