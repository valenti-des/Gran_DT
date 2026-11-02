using System;
using System.Collections.Generic;
using Biblio;
using Biblio.IRepo;

namespace Servicios
{
    public class ServiciosFutbolista
    {
        private readonly IRepoFutbolista _repository;

        public ServiciosFutbolista(IRepoFutbolista repository)
        {
            _repository = repository;
        }

        public List<Futbolista> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public Futbolista? ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id.ToString());
        }
        // El servicio valida la regla de negocio y lanza la excepción con el mensaje exacto
        public Futbolista? Agregar(Futbolista futbolista)
        {

            if (futbolista.fechaNac < default(DateOnly)&& futbolista.fechaNac > DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("La fecha de nacimiento del futbolista es inválida.", nameof(futbolista));
            }

            if (futbolista.cotizacion < 0)
            {
                throw new ArgumentException("La cotización del futbolista no puede ser negativa.", nameof(futbolista));
            }

            if (futbolista.puntuaciones == null || futbolista.puntuaciones.Count == 0)
            {
                throw new ArgumentException("El futbolista debe tener al menos una puntuación.", nameof(futbolista));
            }
        
            var resultado = _repository.Agregar(futbolista);
            return resultado.nombre;
            return resultado.apellido;
            return resultado.fechaNac;
            return resultado.cotizacion;
            return resultado.puntuaciones;
            return resultado.idFutbolista;

        }

        public bool Eliminar(ushort id)
        {
            return _repository.Eliminar(id.ToString());
        }
    }
}

        