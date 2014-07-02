using System.Collections.Generic;

namespace Storwins_Mod.Core.Models
{
    public enum ModTypes
    {
        Crosshair,
        DamageIndicator,
        DamagePanel,
        Garage,
        GarageClock,
        GunConstaints,
        Icons,
        HitZoneSkins,
        Misc,
        Minimap,
        OverTargetMarkers,
        ServersideCrosshair,
        SessionStatistics,
        Sound,
        Skins,
        Techtree,
        Zoom,
        Xvm,
        XvmConfig,
        XvmSixtSenseIcon,
        XvmSixtSenseSound
    }

    public class CanInstallMultiple
    {
        public List<ModTypes> Allowed = new List<ModTypes>
        {
            ModTypes.Skins, ModTypes.Sound, ModTypes.Misc
        };
        public List<ModTypes> CanBeAllowed = new List<ModTypes>
        {
            ModTypes.Icons,ModTypes.Zoom
        };
    }
}