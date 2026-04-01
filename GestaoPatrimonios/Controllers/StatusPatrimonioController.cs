using GestaoPatrimonios.Applications.Services;
using GestaoPatrimonios.DTOs.StatusPatrimonio;
using GestaoPatrimonios.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusPatrimonioController : ControllerBase
    {
        private readonly StatusPatrimonioService _service;

        public StatusPatrimonioController(StatusPatrimonioService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarStatusPatrimonioDto>> Listar()
        {
            List<ListarStatusPatrimonioDto> status = _service.Listar();
            return Ok(status);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarStatusPatrimonioDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarStatusPatrimonioDto status = _service.BuscarPorId(id);
                return Ok(status);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarStatusPatrimonioDto dto)
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
        public ActionResult Atualizar(Guid id, CriarStatusPatrimonioDto dto)
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
