using System.Collections.Generic;
using UnityEngine;
using Fungus;
namespace Naren_Dev
{
    public class GameManager : MonoBehaviour, IInitializer
    {
        public static GameManager instance { get; private set; }
        private static GameState m_gameState;
        private float m_intensity = 0.1f;
        private List<EnemyBehaviour> m_killedEnemiesInTheLevel;


        [Range(0.1f, 5.0f)]
        [SerializeField] private float m_respawnDelay = 0.5f;
        [SerializeField] private AudioCueEventChannelSO m_audioEventChannel;
        [SerializeField] private LevelData m_singlePlayerLevelData;
        [SerializeField] private List<Material> m_gradientMaterials;
        [SerializeField] private Flowchart m_cutSceneChart;
        private UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
            currentlyLoadedSceneHandle;

        public static GameState s_GameState => m_gameState;
        #region Unity Methods

        private void Awake()
        {
            instance = this;
            Init();
            _UpdateGameState(GameState.GamePlay);
            //  SetScreenBounds();
        }
        private void Start()
        {
            m_audioEventChannel.RaiseMusicPlayEvent(AudioId.GamePlayBGM);
        }

        private void OnEnable()
        {
            GlobalEventHandler.AddListener(EventID.EVENT_ON_LEVEL_STARTED, Callback_On_New_level_Starte);
            GlobalEventHandler.AddListener(EventID.EVENT_ON_CUTSCENE_ENDED, Callback_On_CutSceneEnded);
            GlobalEventHandler.AddListener(EventID.EVENT_ON_PLAYER_RESPAWN, Callback_On_Player_Respawned);
            GlobalEventHandler.AddListener(EventID.EVENT_ON_GAMESTATE_CHANGED, Callback_On_GameState_Changed);
            GlobalEventHandler.AddListener(EventID.EVENT_ON_PLAYER_DEAD, Callback_On_Player_Dead);
        }
        private void OnDisable()
        {
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_LEVEL_STARTED, Callback_On_New_level_Starte);
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_CUTSCENE_ENDED, Callback_On_CutSceneEnded);
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_PLAYER_RESPAWN, Callback_On_Player_Respawned);
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_GAMESTATE_CHANGED, Callback_On_GameState_Changed);
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_PLAYER_DEAD, Callback_On_Player_Dead);
        }

        #endregion Unity Methods

        #region Custom Methods

        public void Init()
        {

            m_killedEnemiesInTheLevel = new List<EnemyBehaviour>();
            //foreach (Material mat in m_gradientMaterials)
            //{
            //    mat.SetColor("_Color1", Color.HSVToRGB(0, 0, 12f));
            //    mat.SetColor("_Color2", Color.HSVToRGB(0, 0, 71f));
            //    mat.SetFloat("_Intensity", .001f);
            //}
        }
        private void ApplyColorsToLevel()
        {

            foreach (Material mat in m_gradientMaterials)
            {
                mat.SetFloat("_Intensity", .001f);
            }


            /*
                        foreach (Material mat in m_gradientMaterials)
                        {
                        }*/

            /* if (m_intensity <= 1.25f)
             {
                 m_intensity = Mathf.MoveTowards(m_intensity, 1.25f, m_intensitySpeed * Time.deltaTime);
             }*/

            /*  foreach (Material mat in m_gradientMaterials)
              {
                  mat.SetFloat("_Intensity", m_intensity);
              }*/
        }

        private void SetOriginalColors()
        {

        }
        public void StartNarration(string blockID)
        {
            m_cutSceneChart.ExecuteBlock(blockID);
        }
        public void TriggerCutSceneEnd(LevelID levelID)
        {
            GlobalEventHandler.TriggerEvent(EventID.EVENT_ON_CUTSCENE_ENDED, levelID);
        }
        public void RewardPlayer(ColorID colorID)
        {
            UIManager.instance.ShowUnlockColorAnimation(colorID);
            SovereignUtils.Log($"Unlocking Color: {colorID}");
        }
        public void onNewEnemyKilled(EnemyBehaviour newEnemy)
        {
            if (!m_killedEnemiesInTheLevel.Contains(newEnemy))
                m_killedEnemiesInTheLevel.Add(newEnemy);
        }
        private void _OnNewLevelStarted()
        {
            m_killedEnemiesInTheLevel.Clear();
            CheckpointSystem.completedCheckPoints.Clear();
        }


        /// <summary>
        /// Update game state as any of these <br></br>
        /// 1.Game Started <br></br>
        /// 2.Game Completed<br></br>
        /// 3.MainMenu<br></br>
        /// 4.CutScene<br></br>
        /// </summary>
        /// <returns></returns>
        private void _UpdateGameState(GameState state)
        {
            if (m_gameState == state) return;
            m_gameState = state;
            switch (state)
            {
                case GameState.GamePlay:
                    break;
                case GameState.CutScene:
                    GlobalEventHandler.TriggerEvent(EventID.EVENT_ON_CUTSCENE_STARTED);
                    break;
            }
            SovereignUtils.Log($"GameState Update.. Current GameState: {m_gameState}");
        }
        /// <summary>
        /// This Methods gives us the information about the state of the game. <br></br>
        /// 1.Game Started <br></br>
        /// 2.Game Completed<br></br>
        /// 3.MainMenu<br></br>
        /// 4.CutScene<br></br>
        /// </summary>
        /// <returns></returns>
        public static GameState GetGameState() => m_gameState;

        /// <summary>
        /// Respawns Player after a defined delay of seconds at latest checkpoint if exists.<br></br>
        /// else it will reload the level with all enemis without respawning the collected collectables.
        /// </summary>
        private async void RespawnPlayer()
        {
            await System.Threading.Tasks.Task.Delay((int)m_respawnDelay * 1000);
            SovereignUtils.Log($"Respawning...Player..");
            GlobalEventHandler.TriggerEvent(EventID.EVENT_ON_PLAYER_RESPAWN, PlayerState.Alive);
        }
        private void RespawnAllEnemies()
        {
            foreach (EnemyBehaviour enemy in m_killedEnemiesInTheLevel)
            {
                enemy.UpdateEnemyState(EnemyState.Alive);
            }
        }
        private async void LoadNextLevel(LevelID levelID)
        {
            if (m_singlePlayerLevelData.levelsCollection.Contains(levelID))
            {
                currentlyLoadedSceneHandle = m_singlePlayerLevelData.levelsCollection[levelID].LoadSceneAsync(UnityEngine.SceneManagement.LoadSceneMode.Additive, false);
                await currentlyLoadedSceneHandle.Task;
                currentlyLoadedSceneHandle.Completed += (handle) =>
                   {
                       GlobalEventHandler.TriggerEvent(EventID.EVENT_ON_LEVEL_STARTED);
                   };
            }
        }
        public void EnableNewlyLoadedLevel()
        {
            currentlyLoadedSceneHandle.Result.ActivateAsync();
            RespawnPlayer();
        }

        #endregion Custom Methods

        #region Callbacks
        private void Callback_On_New_level_Starte(object args)
        {
            _OnNewLevelStarted();
        }
        private void Callback_On_Player_Respawned(object ags)
        {
            RespawnAllEnemies();
        }
        private void Callback_On_CutSceneEnded(object args)
        {
            LevelID id = (LevelID)args;
            LoadNextLevel(id);
        }
        private void Callback_On_Player_Dead(object args)
        {
            RespawnPlayer();
        }

        private void Callback_On_GameState_Changed(object args)
        {
            GameState state = (GameState)args;
            _UpdateGameState(state);
        }
        #endregion Callbacks

    }
}