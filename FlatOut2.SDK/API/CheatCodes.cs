using FlatOut2.SDK.Structs;
using Reloaded.Hooks.Definitions;
using Reloaded.Hooks.Definitions.X86;
using System.Runtime.InteropServices;
using static Reloaded.Hooks.Definitions.X86.FunctionAttribute;

namespace FlatOut2.SDK.API;

public unsafe class CheatCodeManager
{
    [Function(new Register[] { Register.ebx, Register.esi }, Register.eax, StackCleanup.Callee)]
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int CheckForCheatCodesPtr(char* unaff_EBX, PlayerProfile* profile_ESI);

    private IHook<CheckForCheatCodesPtr>? CheatCodeHook;

    public delegate void CheatCodeCallback(PlayerProfile* profile);

    private struct NewCheatCode
    {
        public string Code;
        public CheatCodeCallback OnActivation;

        public NewCheatCode(string code, CheatCodeCallback activationFunction)
        {
            Code = code;
            OnActivation = activationFunction;
        }
    }

    private readonly List<NewCheatCode> NewCheats = new();

    private static string CharPtrToString(char* str)
    {
        string newString = "";
        while (*str != '\0')
        {
            newString += *str;
            str += 1;
        }
        return newString;
    }

    [Function(new Register[] { Register.ebx, Register.esi }, Register.eax, StackCleanup.Callee)]
    private int NewCheckForCheatCodes(char* codePtr, PlayerProfile* profile)
    {
        string code = CharPtrToString(codePtr);

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


    public void Add(string code, CheatCodeCallback activationFunction)
    {
        NewCheats.Add(new(code, activationFunction));
    }


    private static void ExampleCheatCode(PlayerProfile* profile)
    {
        profile->Money = 27272727;
    }

    public void Hook(IReloadedHooks hooks)
    {
        CheatCodeHook = hooks.CreateHook<CheckForCheatCodesPtr>(NewCheckForCheatCodes, 0x00476570).Activate();
        Add("ZACK", ExampleCheatCode);
    }
}
