// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator 0.14.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.WebSites.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Microsoft.Rest.Azure;

    /// <summary>
    /// Usage of the quota resource
    /// </summary>
    public partial class CsmUsageQuota
    {
        /// <summary>
        /// Initializes a new instance of the CsmUsageQuota class.
        /// </summary>
        public CsmUsageQuota() { }

        /// <summary>
        /// Initializes a new instance of the CsmUsageQuota class.
        /// </summary>
        public CsmUsageQuota(string unit = default(string), DateTime? nextResetTime = default(DateTime?), long? currentValue = default(long?), long? limit = default(long?), LocalizableString name = default(LocalizableString))
        {
            Unit = unit;
            NextResetTime = nextResetTime;
            CurrentValue = currentValue;
            Limit = limit;
            Name = name;
        }

        /// <summary>
        /// Units of measurement for the quota resourse
        /// </summary>
        [JsonProperty(PropertyName = "unit")]
        public string Unit { get; set; }

        /// <summary>
        /// Next reset time for the resource counter
        /// </summary>
        [JsonProperty(PropertyName = "nextResetTime")]
        public DateTime? NextResetTime { get; set; }

        /// <summary>
        /// The current value of the resource counter
        /// </summary>
        [JsonProperty(PropertyName = "currentValue")]
        public long? CurrentValue { get; set; }

        /// <summary>
        /// The resource limit
        /// </summary>
        [JsonProperty(PropertyName = "limit")]
        public long? Limit { get; set; }

        /// <summary>
        /// Quota name
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public LocalizableString Name { get; set; }

    }
}
