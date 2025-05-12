using System.Runtime.InteropServices;

namespace FlatOut2.SDK.Structs;

[StructLayout(LayoutKind.Explicit)]
public struct CarDatabaseItem
{
    /// <summary>
    /// if 0, you will be able to pick the car in normal race modes
    /// </summary>
    [FieldOffset(0x0)]
    public BOOL IsLocked;

    /// <summary>
    /// 1 if you own the car in career mode, otherwise 0
    /// </summary>
    [FieldOffset(0x4)]
    public BOOL IsOwned;

    /// <summary>
    /// Total amount of money spent on upgrades for this car
    /// </summary>
    [FieldOffset(0x1C)]
    public int MoneySpentOnUpgrades;
}

/// <summary>
/// Stores information about every car in the game, like whether or not you've unlocked it, or bought it
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct Garage
{
    [FieldOffset(0x4)]
    public int NumCars;

    [FieldOffset(0x8)]
    public int NumFunnyCars;

    [FieldOffset(0xC)]
    public unsafe byte* CarClass;

    /// <summary>
    /// The array is NumCars long
    /// </summary>
    [FieldOffset(0x54)]
    public unsafe CarDatabaseItem* Database;
}
