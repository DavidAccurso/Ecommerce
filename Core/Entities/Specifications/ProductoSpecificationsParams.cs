using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Specifications
{
    public class ProductoSpecificationsParams
    {
        public string Sort { get; set; }
        public int? Marca { get; set; }
        public int? Categoria { get; set; }
        public int PageIndex { get; set; } = 1;

        private const int MaxPageSixe = 50;
        private int _pageSize = 3;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSixe ? MaxPageSixe : value);
        }

        public string Search { get; set; }
    }
}
