using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    public class MarcaController : BaseApiController
    {
        private readonly IGenericRepository<Marca> _marcaRepository;

        public MarcaController(IGenericRepository<Marca> marcaRepository) : base()
        {
            _marcaRepository = marcaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Categoria>>> GetAllMarcasAsync()
        {
            return Ok(await _marcaRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Marca>> GetMarcaByIdAsync(int id)
        {
            return await _marcaRepository.GetByIdAsync(id);
        }
    }
}
