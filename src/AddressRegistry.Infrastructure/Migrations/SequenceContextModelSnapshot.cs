// <auto-generated />
namespace AddressRegistry.Infrastructure.Migrations
{
    using Infrastructure;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Metadata;

    [DbContext(typeof(SequenceContext))]
    partial class SequenceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ChangeDetector.SkipDetectChanges", "true")
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);
#pragma warning restore 612, 618
        }
    }
}
