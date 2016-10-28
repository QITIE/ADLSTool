// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.Compute.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Microsoft.Rest.Azure;

    /// <summary>
    /// The API entity reference.
    /// </summary>
    public partial class ApiEntityReference
    {
        /// <summary>
        /// Initializes a new instance of the ApiEntityReference class.
        /// </summary>
        public ApiEntityReference() { }

        /// <summary>
        /// Initializes a new instance of the ApiEntityReference class.
        /// </summary>
        public ApiEntityReference(string id = default(string))
        {
            Id = id;
        }

        /// <summary>
        /// the ARM resource id in the form of
        /// /subscriptions/{SubcriptionId}/resourceGroups/{ResourceGroupName}/...
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

    }
}
