﻿using ByteDev.PwnedPasswords;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SecurityEssentials.Core.Identity
{
    public interface IPwnedPasswordValidator
    {
        Task<PwnedPasswordDto> Validate(string password);
    }

    public class PwnedPasswordValidator : IPwnedPasswordValidator
    {
        public async Task<PwnedPasswordDto> Validate(string password)
        {
            ServicePointManager.Expect100Continue = true;
#if DEBUG
            // SECURE: Advice from Roslyn Analyzers is to allow system to choose best, however this is required to run locally
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
#else
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;
#endif
            var client = new PwnedPasswordsClient(new HttpClient());
            var pwnedPassword = await client.GetHasBeenPwnedAsync(password);
            return new PwnedPasswordDto
            {
                IsPwned = pwnedPassword.IsPwned,
                Count = pwnedPassword.Count
            };
        }
    }
    public class PwnedPasswordDto
    {
        public bool IsPwned { get; set; }
        public long Count { get; set; }
    }
}