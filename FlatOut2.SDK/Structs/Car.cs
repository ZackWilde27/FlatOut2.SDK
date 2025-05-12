using System.Runtime.InteropServices;
using System.Numerics;

namespace FlatOut2.SDK.Structs;

/// <summary>
/// Provides information about an individual vehicle.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public struct Car
{
    /// <summary>
    /// The W component is not relevant, it's for padding
    /// </summary>
    [FieldOffset(0x1E0)]
    public Vector4 Position;

    [FieldOffset(0x270)]
    public Quaternion Rotation;

    /// <summary>
    /// The W component is not relevant, it's for padding
    /// </summary>
    [FieldOffset(0x280)]
    public Vector4 Velocity;

    /// <summary>
    /// The W component is not relevant, it's for padding
    /// </summary>
    [FieldOffset(0x290)]
    public Vector4 RotationalVelocity;

    /// <summary>
    /// The current level of nitro, from 0-5
    /// </summary>
    [FieldOffset(0x5CC)]
    public float Nitro;

    /// <summary>
    /// 1 if ragdolled, otherwise 0
    /// </summary>
    [FieldOffset(0x2AB0)]
    public BOOL RagdollState;

    /// <summary>
    /// Determines things like whose portrait shows up when you are nearby
    /// </summary>
    [FieldOffset(0x463C)]
    public unsafe Player* Owner;

    /// <summary>
    /// The car's total damage, from 0-1.
    /// </summary>
    [FieldOffset(0x6AA0)]
    public float Damage;
}