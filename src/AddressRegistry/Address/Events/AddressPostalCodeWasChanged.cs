namespace AddressRegistry.Address.Events
{
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Be.Vlaanderen.Basisregisters.GrAr.Provenance;
    using Newtonsoft.Json;
    using System;

    [EventName("AddressPostalCodeWasChanged")]
    [EventDescription("De postcode van het adres werd gewijzigd.")]
    public class AddressPostalCodeWasChanged : IHasProvenance, ISetProvenance
    {
        public Guid AddressId { get; }
        public string PostalCode { get; }
        public ProvenanceData Provenance { get; private set; }

        public AddressPostalCodeWasChanged(
            AddressId addressId,
            PostalCode postalCode)
        {
            AddressId = addressId;
            PostalCode = postalCode;
        }

        [JsonConstructor]
        private AddressPostalCodeWasChanged(
            Guid addressId,
            string postalCode,
            ProvenanceData provenance)
            : this(
                new AddressId(addressId),
                new PostalCode(postalCode))
                => ((ISetProvenance)this).SetProvenance(provenance.ToProvenance());

        void ISetProvenance.SetProvenance(Provenance provenance) => Provenance = new ProvenanceData(provenance);
    }
}
