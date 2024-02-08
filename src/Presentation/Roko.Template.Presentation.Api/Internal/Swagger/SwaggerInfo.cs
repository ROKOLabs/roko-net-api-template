namespace Roko.Template.Presentation.Api.Internal.Swagger
{
    public class SwaggerInfo
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? DeprecationDescription { get; set; }

        public ContactInfo? Contact { get; set; }

        public LicenceInfo? Licence { get; set; }

        public class ContactInfo
        {
            public string Name { get; set; } = default!;

            public string Email { get; set; } = default!;

            public string Url { get; set; } = default!;
        }

        public class LicenceInfo
        {
            public string Name { get; set; } = default!;

            public string Url { get; set; } = default!;
        }
    }
}
