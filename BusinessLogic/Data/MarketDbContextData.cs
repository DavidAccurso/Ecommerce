using Core.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusinessLogic.Data
{
    public class MarketDbContextData
    {
        public static async Task CargarDataAscyn(MarketDbContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (context.Marca.Any() == false)
                {
                    string marcaData = File.ReadAllText("../BusinessLogic/CargarData/marca.json");
                    List<Marca> marcas = JsonSerializer.Deserialize<List<Marca>>(marcaData);

                    context.Marca.AddRange(marcas);

                    await context.SaveChangesAsync();
                }

                if (context.Categoria.Any() == false)
                {
                    string categoriaData = File.ReadAllText("../BusinessLogic/CargarData/categoria.json");
                    List<Categoria> categorias = JsonSerializer.Deserialize<List<Categoria>>(categoriaData);

                    context.Categoria.AddRange(categorias);

                    await context.SaveChangesAsync();
                }

                if (context.Producto.Any() == false)
                {
                    string productoData = File.ReadAllText("../BusinessLogic/CargarData/producto.json");
                    List<Producto> productos = JsonSerializer.Deserialize<List<Producto>>(productoData);

                    context.Producto.AddRange(productos);

                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                ILogger logger = loggerFactory.CreateLogger<MarketDbContext>();
                logger.LogError(ex.Message);
            }
        }
    }
}
