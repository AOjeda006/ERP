namespace Domain.Exceptions
{
    /// <summary>
    /// Excepción base para errores originados en la capa de dominio.
    /// Todas las excepciones de negocio de la aplicación deben heredar de esta clase.
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// Inicializa una nueva instancia de <see cref="DomainException"/> con un mensaje de error.
        /// </summary>
        /// <param name="message">Descripción del error de dominio.</param>
        public DomainException(string message) : base(message) { }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="DomainException"/> con un mensaje
        /// y la excepción interna que causó el error.
        /// </summary>
        /// <param name="message">Descripción del error de dominio.</param>
        /// <param name="inner">Excepción subyacente que originó el error.</param>
        public DomainException(string message, Exception inner) : base(message, inner) { }
    }
}
