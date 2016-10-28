// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator 0.14.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.WebSites.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines values for UnauthenticatedClientAction.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UnauthenticatedClientAction
    {
        [EnumMember(Value = "RedirectToLoginPage")]
        RedirectToLoginPage,
        [EnumMember(Value = "AllowAnonymous")]
        AllowAnonymous
    }
}
