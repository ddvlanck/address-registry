﻿// <auto-generated />
using System;
using AddressRegistry.Projections.Syndication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AddressRegistry.Projections.Syndication.Migrations
{
    [DbContext(typeof(SyndicationContext))]
    partial class SyndicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.Municipality.MunicipalityLatestItem", b =>
                {
                    b.Property<Guid>("MunicipalityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("NameDutch");

                    b.Property<string>("NameDutchSearch");

                    b.Property<string>("NameEnglish");

                    b.Property<string>("NameEnglishSearch");

                    b.Property<string>("NameFrench");

                    b.Property<string>("NameFrenchSearch");

                    b.Property<string>("NameGerman");

                    b.Property<string>("NameGermanSearch");

                    b.Property<string>("NisCode");

                    b.Property<long>("Position");

                    b.Property<int?>("PrimaryLanguage");

                    b.Property<DateTimeOffset?>("Version");

                    b.HasKey("MunicipalityId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("NameDutchSearch");

                    b.HasIndex("NameEnglishSearch");

                    b.HasIndex("NameFrenchSearch");

                    b.HasIndex("NameGermanSearch");

                    b.HasIndex("NisCode")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("Position");

                    b.ToTable("MunicipalityLatestSyndication","AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.Municipality.MunicipalitySyndicationItem", b =>
                {
                    b.Property<Guid>("MunicipalityId");

                    b.Property<long>("Position");

                    b.Property<string>("NameDutch");

                    b.Property<string>("NameEnglish");

                    b.Property<string>("NameFrench");

                    b.Property<string>("NameGerman");

                    b.Property<string>("NisCode");

                    b.Property<string>("OfficialLanguagesAsString")
                        .HasColumnName("OfficialLanguages");

                    b.Property<DateTimeOffset?>("Version");

                    b.HasKey("MunicipalityId", "Position")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("NisCode")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.HasIndex("Position");

                    b.HasIndex("Version");

                    b.ToTable("MunicipalitySyndication","AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.StreetName.StreetNameLatestItem", b =>
                {
                    b.Property<Guid>("StreetNameId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HomonymAdditionDutch");

                    b.Property<string>("HomonymAdditionEnglish");

                    b.Property<string>("HomonymAdditionFrench");

                    b.Property<string>("HomonymAdditionGerman");

                    b.Property<bool>("IsComplete");

                    b.Property<string>("NameDutch");

                    b.Property<string>("NameEnglish");

                    b.Property<string>("NameFrench");

                    b.Property<string>("NameGerman");

                    b.Property<string>("NisCode");

                    b.Property<string>("OsloId");

                    b.Property<long>("Position");

                    b.Property<DateTimeOffset?>("Version");

                    b.HasKey("StreetNameId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("HomonymAdditionDutch");

                    b.HasIndex("HomonymAdditionEnglish");

                    b.HasIndex("HomonymAdditionFrench");

                    b.HasIndex("HomonymAdditionGerman");

                    b.HasIndex("IsComplete");

                    b.HasIndex("NameDutch");

                    b.HasIndex("NameEnglish");

                    b.HasIndex("NameFrench");

                    b.HasIndex("NameGerman");

                    b.HasIndex("NisCode");

                    b.ToTable("StreetNameLatestSyndication","AddressRegistrySyndication");
                });

            modelBuilder.Entity("AddressRegistry.Projections.Syndication.StreetName.StreetNameSyndicationItem", b =>
                {
                    b.Property<Guid>("StreetNameId");

                    b.Property<long>("Position");

                    b.Property<string>("HomonymAdditionDutch");

                    b.Property<string>("HomonymAdditionEnglish");

                    b.Property<string>("HomonymAdditionFrench");

                    b.Property<string>("HomonymAdditionGerman");

                    b.Property<string>("NameDutch");

                    b.Property<string>("NameEnglish");

                    b.Property<string>("NameFrench");

                    b.Property<string>("NameGerman");

                    b.Property<string>("NisCode");

                    b.Property<string>("OsloId");

                    b.Property<DateTimeOffset?>("Version");

                    b.HasKey("StreetNameId", "Position")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("HomonymAdditionDutch");

                    b.HasIndex("HomonymAdditionEnglish");

                    b.HasIndex("HomonymAdditionFrench");

                    b.HasIndex("HomonymAdditionGerman");

                    b.HasIndex("NameDutch");

                    b.HasIndex("NameEnglish");

                    b.HasIndex("NameFrench");

                    b.HasIndex("NameGerman");

                    b.ToTable("StreetNameSyndication","AddressRegistrySyndication");
                });

            modelBuilder.Entity("Be.Vlaanderen.Basisregisters.ProjectionHandling.Runner.ProjectionStates.ProjectionStateItem", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Position");

                    b.HasKey("Name")
                        .HasAnnotation("SqlServer:Clustered", true);

                    b.ToTable("ProjectionStates","AddressRegistrySyndication");
                });
#pragma warning restore 612, 618
        }
    }
}
