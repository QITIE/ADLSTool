// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Management.DataLake.Analytics.Models
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Microsoft.Rest.Azure;

    /// <summary>
    /// Generic resource inner error information.
    /// </summary>
    public partial class InnerError
    {
        /// <summary>
        /// Initializes a new instance of the InnerError class.
        /// </summary>
        public InnerError() { }

        /// <summary>
        /// Initializes a new instance of the InnerError class.
        /// </summary>
        /// <param name="trace">the stack trace for the error</param>
        /// <param name="context">the context for the error message</param>
        public InnerError(string trace = default(string), string context = default(string))
        {
            Trace = trace;
            Context = context;
        }

        /// <summary>
        /// Gets the stack trace for the error
        /// </summary>
        [JsonProperty(PropertyName = "trace")]
        public string Trace { get; private set; }

        /// <summary>
        /// Gets the context for the error message
        /// </summary>
        [JsonProperty(PropertyName = "context")]
        public string Context { get; private set; }

    }
}
