namespace AddressRegistry.Projections.Legacy.AddressLinkedDataEventStream
{
    using Be.Vlaanderen.Basisregisters.GrAr.Provenance;
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.Connector;
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.SqlStreamStore;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public static class AddressLinkedDataEventStreamItemExtensions
    {
        public static async Task CreateNewAddressLinkedDataEventStreamItem<T>(
            this LegacyContext context,
            Guid addressId,
            Envelope<T> message,
            Action<AddressLinkedDataEventStreamItem> applyEventInfoOn,
            CancellationToken ct) where T : IHasProvenance
        {
            var addressLinkedDataEventStreamItem = await context.LatestPosition(addressId, ct);

            if (addressLinkedDataEventStreamItem == null)
                throw DatabaseItemNotFound(addressId);

            var provenance = message.Message.Provenance;

            var newAddressLinkedDataEventStreamItem = addressLinkedDataEventStreamItem.CloneAndApplyEventInfo(
                message.Position,
                message.EventName,
                provenance.Timestamp,
                applyEventInfoOn);

            newAddressLinkedDataEventStreamItem.SetObjectHash();

            await context
                .AddressLinkedDataEventStream
                .AddAsync(newAddressLinkedDataEventStreamItem, ct);
        }

        public static async Task<AddressLinkedDataEventStreamItem> LatestPosition(
            this LegacyContext context,
            Guid addressId,
            CancellationToken ct)
            => context
                   .AddressLinkedDataEventStream
                   .Local
                   .Where(x => x.AddressId == addressId)
                   .OrderByDescending(x => x.Position)
                   .FirstOrDefault()
               ?? await context
                   .AddressLinkedDataEventStream
                   .Where(x => x.AddressId == addressId)
                   .OrderByDescending(x => x.Position)
                   .FirstOrDefaultAsync(ct);

        public static ProjectionItemNotFoundException<AddressLinkedDataEventStreamProjections> DatabaseItemNotFound(Guid addressId)
            => new ProjectionItemNotFoundException<AddressLinkedDataEventStreamProjections>(addressId.ToString("D"));

        public static void SetObjectHash(this AddressLinkedDataEventStreamItem addressLinkedDataEventStreamItem)
        {
            var objectString = JsonConvert.SerializeObject(addressLinkedDataEventStreamItem);

            using var md5Hash = MD5.Create();
            var hashBytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(objectString));
            addressLinkedDataEventStreamItem.ObjectHash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        }
    }
}
