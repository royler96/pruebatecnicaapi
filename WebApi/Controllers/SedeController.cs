using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Web.Http;
using WebApi.Business.Contratos;

namespace WebApi.Controllers
{
    [RoutePrefix("api/sede")]
    public class SedeController : ApiController
    {
        private ISedeBO _sedeBO;
        public SedeController(ISedeBO sedeBO)
        {
            _sedeBO = sedeBO;
        }

        [HttpGet]        
        [Authorize]
        public HttpResponseMessage Get(string nombre_sede)
        {
            
            try
            {
                var id_usuario = User.Identity.GetUserId();
                var respuesta = _sedeBO.getAllSedes(nombre_sede, id_usuario);
                if (respuesta != null)
                {
                    
                    if (respuesta.codigoRes == HttpStatusCode.OK)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new { Message = respuesta.mensajeRes, data = respuesta.datos });
                    }
                    else if (respuesta.codigoRes == HttpStatusCode.NoContent)
                    {
                        return Request.CreateResponse(HttpStatusCode.NoContent);
                    }
                    return Request.CreateResponse(respuesta.codigoRes,
                        new { Message = respuesta.mensajeRes });
                }
                else
                {                    
                    return Request.CreateResponse(HttpStatusCode.InternalServerError,
                        new { Message = "Error interno al obtener respuesta." });
                }
            }
            catch (Exception)
            {                
                return Request.CreateResponse(HttpStatusCode.InternalServerError,
                        new { Message = "Error interno en el servicio de listar sedes." });
            }
        }
    }
}
