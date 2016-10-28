// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
// 
// Code generated by Microsoft (R) AutoRest Code Generator 0.17.0.0
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Azure.Graph.RBAC.Models
{
    using Microsoft.Rest;
    using System;
    using System.Net.Http;
    using System.Runtime.Serialization;
#if !PORTABLE 
    using System.Security.Permissions;
#endif

    /// <summary>
    /// Exception thrown for an invalid response with GraphError information.
    /// </summary>
#if !PORTABLE 
    [Serializable]
#endif
    public class GraphErrorException : RestException
    {
        /// <summary>
        /// Gets information about the associated HTTP request.
        /// </summary>
        public HttpRequestMessageWrapper Request { get; set; }

        /// <summary>
        /// Gets information about the associated HTTP response.
        /// </summary>
        public HttpResponseMessageWrapper Response { get; set; }

        /// <summary>
        /// Gets or sets the body object.
        /// </summary>
        public GraphError Body { get; set; }

        /// <summary>
        /// Initializes a new instance of the GraphErrorException class.
        /// </summary>
        public GraphErrorException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the GraphErrorException class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public GraphErrorException(string message)
            : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the GraphErrorException class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">Inner exception.</param>
        public GraphErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

#if !PORTABLE 
        /// <summary>
        /// Initializes a new instance of the GraphErrorException class.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected GraphErrorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Serializes content of the exception.
        /// </summary>
        /// <param name="info">Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("Request", Request);
            info.AddValue("Response", Response);
            info.AddValue("Body", Body);
        }
#endif
    }
}
