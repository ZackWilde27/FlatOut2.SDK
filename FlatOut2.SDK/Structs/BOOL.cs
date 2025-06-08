namespace FlatOut2.SDK.Structs;

/// <summary>
/// Boolean type that is word sized (4 bytes in FlatOut's case), used for maximum speed.
/// It's set up to implicitly convert between int and bool, so you can treat it like either one
/// </summary>
public struct BOOL
{
    public int Value;

    public static implicit operator bool(BOOL val) => val.Value != 0;
    public static implicit operator int(BOOL val) => val.Value;
    public static implicit operator string(BOOL val) => val ? "TRUE" : "FALSE";

    // I feel like given the constructors, these implicit operators should be assumed. -zw
    public static implicit operator BOOL(int val) => new(val);
    public static implicit operator BOOL(bool val) => new(val);

    public BOOL(int val)
    {
        Value = val;
    }

    public BOOL(bool val)
    {
        Value = val ? 1 : 0;
    }

    public override readonly string ToString() => this;
}
