﻿using WebApi.Entities.Sede;

namespace WebApi.DataAccess.Contratos
{
    public interface ISedeDO
    {
        DataItemSede getAllSedes(string nombre_sede, string id_usuario);
    }
}
