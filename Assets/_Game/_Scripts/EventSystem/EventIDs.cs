/// <summary>
/// All events to be used need to be added to this script.
/// </summary>
#region Event Types enum
public enum EventID
{
    //GAME
    EVENT_ON_GAME_MAXIMIZED,
    EVENT_ON_GAME_MINIMIZED,

    //Level
    EVENT_ON_LEVEL_STARTED,
    EVEN_ON_LEVEL_FINISHED,
    EVENT_ON_LEVEL_EXITED,

    //Player
    EVENT_ON_PLAYER_RESPAWN,
    EVENT_ON_PLAYER_DEAD,
    EVENT_ON_PLAYER_WIN,

    //Collectables
    EVENT_ON_COLLECTABLE_COLLECTED,

    //Checkpoints
    EVENT_ON_CHECKPOINT_REACHED
}
#endregion