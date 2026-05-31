namespace Domain.Exceptions
{
    /// <summary>
    /// Excepción de dominio lanzada cuando no se encuentra una entidad por su identificador.
    /// </summary>
    /// <seealso cref="DomainException"/>
    public class NotFoundException : DomainException
    {
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="NotFoundException"/>
        /// generando el mensaje: <c>{entity} con ID {id} no encontrado.</c>
        /// </summary>
        /// <param name="entity">Nombre de la entidad que no se encontró (p. ej.: "Pedido").</param>
        /// <param name="id">Identificador por el que se buscó la entidad.</param>
        public NotFoundException(string entity, int id)
            : base($"{entity} con ID {id} no encontrado.") { }
    }
}
