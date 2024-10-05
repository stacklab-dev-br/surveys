using StackLab.Survey.Domain.Common.Entities;
using StackLab.Survey.Domain.Entities;
using StackLab.Survey.Domain.Exceptions;
using System.Security.Cryptography;

namespace StackLab.Survey.Domain.Auth;
public class VerificationToken : BaseEntity
{
    public string Token { get; protected set; }
    public DateTime Expiration { get; protected set; }
    public int ResendCount { get; set; }

    public User User { get; protected set; }

    public VerificationToken()
    {
        CreateToken();
        SetExpiration();

        ResendCount = 0;
    }

    private void CreateToken()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            var byteArray = new byte[4];
            rng.GetBytes(byteArray);

            var randomInteger = BitConverter.ToUInt32(byteArray, 0);

            var token = (randomInteger % 1000000).ToString("D6");

            Token = token;
        }
    }

    private void SetExpiration()
    {
        Expiration = DateTime.Now.AddMinutes(10);
    }

    public bool IsExpired()
    {
        return Expiration < DateTime.Now;
    }

    public void ResetExpiration()
    {
        SetExpiration();
    }

    public void IncreaseResentCount()
    {
        if (ResendLimitExceeded())
        {
            throw new ValidationException();
        }

        ResendCount++;
    }

    public bool ResendLimitExceeded()
    {
        return ResendCount >= 5;
    }

    public bool Validate(string token)
    {
        return !IsExpired() && Token == token;
    }
}
