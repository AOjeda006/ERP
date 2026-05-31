using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Network
{
    /// <summary>
    /// Clase de utilidad estática para configurar la conexión a Azure SQL Server
    /// mediante Entity Framework Core.
    /// </summary>
    public class DbConfiguration
    {
        /// <summary>
        /// Configura el <see cref="DbContextOptionsBuilder"/> para conectarse a Azure SQL Server
        /// con reintentos automáticos ante fallos transitorios, timeout de comandos y
        /// el ensamblado de migraciones correspondiente.
        /// </summary>
        /// <param name="options">Builder de opciones del contexto de EF Core.</param>
        /// <param name="connectionString">Cadena de conexión a la base de datos Azure SQL.</param>
        /// <remarks>
        /// Parámetros configurados: máximo 5 reintentos (espera máx. 30 s),
        /// timeout de comando de 60 s y ensamblado de migraciones <c>ERP.Backend.Data</c>.
        /// </remarks>
        public static void ConfigureAzureSQL(DbContextOptionsBuilder options, string connectionString)
        {
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null
                );

                sqlOptions.CommandTimeout(60);
                sqlOptions.MigrationsAssembly("ERP.Backend.Data");
            });

            options.EnableSensitiveDataLogging(false);
            options.EnableDetailedErrors(false);
        }
    }
}