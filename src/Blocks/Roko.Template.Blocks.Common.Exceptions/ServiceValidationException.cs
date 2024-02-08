namespace Roko.Template.Blocks.Common.Exceptions
{
    using System;
    using System.Collections.Generic;

    public class ServiceValidationException : ApplicationException
    {
        public ServiceValidationException(Dictionary<string, string[]> errors)
        {
            this.Title = "One or more validation errors occurred.";
            this.Detail = "Check the errors for more details";
            this.Errors = errors;
        }

        public ServiceValidationException(string error)
        {
            this.Title = "One or more validation errors occurred.";
            this.Detail = "Check the errors for more details";
            this.Errors = new Dictionary<string, string[]>()
            {
                { "General", new string[] { error } }
            };
        }

        public string Title { get; }

        public string Detail { get; }

        public Dictionary<string, string[]> Errors { get; }
    }
}
