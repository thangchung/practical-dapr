// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;
using Microsoft.Extensions.Configuration;
using N8T.Infrastructure.Dapr;

namespace CoolStore.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> Apis =>
            new []
            {
                new ApiResource("graph-api", "My Graph API")
            };


        public static IEnumerable<Client> Clients(IConfiguration config) =>
            new []
            {
                new Client
                {
                    ClientId = "webui",
                    ClientSecrets = { new Secret("mysecret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,
                    AllowOfflineAccess = true,

                    // where to redirect to after login
                    RedirectUris = { $"{config.GetDaprClientUrl("webui")}/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { $"{config.GetDaprClientUrl("webui")}/signout" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "graph-api"
                    }
                }
            };
    }
}
