using GestaoPatrimonios.Applications.Services;
using GestaoPatrimonios.DTOs.StatusTransferenciaDto;
using GestaoPatrimonios.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace GestaoPatrimonios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusTransferenciaController : ControllerBase
    {
        private readonly StatusTransferenciaService _service;

        public StatusTransferenciaController(StatusTransferenciaService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarStatusTransferenciaDto>> Listar()
        {
            List<ListarStatusTransferenciaDto> status = _service.Listar();
            return Ok(status);
        }

        [HttpGet("{id}")]
        public ActionResult<ListarStatusTransferenciaDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarStatusTransferenciaDto status = _service.BuscarPorId(id);
                return Ok(status);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarStatusTransferenciaDto dto)
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
        public ActionResult Atualizar(Guid id, CriarStatusTransferenciaDto dto)
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
