using Reloaded.Hooks.Definitions;
using static FlatOut2.SDK.Functions.HelperFuncs;

namespace FlatOut2.SDK.API;

public static class Helpers
{
    private static VoidFunction? UserPerFrame = null;
    private static IHook<RenderSkyPtr>? RenderSkyHook = null;

    /// <summary>
    /// Replacement RenderSky function
    /// </summary>
    /// <param name="in_EAX">Unknown</param>
    /// <param name="pEnvironment">Pointer to the BVISUAL::Environment struct/class</param>
    /// <param name="param_2">Unknown</param>
    private static unsafe void NewRenderSky(int in_EAX, void* pEnvironment, int param_2)
    {
        UserPerFrame?.Invoke();
        RenderSkyHook!.OriginalFunction(in_EAX, pEnvironment, param_2);
    }

    /// <summary>
    /// Helper function that hooks RenderSky to call your function on every frame (of gameplay, not in the menus).
    /// </summary>
    /// <param name="toBeCalledPerFrame">Takes no parameters and returns void</param>
    public static unsafe void HookPerFrame(VoidFunction toBeCalledPerFrame)
    {
        UserPerFrame = toBeCalledPerFrame;
        RenderSkyHook = SDK.ReloadedHooks.CreateHook<RenderSkyPtr>(NewRenderSky, 0x00592470).Activate();
    }
}
