using AutoMapper;
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
    }
}
