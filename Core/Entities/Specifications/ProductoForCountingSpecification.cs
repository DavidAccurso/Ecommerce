using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Specifications
{
    public class ProductoForCountingSpecification : BaseSpecification<Producto>
    {
        public ProductoForCountingSpecification(ProductoSpecificationsParams productParams)
            : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Nombre.Contains(productParams.Search)) &&
            (!productParams.Marca.HasValue || x.MarcaId == productParams.Marca) &&
            (!productParams.Categoria.HasValue || x.CategoriaId == productParams.Categoria)) { }
    }
}
