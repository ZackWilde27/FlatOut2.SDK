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
    
    [FieldOffset(0x480)]
    public int LevelId;

    [FieldOffset(0x488)]
    public int NumLaps;
    
    [FieldOffset(0x4AC)]
    public GameRules GameRules;

    /// <summary>
    /// The ID of the stunt being performed
    /// </summary>
    [FieldOffset(0x4B0)]
    public StuntID StuntID;
    
    [FieldOffset(0x9B8)]
    public unsafe PlayerHost* HostObject;
    
    [FieldOffset(0x9C8)]
    public unsafe void* MenuInterface;
    
    [FieldOffset(0xFE0)]
    public PlayerProfile PlayerProfile;

    /// <summary>
    /// The total distance driven in career mode
    /// </summary>
    [FieldOffset(0x1404)]
    public float CareerOdometer;
}