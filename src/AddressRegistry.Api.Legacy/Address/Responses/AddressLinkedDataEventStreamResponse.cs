namespace AddressRegistry.Api.Legacy.Address.Responses
{
    using AddressRegistry.Api.Legacy.Infrastructure;
    using AddressRegistry.Projections.Syndication.Municipality;
    using Be.Vlaanderen.Basisregisters.GrAr.Common;
    using Be.Vlaanderen.Basisregisters.GrAr.Legacy;
    using Be.Vlaanderen.Basisregisters.GrAr.Legacy.SpatialTools;
    using Newtonsoft.Json;
    using NodaTime;
    using Swashbuckle.AspNetCore.Filters;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Threading.Tasks;

    [DataContract(Name = "AdresLinkedDataEventStream", Namespace = "")]
    public class AddressLinkedDataEventStreamResponse
    {
        [DataMember(Name = "@context", Order = 1)]
        public AddressLinkedDataEventStreamContext Context { get; set; }

        [DataMember(Name = "@id", Order = 2)]
        public Uri Id { get; set; }

        [DataMember(Name = "@type", Order = 3)]
        public readonly string Type = "tree:Node";

        [DataMember(Name = "viewOf", Order = 4)]
        public Uri CollectionLink { get; set; }

        [DataMember(Name = "tree:shape", Order = 5)]
        public Uri AddressShape { get; set; }

        [DataMember(Name = "tree:relation", Order = 6)]
        [JsonProperty(Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        public List<HypermediaControl>? HypermediaControls { get; set; }

        [DataMember(Name = "items", Order =  7)]
        public List<AddressVersionObject> Items { get; set; }
    }

    [DataContract(Name = "AdresVersieObject", Namespace = "")]
    public class AddressVersionObject
    {
        [DataMember(Name = "@id", Order = 1)]
        [JsonProperty(Required = Required.Always)]
        public Uri Id { get; set; }

        [DataMember(Name = "@type", Order = 2)]
        [JsonProperty(Required = Required.Always)]
        public readonly string Type = "Adres";

        [DataMember(Name = "isVersionOf", Order = 3)]
        [JsonProperty(Required = Required.Always)]
        public Uri IsVersionOf { get; set; }

        [DataMember(Name = "generatedAtTime", Order = 4)]
        [JsonProperty(Required = Required.Always)]
        public DateTimeOffset EventGeneratedAtTime { get; set; }

        [DataMember(Name = "eventName", Order = 5)]
        [JsonProperty(Required = Required.Always)]
        public string ChangeType { get; set; }

        [DataMember(Name = "heeftStraatnaam", Order = 6)]
        [JsonProperty(Required = Required.Always)]
        public Uri StreetName{ get; set; }

        [DataMember(Name = "heeftGemeentenaam", Order = 7)]
        [JsonProperty(Required = Required.Always)]
        public List<LanguageValue> MunicipalityNames { get; set; }

        [DataMember(Name = "heeftPostinfo", Order = 8)]
        [JsonProperty(Required = Required.Always)]
        public Uri AddressPostalInformation { get; set; }

        [DataMember(Name = "huisnummer", Order = 9)]
        [JsonProperty(Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? HouseNumber { get; set; }

        [DataMember(Name = "busnummer", Order = 10)]
        [JsonProperty(Required = Required.AllowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? BoxNumber { get; set; }

        [DataMember(Name = "positie", Order = 11)]
        [JsonProperty(Required = Required.Always)]
        public GeographicalPosition Position { get; set; }

        [DataMember(Name = "status", Order = 12)]
        [JsonProperty(Required = Required.Always)]
        public Uri Status { get; set; }

        [DataMember(Name = "isToegekendDoor", Order = 13)]
        [JsonProperty(Required = Required.Always)]
        public Uri AssignedByMunicipality { get; set; }

        [DataMember(Name = "officieelToegekend", Order = 14)]
        [JsonProperty(Required = Required.Always)]
        public bool IsOfficiallyAssigned { get; set; }

        [IgnoreDataMember]
        public LinkedDataEventStreamConfiguration _configuration { get; set; }

        public AddressVersionObject(
            LinkedDataEventStreamConfiguration configuration,
            string objectIdentifier,
            string changeType,
            Instant generatedAtTime,
            int persistentLocalId,
            string postalCode,
            string houseNumber,
            string boxNumber,
            string pointCoordinates,
            GeometryMethod positionMethod,
            GeometrySpecification positionSpecification,
            AddressStatus status,
            bool isOfficiallyAssigned,
            string streetNamePersistentLocalId,
            MunicipalityLatestItem municipality)
        {
            _configuration = configuration;
            ChangeType = changeType;
            EventGeneratedAtTime = generatedAtTime.ToBelgianDateTimeOffset();

            HouseNumber = houseNumber;
            BoxNumber = boxNumber;
            IsOfficiallyAssigned = isOfficiallyAssigned;

            Id = CreateVersionUri(objectIdentifier);
            IsVersionOf = GetPersistentUri(persistentLocalId);
            Status = GetAddressStatus(status);
            StreetName = GetStreetNameUri(streetNamePersistentLocalId);
            MunicipalityNames = GetMunicipalityNames(municipality);
            AssignedByMunicipality = GetMunicipalityUri(municipality.NisCode);
            AddressPostalInformation = GetPostalInformationUri(postalCode);

            Position = new GeographicalPosition
            {
                Geometry = new PointType
                {
                    Value = pointCoordinates
                },
                PositionMethod = GetPositionMethod(positionMethod),
                PositionSpecification = GetPositionSpecification(positionSpecification)
            };
        }

        private Uri CreateVersionUri(string objectIdentifier)
            => new Uri($"{_configuration.ApiEndpoint}#{objectIdentifier}");

        private Uri GetPersistentUri(int id)
            => new Uri($"{_configuration.DataVlaanderenNamespace}/{id}");

        private Uri GetStreetNameUri(string id)
            => new Uri($"{_configuration.DataVlaanderenStreetNameNamespace}/{id}");

        private Uri GetMunicipalityUri(string id)
            => new Uri($"{_configuration.DataVlaanderenMunicipalityNamespace}/{id}");

        private Uri GetPostalInformationUri(string id)
            => new Uri($"{_configuration.DataVlaanderenPostalInformationNamespace}/{id}");

        private List<LanguageValue> GetMunicipalityNames(MunicipalityLatestItem municipality)
        {
            List<LanguageValue> municipalityNames = new List<LanguageValue>();

            if (!string.IsNullOrEmpty(municipality.NameDutch))
                municipalityNames.Add(new LanguageValue
                {
                    Value = municipality.NameDutch,
                    Language = "nl"
                });

            if (!string.IsNullOrEmpty(municipality.NameEnglish))
                municipalityNames.Add(new LanguageValue
                {
                    Value = municipality.NameEnglish,
                    Language = "en"
                });

            if (!string.IsNullOrEmpty(municipality.NameFrench))
                municipalityNames.Add(new LanguageValue
                {
                    Value = municipality.NameFrench,
                    Language = "fr"
                });

            if (!string.IsNullOrEmpty(municipality.NameGerman))
                municipalityNames.Add(new LanguageValue
                {
                    Value = municipality.NameGerman,
                    Language = "de"
                });

            return municipalityNames;
        }

        private Uri GetAddressStatus(AddressStatus status)
        {
            switch (status)
            {
                case AddressStatus.Current:
                    return new Uri("https://data.vlaanderen.be/id/concept/adresstatus/inGebruik");

                case AddressStatus.Proposed:
                    return new Uri("https://data.vlaanderen.be/id/concept/adresstatus/voorgesteld");

                case AddressStatus.Retired:
                    return new Uri("https://data.vlaanderen.be/id/concept/adresstatus/gehistoreerd");

                case AddressStatus.Unknown:
                default:
                    throw new Exception("Address should have a status");
            }
        }

        private Uri GetPositionMethod(GeometryMethod positionMethod)
        {
            switch (positionMethod)
            {
                case GeometryMethod.AppointedByAdministrator:
                    return new Uri("https://data.vlaanderen.be/id/conceptscheme/geometriemethode/aangeduidDoorBeheerder");

                case GeometryMethod.DerivedFromObject:
                    return new Uri("https://data.vlaanderen.be/id/conceptscheme/geometriemethode/afgeleidVanObject");

                case GeometryMethod.Interpolated:
                    return new Uri("https://data.vlaanderen.be/id/conceptscheme/geometriemethode/geinterpoleerd");

                default:
                    throw new Exception("Address should have a GeometryMethod");
            }
        }

        private Uri GetPositionSpecification(GeometrySpecification positionSpecification)
        {
            switch (positionSpecification)
            {
                case GeometrySpecification.Berth:
                    return new Uri("https://data.vlaanderen.be/id/conceptscheme/geometriespecificatie/ligplaats");

                case GeometrySpecification.BuildingUnit:
                    return new Uri("https://data.vlaanderen.be/id/conceptscheme/geometriespecificatie/gebouweenheid");

                case GeometrySpecification.Entry:
                    return new Uri("https://data.vlaanderen.be/id/conceptscheme/geometriespecificatie/ingang");

                case GeometrySpecification.Lot:
                    return new Uri("https://data.vlaanderen.be/id/conceptscheme/geometriespecificatie/lot");

                case GeometrySpecification.Municipality:
                    return new Uri("https://data.vlaanderen.be/id/conceptscheme/geometriespecificatie/gemeente");

                case GeometrySpecification.Parcel:
                    return new Uri("https://data.vlaanderen.be/id/conceptscheme/geometriespecificatie/perceel");

                case GeometrySpecification.RoadSegment:
                    return new Uri("https://data.vlaanderen.be/id/conceptscheme/geometriespecificatie/wegsegment");

                case GeometrySpecification.Stand:
                    return new Uri("https://data.vlaanderen.be/id/conceptscheme/geometriespecificatie/standplaats");

                case GeometrySpecification.Building:
                case GeometrySpecification.Street:
                default:
                    throw new Exception("Address should have a GeometrySpecification");
            }
        }
    }

    public class GeographicalPosition
    {
        [JsonProperty("@type")]
        public readonly string Type = "GeografischePositie";

        [JsonProperty("geometrie")]
        public PointType Geometry { get; set; }

        [JsonProperty("methode")]
        public Uri PositionMethod { get; set; }

        [JsonProperty("specificatie")]
        public Uri PositionSpecification { get; set; }
    }

    public class PointType
    {
        [JsonProperty("@value")]
        public string Value { get; set; }

        [JsonProperty("@type")]
        public readonly string Type = "Punt";
    }

    public class LanguageValue
    {
        [JsonProperty("@value")]
        public string Value { get; set; }

        [JsonProperty("@language")]
        public string Language { get; set; }
    }

    public class AddressLinkedDataEventStreamResponseExamples : IExamplesProvider<AddressLinkedDataEventStreamResponse>
    {
        private readonly LinkedDataEventStreamConfiguration _configuration;

        public AddressLinkedDataEventStreamResponseExamples(LinkedDataEventStreamConfiguration configuration)
            => _configuration = configuration;
        public AddressLinkedDataEventStreamResponse GetExamples()
        {
            var generatedAtTime = Instant.FromDateTimeOffset(DateTimeOffset.Parse("2011-04-29T14:59:55.817+02:00"));

            var hypermediaControls = new List<HypermediaControl>()
            {
                new HypermediaControl
                {
                    Type = "tree:Relation",
                    Node = new Uri($"{_configuration.ApiEndpoint}?page=2")
                }
            };

            var municipality = new MunicipalityLatestItem
            {
                MunicipalityId = new Guid("293E4885-9664-5CE0-9D20-09984D9A0931"),
                NameDutch = "Beernem",
                Position = 215
            };

            var versionObjects = new List<AddressVersionObject>()
            {
                new AddressVersionObject(
                    _configuration,
                    "AB3306457C32108A8AC05115F66AFCA7",
                    "AddressWasPositioned",
                    generatedAtTime,
                    1305718,
                    "8730",
                    "16",
                    null,
                    "77612.02 203444.06",
                    GeometryMethod.DerivedFromObject,
                    GeometrySpecification.Parcel,
                    AddressStatus.Current,
                    true,
                    "44573",
                    municipality)
            };

            return new AddressLinkedDataEventStreamResponse
            {
                Context = new AddressLinkedDataEventStreamContext(),
                Id = new Uri($"{_configuration.ApiEndpoint}?page=1"),
                CollectionLink = new Uri($"{_configuration.ApiEndpoint}"),
                AddressShape = new Uri($"{_configuration.ApiEndpoint}/shape"),
                HypermediaControls = hypermediaControls,
                Items = versionObjects
            };
        }
    }
}
