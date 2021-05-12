using Microsoft.AspNetCore.Mvc;
using ProEventos.Domain;
using ProEventos.Application.Contratos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _eventoService;

        public EventosController(IEventoService eventoService)
        {
            _eventoService = eventoService;
        }

        #region GET -> getAllEventos => api/evento
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosAsync(true);
                if (eventos == null) return NotFound("Nenhum evento encontrado.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar os eventos. Error: {ex.Message}");
            }
        }
        #endregion

        #region GET -> getEventoById => api/evento/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var evento = await _eventoService.GetEventoByIdAsync(id, true);
                if (evento == null) return NotFound($"Nenhum evento encontrado com o ID = {id}.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar o evento de ID = {id}. Error: {ex.Message}");
            }
        }
        #endregion

        #region GET -> getAllEventosByTema => api/evento/tema/{tema}
        [HttpGet("/api/Eventos/tema/{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var eventos = await _eventoService.GetAllEventosByTemaAsync(tema, true);
                if (eventos == null) return NotFound($"Nenhum evento com o tema {tema} encontrado.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao tentar recuperar os eventos com o tema {tema}. Error: {ex.Message}");
            }
        }
        #endregion

        #region POST -> insert => api/evento
        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var eventos = await _eventoService.AddEvento(model);
                if (eventos == null) return BadRequest("Não foi possível adicionar o evento.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, $"Não foi possível adicionar o evento. Error: {ex.Message}");
            }
        }
        #endregion

        #region PUT -> update => api/evento/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Evento model)
        {
            try
            {
                var eventos = await _eventoService.UpdateEvento(id, model);
                if (eventos == null) return BadRequest($"Não foi possível atualizar o evento de ID = {id}.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, $"Não foi possível atualizar o evento de ID ={id}. Error: {ex.Message}");
            }
        }
        #endregion

        #region DELETE -> delete => api/evento/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await _eventoService.DeleteEvento(id))
                {
                    return Ok("Deletado");
                }
                else
                {
                    return BadRequest($"Não foi possível deletar o evento de ID = {id}.");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, $"Não foi possível deletar o evento de ID = {id}. Error: {ex.Message}");
            }
        }
        #endregion
    }
}

