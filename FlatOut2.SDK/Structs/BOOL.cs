namespace FlatOut2.SDK.Structs;

/// <summary>
/// Boolean type that is word sized (4 bytes in FlatOut's case), used for maximum speed.
/// It's set up to implicity convert between int and bool, so you can treat it like either one
/// </summary>
public struct BOOL
{
    public int value;

    public static implicit operator bool(BOOL val) => val.value != 0;
    public static implicit operator int(BOOL val) => val.value;
    public static implicit operator string(BOOL val) => val ? "TRUE" : "FALSE";

    // I feel like if I have a constructor with this type, this implicit operator should be assumed.
    public static implicit operator BOOL(int val) => new(val);
    public static implicit operator BOOL(bool val) => new(val);

    public BOOL(int val)
    {
        value = val;
    }

    public BOOL(bool val)
    {
        value = val ? 1 : 0;
    }

    public override readonly string ToString() => this ? "TRUE" : "FALSE";
}
