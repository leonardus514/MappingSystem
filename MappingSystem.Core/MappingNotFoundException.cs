using System;

namespace MappingSystem.Core
{
    public sealed class MappingNotFoundException : Exception
    {
        public MappingNotFoundException(string source, string target)
            : base($"No mapping registered for '{source}' → '{target}'.")
        {
        }
    }
}
