using BepInEx;
using UnityEngine;

namespace Liquidwarp.BorderlessMFBorderless;

[BepInPlugin(MyModInfo.MOD_GUID, MyModInfo.MOD_NAME, MyModInfo.MOD_VERSION)]
[BepInDependency("com.SPT.custom", MyModInfo.CLIENT_SPT_VERSION)]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        if (Application.isBatchMode)
            return;

        new StartupDisplayPatch().Enable();
    }
}
