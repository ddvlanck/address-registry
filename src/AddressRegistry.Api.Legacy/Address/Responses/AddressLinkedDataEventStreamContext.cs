namespace AddressRegistry.Api.Legacy.Address.Responses
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    [DataContract(Name = "AddressContext", Namespace = "")]
    public class AddressLinkedDataEventStreamContext
    {
        [DataMember(Name = "tree")]
        public readonly Uri HypermediaSpecificationUri = new Uri("https://w3id.org/tree#");

        [DataMember(Name = "skos")]
        public readonly Uri CodelistsUri = new Uri("http://www.w3.org/2004/02/skos/core#");

        [DataMember(Name = "xsd")]
        public readonly Uri XmlSchemaUri = new Uri("http://www.w3.org/2001/XMLSchema#");

        [DataMember(Name = "prov")]
        public readonly Uri ProvenanceUri = new Uri("http://www.w3.org/ns/prov#");

        [DataMember(Name = "dct")]
        public readonly Uri MetadataTermsUri = new Uri("http://purl.org/dc/terms/");

        [DataMember(Name = "locn")]
        public readonly Uri LocationCoreVocabulary = new Uri("http://w3.org/ns/locn#");

        [DataMember(Name = "adms")]
        public readonly Uri AssetDescription = new Uri("http://www.w3.org/ns/adms#");

        [DataMember(Name = "adres")]
        public readonly Uri OsloAddressUri = new Uri("https://data.vlaanderen.be/ns/adres#");

        [DataMember(Name = "generiek")]
        public readonly Uri OsloGenericUri = new Uri("https://data.vlaanderen.be/ns/generiek#");

        [DataMember(Name = "br")]
        public readonly Uri BaseRegistryImplementationModelUri = new Uri("https://basisregisters.vlaanderen.be/ns/adres#");

        [DataMember(Name = "items")]
        public readonly string ItemsDefinitionUri = "tree:member";

        [DataMember(Name = "viewOf")]
        public readonly TreeCollectionContext ViewOf = new TreeCollectionContext();

        [DataMember(Name = "generatedAtTime")]
        public readonly ProvenanceContext Provenance = new ProvenanceContext();

        [DataMember(Name = "eventName")]
        public readonly string EventNameUri = "adms:versionNotes";

        [DataMember(Name = "isVersionOf")]
        public readonly ParentInformationContext IsVersionOf = new ParentInformationContext();

        [DataMember(Name = "tree:node")]
        public readonly PropertyOverride TreePath = new PropertyOverride
        {
            Type = "@id"
        };

        [DataMember(Name = "tree:shape")]
        public readonly PropertyOverride TreeShape = new PropertyOverride
        {
            Type = "@id"
        };

        [DataMember(Name = "Adres")]
        [JsonProperty(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
        public readonly string Type = "adres:Adres";

        [DataMember(Name = "busnummer")]
        public readonly string BoxNumber = "adres:busnummer";

        [DataMember(Name = "heeftGemeentenaam")]
        public readonly string MunicipalityName = "adres:heeftGemeentenaam";

        [DataMember(Name = "heeftPostinfo")]
        public readonly PropertyOverride AddressPostalInformation = new PropertyOverride
        {
            Id = "adres:heeftPostinfo",
            Type = "@id"
        };

        [DataMember(Name = "heeftStraatnaam")]
        public readonly PropertyOverride StreetName = new PropertyOverride
        {
            Id = "adres:heeftStraatnaam",
            Type = "@id"
        };

        [DataMember(Name = "huisnummer")]
        public readonly string HouseNumber = "adres:huisnummer";

        [DataMember(Name = "isToegekendDoor")]
        public readonly PropertyOverride AssignedByMunicipality = new PropertyOverride
        {
            Id = "prov:wasAttributedTo",
            Type = "@id"
        };

        [DataMember(Name = "officieelToegekend")]
        public readonly string OfficiallyAssigned = "adres:officieelToegekend";

        [DataMember(Name = "positie")]
        public readonly string AddressPosition = "adres:positie";

        [DataMember(Name = "GeografischePositie")]
        [JsonProperty(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
        public readonly string PositionType = "generiek:GeografischePositie";

        [DataMember(Name = "Punt")]
        [JsonProperty(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
        public readonly string GeometryType = "generiek:Punt";

        [DataMember(Name = "geometrie")]
        public readonly PropertyOverride Geometry = new PropertyOverride
        {
            Id = "locn:geometry",
            Type = "Punt"
        };

        [DataMember(Name = "methode")]
        public readonly PropertyOverride PositionMethod = new PropertyOverride
        {
            Id = "generiek:methode",
            Type = "skos:Concept"
        };

        [DataMember(Name = "specificatie")]
        public readonly PropertyOverride PositionSpecification = new PropertyOverride
        {
            Id = "generiek:specificatie",
            Type = "skos:Concept"
        };

        [DataMember(Name = "status")]
        public readonly string AddressStatus = "adres:Adres.status";
    }

    public class TreeCollectionContext
    {
        [JsonProperty("@reverse")]
        public readonly string ReverseRelation = "tree:view";

        [JsonProperty("@type")]
        public readonly string Type = "@id";
    }

    public class ProvenanceContext
    {
        [JsonProperty("@id")]
        public readonly Uri Id = new Uri("prov:generatedAtTime");

        [JsonProperty("@type")]
        public readonly string Type = "xsd:dateTime";
    }


    public class ParentInformationContext
    {
        [JsonProperty("@id")]
        public readonly Uri Id = new Uri("dct:isVersionOf");

        [JsonProperty("@type")]
        public readonly string Type = "@id";
    }

    public class PropertyOverride
    {
        [JsonProperty("@id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("@type")]
        public string Type { get; set; }
    }

    public class AddressShaclContext
    {
        [JsonProperty("sh")]
        public readonly Uri ShaclUri = new Uri("https://www.w3.org/ns/shacl#");

        [JsonProperty("rdf")]
        public readonly Uri RdfUri = new Uri("http://www.w3.org/1999/02/22-rdf-syntax-ns#");

        [JsonProperty("xsd")]
        public readonly Uri XmlSchemaUri = new Uri("http://www.w3.org/2001/XMLSchema#");

        [JsonProperty("skos")]
        public readonly Uri CodelistUri = new Uri("http://www.w3.org/2004/02/skos/core#");

        [JsonProperty("prov")]
        public readonly Uri ProvenanceUri = new Uri("http://www.w3.org/ns/prov#");

        [JsonProperty("dct")]
        public readonly Uri MetadataTermsUri = new Uri("http://purl.org/dc/terms/");

        [JsonProperty("adms")]
        public readonly Uri AssetDescription = new Uri("http://www.w3.org/ns/adms#");

        [JsonProperty("adres")]
        public readonly Uri OsloAddressUri = new Uri("https://data.vlaanderen.be/ns/adres#");

        [JsonProperty("generiek")]
        public readonly Uri OsloGenericUri = new Uri("https://data.vlaanderen.be/ns/generiek#");

        [JsonProperty("sf")]
        public readonly Uri OpenGis = new Uri("http://opengis.net/ont/sf#");

        [JsonProperty("sh:datatype")]
        public readonly ShaclPropertyExtension DataTypeExtended = new ShaclPropertyExtension();

        [JsonProperty("sh:nodeKind")]
        public readonly ShaclPropertyExtension NodeKindExtended = new ShaclPropertyExtension();

        [JsonProperty("sh:path")]
        public readonly ShaclPropertyExtension PathExtended = new ShaclPropertyExtension();
    }

    public class ShaclPropertyExtension
    {
        [JsonProperty("@type")]
        private readonly string Type = "@id";
    }

}
