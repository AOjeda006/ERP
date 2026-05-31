using Data.DataSources;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    /// <summary>
    /// Implementación del repositorio de pedidos a proveedores.
    /// Gestiona todas las operaciones CRUD y de consulta sobre la tabla <c>Pedidos</c>
    /// utilizando <see cref="ApplicationDbContext"/>.
    /// </summary>
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="PedidoRepository"/>.
        /// </summary>
        /// <param name="context">Contexto de base de datos inyectado.</param>
        public PedidoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los pedidos activos con proveedor, estado y detalles activos,
        /// ordenados por fecha de pedido descendente. Sin seguimiento de cambios.
        /// </summary>
        /// <returns>Lista de <see cref="Pedido"/> activos.</returns>
        public async Task<List<Pedido>> GetAllActivosAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Proveedor)
                .Include(p => p.Estado)
                .Include(p => p.Detalles.Where(d => d.Activo))
                .Where(p => p.Activo)
                .OrderByDescending(p => p.FechaPedido)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene un pedido por su identificador, incluyendo proveedor, estado,
        /// detalles activos y el producto de cada detalle. Con seguimiento de cambios.
        /// </summary>
        /// <param name="id">Identificador del pedido.</param>
        /// <returns>El <see cref="Pedido"/> encontrado, o <c>null</c> si no existe.</returns>
        public async Task<Pedido?> GetByIdAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Proveedor)
                .Include(p => p.Estado)
                .Include(p => p.Detalles.Where(d => d.Activo))
                    .ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(p => p.PedidoID == id);
        }

        /// <summary>
        /// Obtiene todos los pedidos activos de un proveedor concreto, ordenados por fecha descendente.
        /// Sin seguimiento de cambios.
        /// </summary>
        /// <param name="proveedorId">Identificador del proveedor.</param>
        /// <returns>Lista de <see cref="Pedido"/> del proveedor.</returns>
        public async Task<List<Pedido>> GetByProveedorAsync(int proveedorId)
        {
            return await _context.Pedidos
                .Include(p => p.Proveedor)
                .Include(p => p.Estado)
                .Include(p => p.Detalles.Where(d => d.Activo))
                .Where(p => p.ProveedorID == proveedorId && p.Activo)
                .OrderByDescending(p => p.FechaPedido)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene todos los pedidos activos en un estado concreto, ordenados por fecha descendente.
        /// Sin seguimiento de cambios.
        /// </summary>
        /// <param name="estadoId">Identificador del estado.</param>
        /// <returns>Lista de <see cref="Pedido"/> en el estado especificado.</returns>
        public async Task<List<Pedido>> GetByEstadoAsync(int estadoId)
        {
            return await _context.Pedidos
                .Include(p => p.Proveedor)
                .Include(p => p.Estado)
                .Include(p => p.Detalles.Where(d => d.Activo))
                .Where(p => p.EstadoID == estadoId && p.Activo)
                .OrderByDescending(p => p.FechaPedido)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene todos los pedidos activos cuyo estado sea "Recibido", incluyendo el detalle
        /// de producto en cada línea, ordenados por fecha de recepción descendente.
        /// Sin seguimiento de cambios.
        /// </summary>
        /// <returns>Lista de <see cref="Pedido"/> recibidos.</returns>
        public async Task<List<Pedido>> GetRecibidosAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Proveedor)
                .Include(p => p.Estado)
                .Include(p => p.Detalles.Where(d => d.Activo))
                    .ThenInclude(d => d.Producto)
                .Where(p => p.Estado!.NombreEstado == "Recibido" && p.Activo)
                .OrderByDescending(p => p.FechaRecepcion)
                .AsNoTracking()
                .ToListAsync();
        }

        /// <summary>
        /// Persiste un nuevo pedido en la base de datos junto con sus líneas de detalle.
        /// </summary>
        /// <param name="pedido">Entidad <see cref="Pedido"/> a insertar.</param>
        /// <returns>El <see cref="Pedido"/> insertado con el ID generado.</returns>
        public async Task<Pedido> CreateAsync(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido;
        }

        /// <summary>
        /// Actualiza la cabecera y las líneas de detalle de un pedido existente.
        /// Las líneas ausentes de la nueva lista se eliminan físicamente;
        /// las que tienen <c>DetallePedidoID > 0</c> se actualizan;
        /// las nuevas (ID == 0) se insertan.
        /// </summary>
        /// <param name="pedido">Entidad <see cref="Pedido"/> con los datos actualizados.</param>
        /// <exception cref="KeyNotFoundException">
        /// Se lanza si no existe ningún pedido con el <c>PedidoID</c> indicado.
        /// </exception>
        public async Task UpdateAsync(Pedido pedido)
        {
            Pedido? pedidoExistente = await _context.Pedidos
                .Include(p => p.Detalles)
                .FirstOrDefaultAsync(p => p.PedidoID == pedido.PedidoID);

            if (pedidoExistente is null)
            {
                throw new KeyNotFoundException($"No se encontró el pedido {pedido.PedidoID}");

            }

            pedidoExistente.FechaEntregaPrevista = pedido.FechaEntregaPrevista;
            pedidoExistente.Observaciones = pedido.Observaciones;

            List<int> detallesNuevosIds = pedido.Detalles
                .Where(d => d.DetallePedidoID > 0)
                .Select(d => d.DetallePedidoID)
                .ToList();

            List<DetallePedido> detallesAEliminar = pedidoExistente.Detalles
                .Where(d => !detallesNuevosIds.Contains(d.DetallePedidoID))
                .ToList();

            foreach (DetallePedido detalle in detallesAEliminar)
            {
                _context.DetallesPedido.Remove(detalle);
            }

            foreach (DetallePedido detalleNuevo in pedido.Detalles)
            {
                if (detalleNuevo.DetallePedidoID > 0)
                {
                    DetallePedido? detalleExistente = pedidoExistente.Detalles
                        .FirstOrDefault(d => d.DetallePedidoID == detalleNuevo.DetallePedidoID);

                    if (detalleExistente is not null)
                    {
                        detalleExistente.ProductoID = detalleNuevo.ProductoID;
                        detalleExistente.Cantidad = detalleNuevo.Cantidad;
                        detalleExistente.PrecioUnitario = detalleNuevo.PrecioUnitario;
                        detalleExistente.Descuento = detalleNuevo.Descuento;
                    }
                }
                else
                {
                    pedidoExistente.Detalles.Add(new DetallePedido
                    {
                        ProductoID = detalleNuevo.ProductoID,
                        Cantidad = detalleNuevo.Cantidad,
                        PrecioUnitario = detalleNuevo.PrecioUnitario,
                        Descuento = detalleNuevo.Descuento,
                        Activo = true
                    });
                }
            }

            _context.Pedidos.Update(pedidoExistente);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Realiza un borrado lógico del pedido marcando <c>Activo = false</c>.
        /// Si el pedido no existe, la operación no tiene efecto.
        /// </summary>
        /// <param name="id">Identificador del pedido a desactivar.</param>
        public async Task SoftDeleteAsync(int id)
        {
            Pedido? pedido = await _context.Pedidos.FindAsync(id);

            if (pedido is not null)
            {
                pedido.Activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
