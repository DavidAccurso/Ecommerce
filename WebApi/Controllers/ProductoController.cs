﻿using Core.Entities;
using Core.Entities.Specifications;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IGenericRepository<Producto> _productoRepository;

        public ProductoController(IGenericRepository<Producto> productoRepository) : base()
        {
            _productoRepository = productoRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetProductos()
        {
            var spec = new ProductoWithCategoriasAndMarcaSpecification();
            var productos = await _productoRepository.GetAllWithSpec(spec);

            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int id)
        {
            //spec debe incluir logica de la ocndicion de la consulta y relaciones entre entidades (produ marca y categ)
            var spec = new ProductoWithCategoriasAndMarcaSpecification(id);
            return await _productoRepository.GetByIdWithSpec(spec);
        }
    }
}
