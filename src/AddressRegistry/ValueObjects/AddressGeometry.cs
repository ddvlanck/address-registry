namespace AddressRegistry
{
    using Be.Vlaanderen.Basisregisters.AggregateSource;
    using System.Collections.Generic;

    public class AddressGeometry : ValueObject<AddressGeometry>
    {
        public GeometryMethod GeometryMethod { get; }

        public GeometrySpecification GeometrySpecification { get; }

        public ExtendedWkbGeometry Geometry { get; }

        public AddressGeometry(
            GeometryMethod geometryMethod,
            GeometrySpecification geometrySpecification,
            ExtendedWkbGeometry geometry)
        {
            GeometryMethod = geometryMethod;
            GeometrySpecification = geometrySpecification;
            Geometry = geometry;
        }

        protected override IEnumerable<object> Reflect()
        {
            yield return GeometryMethod;
            yield return GeometrySpecification;
            yield return Geometry;
        }
    }
}
