using Biblio;

namespace TestUnitarios;

[Trait("Category", "Unit")]
public class TestUnidadPuraUsuario
{
    [Fact]
    public void UsuarioCreacion_DeberiaInicializarYConservarSusPropiedades()
    {
        var usuario = new UsuarioCreacion();

        Assert.Equal(string.Empty, usuario.nombre);
        Assert.Equal(string.Empty, usuario.apellido);
        Assert.Equal(string.Empty, usuario.email);
        Assert.Null(usuario.fechaNac);
        Assert.Equal(string.Empty, usuario.contraseña);
        Assert.False(usuario.es_admin);

        usuario.nombre = "Ana";
        usuario.apellido = "Prueba";
        usuario.email = "ana@test.com";
        usuario.fechaNac = new DateOnly(1998, 4, 12);
        usuario.contraseña = "hash";
        usuario.es_admin = true;

        Assert.Equal("Ana", usuario.nombre);
        Assert.Equal("Prueba", usuario.apellido);
        Assert.Equal("ana@test.com", usuario.email);
        Assert.Equal(new DateOnly(1998, 4, 12), usuario.fechaNac);
        Assert.Equal("hash", usuario.contraseña);
        Assert.True(usuario.es_admin);
    }
}
