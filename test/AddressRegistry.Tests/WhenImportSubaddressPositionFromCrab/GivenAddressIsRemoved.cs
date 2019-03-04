namespace AddressRegistry.Tests.WhenImportSubaddressPositionFromCrab
{
    using Address.Commands.Crab;
    using Address.Events;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing;
    using Be.Vlaanderen.Basisregisters.Crab;
    using AutoFixture;
    using Xunit;
    using Xunit.Abstractions;

    public class GivenAddressIsRemoved : AddressRegistryTest
    {
        public GivenAddressIsRemoved(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Theory, DefaultDataForSubaddress]
        public void ThenAddressRemovedException(AddressId addressId,
            AddressWasRegistered addressWasRegistered,
            AddressWasRemoved addressWasRemoved,
            ImportSubaddressPositionFromCrab importSubaddressPositionFromCrab)
        {
            Assert(new Scenario()
                .Given(addressId, addressWasRegistered, addressWasRemoved)
                .When(importSubaddressPositionFromCrab)
                .Throws(new AddressRemovedException($"Cannot change removed address for address id {addressId}")));
        }

        [Theory, DefaultDataForSubaddress]
        public void ThenNoExceptionWhenModificationIsDelete(AddressId addressId,
            AddressWasRegistered addressWasRegistered,
            AddressWasRemoved addressWasRemoved,
            ImportSubaddressPositionFromCrab importSubaddressPositionFromCrab)
        {
            importSubaddressPositionFromCrab = importSubaddressPositionFromCrab
                .WithCrabModification(CrabModification.Delete);

            Assert(new Scenario()
                .Given(addressId, addressWasRegistered, addressWasRemoved)
                .When(importSubaddressPositionFromCrab)
                .Then(addressId,
                    importSubaddressPositionFromCrab.ToLegacyEvent()));
        }
    }
}
