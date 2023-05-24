using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Business.Contratos;
using WebApi.DataAccess.Contratos;
using WebApi.Entities.Sede;

namespace WebApi.Business.Implementaciones
{
    public class SedeBO : ISedeBO
    {
        private ISedeDO _sedeDO;
        public SedeBO(ISedeDO sedeDO)
        {
            _sedeDO = sedeDO;
        }

        public DataItemSede getAllSedes(string nombre_sede, string id_usuario)
        {
            return _sedeDO.getAllSedes(nombre_sede, id_usuario);
        }

        public DataPostSede postSede(PostSede datos, string id_usuario)
        {
            return _sedeDO.postSede(datos, id_usuario);
        }
    }
}
