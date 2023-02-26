using CodeBase.Enums;

namespace CodeBase.ObjectPooling
{
    public interface IPoolObject
    {
        ObjectType Type { get; }
    }
}