using System;
using System.Collections.Generic;

namespace MappingSystem.Core
{
    public class MapHandler
    {
        private readonly Dictionary<(string Source, string Target), IObjectMapper> _registry;

        public MapHandler(IEnumerable<IObjectMapper> mappers)
        {
            if (mappers == null) throw new ArgumentNullException(nameof(mappers));

            _registry = new Dictionary<(string, string), IObjectMapper>();

            foreach (var mapper in mappers)
            {
                if (mapper == null) continue;

                if (string.IsNullOrWhiteSpace(mapper.SourceType))
                    throw new ArgumentException("A mapper has an empty SourceType.");

                if (string.IsNullOrWhiteSpace(mapper.TargetType))
                    throw new ArgumentException("A mapper has an empty TargetType.");

                var key = (mapper.SourceType.Trim(), mapper.TargetType.Trim());

                if (_registry.ContainsKey(key))
                    throw new ArgumentException($"Duplicate mapper registered for '{key.Item1}' -> '{key.Item2}'.");

                _registry[key] = mapper;
            }
        }

        public object Map(object data, string sourceType, string targetType)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            if (string.IsNullOrWhiteSpace(sourceType)) throw new ArgumentNullException(nameof(sourceType));
            if (string.IsNullOrWhiteSpace(targetType)) throw new ArgumentNullException(nameof(targetType));

            var key = (sourceType.Trim(), targetType.Trim());

            if (!_registry.TryGetValue(key, out var mapper))
                throw new MappingNotFoundException(key.Item1, key.Item2);

            return mapper.Map(data);
        }
    }
}
