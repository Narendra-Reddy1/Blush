using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    public class GlobalVariables
    {
        /// <summary>
        /// This is the vector which is the starting position of the player on a level.<br></br>
        /// This should be updated to relevant level start position whenever new level loads.
        /// </summary>
        public static Vector3 STARTING_POINT;
        public static bool START_POINT_HAS_INVERSE_GRAVITY;
        public static int HighestUnlockedLevel = 1;
        public static int CurrentSelectedLevelIndex;
        public const int TotalLevelsInGame = 100;

        public static string lastPurchasedProductId;
        public static string lastSelectedProductId;

        public static int unlockGoldenProgressBarAtLevel = 16;
        public static PlayerState playerState = PlayerState.Alive;
        public static void UpdateCurrentSelectedLevelIndex(int levelIndex)
        {
            CurrentSelectedLevelIndex = levelIndex;
        }

        #region Commented Code

        //public static int CurrentSelectedLevelIndex = 0;//This is the level that the user click on in the Level Map
        // public static bool isAdressableInitlized;
        // public static string HighestUnlockedLevelKey = "1_0"; //Currently this game only needs the highest level index. Set -1 to enable testing from GameplayManager
        //public static string CurrentDailyPuzzleKey = "1_1";
        // public static string CurrentSelectedLevelKey;
        // public static States CurrentState = States.Home; //Holds the current state of the game
        // public static GameType SelectedGameType;
        //  public static string goldenTileImageId;//This image id will be set as Golden Tile
        // public static TabType currentTab;
        // public static bool ifPreviousGameDailyPuzzle = false;

        //  public static string selectedThemeId = string.Empty;
        //  public static string defaultThemeId = "bg_1";
        //  public static Dictionary<string, ThemeItem> unlockedThemeBgHolder = new Dictionary<string, ThemeItem>();
        //   public static Dictionary<string, Sprite> unlockedTileHolder = new Dictionary<string, Sprite>();

        // public static bool navigateToDecorScreen = false;


        //Debug Variables
        //public static bool canSliderDebugShuffleDiff = false;
        //public static bool canSliderDebugTileDiff = false;
        //public static int shuffleDifficulty_Debug = 1;
        //public static int tileDifficulty_Debug = 3;
        //public static int shuffleDiffSplitValue = 0;

        //room decor
        //public static int colorCount = 0;
        //public static int lotusSunkCounter = 0;
        //public static bool isClickedonCustomisePlace = false;

        //Purchase
        //  public static Bundletype lastPurchasedBundleType;
        // public static CoinPackType? lastPurchaseCoinPackType;


        //public static bool enableUIEffects = true;

        //public static string noOfAttemptsToCompleteLevel //This string will have the level and attempts made to complete. eg: 20_2 .
        //{
        //    set
        //    {
        //        PlayerPrefWrapper.SetPlayerPrefsAsString(PlayerPrefWrapper.PlayerPrefKeys.no_of_attempts_to_complete_level, value);
        //    }
        //    get
        //    {
        //        return PlayerPrefWrapper.GetPlayerPrefsString(PlayerPrefWrapper.PlayerPrefKeys.no_of_attempts_to_complete_level,
        //            CurrentSelectedLevelIndex.ToString() + "_0");
        //    }
        //}


        //public static GameResult gameResult;

        //public static string installTime
        //{
        //    private set
        //    {

        //    }
        //    get
        //    {
        //        return PlayerPrefWrapper.GetPlayerPrefsString(PlayerPrefWrapper.PlayerPrefKeys.install_time);
        //    }
        //}
        //public static int DaysSinceInstall
        //{
        //    set
        //    {
        //        DaysSinceInstall = value;
        //    }
        //    get { return GetDaysSinceInstall(); }
        //}

        //  public static bool pDebugAutoWin { get; private set; } = false;

        //  public static string TodayDate = System.DateTime.Now.ToString().Split(" ")[0];

        //public static int GetDaysSinceInstall()
        //{
        //    CheckForDayChange();
        //    return PlayerPrefWrapper.HasKey(PlayerPrefWrapper.PlayerPrefKeys.day_since_install) ? PlayerPrefWrapper.GetPlayerPrefsInt(PlayerPrefWrapper.PlayerPrefKeys.day_since_install) : 0;
        //}

        //public static void UpdateCurrentSelectedLevelKey(string levelKey)
        //{
        //    //   CurrentSelectedLevelKey = levelKey;
        //}


        //public static void UpdateHighestUnlockedLevelIndex()
        //{
        //    HighestUnlockedLevel = GetFirstPartOfTheLevelKey(HighestUnlockedLevelKey);
        //}
        //public static void CheckForDayChange()
        //{
        //    //TodayDate = System.DateTime.Now.ToString().Split(" ")[0];
        //    int days;
        //    if (!PlayerPrefWrapper.HasKey(PlayerPrefWrapper.PlayerPrefKeys.day_since_install))
        //    {
        //        days = 0;
        //        PlayerPrefWrapper.SetPlayerPrefsInt(PlayerPrefWrapper.PlayerPrefKeys.day_since_install, days);
        //        PlayerPrefWrapper.SetPlayerPrefsAsString(PlayerPrefWrapper.PlayerPrefKeys.today_date, TodayDate);
        //        return;
        //    }

        //    if (GlobalVariables.TodayDate.Equals(PlayerPrefWrapper.GetPlayerPrefsString(PlayerPrefWrapper.PlayerPrefKeys.today_date)))
        //        return;

        //    PlayerPrefWrapper.SetPlayerPrefsAsString(PlayerPrefWrapper.PlayerPrefKeys.today_date, TodayDate);
        //    days = PlayerPrefWrapper.GetPlayerPrefsInt(PlayerPrefWrapper.PlayerPrefKeys.day_since_install);
        //    days += 1;
        //    PlayerPrefWrapper.SetPlayerPrefsInt(PlayerPrefWrapper.PlayerPrefKeys.day_since_install, days);
        //}

        //        public static void ToggleAutoWin()
        //        {
        //#if DEBUG_DEFINE && DEVLOPMENT_BUILD
        //            pDebugAutoWin = !pDebugAutoWin;
        //#else
        //            pDebugAutoWin = false;
        //#endif
        //        }

        //private static int GetFirstPartOfTheLevelKey(string levelKey)
        //{

        //    string[] values = levelKey.Split('_');

        //    return values[0].ToInt();
        //}
        #endregion Commented Code
    }
}