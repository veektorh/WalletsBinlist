using DemoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using System.Net.Http;
using Microsoft.Extensions.Options;
using IdentityModel.Client;
using DemoApp.Interfaces.Managers;
using Microsoft.Extensions.Logging;

namespace DemoApp.Managers
{
    public class TokenManager : ITokenManager
    {
        private readonly AppDbContext appDbContext;
        private readonly ILogger<TokenManager> logger;
        private readonly IdentityConfigOptions identityConfig;
        private readonly ApiUrl apiUrl;

        public TokenManager(AppDbContext appDbContext, IOptions<ApiUrl> apiUrl, IOptions<IdentityConfigOptions> identityConfig, ILogger<TokenManager> logger)
        {
            this.appDbContext = appDbContext;
            this.logger = logger;
            this.identityConfig = identityConfig.Value;
            this.apiUrl = apiUrl.Value;
        }

        public async Task<string> GetToken()
        {
            var token = appDbContext.Credentials.FirstOrDefault(a => a.Name == "binlist");
            if (token == null)
            {
                return await RequestNewToken();
            }

            return token.Token;
        }

        public async Task<string> RequestNewToken()
        {
            var client = new HttpClient();
            var disco = client.GetDiscoveryDocumentAsync(apiUrl.IdentityServer).GetAwaiter().GetResult();
            if (disco.IsError)
            {
                logger.LogError(disco.Error);
                throw new Exception(disco.Error);
            }

            var tokenResponse = client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = identityConfig.ClientId,
                ClientSecret = identityConfig.ClientSecret,
                //Scope = identityConfig.Scope
            }).GetAwaiter().GetResult();

            if (tokenResponse.IsError)
            {
                logger.LogError(tokenResponse.Error);
                throw new Exception(tokenResponse.Error);
            }

            var oldtoken = appDbContext.Credentials.FirstOrDefault(a => a.Name == "binlist");
            if (oldtoken == null)
            {
                await appDbContext.Credentials.AddAsync(new Credentials { Name = "binlist", Token = tokenResponse.AccessToken });                
            }
            else
            {
                oldtoken.Token = tokenResponse.AccessToken;
            }

            await appDbContext.SaveChangesAsync();

            return tokenResponse.AccessToken;
        }
    }
}
