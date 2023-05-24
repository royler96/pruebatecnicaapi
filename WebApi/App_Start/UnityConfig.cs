using System.Web.Http;
using Unity;
using Unity.WebApi;
using WebApi.Business.Contratos;
using WebApi.Business.Implementaciones;
using WebApi.DataAccess.Contratos;
using WebApi.DataAccess.Implementaciones;

namespace WebApi
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            container.RegisterType<ISedeBO, SedeBO>();
            container.RegisterType<ISedeDO, SedeDO>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}