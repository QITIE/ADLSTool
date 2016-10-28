// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator 0.14.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.ResourceManager.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Microsoft.Rest.Azure;

    /// <summary>
    /// Deployment operation information.
    /// </summary>
    public partial class DeploymentOperation
    {
        /// <summary>
        /// Initializes a new instance of the DeploymentOperation class.
        /// </summary>
        public DeploymentOperation() { }

        /// <summary>
        /// Initializes a new instance of the DeploymentOperation class.
        /// </summary>
        public DeploymentOperation(string id = default(string), string operationId = default(string), DeploymentOperationProperties properties = default(DeploymentOperationProperties))
        {
            Id = id;
            OperationId = operationId;
            Properties = properties;
        }

        /// <summary>
        /// Full deployment operation id.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Deployment operation id.
        /// </summary>
        [JsonProperty(PropertyName = "operationId")]
        public string OperationId { get; set; }

        /// <summary>
        /// Deployment properties.
        /// </summary>
        [JsonProperty(PropertyName = "properties")]
        public DeploymentOperationProperties Properties { get; set; }

    }
}
