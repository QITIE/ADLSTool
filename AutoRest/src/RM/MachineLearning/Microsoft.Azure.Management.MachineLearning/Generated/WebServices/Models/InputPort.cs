// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.MachineLearning.WebServices.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Microsoft.Rest.Azure;

    /// <summary>
    /// Asset input port
    /// </summary>
    public partial class InputPort
    {
        /// <summary>
        /// Initializes a new instance of the InputPort class.
        /// </summary>
        public InputPort() { }

        /// <summary>
        /// Initializes a new instance of the InputPort class.
        /// </summary>
        public InputPort(string type = default(string))
        {
            Type = type;
        }

        /// <summary>
        /// Port data type. Possible values include: 'Dataset'
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

    }
}
