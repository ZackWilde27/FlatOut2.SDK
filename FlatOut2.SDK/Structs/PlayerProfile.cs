using System.Runtime.InteropServices;

namespace FlatOut2.SDK.Structs;

/// <summary>
/// Information about the player's currently loaded in profile/savegame.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public unsafe struct PlayerProfile
{
    [FieldOffset(0x3C8)]
    public int NumCars;
    
    [FieldOffset(0x3D0)]
    public byte* CarClass;

    /// <summary>
    /// The total distance driven in career mode
    /// </summary>
    [FieldOffset(0x424)]
    public float CareerOdometer;

    [FieldOffset(0xE34)]
    public fixed byte ProfileName[16];
    
    [FieldOffset(0xE58)]
    public int Money;
    
    [FieldOffset(0xE5E)]
    public byte CareerCar;
}