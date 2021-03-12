namespace AddressRegistry.Api.Legacy.Address.Responses
{
    using AddressRegistry.Api.Legacy.Infrastructure;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AddressLinkedDataEventStreamMetadata
    {
        public static Uri GetPageIdentifier(LinkedDataEventStreamConfiguration configuration, int page)
            => new Uri($"{configuration.ApiEndpoint}?page={page}");

        public static Uri GetCollectionLink(LinkedDataEventStreamConfiguration configuration)
            => new Uri($"{configuration.ApiEndpoint}");

        public static List<HypermediaControl>? GetHypermediaControls(
            List<AddressVersionObject> items,
            LinkedDataEventStreamConfiguration configuration,
            int page,
            int pageSize)
        {
            List<HypermediaControl> controls = new List<HypermediaControl>();

            var previous = AddPrevious(items, configuration, page);
            if (previous != null)
                controls.Add(previous);

            var next = AddNext(items, configuration, page, pageSize);
            if (next != null)
                controls.Add(next);

            return controls.Count > 0 ? controls : null;
        }

        private static HypermediaControl? AddPrevious(
           List<AddressVersionObject> items,
           LinkedDataEventStreamConfiguration configuration,
           int page)
        {
            if (page <= 1)
                return null;

            var previousUrl = new Uri($"{configuration.ApiEndpoint}?page={page - 1}");

            return new HypermediaControl
            {
                Type = "tree:Relation",
                Node = previousUrl
            };
        }

        private static HypermediaControl? AddNext(
            List<AddressVersionObject> items,
            LinkedDataEventStreamConfiguration configuration,
            int page,
            int pageSize)
        {
            if (items.Count != pageSize)
                return null;

            var nextUrl = new Uri($"{configuration.ApiEndpoint}?page={page + 1}");

            return new HypermediaControl
            {
                Type = "tree:Relation",
                Node = nextUrl
            };
        }
    }

    public class HypermediaControl
    {
        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("tree:node")]
        public Uri Node { get; set; }
    }
}


