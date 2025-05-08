using System.Runtime.InteropServices;
using Reloaded.Hooks.Definitions.X86;
using static Reloaded.Hooks.Definitions.X86.FunctionAttribute;

namespace FlatOut2.SDK.Functions;

public static class HelperFuncs
{
    [Function(new Register[] { Register.eax }, Register.eax, StackCleanup.Callee)]
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
    public unsafe delegate void RenderSkyPtr(int in_EAX, void* pEnvironment, int param_2);

    public delegate void VoidFunction();
}

