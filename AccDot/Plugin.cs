using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Loader;
using SiraUtil.Zenject;
using AccDot.Installers;
using IPALogger = IPA.Logging.Logger;
using Conf = IPA.Config.Config;

namespace AccDot
{
    [Plugin(RuntimeOptions.DynamicInit), NoEnableDisable]
    public class Plugin
    {
        // logger? i hardly know her!!!
        [Init]
        public Plugin(IPALogger logger, Conf conf, Zenjector zenjector)
        {
            Config config = conf.Generated<Config>();

            zenjector.UseLogger(logger);
            zenjector.Install<ADAppInstaller>(Location.App, config);
            zenjector.Install<ADWarmupInstaller, ShaderWarmupSceneSetup>();
            zenjector.Install<ADMenuInstaller>(Location.Menu);
            zenjector.Install<ADGameInstaller>(Location.Player);
        }
    }
}
