namespace FlatOut2.SDK.Enums;

/// <summary>
/// Current set of in-race rules applied.
/// </summary>
public enum GameRules
{
    Default,
    Race,
    Stunt,
    Derby,

    // HunterPrey is a 4-minute game of tag, whoever is the hunter is 'it' and they must hit someone else to make them the hunter
    // The score for each player is decided by 2 things: Whether or not they've ever been the hunter, and how long they've spent being the hunter
    // Unfortunately the AIs don't care and will just drive normally, the only way to make it interesting in single player is in derby
    HunterPrey,

    // ArcadeAdventure crashes the game
    ArcadeAdventure,

    // PongRace is the same as Race
    PongRace,

    // TimeAttack crashes the game
    TimeAttack,

    // That mode in career where you can try out a car
    // With these rules, no other cars will spawn, and you get to drive around freely (well, except for out of bounds, you'll still get reset)
    Test,
}