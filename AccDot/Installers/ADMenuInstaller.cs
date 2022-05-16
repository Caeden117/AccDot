using AccDot.UI;
using Zenject;

namespace AccDot.Installers
{
    class ADMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<DotTab>().FromNewComponentOnRoot().AsSingle();
        }
    }
}
