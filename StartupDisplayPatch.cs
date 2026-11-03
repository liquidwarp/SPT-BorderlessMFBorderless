using System;
using System.Reflection;
using System.Threading.Tasks;
using EFT.Settings.Graphics;
using SPT.Reflection.Patching;
using UnityEngine;

namespace Liquidwarp.BorderlessMFBorderless;

internal class StartupDisplayPatch : ModulePatch
{
    protected override MethodBase GetTargetMethod() =>
        typeof(GraphicsSettingsController).GetMethod(nameof(GraphicsSettingsController.CreateBindingsAsync));

    [PatchPostfix]
    private static void Postfix(GraphicsSettingsController __instance, ref Task __result) =>
        __result = ApplyBorderless(__instance, __result);

    private static async Task ApplyBorderless(GraphicsSettingsController controller, Task initialization)
    {
        await initialization;

        try
        {
            var settings = controller.Group.GetStoredDisplaySettings(FullScreenMode.FullScreenWindow);
            await controller.Group.DisplaySettings.SetValue(settings);
            Logger.LogInfo("Applied borderless display mode at startup.");
        }
        catch (Exception exception)
        {
            Logger.LogError($"Could not apply borderless display mode: {exception}");
        }
    }
}
