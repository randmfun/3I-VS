using Castle.MicroKernel.Registration;
using SCD_UVSS.Dal;
using SCD_UVSS.Dal.DBProviders;
using SCD_UVSS.View;

namespace SCD_UVSS.Registers
{
    public class Installers : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container,
            Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container
                .Register(Component.For<IDatabaseProvider>().ImplementedBy<MySqlDatabaseProvider>().LifestyleTransient())
                .Register(Component.For<DataAccessLayer>().LifestyleSingleton())

                .Register(Component.For<MainCameraView>().LifestyleSingleton())
                .Register(Component.For<SearchView>().LifestyleSingleton())
                .Register(Component.For<GateView>().LifestyleSingleton())
                .Register(Component.For<BlackListView>().LifestyleSingleton());
        }
    }
}
