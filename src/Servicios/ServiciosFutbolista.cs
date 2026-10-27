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
            


            var resultado = _repository.Agregar(futbolista);
            return resultado.idFutbolista;
        }

        public bool Eliminar(ushort id)
        {
            return _repository.Eliminar(id.ToString());
        }
    }
}

        