namespace Nop.Plugin.DdpApi.Factories
{
    public interface IFactory<T>
    {
        T Initialize();
    }
}