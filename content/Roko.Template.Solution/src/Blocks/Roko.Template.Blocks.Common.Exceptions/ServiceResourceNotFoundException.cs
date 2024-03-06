namespace Roko.Template.Blocks.Common.Exceptions
{
    using System;

    public class ServiceResourceNotFoundException(Type resource, Guid id) :
        ApplicationException($"Resource {id} of type {resource.Name} not found");
}