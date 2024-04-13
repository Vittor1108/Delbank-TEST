using Delbank.Application.DTOs.DirectorDTOs;
using Delbank.Commons.Interfaces;
using Delbank.Domain.Entities.SQL;
using Delbank.Domain.Interfaces.Services.SQL;
using Delbank.Messaging.Interfaces;
using Delbank.Service.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Delbank.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly IResponseCommon _responseCommon;
        private readonly IBaseServiceSQL<DirectorEntitySQL> _baseService;
        
        public DirectorController(IResponseCommon responseCommon, IBaseServiceSQL<DirectorEntitySQL> baseSerivce)
        {
            _responseCommon = responseCommon;
            _baseService = baseSerivce;            
        }

        [HttpPost]
        public async Task<ActionResult<Dictionary<string, object>>> CreateDirector([FromBody] DirectorRequestDTO dto)
        {
            try
            {
                DirectorResponseDTO director = await _baseService.Add<DirectorRequestDTO, DirectorResponseDTO, DirectorValidator>(dto);
                Dictionary<string, object> response = _responseCommon.GenerateHttpResponse("Diretor adicionado com sucesso!", 201, director);

                return Created("", response);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> badRequestResponse = _responseCommon.GenerateHttpResponse(ex.Message, 400, null);
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<ActionResult<Dictionary<string, object>>> GetAllDirectors()
        {
            try
            {
                IList<DirectorResponseDTO> listDirector = await _baseService.GetAll<DirectorResponseDTO>();
                Dictionary<string, object> response = _responseCommon.GenerateHttpResponse("Diretores listados com sucesso!", 200, listDirector);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> badRequestResponse = _responseCommon.GenerateHttpResponse(ex.Message, 400, null);
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dictionary<string, object>>> FindOneDirector(Guid id)
        {
            try
            {
                DirectorResponseDTO director = await _baseService.GetById<DirectorResponseDTO>(id);
                Dictionary<string, object> response = _responseCommon.GenerateHttpResponse("Diretor listado com sucesso!", 200, director);

                return Ok(response);
            }
            catch (InvalidOperationException)
            {
                Dictionary<string, object> notFoundRepsonse = _responseCommon.GenerateHttpResponse($"Diretor com o id {id} não encontrado.", 404, null);
                return NotFound(notFoundRepsonse);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> badRequestResponse = _responseCommon.GenerateHttpResponse(ex.Message, 400, null);
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Dictionary<string, object>>> DesactiveDirector(Guid id)
        {
            try
            {
                await _baseService.Desactive(id);
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                Dictionary<string, object> notFoundRepsonse = _responseCommon.GenerateHttpResponse($"Diretor com o id {id} não encontrado.", 404, null);
                return NotFound(notFoundRepsonse);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> badRequestResponse = _responseCommon.GenerateHttpResponse(ex.Message, 400, null);
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Dictionary<string, object>>> UpdateDirector([FromBody] DirectorRequestDTO dto, Guid id)
        {
            try
            {
                DirectorResponseDTO director = await _baseService.Update<DirectorRequestDTO, DirectorResponseDTO, DirectorValidator>(dto, id);
                Dictionary<string, object> response = _responseCommon.GenerateHttpResponse($"Diretor com o id {id} atualizado com sucesso!", 200, director);
                return Ok(response);
            }
            catch (InvalidOperationException)
            {
                Dictionary<string, object> notFoundRepsonse = _responseCommon.GenerateHttpResponse($"Diretor com o id {id} não encontrado.", 404, null);
                return NotFound(notFoundRepsonse);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> badRequestResponse = _responseCommon.GenerateHttpResponse(ex.Message, 400, null);
                return BadRequest();
            }
        }
    }
}
