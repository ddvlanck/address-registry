namespace AddressRegistry.Api.Legacy.Infrastructure
{
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LinkedDataEventStreamConfiguration
    {
        public string DataVlaanderenNamespace { get; }
        public string DataVlaanderenStreetNameNamespace { get; }
        public string DataVlaanderenMunicipalityNamespace { get; }
        public string DataVlaanderenPostalInformationNamespace { get; set; }
        public string ApiEndpoint { get; }

        public LinkedDataEventStreamConfiguration(IConfigurationSection configuration)
        {
            DataVlaanderenNamespace = configuration["DataVlaanderenNamespace"];
            DataVlaanderenStreetNameNamespace = configuration["DataVlaanderenStreetNameNamespace"];
            DataVlaanderenMunicipalityNamespace = configuration["DataVlaanderenMunicipalityNamespace"];
            DataVlaanderenPostalInformationNamespace = configuration["DataVlaanderenPostalInformationNamespace"];
            ApiEndpoint = configuration["ApiEndpoint"];
        }
    }
}
