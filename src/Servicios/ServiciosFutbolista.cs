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
        public int Agregar(Futbolista futbolista)
        {
            if (string.IsNullOrWhiteSpace(futbolista.nombre) || string.IsNullOrWhiteSpace(futbolista.apellido))
            {
                throw new ArgumentException("El nombre y apellido del futbolista son obligatorios.", nameof(futbolista));
            }
            
            if (futbolista.idFutbolista == 0)
            {
                throw new ArgumentException("Solo se pueden utilizar números positivos para el ID del futbolista.", nameof(futbolista));
            }

            if (futbolista.fechaNac < default(DateOnly)&& futbolista.fechaNac > DateOnly.FromDateTime(DateTime.Now))
            {
                throw new ArgumentException("La fecha de nacimiento del futbolista es inválida.", nameof(futbolista));
            }

            if (futbolista.idTipoFutbolista != 1 && futbolista.idTipoFutbolista != 2 && futbolista.idTipoFutbolista != 3 && futbolista.idTipoFutbolista != 4)
            {
                throw new ArgumentException("El tipo de futbolista debe ser válido dentro de esto parametro Arquero 1, Defensa 2, Medio 3, Delantero 4.",nameof(futbolista));
            }

            if (futbolista.cotizacion < 0)
            {
                throw new ArgumentException("La cotización del futbolista no puede ser negativa.", nameof(futbolista));
            }

            if (futbolista.idFutbolista < 1 || futbolista.idFutbolista > 26)
            {
                throw new ArgumentException("El ID del futbolista debe estar entre 1 y 26.", nameof(futbolista));
            }

            if (futbolista.puntuaciones == null || futbolista.puntuaciones.Count == 0)
            {
                throw new ArgumentException("El futbolista debe tener al menos una puntuación.", nameof(futbolista));
            }
        
            var resultado = _repository.Agregar(futbolista);
            return resultado.idFutbolista;
        }

        public bool Eliminar(ushort id)
        {
            return _repository.Eliminar(id.ToString());
        }
    }
}

        