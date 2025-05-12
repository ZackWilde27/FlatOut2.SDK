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

    /// <summary>
    /// The total distance driven in career mode
    /// </summary>
    [FieldOffset(0x424)]
    public float CareerOdometer;

    [FieldOffset(0xE34)]
    public fixed byte ProfileName[16];
    
    [FieldOffset(0xE58)]
    public int Money;

    /// <summary>
    /// The car you currently have selected in career mode
    /// </summary>
    [FieldOffset(0xE5E)]
    public byte CareerCar;

    /// <summary>
    /// The index of the active cup in the current car class
    /// </summary>
    [FieldOffset(0xE60)]
    public byte ActiveEvent;

    [FieldOffset(0xED6)]
    public byte PreviousRaceClass;

    [FieldOffset(0xED8)]
    public byte PreviousRaceIndex;

    [FieldOffset(0xEDC)]
    public BOOL IsAutosaveEnabled;

    /// <summary>
    /// The profile's number - 1
    /// </summary>
    [FieldOffset(0xEE0)]
    public int AutosaveSlot;

    /// <summary>
    /// Total money spent on cars in career mode
    /// </summary>
    [FieldOffset(0xEF8)]
    public int MoneySpentCars;

    /// <summary>
    /// Total money spent on upgrades in career mode
    /// </summary>
    [FieldOffset(0xEFC)]
    public int MoneySpentUpgrades;

    [FieldOffset(0xF18)]
    public int NumTimesRagdolled;

    [FieldOffset(0xF1C)]
    public float TotalRagdollFlyDistance;

    [FieldOffset(0x1088)]
    public byte* LevelPositions;
}