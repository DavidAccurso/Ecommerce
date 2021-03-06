using AutoMapper;
using Core.Entities;
using Core.Entities.Specifications;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Errors;

namespace WebApi.Controllers
{
    public class ProductoController : BaseApiController
    {
        private readonly IGenericRepository<Producto> _productoRepository;
        private readonly IMapper _mapper;

        public ProductoController(IGenericRepository<Producto> productoRepository, IMapper mapper) : base()
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<Producto>>> GetProductos([FromQuery] ProductoSpecificationsParams productParams)
        {
            var spec = new ProductoWithCategoriasAndMarcaSpecification(productParams);
            IReadOnlyList<Producto> productos = await _productoRepository.GetAllWithSpec(spec);

            var specCount = new ProductoForCountingSpecification(productParams);

            int totalProductos = await _productoRepository.CountAsync(specCount);

            decimal rounded = Math.Ceiling(Convert.ToDecimal(totalProductos / productParams.PageSize));
            int totalPages = Convert.ToInt32(rounded);

            IReadOnlyList<ProductoDto> data = _mapper.Map<IReadOnlyList<Producto>, IReadOnlyList<ProductoDto>> (productos);

            return Ok(
                new Pagination<ProductoDto>
                {
                    Count = totalProductos,
                    Data = data,
                    PageCount = totalPages,
                    PageIndex = productParams.PageIndex,
                    PageSize = productParams.PageSize
                });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            //spec debe incluir logica de la ocndicion de la consulta y relaciones entre entidades (produ marca y categ)
            var spec = new ProductoWithCategoriasAndMarcaSpecification(id);
            var producto = await _productoRepository.GetByIdWithSpec(spec);

            if (producto == null)
                return NotFound(new CodeErrorResponse(404, "El producto no existe"));

            return _mapper.Map<Producto, ProductoDto>(producto);
        }
    }
}
