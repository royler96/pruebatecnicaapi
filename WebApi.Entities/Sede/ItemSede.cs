using System;
using System.Collections.Generic;
using System.Net;

namespace WebApi.Entities.Sede
{
    public class DataItemSede
    {
        public HttpStatusCode codigoRes { get; set; }
        public string mensajeRes { get; set; }
        public List<ItemSede> datos { get; set; }
    }
    public class ItemSede
    {
        public int id_sede { get; set; }
        public string cod_sede { get; set; }
        public string nombre_sede { get; set; }
        public Nullable<int> numero_complejos { get; set; }
        public Nullable<decimal> presupuesto { get; set; }
        public Nullable<bool> estado { get; set; }
        public string fecha_actualizacion { get; set; }
    }
}
