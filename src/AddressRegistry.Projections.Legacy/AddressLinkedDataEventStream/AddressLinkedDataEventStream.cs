namespace AddressRegistry.Projections.Legacy.AddressLinkedDataEventStream
{
    using AddressRegistry.Infrastructure;
    using Be.Vlaanderen.Basisregisters.ProjectionHandling.Runner.MigrationExtensions;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Newtonsoft.Json;
    using NodaTime;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public class AddressLinkedDataEventStreamItem
    {
        public long Position { get; set; }

        public Guid? AddressId { get; set; }
        public int? PersistentLocalId { get; set; }
        public string? ChangeType { get; set; }

        public Guid? StreetNameId { get; set; }
        public string? PostalCode { get; set; }
        public string? HouseNumber { get; set; }
        public string? BoxNumber { get; set; }

        public byte[]? PointPosition { get; set; }
        public GeometryMethod? PositionMethod { get; set; }
        public GeometrySpecification? PositionSpecification { get; set; }

        public AddressStatus? Status { get; set; }
        public bool IsOfficiallyAssigned { get; set; }

        public DateTimeOffset EventGeneratedAtTimeAsDateTimeOffset { get; set; }

        public Instant EventGeneratedAtTime
        {
            get => Instant.FromDateTimeOffset(EventGeneratedAtTimeAsDateTimeOffset);
            set => EventGeneratedAtTimeAsDateTimeOffset = value.ToDateTimeOffset();
        }

        public string ObjectHash { get; set; }

        public bool RecordCanBePublished { get; set; }

        public AddressLinkedDataEventStreamItem CloneAndApplyEventInfo(
            long position,
            string changeType,
            Instant eventGeneratedAtTime,
            Action<AddressLinkedDataEventStreamItem> editFunc)
        {
            var newItem = new AddressLinkedDataEventStreamItem
            {
                ChangeType = changeType,
                Position = position,
                EventGeneratedAtTime = eventGeneratedAtTime,

                AddressId = AddressId,
                PersistentLocalId = PersistentLocalId,
                StreetNameId = StreetNameId,
                PostalCode = PostalCode,
                HouseNumber = HouseNumber,
                BoxNumber = BoxNumber,
                Status = Status,
                PointPosition = PointPosition,
                PositionMethod = PositionMethod,
                PositionSpecification = PositionSpecification,
                IsOfficiallyAssigned = IsOfficiallyAssigned,
                RecordCanBePublished = RecordCanBePublished
            };

            editFunc(newItem);
            newItem.SetObjectHash();

            return newItem;
        }

        public void SetObjectHash()
        {
            ObjectHash = string.Empty;
            var objectString = JsonConvert.SerializeObject(this);

            using var md5Hash = MD5.Create();
            var hashBytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(objectString));
            ObjectHash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        }
    }

    public class AddressLinkedDataEventStreamConfiguration : IEntityTypeConfiguration<AddressLinkedDataEventStreamItem>
    {
        private const string TableName = "Address";
        public void Configure(EntityTypeBuilder<AddressLinkedDataEventStreamItem> b)
        {
            b.ToTable(TableName, Schema.LinkedDataEventStream)
                .HasKey(x => x.Position)
                .IsClustered();

            b.Property(x => x.Position).ValueGeneratedNever();
            b.HasIndex(x => x.Position).IsColumnStore($"CI_{TableName}_Position");

            b.Property(x => x.AddressId).IsRequired();
            b.Property(x => x.ChangeType);

            b.Property(x => x.StreetNameId);
            b.Property(x => x.PostalCode);
            b.Property(x => x.HouseNumber);
            b.Property(x => x.BoxNumber);

            b.Property(x => x.Status);
            b.Property(x => x.IsOfficiallyAssigned);

            b.Property(x => x.PointPosition);
            b.Property(x => x.PositionMethod);
            b.Property(x => x.PositionSpecification);

            b.Property(x => x.EventGeneratedAtTimeAsDateTimeOffset).HasColumnName("EventGeneratedAtTime");
            b.Property(x => x.ObjectHash).HasColumnName("ObjectIdentifier");
            b.Property(x => x.RecordCanBePublished);

            b.Ignore(x => x.EventGeneratedAtTime);

            b.HasIndex(x => x.AddressId);
            b.HasIndex(x => x.PersistentLocalId);
        }
    }
}
