namespace Application.DTOs
{
    /// <summary>
    /// Objeto de transferencia de datos para un proveedor.
    /// Expone todos los campos del maestro de proveedores.
    /// </summary>
    public class ProveedorDTO
    {
        /// <summary>Identificador único del proveedor.</summary>
        public int ProveedorID { get; }

        /// <summary>CIF o NIF del proveedor.</summary>
        public string CIF { get; }

        /// <summary>Razón social (nombre legal) del proveedor.</summary>
        public string RazonSocial { get; }

        /// <summary>Nombre comercial del proveedor. Puede ser nulo.</summary>
        public string? NombreComercial { get; }

        /// <summary>Dirección postal del proveedor. Puede ser nula.</summary>
        public string? Direccion { get; }

        /// <summary>Ciudad del proveedor. Puede ser nula.</summary>
        public string? Ciudad { get; }

        /// <summary>Provincia del proveedor. Puede ser nula.</summary>
        public string? Provincia { get; }

        /// <summary>Teléfono de contacto. Puede ser nulo.</summary>
        public string? Telefono { get; }

        /// <summary>Correo electrónico de contacto. Puede ser nulo.</summary>
        public string? Email { get; }

        /// <summary>Nombre de la persona de contacto en el proveedor. Puede ser nulo.</summary>
        public string? PersonaContacto { get; }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ProveedorDTO"/> con todos sus campos.
        /// </summary>
        public ProveedorDTO(int proveedorID, string cif, string razonSocial,
            string? nombreComercial, string? direccion, string? ciudad, string? provincia,
            string? telefono, string? email, string? personaContacto)
        {
            ProveedorID = proveedorID;
            CIF = cif;
            RazonSocial = razonSocial;
            NombreComercial = nombreComercial;
            Direccion = direccion;
            Ciudad = ciudad;
            Provincia = provincia;
            Telefono = telefono;
            Email = email;
            PersonaContacto = personaContacto;
        }
    }
}