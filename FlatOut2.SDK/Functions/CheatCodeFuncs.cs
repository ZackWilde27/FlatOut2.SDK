using FlatOut2.SDK.Structs;
using System.Runtime.InteropServices;
using Reloaded.Hooks.Definitions.X86;
using static Reloaded.Hooks.Definitions.X86.FunctionAttribute;

namespace FlatOut2.SDK.Functions;

/// <summary>
/// Function definitions for hooking and activating cheat codes.
/// </summary>
public static unsafe class CheatCodeFuncs
{
    /// <summary>
    /// Internal definition for hooking the game's check
    /// </summary>
    /// <param name="codePtr">The pointer to the entered code</param>
    /// <param name="profile">Pointer to the current profile</param>
    /// <returns>1 if the cheat was valid, otherwise 0</returns>
    [Function(new Register[] { Register.ebx, Register.esi }, Register.eax, StackCleanup.Callee)]
    [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
    public delegate BOOL CheckForCheatCodesPtr(string codePtr, PlayerProfile* profile);

    /// <summary>
    /// The callback function for activating a custom cheat code
    /// </summary>
    /// <param name="profile">Pointer to the current profile</param>
    public delegate void CheatCodeCallback(PlayerProfile* profile);
}