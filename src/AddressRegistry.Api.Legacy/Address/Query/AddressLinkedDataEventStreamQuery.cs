namespace AddressRegistry.Api.Legacy.Address.Query
{
    using AddressRegistry.Projections.Legacy;
    using AddressRegistry.Projections.Legacy.AddressLinkedDataEventStream;
    using Be.Vlaanderen.Basisregisters.Api.Search;
    using Be.Vlaanderen.Basisregisters.Api.Search.Filtering;
    using Be.Vlaanderen.Basisregisters.Api.Search.Sorting;
    using Microsoft.EntityFrameworkCore;
    using NodaTime;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class AddressLinkedDataEventStreamQueryResult
    {
        public int PersistentLocalId { get; set; }
        public string ChangeType { get; set; }
        public Guid StreetNameId { get; set; }
        public string PostalCode { get; set; }
        public string? HouseNumber { get; set; }
        public string? BoxNumber { get; set; }

        public byte[] PointPosition { get; set; }
        public GeometryMethod PositionMethod { get; set; }
        public GeometrySpecification PositionSpecification { get; set; }

        public AddressStatus Status { get; set; }
        public bool IsOfficiallyAssigned { get; set; }

        public Instant EventGeneratedAtTime { get; set; }
        public string ObjectIdentifier { get; set; }

        public AddressLinkedDataEventStreamQueryResult(
            int persistentLocalId,
            string objectIdentifier,
            Instant generatedAtTime,
            string changeType,
            Guid streetNameId,
            string postalCode,
            string? houseNumber,
            string? boxNumber,
            byte[] pointPosition,
            GeometryMethod positionMethod,
            GeometrySpecification positionSpecification,
            AddressStatus status,
            bool isOfficiallyAssigned)
        {
            PersistentLocalId = persistentLocalId;
            ObjectIdentifier = objectIdentifier;

            EventGeneratedAtTime = generatedAtTime;
            ChangeType = changeType;

            StreetNameId = streetNameId;
            PostalCode = postalCode;
            HouseNumber = houseNumber;
            BoxNumber = boxNumber;
            PointPosition = pointPosition;
            PositionMethod = positionMethod;
            PositionSpecification = positionSpecification;

            Status = status;
            IsOfficiallyAssigned = isOfficiallyAssigned;
        }
    }

    public class AddressLinkedDataEventStreamQuery : Query<AddressLinkedDataEventStreamItem, AddressLinkedDataEventStreamFilter, AddressLinkedDataEventStreamQueryResult>
    {
        private readonly LegacyContext _context;

        public AddressLinkedDataEventStreamQuery(LegacyContext context)
            => _context = context;

        protected override ISorting Sorting => new AddressLinkedDataEventStreamSorting();

        protected override Expression<Func<AddressLinkedDataEventStreamItem, AddressLinkedDataEventStreamQueryResult>> Transformation
        {
            get
            {
                return addressLinkedDataEventStreamItem => new AddressLinkedDataEventStreamQueryResult(
                    (int)addressLinkedDataEventStreamItem.PersistentLocalId,
                    addressLinkedDataEventStreamItem.ObjectHash,
                    addressLinkedDataEventStreamItem.EventGeneratedAtTime,
                    addressLinkedDataEventStreamItem.ChangeType,
                    (Guid)addressLinkedDataEventStreamItem.StreetNameId,
                    addressLinkedDataEventStreamItem.PostalCode,
                    addressLinkedDataEventStreamItem.HouseNumber,
                    addressLinkedDataEventStreamItem.BoxNumber,
                    addressLinkedDataEventStreamItem.PointPosition,
                    (GeometryMethod)addressLinkedDataEventStreamItem.PositionMethod,
                    (GeometrySpecification)addressLinkedDataEventStreamItem.PositionSpecification,
                    (AddressStatus)addressLinkedDataEventStreamItem.Status,
                    addressLinkedDataEventStreamItem.IsOfficiallyAssigned);
            }
        }

        protected override IQueryable<AddressLinkedDataEventStreamItem> Filter(FilteringHeader<AddressLinkedDataEventStreamFilter> filtering)
            => _context
                 .AddressLinkedDataEventStream
                 .Where(x => x.RecordCanBePublished == true)
                 .OrderBy(x => x.Position)
                 .AsNoTracking();
    }

    internal class AddressLinkedDataEventStreamSorting : ISorting
    {
        public IEnumerable<string> SortableFields { get; } = new[]
        {
            nameof(AddressLinkedDataEventStreamItem.Position)
        };

        public SortingHeader DefaultSortingHeader { get; } = new SortingHeader(nameof(AddressLinkedDataEventStreamItem.Position), SortOrder.Ascending);
    }

    public class AddressLinkedDataEventStreamFilter
    {
        public int PageNumebr { get; set; }
    }
}
