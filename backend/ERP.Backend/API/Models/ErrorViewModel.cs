namespace API.Models
{
    /// <summary>
    /// Modelo de vista para representar el error de una solicitud HTTP.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>Identificador único de la solicitud que originó el error.</summary>
        public string? RequestId { get; set; }

        /// <summary>Indica si el identificador de solicitud debe mostrarse en la interfaz.</summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
