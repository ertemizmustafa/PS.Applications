using System;

namespace PS.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class EndpointServiceAttribute : Attribute
    {
        public string Name { get; set; }
        public EndpointServiceAttribute(string name) => Name = name;
    }
}
