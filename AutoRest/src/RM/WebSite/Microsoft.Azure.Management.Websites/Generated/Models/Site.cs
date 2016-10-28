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
    /// Represents a web app
    /// </summary>
    public partial class Site : Resource
    {
        /// <summary>
        /// Initializes a new instance of the Site class.
        /// </summary>
        public Site() { }

        /// <summary>
        /// Initializes a new instance of the Site class.
        /// </summary>
        public Site(string location, string id = default(string), string name = default(string), string type = default(string), IDictionary<string, string> tags = default(IDictionary<string, string>), string siteName = default(string), string state = default(string), IList<string> hostNames = default(IList<string>), string repositorySiteName = default(string), UsageState? usageState = default(UsageState?), bool? enabled = default(bool?), IList<string> enabledHostNames = default(IList<string>), SiteAvailabilityState? availabilityState = default(SiteAvailabilityState?), IList<HostNameSslState> hostNameSslStates = default(IList<HostNameSslState>), string serverFarmId = default(string), DateTime? lastModifiedTimeUtc = default(DateTime?), SiteConfig siteConfig = default(SiteConfig), IList<string> trafficManagerHostNames = default(IList<string>), bool? premiumAppDeployed = default(bool?), bool? scmSiteAlsoStopped = default(bool?), string targetSwapSlot = default(string), HostingEnvironmentProfile hostingEnvironmentProfile = default(HostingEnvironmentProfile), string microService = default(string), string gatewaySiteName = default(string), bool? clientAffinityEnabled = default(bool?), bool? clientCertEnabled = default(bool?), bool? hostNamesDisabled = default(bool?), string outboundIpAddresses = default(string), int? containerSize = default(int?), int? maxNumberOfWorkers = default(int?), CloningInfo cloningInfo = default(CloningInfo), string resourceGroup = default(string), bool? isDefaultContainer = default(bool?), string defaultHostName = default(string))
            : base(location, id, name, type, tags)
        {
            SiteName = siteName;
            State = state;
            HostNames = hostNames;
            RepositorySiteName = repositorySiteName;
            UsageState = usageState;
            Enabled = enabled;
            EnabledHostNames = enabledHostNames;
            AvailabilityState = availabilityState;
            HostNameSslStates = hostNameSslStates;
            ServerFarmId = serverFarmId;
            LastModifiedTimeUtc = lastModifiedTimeUtc;
            SiteConfig = siteConfig;
            TrafficManagerHostNames = trafficManagerHostNames;
            PremiumAppDeployed = premiumAppDeployed;
            ScmSiteAlsoStopped = scmSiteAlsoStopped;
            TargetSwapSlot = targetSwapSlot;
            HostingEnvironmentProfile = hostingEnvironmentProfile;
            MicroService = microService;
            GatewaySiteName = gatewaySiteName;
            ClientAffinityEnabled = clientAffinityEnabled;
            ClientCertEnabled = clientCertEnabled;
            HostNamesDisabled = hostNamesDisabled;
            OutboundIpAddresses = outboundIpAddresses;
            ContainerSize = containerSize;
            MaxNumberOfWorkers = maxNumberOfWorkers;
            CloningInfo = cloningInfo;
            ResourceGroup = resourceGroup;
            IsDefaultContainer = isDefaultContainer;
            DefaultHostName = defaultHostName;
        }

        /// <summary>
        /// Name of web app
        /// </summary>
        [JsonProperty(PropertyName = "properties.name")]
        public string SiteName { get; set; }

        /// <summary>
        /// State of the web app
        /// </summary>
        [JsonProperty(PropertyName = "properties.state")]
        public string State { get; private set; }

        /// <summary>
        /// Hostnames associated with web app
        /// </summary>
        [JsonProperty(PropertyName = "properties.hostNames")]
        public IList<string> HostNames { get; private set; }

        /// <summary>
        /// Name of repository site
        /// </summary>
        [JsonProperty(PropertyName = "properties.repositorySiteName")]
        public string RepositorySiteName { get; private set; }

        /// <summary>
        /// State indicating whether web app has exceeded its quota usage.
        /// Possible values for this property include: 'Normal', 'Exceeded'.
        /// </summary>
        [JsonProperty(PropertyName = "properties.usageState")]
        public UsageState? UsageState { get; private set; }

        /// <summary>
        /// True if the site is enabled; otherwise, false. Setting this  value
        /// to false disables the site (takes the site off line).
        /// </summary>
        [JsonProperty(PropertyName = "properties.enabled")]
        public bool? Enabled { get; set; }

        /// <summary>
        /// Hostnames for the web app that are enabled. Hostnames need to be
        /// assigned and enabled. If some hostnames are assigned but not
        /// enabled
        /// the app is not served on those hostnames
        /// </summary>
        [JsonProperty(PropertyName = "properties.enabledHostNames")]
        public IList<string> EnabledHostNames { get; private set; }

        /// <summary>
        /// Management information availability state for the web app.
        /// Possible values are Normal or Limited.
        /// Normal means that the site is running correctly and
        /// that management information for the site is available.
        /// Limited means that only partial management information
        /// for the site is available and that detailed site information is
        /// unavailable. Possible values for this property include: 'Normal',
        /// 'Limited', 'DisasterRecoveryMode'.
        /// </summary>
        [JsonProperty(PropertyName = "properties.availabilityState")]
        public SiteAvailabilityState? AvailabilityState { get; private set; }

        /// <summary>
        /// Hostname SSL states are  used to manage the SSL bindings for
        /// site's hostnames.
        /// </summary>
        [JsonProperty(PropertyName = "properties.hostNameSslStates")]
        public IList<HostNameSslState> HostNameSslStates { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "properties.serverFarmId")]
        public string ServerFarmId { get; set; }

        /// <summary>
        /// Last time web app was modified in UTC
        /// </summary>
        [JsonProperty(PropertyName = "properties.lastModifiedTimeUtc")]
        public DateTime? LastModifiedTimeUtc { get; private set; }

        /// <summary>
        /// Configuration of web app
        /// </summary>
        [JsonProperty(PropertyName = "properties.siteConfig")]
        public SiteConfig SiteConfig { get; set; }

        /// <summary>
        /// Read-only list of Azure Traffic manager hostnames associated with
        /// web app
        /// </summary>
        [JsonProperty(PropertyName = "properties.trafficManagerHostNames")]
        public IList<string> TrafficManagerHostNames { get; private set; }

        /// <summary>
        /// If set indicates whether web app is deployed as a premium app
        /// </summary>
        [JsonProperty(PropertyName = "properties.premiumAppDeployed")]
        public bool? PremiumAppDeployed { get; private set; }

        /// <summary>
        /// If set indicates whether to stop SCM (KUDU) site when the web app
        /// is stopped. Default is false.
        /// </summary>
        [JsonProperty(PropertyName = "properties.scmSiteAlsoStopped")]
        public bool? ScmSiteAlsoStopped { get; set; }

        /// <summary>
        /// Read-only property that specifies which slot this app will swap
        /// into
        /// </summary>
        [JsonProperty(PropertyName = "properties.targetSwapSlot")]
        public string TargetSwapSlot { get; private set; }

        /// <summary>
        /// Specification for the hosting environment (App Service
        /// Environment) to use for the web app
        /// </summary>
        [JsonProperty(PropertyName = "properties.hostingEnvironmentProfile")]
        public HostingEnvironmentProfile HostingEnvironmentProfile { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "properties.microService")]
        public string MicroService { get; set; }

        /// <summary>
        /// Name of gateway app associated with web app
        /// </summary>
        [JsonProperty(PropertyName = "properties.gatewaySiteName")]
        public string GatewaySiteName { get; set; }

        /// <summary>
        /// Specifies if the client affinity is enabled when load balancing
        /// http request for multiple instances of the web app
        /// </summary>
        [JsonProperty(PropertyName = "properties.clientAffinityEnabled")]
        public bool? ClientAffinityEnabled { get; set; }

        /// <summary>
        /// Specifies if the client certificate is enabled for the web app
        /// </summary>
        [JsonProperty(PropertyName = "properties.clientCertEnabled")]
        public bool? ClientCertEnabled { get; set; }

        /// <summary>
        /// Specifies if the public hostnames are disabled the web app.
        /// If set to true the app is only accessible via API
        /// Management process
        /// </summary>
        [JsonProperty(PropertyName = "properties.hostNamesDisabled")]
        public bool? HostNamesDisabled { get; set; }

        /// <summary>
        /// List of comma separated IP addresses that this web app uses for
        /// outbound connections. Those can be used when configuring firewall
        /// rules for databases accessed by this web app.
        /// </summary>
        [JsonProperty(PropertyName = "properties.outboundIpAddresses")]
        public string OutboundIpAddresses { get; private set; }

        /// <summary>
        /// Size of a function container
        /// </summary>
        [JsonProperty(PropertyName = "properties.containerSize")]
        public int? ContainerSize { get; set; }

        /// <summary>
        /// Maximum number of workers
        /// This only applies to function container
        /// </summary>
        [JsonProperty(PropertyName = "properties.maxNumberOfWorkers")]
        public int? MaxNumberOfWorkers { get; set; }

        /// <summary>
        /// This is only valid for web app creation. If specified, web app is
        /// cloned from
        /// a source web app
        /// </summary>
        [JsonProperty(PropertyName = "properties.cloningInfo")]
        public CloningInfo CloningInfo { get; set; }

        /// <summary>
        /// Resource group web app belongs to
        /// </summary>
        [JsonProperty(PropertyName = "properties.resourceGroup")]
        public string ResourceGroup { get; private set; }

        /// <summary>
        /// Site is a default container
        /// </summary>
        [JsonProperty(PropertyName = "properties.isDefaultContainer")]
        public bool? IsDefaultContainer { get; private set; }

        /// <summary>
        /// Default hostname of the web app
        /// </summary>
        [JsonProperty(PropertyName = "properties.defaultHostName")]
        public string DefaultHostName { get; private set; }

        /// <summary>
        /// Validate the object. Throws ValidationException if validation fails.
        /// </summary>
        public override void Validate()
        {
            base.Validate();
            if (this.HostNameSslStates != null)
            {
                foreach (var element in this.HostNameSslStates)
                {
                    if (element != null)
                    {
                        element.Validate();
                    }
                }
            }
            if (this.SiteConfig != null)
            {
                this.SiteConfig.Validate();
            }
        }
    }
}
