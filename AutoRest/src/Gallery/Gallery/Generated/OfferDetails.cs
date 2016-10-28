// 
// Copyright (c) Microsoft and contributors.  All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// 
// See the License for the specific language governing permissions and
// limitations under the License.
// 

// Warning: This code was generated by a tool.
// 
// Changes to this file may cause incorrect behavior and will be lost if the
// code is regenerated.

using System;
using System.Collections.Generic;
using System.Linq;
using Hyak.Common;
using Microsoft.Azure.Gallery;

namespace Microsoft.Azure.Gallery
{
    /// <summary>
    /// A gallery item offer details.
    /// </summary>
    public partial class OfferDetails
    {
        private string _offerIdentifier;
        
        /// <summary>
        /// Optional. Gets or sets offer identifier.
        /// </summary>
        public string OfferIdentifier
        {
            get { return this._offerIdentifier; }
            set { this._offerIdentifier = value; }
        }
        
        private IList<Plan> _plans;
        
        /// <summary>
        /// Optional. Gets or sets plans.
        /// </summary>
        public IList<Plan> Plans
        {
            get { return this._plans; }
            set { this._plans = value; }
        }
        
        private string _publisherIdentifier;
        
        /// <summary>
        /// Optional. Gets or sets publisher identifier.
        /// </summary>
        public string PublisherIdentifier
        {
            get { return this._publisherIdentifier; }
            set { this._publisherIdentifier = value; }
        }
        
        /// <summary>
        /// Initializes a new instance of the OfferDetails class.
        /// </summary>
        public OfferDetails()
        {
            this.Plans = new LazyList<Plan>();
        }
    }
}
