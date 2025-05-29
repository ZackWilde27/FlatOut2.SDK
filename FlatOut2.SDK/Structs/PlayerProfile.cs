using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;

namespace FlatOut2.SDK.Structs;

/// <summary>
/// Information about the player's currently loaded in profile/savegame.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public unsafe struct PlayerProfile
{
    [FieldOffset(0x3C4)]
    public Garage Garage;

    [FieldOffset(0xE34)]
    public fixed byte ProfileName[16];
    
    [FieldOffset(0xE58)]
    public int Money;

    /// <summary>
    /// The car you currently have selected in career mode
    /// </summary>
    [FieldOffset(0xE5E)]
    public byte CareerCar;

    [FieldOffset(0xEDC)]
    public BOOL AutosaveEnabled;

    /// <summary>
    /// The profile's number - 1
    /// </summary>
    [FieldOffset(0xEE0)]
    public int AutosaveSlot;

    /// <summary>
    /// Total amount of money spent on cars in career mode
    /// </summary>
    [FieldOffset(0xEF8)]
    public int MoneySpentCars;

    /// <summary>
    /// Total amount of money spent on upgrades in career mode
    /// </summary>
    [FieldOffset(0xEFC)]
    public int MoneySpentUpgrades;

    /// <summary>
    /// In careeer mode specifically
    /// </summary>
    [FieldOffset(0xF18)]
    public int NumTimesRagdolled;

    /// <summary>
    /// In careeer mode specifically
    /// </summary>
    [FieldOffset(0xF1C)]
    public float TotalRagdollFlyDistance;
}