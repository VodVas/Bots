public interface IResourceble
{
    ResourceType Type { get; }
}

public enum ResourceType
{
    Wood,
    Stone
}