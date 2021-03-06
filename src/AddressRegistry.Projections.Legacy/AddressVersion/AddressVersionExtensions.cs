namespace AddressRegistry.Projections.Legacy.AddressVersion
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Be.Vlaanderen.Basisregisters.GrAr.Provenance;
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.Connector;
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.SqlStreamStore;
    using Microsoft.EntityFrameworkCore;

    public static class AddressVersionExtensions
    {
        public static async Task CreateNewAddressVersion<T>(
            this LegacyContext context,
            Guid addressId,
            Envelope<T> message,
            Action<AddressVersion> applyEventInfoOn,
            CancellationToken ct) where T : IHasProvenance
        {
            var addressVersion = await context
                .AddressVersions
                .LatestPosition(addressId, ct);

            if (addressVersion == null)
                throw DatabaseItemNotFound(addressId);

            var provenance = message.Message.Provenance;

            var newAddressVersion = addressVersion.CloneAndApplyEventInfo(
                message.Position,
                applyEventInfoOn);

            newAddressVersion.ApplyProvenance(provenance);

            await context
                .AddressVersions
                .AddAsync(newAddressVersion, ct);
        }

        private static async Task<AddressVersion> LatestPosition(
            this DbSet<AddressVersion> addressVersions,
            Guid addressId,
            CancellationToken ct)
            => addressVersions
                   .Local
                   .Where(x => x.AddressId == addressId)
                   .OrderByDescending(x => x.StreamPosition)
                   .FirstOrDefault()
               ?? await addressVersions
                   .Where(x => x.AddressId == addressId)
                   .OrderByDescending(x => x.StreamPosition)
                   .FirstOrDefaultAsync(ct);

        private static void ApplyProvenance(
            this AddressVersion addressVersion,
            ProvenanceData provenance)
        {
            addressVersion.Organisation = provenance.Organisation;
            addressVersion.Application = provenance.Application;
            addressVersion.Reason = provenance.Reason;
            addressVersion.Modification = provenance.Modification;
            addressVersion.Operator = provenance.Operator;
            addressVersion.VersionTimestamp = provenance.Timestamp;
        }

        private static ProjectionItemNotFoundException<AddressVersionProjections> DatabaseItemNotFound(Guid addressId)
            => new ProjectionItemNotFoundException<AddressVersionProjections>(addressId.ToString("D"));
    }
}
