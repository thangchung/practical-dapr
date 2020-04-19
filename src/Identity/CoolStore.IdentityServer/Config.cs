// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;
using Microsoft.Extensions.Configuration;
using N8T.Infrastructure.Tye;

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


        public static IEnumerable<Client> Clients(IConfiguration config)
        {
            var webUiUrl = config.GetTyeAppUrl("webui");
            if (config.GetValue<bool>("IsDev"))
            {
                webUiUrl = config.GetValue<string>("WebUIUrl");
            }

            return new[]
            {
                new Client
                {
                    ClientId = "webui",
                    ClientSecrets = {new Secret("mysecret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,
                    RequirePkce = true,
                    AllowOfflineAccess = true,

                    // where to redirect to after login
                    RedirectUris = {$"{webUiUrl}/signin-oidc"},

                    // where to redirect to after logout
                    PostLogoutRedirectUris = {$"{webUiUrl}/signout"},
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
}
