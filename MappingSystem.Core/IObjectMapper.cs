namespace MappingSystem.Core
{
    public interface IObjectMapper
    {
        string SourceType { get; }
        string TargetType { get; }

        object Map(object source);
    }
}
