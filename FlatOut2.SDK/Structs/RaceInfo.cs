using System.Runtime.InteropServices;
using FlatOut2.SDK.Enums;

namespace FlatOut2.SDK.Structs;

/// <summary>
/// Information about the currently ongoing race.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct RaceInfo
{
    /// <summary>
    /// Pointer to game's instance.
    /// </summary>
    public static readonly unsafe RaceInfo** Instance = (RaceInfo**)0x8E8410;
    
    [FieldOffset(0x458)]
    public SessionType SessionType;
    
    [FieldOffset(0x464)]
    public GameMode GameMode;

    /// <summary>
    /// Overrides the derby type when loading into a map, unless it's DerbyType.None
    /// </summary>
    [FieldOffset(0x46C)]
    public DerbyType DerbyTypeOverride;
    
    [FieldOffset(0x480)]
    public int LevelId;

    [FieldOffset(0x488)]
    public int NumLaps;
    
    [FieldOffset(0x4AC)]
    public GameRules GameRules;

    /// <summary>
    /// The type of the stunt being performed
    /// </summary>
    [FieldOffset(0x4B0)]
    public StuntType StuntType;

    /// <summary>
    /// The type of derby currently being played
    /// </summary>
    [FieldOffset(0x4B4)]
    public DerbyType DerbyType;

    /// <summary>
    /// 1 if nitro refills over time, otherwise 0
    /// </summary>
    [FieldOffset(0x4BC)]
    public int NitroRegen;

    [FieldOffset(0x9B8)]
    public unsafe PlayerHost* HostObject;
    
    [FieldOffset(0x9C8)]
    public unsafe void* MenuInterface;
    
    [FieldOffset(0xFE0)]
    public PlayerProfile PlayerProfile;
}