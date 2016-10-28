// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator 0.16.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.Network.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Microsoft.Rest.Azure;

    /// <summary>
    /// The routes table associated with the ExpressRouteCircuit
    /// </summary>
    public partial class ExpressRouteCircuitRoutesTableSummary
    {
        /// <summary>
        /// Initializes a new instance of the
        /// ExpressRouteCircuitRoutesTableSummary class.
        /// </summary>
        public ExpressRouteCircuitRoutesTableSummary() { }

        /// <summary>
        /// Initializes a new instance of the
        /// ExpressRouteCircuitRoutesTableSummary class.
        /// </summary>
        public ExpressRouteCircuitRoutesTableSummary(string neighbor = default(string), int? v = default(int?), int? asProperty = default(int?), string upDown = default(string), string statePfxRcd = default(string))
        {
            Neighbor = neighbor;
            V = v;
            AsProperty = asProperty;
            UpDown = upDown;
            StatePfxRcd = statePfxRcd;
        }

        /// <summary>
        /// Neighbor.
        /// </summary>
        [JsonProperty(PropertyName = "neighbor")]
        public string Neighbor { get; set; }

        /// <summary>
        /// BGP version number spoken to the neighbor.
        /// </summary>
        [JsonProperty(PropertyName = "v")]
        public int? V { get; set; }

        /// <summary>
        /// Autonomous system number.
        /// </summary>
        [JsonProperty(PropertyName = "as")]
        public int? AsProperty { get; set; }

        /// <summary>
        /// The length of time that the BGP session has been in the
        /// Established state, or the current status if not in the
        /// Established state.
        /// </summary>
        [JsonProperty(PropertyName = "upDown")]
        public string UpDown { get; set; }

        /// <summary>
        /// Current state of the BGP session, and the number of prefixes that
        /// have been received from a neighbor or peer group.
        /// </summary>
        [JsonProperty(PropertyName = "statePfxRcd")]
        public string StatePfxRcd { get; set; }

    }
}
