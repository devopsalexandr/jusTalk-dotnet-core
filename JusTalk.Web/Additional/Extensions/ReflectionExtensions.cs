using System;
using System.Linq;

namespace JusTalk.Web
{
    public static class ReflectionExtensions
    {
        public static bool HasDefaultConstructor(this Type source) =>
        source.GetConstructors().Any(x => !x.GetParameters().Any() && x.IsPublic);
    }
}