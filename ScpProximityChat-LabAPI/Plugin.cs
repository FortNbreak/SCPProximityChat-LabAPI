using LabApi.Loader.Features.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabApi.Events.CustomHandlers;
using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins.Enums;
using HarmonyLib;

namespace ScpProximityChat_LabAPI
{
    public class ScpProximityChat : Plugin<Config>
    {
        public static ScpProximityChat Instance { get; private set; }
        public Events Events { get; } = new Events();
        public override string Name { get; } = "ScpProximityChat";
        public override string Description { get; } = "Makes SCPs able to talk inside the proximity chat.";
        public override string Author { get; } = "FortNbreak";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredApiVersion { get; } = new Version(LabApiProperties.CompiledVersion);
        public override string ConfigFileName { get; set; } = "scpproximitychat.yml";
        private static readonly Harmony HarmonyPatcher = new("scpproximitymodule.fortnbreak.com");
        public static Config SharedConfig { get; private set; }

        public override void Enable()
        {
            if (!base.Config.Enabled)
            {
                Logger.Info("Plugin is set to not start, change the configuration file if this is a mistake");
                return;
            }

            Logger.Info("Starting plugin...");

            ScpProximityChat.Instance = this;

            Logger.Info("Loading config.");

            SharedConfig = base.Config;

            Logger.Info("Registering events...");

            CustomHandlersManager.RegisterEventsHandler<Events>(this.Events);

            Logger.Info("Applying Harmony patches.");

            HarmonyPatcher.PatchAll();

            Logger.Info("Plugin has been enabled.");
        }

        public override void Disable()
        {
            Logger.Info("Stopping plugin...");

            ScpProximityChat.Instance = null;

            Logger.Info("Loading config.");

            SharedConfig = null;

            Logger.Info("Unregistering events...");

            CustomHandlersManager.UnregisterEventsHandler<Events>(this.Events);

            Logger.Info("Unapplying Harmony patches.");

            HarmonyPatcher.UnpatchAll();

            Logger.Info("Plugin has been disabled.");
        }
    }
}
