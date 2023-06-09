﻿using Microsoft.AspNet.Identity;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Business.Contratos;
using WebApi.Entities.Sede;

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

        [HttpGet]
        [Authorize]
        public HttpResponseMessage Get(int id)
        {

            try
            {
                var id_usuario = User.Identity.GetUserId();
                var respuesta = _sedeBO.getSede(id, id_usuario);
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

        [HttpPost]
        [Authorize]
        public HttpResponseMessage Post(PostSede datos)
        {

            try
            {
                var id_usuario = User.Identity.GetUserId();
                var respuesta = _sedeBO.postSede(datos, id_usuario);
                if (respuesta != null)
                {

                    if (respuesta.codigoRes == HttpStatusCode.Created)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created,
                            new { Message = respuesta.mensajeRes, respuesta.id_sede });
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
                        new { Message = "Error interno en el servicio de registro." });
            }
        }
        
        [HttpPut]
        [Authorize]
        public HttpResponseMessage Put(int id, PostSede datos)
        {

            try
            {
                var id_usuario = User.Identity.GetUserId();
                var respuesta = _sedeBO.putSede(id, datos, id_usuario);
                if (respuesta != null)
                {

                    if (respuesta.codigoRes == HttpStatusCode.OK)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new { Message = respuesta.mensajeRes });
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
                        new { Message = "Error interno en el servicio de modificación." });
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("eliminacion/{id}")]
        public HttpResponseMessage Delete(int id)
        {

            try
            {
                var id_usuario = User.Identity.GetUserId();
                var respuesta = _sedeBO.deleteSede(id, id_usuario);
                if (respuesta != null)
                {

                    if (respuesta.codigoRes == HttpStatusCode.OK)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK,
                            new { Message = respuesta.mensajeRes });
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
                        new { Message = "Error interno en el servicio de eliminación." });
            }
        }
    }
}

