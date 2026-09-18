namespace Nop.Plugin.Misc.Alanube.Mapping;

public sealed class AlanubeMappingResult<T>
{
    public T Value { get; init; }
    public IList<AlanubeMappingFailure> Failures { get; init; } = new List<AlanubeMappingFailure>();
    public bool Succeeded => Failures.Count == 0;
}

public sealed class AlanubeMappingFailure
{
    public string Code { get; init; }
    public string Field { get; init; }
    public string Message { get; init; }
}
