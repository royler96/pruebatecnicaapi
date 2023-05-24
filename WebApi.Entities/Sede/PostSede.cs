using System;
using System.Collections.Generic;
using System.Net;

namespace WebApi.Entities.Sede
{
    public class DataPostSede
    {
        public HttpStatusCode codigoRes { get; set; }
        public string mensajeRes { get; set; }
        public int id_sede { get; set; }
    }
    public class PostSede
    {
        public string nombre_sede { get; set; }
        public int numero_complejos { get; set; }
        public decimal presupuesto { get; set; }
        public bool estado { get; set; }        
    }
}
