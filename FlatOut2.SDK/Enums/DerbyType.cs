namespace FlatOut2.SDK.Enums;

/// <summary>
/// Mode used within Derby.
/// All of them are unused except for Wrecking
/// </summary>
public enum DerbyType
{
    /// <summary>
    /// Indicates the current game mode is not derby
    /// </summary>
    None,

    /// <summary>
    /// Same as Wrecking, except when you win, you are labelled as 'Survived' on the results page
    /// </summary>
    LastManStanding,

    /// <summary>
    /// The game mode it always plays normally
    /// </summary>
    Wrecking,

    /// <summary>
    /// Same as Wrecking, except there's a bug where the game doesn't end when you are wrecked
    /// </summary>
    Frag,

    /// <summary>
    /// Crashes the game if you play it
    /// </summary>
    End
}