// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using static IdentityModel.OidcConstants;

namespace ids4
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("custom", "Custom Properties", new List<string>{"role", "id" } )
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api1", "My API"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[] 
            {
                new Client{ ClientName = "Binlist Api", ClientId = "BinlistApi", 
                    
                    AllowedGrantTypes = IdentityServer4.Models.GrantTypes.ClientCredentials , 
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:4566/signin-oidc"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes =
                    {
                        "api1"
                    },
                    AccessTokenLifetime = 3600 * 24 * 7, // 7 days
                    IdentityTokenLifetime = 3600 
                },
                new Client{ ClientName = "Client 2", ClientId = "Client2",

                    AllowedGrantTypes =IdentityServer4.Models.GrantTypes.CodeAndClientCredentials ,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:44328/signin-oidc"
                    },
                    AllowedScopes =
                    {
                        "openid","profile", "custom"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RequirePkce = false,
                    RequireConsent = true,
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:44328/signout-callback-oidc"
                    }
                },
            };
    }
}