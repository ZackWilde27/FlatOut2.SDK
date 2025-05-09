using System.Runtime.InteropServices;
using Reloaded.Hooks.Definitions.X86;
using static Reloaded.Hooks.Definitions.X86.FunctionAttribute;

namespace FlatOut2.SDK.Functions;

/// <summary>
/// Function definitions for the Helpers class
/// </summary>
public static class HelperFuncs
{
    /// <summary>
    /// Internal function type needed for hooking RenderSky
    /// </summary>
    /// <param name="in_EAX">Unknown</param>
    /// <param name="pEnvironment">Pointer to the BVISUAL::Environment struct</param>
    /// <param name="param_2">Unknown</param>
    [Function(new Register[] { Register.eax }, Register.eax, StackCleanup.Callee)]
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public unsafe delegate void RenderSkyPtr(int in_EAX, void* pEnvironment, int param_2);

    /// <summary>
    /// Common delegate for a function that takes no arguments and returns void
    /// </summary>
    public delegate void VoidFunction();
}

