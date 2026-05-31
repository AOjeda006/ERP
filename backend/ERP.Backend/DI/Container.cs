using Data.DataSources;
using Data.Repositories;
using Domain.Interfaces.IMappers;
using Domain.Interfaces.IUseCases;
using Domain.Mappers;
using Domain.Interfaces;
using Application.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    /// <summary>
    /// Clase estática de configuración del contenedor de inyección de dependencias.
    /// Centraliza el registro de todos los servicios, repositorios, casos de uso y mappers
    /// de la solución ERP.Backend.
    /// </summary>
    public static class Container
    {
        /// <summary>
        /// Registra todos los servicios de la aplicación en el contenedor DI de ASP.NET Core.
        /// </summary>
        /// <param name="services">Colección de servicios del contenedor DI.</param>
        /// <param name="config">
        /// Configuración de la aplicación, usada para obtener la cadena de conexión
        /// <c>DefaultConnection</c>.
        /// </param>
        /// <remarks>
        /// Ciclos de vida aplicados:
        /// <list type="bullet">
        ///   <item><c>Scoped</c> — Repositorios y casos de uso (un ámbito por petición HTTP).</item>
        ///   <item><c>Singleton</c> — Mappers (sin estado, reutilizables entre peticiones).</item>
        /// </list>
        /// </remarks>
        public static void RegisterServices(IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = config.GetConnectionString("DefaultConnection");
                Data.Network.DbConfiguration.ConfigureAzureSQL(options, connectionString);
            });

            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IEstadoPedidoRepository, EstadoPedidoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IProductoProveedorRepository, ProductoProveedorRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IProveedorRepository, ProveedorRepository>();

            services.AddScoped<ICategoriaUseCases, CategoriaUseCase>();
            services.AddScoped<IEstadoPedidoUseCases, EstadoPedidoUseCase>();
            services.AddScoped<IPedidoUseCase, PedidoUseCase>();
            services.AddScoped<IProductoProveedorUseCases, ProductoProveedorUseCase>();
            services.AddScoped<IProductoUseCase, ProductoUseCase>();
            services.AddScoped<IProveedorUseCase, ProveedorUseCase>();

            services.AddSingleton<ICategoriaMapper, CategoriaMapper>();
            services.AddSingleton<IEstadoPedidoMapper, EstadoPedidoMapper>();
            services.AddSingleton<IPedidoMapper, PedidoMapper>();
            services.AddSingleton<IProductoMapper, ProductoMapper>();
            services.AddSingleton<IProductoProveedorMapper, ProductoProveedorMapper>();
            services.AddSingleton<IProveedorMapper, ProveedorMapper>();
        }
    }
}