public interface IResourceble
{
    ResourceType ResourceType { get; }
}

public enum ResourceType
{
    Wood,
    Stone
}