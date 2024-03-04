namespace Roko.Template.Blocks.Common.Exceptions
{
    using System.Collections.Generic;

    public class ServiceAuthorizationException : ServiceValidationException
    {
        public ServiceAuthorizationException(Dictionary<string, string[]> errors) : base(errors)
        {
        }

        public ServiceAuthorizationException(string error) : base(error)
        {
        }
    }
}
