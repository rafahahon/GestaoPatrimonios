using GestaoPatrimonios.Applications.Services;
using GestaoPatrimonios.DTOs.TipoPatrimonioDto;
using GestaoPatrimonios.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPatrimonioController : ControllerBase
    {
        private readonly TipoPatrimonioService _service;

        public TipoPatrimonioController(TipoPatrimonioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarTipoPatrimonioDto>> Listar()
        {
            List<ListarTipoPatrimonioDto> tipos = _service.Listar();
            return Ok(tipos);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarTipoPatrimonioDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarTipoPatrimonioDto tipo = _service.BuscarPorId(id);
                return Ok(tipo);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarTipoPatrimonioDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarTipoPatrimonioDto dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
