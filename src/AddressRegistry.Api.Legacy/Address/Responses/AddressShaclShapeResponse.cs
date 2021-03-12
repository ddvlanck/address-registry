namespace AddressRegistry.Api.Legacy.Address.Responses
{
    using AddressRegistry.Api.Legacy.Infrastructure;
    using Newtonsoft.Json;
    using Swashbuckle.AspNetCore.Filters;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    [DataContract(Name = "AdresShaclShape", Namespace = "")]
    public class AddressShaclShapeResponse
    {
        [DataMember(Name = "@context", Order = 1)]
        public readonly AddressShaclContext Context = new AddressShaclContext();

        [DataMember(Name = "@id", Order = 2)]
        public Uri Id { get; set; }

        [DataMember(Name = "@type", Order = 3)]
        public readonly string Type = "sh:NodeShape";

        [DataMember(Name = "sh:property", Order = 4)]
        public readonly List<AddressShaclProperty> Shape = new AddressShaclShape().Properties;
    }

    public class AddressShaclShape
    {
        public readonly List<AddressShaclProperty> Properties = new List<AddressShaclProperty>()
        {
            new AddressShaclProperty
            {
                PropertyPath = "dct:isVersionOf",
                DataType = "sh:IRI",
                MinimumCount = 1,
                MaximumCount = 1
            },
            new AddressShaclProperty
            {
                PropertyPath = "prov:generatedAtTime",
                DataType = "xsd:dateTime",
                MinimumCount = 1,
                MaximumCount = 1
            },
            new AddressShaclProperty
            {
                PropertyPath = "adms:versionNotes",
                DataType = "xsd:string",
                MinimumCount = 1,
                MaximumCount = 1
            },
            new AddressShaclProperty
            {
                PropertyPath = "adres:heeftStraatnaam",
                DataType = "sh:IRI",
                MinimumCount = 1,
                MaximumCount = 1
            },
            new AddressShaclProperty
            {
                PropertyPath = "adres:heeftGemeentenaam",
                DataType = "rdf:langString",
                MinimumCount = 1
            },
            new AddressShaclProperty
            {
                PropertyPath = "adres:heeftPostinfo",
                DataType = "sh:IRI",
                MinimumCount = 1,
                MaximumCount = 1
            },
            new AddressShaclProperty
            {
                PropertyPath = "adres:huisnummer",
                DataType = "xsd:string",
                MaximumCount = 1
            },
            new AddressShaclProperty
            {
                PropertyPath = "adres:busnummer",
                DataType = "xsd:string",
                MaximumCount = 1
            },
            new AddressShaclProperty
            {
                PropertyPath = "adres:positie",
                Node = new AddressNodeProperty
                {
                    Nodes = new List<AddressShaclProperty>
                    {
                        new AddressShaclProperty
                        {
                            PropertyPath = "locn:geometry",
                            DataType = "sf:Point",
                            MinimumCount = 1,
                            MaximumCount = 1
                        },
                        new AddressShaclProperty
                        {
                            PropertyPath = "generiek:methode",
                            DataType = "skos:Concept",
                            MinimumCount = 1,
                            MaximumCount = 1
                        },
                        new AddressShaclProperty
                        {
                            PropertyPath = "generiek:specificatie",
                            DataType = "skos:Concept",
                            MinimumCount = 1,
                            MaximumCount = 1
                        }
                    }
                }
            },
            new AddressShaclProperty
            {
                PropertyPath = "adres:status",
                DataType = "skos:Concept",
                MinimumCount = 1,
                MaximumCount = 1
            },
            new AddressShaclProperty
            {
                PropertyPath = "prov:wasAttributedTo",
                DataType = "sh:IRI",
                MinimumCount= 1,
                MaximumCount = 1
            },
            new AddressShaclProperty
            {
                PropertyPath = "adres:officieelToegekend",
                DataType = "xsd:boolean",
                MinimumCount = 1,
                MaximumCount = 1
            },
        };
    }

    public class AddressShaclProperty
    {
        [JsonProperty("sh:path")]
        public string PropertyPath { get; set; }

        [JsonProperty(PropertyName = "sh:datatype", NullValueHandling = NullValueHandling.Ignore)]
        public string? DataType { get; set; }

        [JsonProperty(PropertyName = "sh:nodeKind", NullValueHandling = NullValueHandling.Ignore)]
        public string? NodeKind { get; set; }

        [JsonProperty(PropertyName = "sh:minCount", NullValueHandling = NullValueHandling.Ignore)]
        public int? MinimumCount { get; set; }

        [JsonProperty(PropertyName = "sh:maxCount", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaximumCount { get; set; }

        [JsonProperty("sh:node", NullValueHandling = NullValueHandling.Ignore)]
        public AddressNodeProperty? Node { get; set; }
    }

    public class AddressNodeProperty
    {
        [JsonProperty("sh:properties")]
        public List<AddressShaclProperty> Nodes { get; set; }
    }

    public class AddressShaclShapeResponseExamples : IExamplesProvider<AddressShaclShapeResponse>
    {
        private readonly LinkedDataEventStreamConfiguration _configuration;

        public AddressShaclShapeResponseExamples(LinkedDataEventStreamConfiguration configuration)
            => _configuration = configuration;

        public AddressShaclShapeResponse GetExamples()
            => new AddressShaclShapeResponse
            {
                Id = new Uri($"{_configuration.ApiEndpoint}/shape");
            };
    }
}
