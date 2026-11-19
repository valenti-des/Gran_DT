using GranDT.Aplicacion;
using GranDT.Aplicacion.IRepo;
using GranDT.Aplicacion.Servicios;
using GranDT.Dominio;
using Xunit;

namespace GranDT.Pruebas;

public sealed class UsuarioServicioTests
{
    [Fact]
    public async Task RechazaCorreoYaRegistrado()
    {
        var repo = new UsuariosEnMemoria("ana@example.com");
        var hasher = new HasherPrueba();
        var servicio = new UsuarioServicio(repo, hasher);

        await Assert.ThrowsAsync<ConflictoException>(() => servicio.CrearAsync(
            new CrearUsuario("Ana", "Ejemplo", "ANA@example.com", null, "ClaveTemporal123", false)));
        Assert.Equal(0, hasher.Llamadas);
    }

    [Fact]
    public async Task GuardaHashYDevuelveUsuarioSinContrasena()
    {
        var repo = new UsuariosEnMemoria();
        var servicio = new UsuarioServicio(repo, new HasherPrueba());

        var usuario = await servicio.CrearAsync(
            new CrearUsuario("Ana", "Ejemplo", "ana@example.com", null, "ClaveTemporal123", false));

        Assert.Equal("hash:ClaveTemporal123", repo.HashAlmacenado);
        Assert.Equal("ana@example.com", usuario.Email);
        Assert.DoesNotContain("Contrasena", typeof(Usuario).GetProperties().Select(p => p.Name));
    }

    private sealed class HasherPrueba : IHashContrasenas
    {
        public int Llamadas { get; private set; }
        public string Crear(string contrasena)
        {
            Llamadas++;
            return $"hash:{contrasena}";
        }
    }

    private sealed class UsuariosEnMemoria(string? emailExistente = null) : IUsuarioRepositorio
    {
        public string? HashAlmacenado { get; private set; }
        public Task<Usuario?> ObtenerAsync(long id, CancellationToken cancellationToken = default) => Task.FromResult<Usuario?>(null);
        public Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken = default) =>
            Task.FromResult(string.Equals(email, emailExistente, StringComparison.OrdinalIgnoreCase));
        public Task<Usuario> CrearAsync(string nombre, string apellido, string email, DateOnly? fechaNac,
            string hashContrasena, bool esAdmin, CancellationToken cancellationToken = default)
        {
            HashAlmacenado = hashContrasena;
            return Task.FromResult(new Usuario(1, nombre, apellido, email, fechaNac, esAdmin));
        }
    }
}