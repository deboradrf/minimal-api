using MinimalApi.Dominio.Enuns;
using MinimalApi.Dominio.ModelViews;

namespace MinimalApi.DTOs;
public record AdministradorModelView
{
    public int Id { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Perfil {get; set; } = default!;
}