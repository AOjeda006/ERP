using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Entidad de dominio que representa un proveedor del sistema ERP.
    /// </summary>
    public class Proveedor
    {
        #region atributos
        private int _proveedorID;
        private string _cif;
        private string _razonSocial;
        private string? _nombreComercial;
        private string? _direccion;
        private string? _ciudad;
        private string? _provincia;
        private string? _telefono;
        private string? _email;
        private string? _personaContacto;
        #endregion

        #region constructores
        /// <summary>
        /// Constructor requerido por el ORM / serializadores.
        /// No debe utilizarse directamente en lógica de dominio.
        /// </summary>
        public Proveedor() { }

        /// <summary>
        /// Crea un proveedor con sus valores iniciales.
        /// </summary>
        /// <param name="proveedorID">Identificador único del proveedor.</param>
        /// <param name="cif">CIF o NIF del proveedor.</param>
        /// <param name="razonSocial">Razón social del proveedor (nombre legal).</param>
        /// <param name="nombreComercial">Nombre comercial del proveedor (opcional).</param>
        /// <param name="direccion">Dirección postal del proveedor (opcional).</param>
        /// <param name="ciudad">Ciudad del proveedor (opcional).</param>
        /// <param name="provincia">Provincia del proveedor (opcional).</param>
        /// <param name="telefono">Teléfono de contacto del proveedor (opcional).</param>
        /// <param name="email">Correo electrónico de contacto del proveedor (opcional).</param>
        /// <param name="personaContacto">Persona de contacto en el proveedor (opcional).</param>
        public Proveedor(int proveedorID, string cif, string razonSocial, string? nombreComercial,
            string? direccion, string? ciudad, string? provincia, string? telefono,
            string? email, string? personaContacto)
        {
            _proveedorID = proveedorID;
            _cif = cif;
            _razonSocial = razonSocial;
            _nombreComercial = nombreComercial;
            _direccion = direccion;
            _ciudad = ciudad;
            _provincia = provincia;
            _telefono = telefono;
            _email = email;
            _personaContacto = personaContacto;
        }
        #endregion

        #region propiedades
        /// <summary>Identificador único del proveedor (clave primaria).</summary>
        [Key]
        public int ProveedorID { get => _proveedorID; set => _proveedorID = value; }

        /// <summary>CIF o NIF único del proveedor. Máximo 15 caracteres.</summary>
        [Required]
        [StringLength(15)]
        public string CIF { get => _cif; set => _cif = value; }

        /// <summary>Razón social (nombre legal) del proveedor. Máximo 255 caracteres.</summary>
        [Required]
        [StringLength(255)]
        public string RazonSocial { get => _razonSocial; set => _razonSocial = value; }

        /// <summary>Nombre comercial del proveedor. Máximo 255 caracteres. Opcional.</summary>
        [StringLength(255)]
        public string? NombreComercial { get => _nombreComercial; set => _nombreComercial = value; }

        /// <summary>Dirección postal del proveedor. Máximo 500 caracteres. Opcional.</summary>
        [StringLength(500)]
        public string? Direccion { get => _direccion; set => _direccion = value; }

        /// <summary>Ciudad del proveedor. Máximo 100 caracteres. Opcional.</summary>
        [StringLength(100)]
        public string? Ciudad { get => _ciudad; set => _ciudad = value; }

        /// <summary>Provincia del proveedor. Máximo 100 caracteres. Opcional.</summary>
        [StringLength(100)]
        public string? Provincia { get => _provincia; set => _provincia = value; }

        /// <summary>Teléfono de contacto. Máximo 20 caracteres. Opcional.</summary>
        [StringLength(20)]
        public string? Telefono { get => _telefono; set => _telefono = value; }

        /// <summary>Correo electrónico de contacto. Debe ser una dirección válida. Máximo 255 caracteres. Opcional.</summary>
        [StringLength(255)]
        [EmailAddress]
        public string? Email { get => _email; set => _email = value; }

        /// <summary>Nombre de la persona de contacto en el proveedor. Máximo 255 caracteres. Opcional.</summary>
        [StringLength(255)]
        public string? PersonaContacto { get => _personaContacto; set => _personaContacto = value; }


        /// <summary>Colección de pedidos realizados a este proveedor.</summary>
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();

        /// <summary>Colección de relaciones con productos que suministra este proveedor.</summary>
        public ICollection<ProductoProveedor> ProductosProveedores { get; set; } = new List<ProductoProveedor>();
        #endregion
    }
}