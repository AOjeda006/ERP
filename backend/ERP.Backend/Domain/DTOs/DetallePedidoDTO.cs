namespace Application.DTOs
{
    /// <summary>
    /// Objeto de transferencia de datos para una línea de detalle de pedido.
    /// Incluye información desnormalizada del producto para facilitar la presentación.
    /// </summary>
    public class DetallePedidoDTO
    {
        /// <summary>Identificador único del detalle de pedido.</summary>
        public int DetallePedidoID { get; }

        /// <summary>Identificador del producto asociado a esta línea.</summary>
        public int ProductoID { get; }

        /// <summary>Nombre del producto.</summary>
        public string ProductoNombre { get; }

        /// <summary>Código interno del producto.</summary>
        public string CodigoProducto { get; }

        /// <summary>Cantidad de unidades pedidas.</summary>
        public int Cantidad { get; }

        /// <summary>Precio unitario aplicado en esta línea.</summary>
        public decimal PrecioUnitario { get; }

        /// <summary>Porcentaje de descuento aplicado (0–100).</summary>
        public decimal Descuento { get; }

        /// <summary>
        /// Importe total de la línea: <c>Cantidad × PrecioUnitario × (1 - Descuento / 100)</c>.
        /// </summary>
        public decimal ImporteLinea { get; }

        /// <summary>Indica si la línea está activa o eliminada lógicamente.</summary>
        public bool Activo { get; }

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="DetallePedidoDTO"/> con todos sus campos.
        /// </summary>
        public DetallePedidoDTO(
            int detallePedidoID, 
            int productoID, 
            string productoNombre,
            string codigoProducto, 
            int cantidad, 
            decimal precioUnitario,
            decimal descuento, 
            decimal importeLinea, 
            bool activo)
        {
            DetallePedidoID = detallePedidoID;
            ProductoID = productoID;
            ProductoNombre = productoNombre;
            CodigoProducto = codigoProducto;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            Descuento = descuento;
            ImporteLinea = importeLinea;
            Activo = activo;
        }
    }
}