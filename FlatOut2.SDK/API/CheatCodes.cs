using FlatOut2.SDK.Structs;
using Reloaded.Hooks.Definitions;
using System.Runtime.InteropServices;
using static FlatOut2.SDK.Functions.CheatCodeFuncs;

namespace FlatOut2.SDK.API;

/// <summary>
/// Handles hooking, storing, and checking for custom cheat codes
/// </summary>
public static unsafe class CheatCodeManager
{
    /// <summary>
    /// Hook for the game's cheat code check
    /// </summary>
    private static IHook<CheckForCheatCodesPtr>? CheatCodeHook = null;

    /// <summary>
    /// Stores all the custom cheats
    /// </summary>
    private static readonly List<NewCheatCode> NewCheats = new();

    /// <summary>
    /// The replacement function for checking cheat codes
    /// </summary>
    /// <param name="codePtr">The pointer to the entered code</param>
    /// <param name="profile">The profile to apply the cheat to</param>
    /// <returns>1 if the cheat was valid, 0 if not</returns>
    private static int NewCheckForCheatCodes(char* codePtr, PlayerProfile* profile)
    {
        string code = Marshal.PtrToStringUni((nint)codePtr)!;

        foreach (var cheat in NewCheats)
        {
            if (cheat.Code == code)
            {
                cheat.OnActivation(profile);
                return 1;
            }
        }

        return CheatCodeHook!.OriginalFunction(codePtr, profile);
    }

    /// <summary>
    /// An example of a cheat code callback
    /// </summary>
    /// <param name="profile">The profile to apply the cheat to</param>
    private static void ExampleCheatCode(PlayerProfile* profile)
    {
        profile->Money = 27272727;
    }

    /// <summary>
    /// Adds a new cheat code. Automatically hooks if this is the first call
    /// </summary>
    /// <param name="code">The new code to check for. Should be all caps given the on-screen keyboard's limitations</param>
    /// <param name="activationFunction">The function to call when the cheat is entered. It takes a PlayerProfile* parameter and returns void</param>
    public static void Add(string code, CheatCodeCallback activationFunction)
    {
        if (CheatCodeHook == null)
        {
            CheatCodeHook = SDK.ReloadedHooks.CreateHook<CheckForCheatCodesPtr>(NewCheckForCheatCodes, 0x00476570).Activate();
            Add("ZACK", ExampleCheatCode);
        }

        NewCheats.Add(new(code, activationFunction));
    }
}

/// <summary>
/// Struct for storing both the code and callback together
/// </summary>
struct NewCheatCode
{
    /// <summary>
    /// The code to check for
    /// </summary>
    public string Code;

    /// <summary>
    /// The function to call when the code is entered
    /// </summary>
    public CheatCodeCallback OnActivation;

    public NewCheatCode(string code, CheatCodeCallback activationFunction)
    {
        Code = code;
        OnActivation = activationFunction;
    }
}