namespace AddressRegistry.Address.Events
{
    using Be.Vlaanderen.Basisregisters.EventHandling;
    using Be.Vlaanderen.Basisregisters.GrAr.Provenance;
    using Newtonsoft.Json;
    using System;

    [EventName("AddressWasCorrectedToNotOfficiallyAssigned")]
    [EventDescription("Het adres werd niet officieel toegekend via correctie.")]
    public class AddressWasCorrectedToNotOfficiallyAssigned : IHasProvenance, ISetProvenance
    {
        public Guid AddressId { get; }
        public ProvenanceData Provenance { get; private set; }

        public AddressWasCorrectedToNotOfficiallyAssigned(AddressId addressId) => AddressId = addressId;

        [JsonConstructor]
        private AddressWasCorrectedToNotOfficiallyAssigned(Guid addressId,
            ProvenanceData provenance)
            : this(
                new AddressId(addressId))
                => ((ISetProvenance)this).SetProvenance(provenance.ToProvenance());

        void ISetProvenance.SetProvenance(Provenance provenance) => Provenance = new ProvenanceData(provenance);
    }
}
