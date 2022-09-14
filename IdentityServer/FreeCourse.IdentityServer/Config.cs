// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource("resource_catalog") { Scopes = { "catalog_fullpermission" } },
            new ApiResource("resource_photo_stock") { Scopes = { "photo_stock_fullpermission" } },
            new ApiResource("resource_basket") { Scopes = { "basket_fullpermission" } },
            new ApiResource("resource_order") { Scopes = { "order_fullpermission" } },
            new ApiResource("resource_fake_payment") { Scopes = { "fake_payment_fullpermission" } },
            new ApiResource("resource_gateway") { Scopes = { "gateway_fullpermission" } },
            new ApiResource("resource_discount"){ Scopes = { "discount_fullpermission" } },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new[]
            {
                new IdentityResources.Email(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource
                    { Name = "roles", DisplayName = "Roles", Description = "User Roles", UserClaims = new[] { "role" } }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new[]
            {
                new ApiScope("catalog_fullpermission", "Full Access For Catalog API"),

                new ApiScope("photo_stock_fullpermission", "Full Access For Photo Stock API"),

                new ApiScope("basket_fullpermission", "Full Access For Basket API"),

                new ApiScope("order_fullpermission", "Full Access For Order API"),

                new ApiScope("fake_payment_fullpermission", "Full Access For Payment API"),

                new ApiScope("gateway_fullpermission", "Full Access For Gateway"),
                
                new ApiScope("discount_fullpermission", "Full Access For Discount API"),

                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new[]
            {
                new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClient",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        "gateway_fullpermission", "catalog_fullpermission", "photo_stock_fullpermission",
                        IdentityServerConstants.LocalApi.ScopeName
                    }
                },
                new Client
                {
                    ClientName = "Asp.Net Core MVC",
                    ClientId = "WebMvcClientForUser",
                    AllowOfflineAccess = true,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        "gateway_fullpermission", "order_fullpermission", "fake_payment_fullpermission",
                        "basket_fullpermission", "discount_fullpermission", IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.LocalApi.ScopeName, "roles"
                    },
                    AccessTokenLifetime = 1 * 60 * 60,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse
                }
            };
    }
}