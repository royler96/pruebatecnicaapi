﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebApi.DataAccess.Contratos;
using WebApi.DataAccess.Models;
using WebApi.Entities.Sede;

namespace WebApi.DataAccess.Implementaciones
{
    public class SedeDO : ISedeDO
    {        

        public DataItemSede getAllSedes(string nombre_sede, string id_usuario)
        {
            try
            {
                var ctx = new BD_COMPLEJOSEntities();
                var datosBusqueda = ctx.SP_MOD_SEDE_LISTADO(id_usuario, nombre_sede).ToList();

                if (datosBusqueda != null && datosBusqueda.Count > 0)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<SP_MOD_SEDE_LISTADO_Result, ItemSede>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<List<SP_MOD_SEDE_LISTADO_Result>, List<ItemSede>>(datosBusqueda);

                    return new DataItemSede()
                    {
                        codigoRes = HttpStatusCode.OK,
                        mensajeRes = "Se obtuvieron los datos correctamente.",
                        datos = datosMapeados.ToList()
                    };
                }
                else
                {
                    return new DataItemSede()
                    {
                        codigoRes = HttpStatusCode.NoContent,
                        mensajeRes = "No se obtuvieron datos.",
                        datos = new List<ItemSede>()
                    };
                }
            }
            catch (Exception)
            {
                return new DataItemSede()
                {
                    codigoRes = HttpStatusCode.InternalServerError,
                    mensajeRes = "Error al obtenerlos datos",
                    datos = new List<ItemSede>()
                };
            }
        }

        public DataItemSedeUnico getSede(int id_sede, string id_usuario)
        {
            try
            {
                var ctx = new BD_COMPLEJOSEntities();
                var datosBusqueda = ctx.SP_MOD_SEDE_DETALLE(id_usuario, id_sede).FirstOrDefault();

                if (datosBusqueda != null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<SP_MOD_SEDE_DETALLE_Result, ItemSede>();
                    });

                    IMapper mapper = config.CreateMapper();
                    var datosMapeados = mapper.Map<SP_MOD_SEDE_DETALLE_Result, ItemSede>(datosBusqueda);

                    return new DataItemSedeUnico()
                    {
                        codigoRes = HttpStatusCode.OK,
                        mensajeRes = "Se obtuvieron los datos correctamente.",
                        datos = datosMapeados
                    };
                }
                else
                {
                    return new DataItemSedeUnico()
                    {
                        codigoRes = HttpStatusCode.NoContent,
                        mensajeRes = "No se obtuvieron datos."                        
                    };
                }
            }
            catch (Exception)
            {
                return new DataItemSedeUnico()
                {
                    codigoRes = HttpStatusCode.InternalServerError,
                    mensajeRes = "Error al obtenerlos datos"                    
                };
            }
        }
        public DataPostSede postSede(PostSede datos, string id_usuario)
        {
            try
            {
                var ctx = new BD_COMPLEJOSEntities();
                var datosBusqueda = ctx.SP_MOD_SEDE_CREACION(id_usuario, datos.nombre_sede, datos.numero_complejos, datos.presupuesto, datos.estado)
                    .FirstOrDefault();

                if (datosBusqueda != null)
                {                   

                    return new DataPostSede()
                    {
                        codigoRes = (HttpStatusCode)datosBusqueda.codigo_respuesta,
                        mensajeRes = datosBusqueda.mensaje_usuario_respuesta,
                        id_sede = datosBusqueda.id_sede.GetValueOrDefault()
                    };
                }
                else
                {
                    return new DataPostSede()
                    {
                        codigoRes = HttpStatusCode.InternalServerError,
                        mensajeRes = "No se obtuvo respuesta de la transacción."                        
                    };
                }
            }
            catch (Exception)
            {
                return new DataPostSede()
                {
                    codigoRes = HttpStatusCode.InternalServerError,
                    mensajeRes = "Error al registrar los datos."                    
                };
            }
        }

        public DataPostSede putSede(int id_sede, PostSede datos, string id_usuario)
        {
            try
            {
                var ctx = new BD_COMPLEJOSEntities();
                var datosBusqueda = ctx.SP_MOD_SEDE_MODIFICACION(id_usuario, id_sede, datos.nombre_sede, datos.numero_complejos, datos.presupuesto, datos.estado)
                    .FirstOrDefault();

                if (datosBusqueda != null)
                {

                    return new DataPostSede()
                    {
                        codigoRes = (HttpStatusCode)datosBusqueda.codigo_respuesta,
                        mensajeRes = datosBusqueda.mensaje_usuario_respuesta                        
                    };
                }
                else
                {
                    return new DataPostSede()
                    {
                        codigoRes = HttpStatusCode.InternalServerError,
                        mensajeRes = "No se obtuvo respuesta de la transacción."
                    };
                }
            }
            catch (Exception)
            {
                return new DataPostSede()
                {
                    codigoRes = HttpStatusCode.InternalServerError,
                    mensajeRes = "Error al editar los datos."
                };
            }
        }
        public DataPostSede deleteSede(int id_sede, string id_usuario)
        {
            try
            {
                var ctx = new BD_COMPLEJOSEntities();
                var datosBusqueda = ctx.SP_MOD_SEDE_ELIMINACION(id_usuario, id_sede)
                    .FirstOrDefault();

                if (datosBusqueda != null)
                {

                    return new DataPostSede()
                    {
                        codigoRes = (HttpStatusCode)datosBusqueda.codigo_respuesta,
                        mensajeRes = datosBusqueda.mensaje_usuario_respuesta
                    };
                }
                else
                {
                    return new DataPostSede()
                    {
                        codigoRes = HttpStatusCode.InternalServerError,
                        mensajeRes = "No se obtuvo respuesta de la transacción."
                    };
                }
            }
            catch (Exception)
            {
                return new DataPostSede()
                {
                    codigoRes = HttpStatusCode.InternalServerError,
                    mensajeRes = "Error al eliminar los datos."
                };
            }
        }
    }
}
