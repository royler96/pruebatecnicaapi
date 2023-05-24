using WebApi.Entities.Sede;

namespace WebApi.DataAccess.Contratos
{
    public interface ISedeDO
    {
        DataItemSede getAllSedes(string nombre_sede, string id_usuario);
        DataPostSede postSede(PostSede datos, string id_usuario);
        DataPostSede putSede(int id_sede, PostSede datos, string id_usuario);
    }
}
