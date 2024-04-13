using Delbank.Application.DTOs.DvdDTOs;
using Delbank.Commons.Interfaces;
using Delbank.Domain.Interfaces.Services.SQL;
using Microsoft.AspNetCore.Mvc;

namespace Delbank.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DvdController : Controller
    {
        private readonly IResponseCommon _responseCommon;
        private readonly IDvdServiceSQL _dvdServiceSql;

        public DvdController(IResponseCommon responseCommon, IDvdServiceSQL dvdServiceSql)
        {
            _responseCommon = responseCommon;
            _dvdServiceSql = dvdServiceSql;
        }

        [HttpPost]
        public async Task<ActionResult<Dictionary<string, object>>> CreateDVD([FromBody] DvdRequestDTO dto)
        {
            try
            {
                DvdResponseDTO dvd = await _dvdServiceSql.CreateDvd<DvdRequestDTO, DvdResponseDTO>(dto);
                Dictionary<string, object> response = _responseCommon.GenerateHttpResponse("Dvd criado com sucesso!", 201, dvd);


                return Created("", response);
            }
            catch (InvalidOperationException ex)
            {
                Dictionary<string, object> directorNotFoundResponse = _responseCommon.GenerateHttpResponse($"Diretor com o id {dto.FkDirector} não encontrado. Tente novamente!", 404, null);
                return NotFound(directorNotFoundResponse);
            }
            catch(Exception ex)
            {
                Dictionary<string, object> badRequestResponse = _responseCommon.GenerateHttpResponse(ex.Message, 400, null);
                return BadRequest(badRequestResponse);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Dictionary<string, object>>> FindOneDvd(Guid id)
        {
            try
            {
                DvdResponseDTO dvd = await _dvdServiceSql.FindOneDvd<DvdResponseDTO>(id);
                Dictionary<string, object> response = _responseCommon.GenerateHttpResponse("Dvd listado com sucesso!", 200, dvd);
                return Ok(response);
            }
            catch (InvalidOperationException)
            {
                Dictionary<string, object> directorNotFoundResponse = _responseCommon.GenerateHttpResponse($"DVD com o id {id} não encontrado. Tente novamente!", 404, null);
                return BadRequest(directorNotFoundResponse);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> badRequestResponse = _responseCommon.GenerateHttpResponse(ex.Message, 400, null);
                return BadRequest(badRequestResponse);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Dictionary<string, object>>> DeleteDvd(Guid id)
        {
            try
            {
                await _dvdServiceSql.DeleteDvd(id);
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                Dictionary<string, object> directorNotFoundResponse = _responseCommon.GenerateHttpResponse($"DVD com o id {id} não encontrado. Tente novamente!", 404, null);
                return BadRequest(directorNotFoundResponse);
            }
            catch (Exception ex) 
            {
                Dictionary<string, object> badRequestResponse = _responseCommon.GenerateHttpResponse(ex.Message, 400, null);
                return BadRequest(badRequestResponse);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Dictionary<string, object>>> UpdateDvd([FromBody] DvdRequestDTO dto, Guid id)
        {
            try
            {
                DvdResponseDTO dvd = await _dvdServiceSql.UpdateDvd<DvdRequestDTO, DvdResponseDTO>(dto, id);
                Dictionary<string, object> response = _responseCommon.GenerateHttpResponse($"Dvd com o id {id} atualizado com sucesso!", 200, dvd);

                return Ok(response);
            }
            catch (InvalidOperationException)
            {
                Dictionary<string, object> directorNotFoundResponse = _responseCommon.GenerateHttpResponse($"DVD com o id {id} não encontrado. Tente novamente!", 404, null);
                return BadRequest(directorNotFoundResponse);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> badRequestResponse = _responseCommon.GenerateHttpResponse(ex.Message, 400, null);
                return BadRequest(badRequestResponse);
            }
        }
    }
}
