public record UsuarioLogado
{
    public string Nome { get; init; } = default!;
    public string Role { get; init; } = default!;
    public string Token { get; init; } = default!;
}