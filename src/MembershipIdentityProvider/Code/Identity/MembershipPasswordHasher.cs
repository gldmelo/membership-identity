﻿using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;

namespace MembershipIdentityProvider.Code.Identity
{
    public class MembershipPasswordHasher<TUser> : IPasswordHasher<TUser>
        where TUser : MembershipUser
    {
        public string HashPassword(TUser user, string password)
        {
            var saltBytes = Convert.FromBase64String(user.PasswordSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(63));

            return hashPassword;
        }

        public PasswordVerificationResult VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword)
        {
            if (hashedPassword.Equals(HashPassword(user, providedPassword)))
                return PasswordVerificationResult.Success;

            return PasswordVerificationResult.Failed;
        }

    }
}
