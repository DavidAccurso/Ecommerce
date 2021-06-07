using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Specifications
{
    public class ProductoWithCategoriasAndMarcaSpecification : BaseSpecification<Producto>
    {
        public ProductoWithCategoriasAndMarcaSpecification(ProductoSpecificationsParams productParams)
            : base(x => (!productParams.Marca.HasValue || x.MarcaId == productParams.Marca) && 
                        (!productParams.Categoria.HasValue || x.CategoriaId == productParams.Categoria))
        {
            AddInclude(p => p.Categoria);
            AddInclude(p => p.Marca);

            ApplyPaging( productParams.PageSize * (productParams.PageIndex-1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "nombreAsc":
                        AddOrderBy(p => p.Nombre);
                        break;
                    case "nombreDesc":
                        AddOrderByDescending(p => p.Nombre);
                        break;
                    case "precioAsc":
                        AddOrderBy(p => p.Precio);
                        break;
                    case "precioDesc":
                        AddOrderByDescending(p => p.Precio);
                        break;
                    case "descripcionAsc":
                        AddOrderBy(p => p.Descripcion);
                        break;
                    case "descripcionDesc":
                        AddOrderByDescending(p => p.Descripcion);
                        break;
                    default:
                        AddOrderBy(p => p.Nombre);
                        break;
                }
            }
        }
        public ProductoWithCategoriasAndMarcaSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(p => p.Categoria);
            AddInclude(p => p.Marca);
        }
    }
}
