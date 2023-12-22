﻿using Duende.IdentityServer.Models;
using OriginalLanguage.Consts;

namespace OriginalLanguage.Identity.Configuration;

public static class AppClients
{
    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "swagger",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.ClientCredentials,

                AccessTokenLifetime = 3600, // 1 hour

                AllowedScopes = {
                    AppScopes.ArticlesRead,
                    AppScopes.ArticlesWrite,
                }
            }
            ,
            new Client
            {
                ClientId = "frontend",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                AllowOfflineAccess = true,
                AccessTokenType = AccessTokenType.Jwt,

                AccessTokenLifetime = 3600, // 1 hour

                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                RefreshTokenExpiration = TokenExpiration.Sliding,
                AbsoluteRefreshTokenLifetime = 2592000, // 30 days
                SlidingRefreshTokenLifetime = 1296000, // 15 days

                AllowedScopes = {
                    AppScopes.ArticlesRead,
                    AppScopes.ArticlesWrite,
                }
            }
        };
}
