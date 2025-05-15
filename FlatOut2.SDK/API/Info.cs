using System.Runtime.InteropServices;
using FlatOut2.SDK.Enums;
using FlatOut2.SDK.Functions;
using FlatOut2.SDK.Structs;
using FlatOut2.SDK.Utilities;
using LiteDb = FlatOut2.SDK.Structs.LiteDb;

namespace FlatOut2.SDK.API;

/// <summary>
/// High level APIs that provide various bits of information about the game.
/// </summary>
public static class Info
{
    /// <summary>
    /// Multiplayer related info.
    /// </summary>
    public static class Multiplayer
    {
        /// <summary>
        /// True if the player is inside an online lobby or race.
        /// </summary>
        public static unsafe bool IsMultiplayer()
        {
            return LinkSession.Instance->VTable == (void*)LinkSession.MultiplayerVTable;
        }
        
        /// <summary>
        /// Gets the player count for multiplayer room/lobby.
        /// </summary>
        public static unsafe int GetPlayerCount()
        {
            var info = LinkSession.Instance->SessionInfo;
            if (info == null)
                return 1;

            return info->GetPlayerCount();
        }
        
        /// <summary>
        /// Gets the name of the current FlatOut lobby.
        /// </summary>
        public static unsafe string GetLobbyName()
        {
            var info = LinkSession.Instance->SessionInfo;
            if (info == null)
                return "";

            // Lobby is named after 1st player, even after host migration.
            var playerData = info->PlayerData;
            if (playerData == null)
                return "";

            var sessionPlayer = playerData->SessionPlayer;
            if (sessionPlayer == null)
                return "";
            
            // VTable Check just in case. Might resolve mysterious crashes on lobby entry.
            if (sessionPlayer->VTable != (void*)0x675C28)
                return "";
            
            return SessionPlayer.GetPlayerName(sessionPlayer);
        }

        /// <summary>
        /// Returns maximum allowed players in multiplayer session.
        /// </summary>
        public static unsafe int GetMaxPlayers() => *(int*)0x893DBC;
        
        // Technically not GameSpy player count but is synced ^^
    }

    /// <summary>
    /// General race related info,
    /// </summary>
    public static class Race
    {
        /// <summary>
        /// Gets the name of the current race track.
        /// </summary>
        public static string GetCurrentLevelName()
        {
            var id = GetCurrentLevelId();
            return Marshal.PtrToStringAnsi(Level.GetLevelName.GetWrapper()(id))!;
        }
        
        /// <summary>
        /// Gets the name of the current race track.
        /// </summary>
        public static unsafe string GetCurrentLevelLoadingScreenPath()
        {
            var id = GetCurrentLevelId();
            var screen = *LoadScreenInfo.Instance;
            if (screen == null)
                return "";

            var tuple = screen->LevelAndBackgroundImageTuples;
            for (int x = 0; x < screen->NumLevelAndBackgroundImageTuples; x++)
            {
                if (tuple->LevelId == id)
                    return tuple->BackgroundImagePath;
                
                tuple++;
            }

            return "";
        }
        
        /// <summary>
        /// Gets the name of the current race track.
        /// </summary>
        public static unsafe int GetCurrentLevelId()
        {
            var racePtr = *RaceInfo.Instance;
            return racePtr == null ? 0 : racePtr->LevelId;
        }
        
        /// <summary>
        /// Gets the id of the current car used.
        /// </summary>
        public static unsafe int GetCurrentCarId(int playerNo = 0)
        {
            var info = *RaceInfo.Instance;
            if (info == null)
                return 0;

            var playerHost = info->HostObject;
            if (playerHost == null)
                return 0;

            var sessionPlayer = playerHost->Players[playerNo];
            if (sessionPlayer == null)
                return 0;

            return sessionPlayer->CarId;
        }

        /// <summary>
        /// Gets the name of a car with specified ID.
        /// </summary>
        /// <param name="carId">The id of the car.</param>
        /// <returns>Name of the car in question.</returns>
        public static unsafe string GetCarName(int carId)
        {
            var liteDb = *LiteDb.Instance;
            if (liteDb == null)
                return "";

            var nameProperty = "Name\0"u8;
            using var tableName = new TemporaryNativeString($"FlatOut2.Cars.Car[{carId}]");
            var table = LiteDbFuncs.GetTable.GetWrapper()((nint)liteDb, (byte*)tableName.Address);
            if (table == IntPtr.Zero)
                return "";

            fixed (byte* namePropPtr = &nameProperty[0])
            {
                var name = LiteDbFuncs.GetStringFromTable.GetWrapper()(table, namePropPtr);
                if (name == (void*)IntPtr.Zero)
                    return "";

                return Marshal.PtrToStringAnsi((nint)name)!;
            }
        }

        /// <summary>
        /// Retrieves the current race mode as a readable string.
        /// </summary>
        /// <returns></returns>
        public static unsafe GameMode GetCurrentGameMode()
        {
            var racePtr = *RaceInfo.Instance;
            return racePtr == null ? GameMode.None : racePtr->GameMode;
        }
        
        /// <summary>
        /// Retrieves the current game rules as a readable string.
        /// </summary>
        public static unsafe GameRules GetCurrentGameRules()
        {
            var racePtr = *RaceInfo.Instance;
            return racePtr == null ? GameRules.Default : racePtr->GameRules;
        }
        
        /// <summary>
        /// Retrieves the current game rules as a readable string.
        /// </summary>
        public static string GetCurrentGameRulesString() => GetCurrentGameRulesString(GetCurrentGameRules());

        /// <summary>
        /// Retrieves the current game rules as a readable string.
        /// </summary>
        public static string GetCurrentGameRulesString(GameRules rules)
        {
            return rules switch
            {
                GameRules.Race => "Race",
                GameRules.Stunt => "Stunt",
                GameRules.Derby => "Derby",
                GameRules.HunterPrey => "Hunter, Prey",
                GameRules.ArcadeAdventure => "Arcade Adventure",
                GameRules.PongRace => "Pong Race",
                GameRules.TimeAttack => "Time Attack",
                GameRules.Test => "Test Mode",
                _ => "Race",
            };
        }

        /// <summary>
        /// Retrieves the current race mode as a readable string.
        /// </summary>
        public static string GetCurrentGameModeStr(GameMode mode)
        {
            return mode switch
            {
                GameMode.Career => "Career Mode",
                GameMode.TimeAttack => "Time Attack",
                GameMode.SingleRace => "Single Race",
                GameMode.InstantAction => "Instant Action",
                GameMode.CrashTestDummy => "Crash Test",
                GameMode.OnlineMultiplayer => "Online Multiplayer",
                GameMode.PartyMode => "Party Mode",
                GameMode.Developer => "Developer Mode",
                GameMode.Tournament => "Tournament",
                GameMode.Splitscreen => "Split Screen",
                GameMode.Test => "Test",
                _ => ""
            };
        }

        /// <summary>
        /// Retrieves the current race mode as a readable string.
        /// </summary>
        public static string GetCurrentGameModeStr() => GetCurrentGameModeStr(GetCurrentGameMode());

        /// <summary>
        /// Gets the ID of the stunt being performed
        /// </summary>
        public static unsafe StuntType GetCurrentStuntType()
        {
            var racePtr = *RaceInfo.Instance;
            return racePtr == null ? StuntType.None : racePtr->StuntType;
        }

        /// <summary>
        /// Gets the current stunt type as a user-friendly string with spaces
        /// </summary>
        public static string GetCurrentStuntTypeStr(StuntType stunt)
        {
            return stunt switch
            {
                StuntType.BlackDanny => "Royal Flush",
                StuntType.FieldGoal => "Field Goal",
                StuntType.HighJump => "High Jump",
                StuntType.RingOfFire => "Ring Of Fire",
                StuntType.SkiJump => "Ski Jump",
                StuntType.StoneSkipping => "Stone Skipping",
                _ => stunt.ToString()
            };
        }

        /// <summary>
        /// Gets the current stunt type as a user-friendly string with spaces
        /// </summary>
        public static string GetCurrentStuntTypeStr() => GetCurrentStuntTypeStr(GetCurrentStuntType());

        /// <summary>
        /// True if game is currently paused.
        /// </summary>
        public static unsafe bool IsPaused()
        {
            var racePtr = *RaceInfo.Instance;
            if (racePtr == null)
                return false;

            if (racePtr->HostObject == null)
                return false;

            return racePtr->HostObject->IsPaused;
        }

        /// <summary>
        /// True if player is currently inside a race. (not in menus)
        /// </summary>
        /// <returns>True if racing, else false.</returns>
        public static bool IsRacing()
        {
            return State.GetCurrentGameState() == SessionType.Race;
        }

        /// <summary>
        /// Gets current in-game timer as TimeSpan.
        /// </summary>
        /// <returns></returns>
        public static unsafe TimeSpan GetCurrentTimer()
        {
            var racePtr = *RaceInfo.Instance;
            if (racePtr == null)
                return TimeSpan.Zero;

            if (racePtr->HostObject == null)
                return TimeSpan.Zero;

            return racePtr->HostObject->TimerAsTimeSpan;
        }
    }
    
    /// <summary>
    /// General state related info,
    /// </summary>
    public static class State
    {
        /// <summary>
        /// Gets the name of the current game state.
        /// </summary>
        public static unsafe SessionType GetCurrentGameState()
        {
            var racePtr = *RaceInfo.Instance;
            return racePtr == null ? SessionType.None : racePtr->SessionType;
        }
    
        /// <summary>
        /// Gets the name of the current game state as a string.
        /// </summary>
        public static string GetCurrentGameStateString(SessionType sessionType)
        {
            return sessionType switch
            {
                SessionType.Menu => "In Menu",
                SessionType.Race => "In Race",
                _ => "Unknown State"
            };
        }
        
        /// <summary>
        /// Gets the name of the current game state as a string.
        /// </summary>
        public static string GetCurrentGameStateString() => GetCurrentGameStateString(GetCurrentGameState());
    }

    /// <summary>
    /// Information about the player's profile
    /// </summary>
    public static class Profile
    {
        /// <summary>
        /// Returns whether or not autosave is enabled (I don't think there's a way to turn it off, so it's always true)
        /// </summary>
        public static unsafe bool IsAutosaveEnabled()
        {
            var racePtr = *RaceInfo.Instance;
            return racePtr == null ? false : racePtr->PlayerProfile.IsAutosaveEnabled;
        }

        /// <summary>
        /// Gets the current profile's slot number
        /// </summary>
        public static unsafe int GetAutosaveSlot()
        {
            var racePtr = *RaceInfo.Instance;
            return racePtr == null ? -1 : racePtr->PlayerProfile.AutosaveSlot;
        }
    }

    /// <summary>
    /// Reads the state of the keyboard
    /// </summary>
    public static class Controller
    {
        /// <summary>
        /// Pointer to the global key state array.
        /// It's an array of bytes, 256 large.
        /// </summary>
        // In binary it can be thought of like D--- ---P
        // Where D is whether or not the key is currently down
        // P is parity, it swaps between 0 and 1 each time you press the key
        private static readonly unsafe byte* KeyState = (byte*)0x008D7E60;

        /// <summary>
        /// Stores the previous key parity to check for new presses
        /// </summary>
        private static readonly byte[] ParityArray = new byte[256];

        /// <summary>
        /// Returns whether or not a particular key is down
        /// </summary>
        /// <param name="key">The key to check for</param>
        public static bool IsKeyHeld(KeyboardKeys key) => IsKeyHeld((byte)key);

        /// <summary>
        /// Returns whether or not a particular key is down
        /// </summary>
        /// <param name="index">Index into the KeyState array</param>
        public static unsafe bool IsKeyHeld(byte index) => (KeyState[index] & 0x80) != 0;

        /// <summary>
        /// Returns whether or not a particular key has just been pressed.
        /// It keeps track of parity itself, which might not be accurate if it's been long enough since the last call
        /// </summary>
        /// <param name="key">The key to check for</param>
        public static bool IsKeyPressed(KeyboardKeys key) => IsKeyPressed((byte)key);

        /// <summary>
        /// Returns whether or not a particular key has just been pressed.
        /// It keeps track of parity itself, which might not be accurate if it's been long enough since the last call
        /// </summary>
        /// <param name="index">Index into the KeyState array</param>
        public static unsafe bool IsKeyPressed(byte index)
        {
            byte parity = (byte)(KeyState[index] & 1);
            bool pressed = IsKeyHeld(index) && parity != ParityArray[index];
            ParityArray[index] = parity;
            return pressed;
        }

        /// <summary>
        /// True if any keys are currently held down
        /// </summary>
        public static bool AnyKeysDown()
        {
            for (int i = 0; i < 256; i++)
            {
                if (IsKeyHeld((byte)i))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// True if any keys have just been pressed
        /// </summary>
        public static bool AnyKeysPressed()
        {
            for (int i = 0; i < 256; i++)
            {
                if (IsKeyPressed((byte)i))
                    return true;
            }
            return false;
        }
    }
}