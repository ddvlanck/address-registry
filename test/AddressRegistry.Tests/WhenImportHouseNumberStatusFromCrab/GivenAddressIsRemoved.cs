namespace AddressRegistry.Tests.WhenImportHouseNumberStatusFromCrab
{
    using Address.Commands.Crab;
    using Address.Events;
    using Be.Vlaanderen.Basisregisters.AggregateSource.Testing;
    using AutoFixture;
    using Xunit;
    using Xunit.Abstractions;

    public class GivenAddressIsRemoved : AddressRegistryTest
    {
        public GivenAddressIsRemoved(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Theory]
        [DefaultData]
        public void ThenAddressRemovedException(
            AddressId addressId,
            AddressWasRegistered addressWasRegistered,
            AddressWasRemoved addressWasRemoved,
            ImportHouseNumberStatusFromCrab importHouseNumberStatusFromCrab)
        {
            Assert(new Scenario()
                .Given(addressId,
                    addressWasRegistered,
                    addressWasRemoved
                )
                .When(importHouseNumberStatusFromCrab)
                .Throws(new AddressRemovedException($"Cannot change removed address for address id {addressId}")));
        }
    }
}
