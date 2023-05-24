using WebApi.Entities.Sede;

namespace WebApi.Business.Contratos
{
    public interface ISedeBO
    {
        DataItemSede getAllSedes(string nombre_sede, string id_usuario);
    }
}
