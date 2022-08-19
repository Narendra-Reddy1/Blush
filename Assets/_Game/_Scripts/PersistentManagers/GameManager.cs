using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    public class GameManager : MonoBehaviour, IInitializer
    {
        public static GameManager instance { get; private set; }
        private GameState gameState;

        [Range(0.1f, 5.0f)]
        [SerializeField] private float m_respawnDelay = 0.5f;
        private List<EnemyBehaviour> m_killedEnemiesInTheLevel;
        //   public GameObject enemyDeathEffect;
        public GameObject wheelSelectionHighlight;
        [SerializeField] private AudioCueEventChannelSO m_audioEventChannel;
        // [SerializeField] private Material m_bgMaterial;
        [SerializeField] private List<Material> m_gradientMaterials;
        private float m_intensity = 0.1f;

        public GameState GameState => gameState;
        #region Unity Methods

        private void Awake()
        {
            instance = this;
            Init();
            //  SetScreenBounds();
        }
        private void Start()
        {
            m_audioEventChannel.RaiseMusicPlayEvent(AudioId.GamePlayBGM);
        }

        private void OnEnable()
        {
            GlobalEventHandler.AddListener(EventID.EVENT_ON_PLAYER_DEAD, Callback_On_Player_Dead);
        }
        private void OnDisable()
        {
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_PLAYER_DEAD, Callback_On_Player_Dead);
        }

        #endregion Unity Methods
        //private void Update()
        //{
        //   // SetScreenBounds();
        //    //if (!InputManager.instance.hasControlAcces)
        //    //    ApplyColorsToLevel();

        //}


        public void Init()
        {
            // if (m_camera == null) m_camera = Camera.main;
            /* if (enemyDeathEffect == null) { Resources.Load("Assets/_Game/Prefabs/Effects/Death Effect.prefab"); }
             if (wheelSelectionHighlight == null)
                 Resources.Load("Assets/_Game/Prefabs/Wheel Sec_Highlight.prefab");*/
            m_killedEnemiesInTheLevel = new List<EnemyBehaviour>();

            foreach (Material mat in m_gradientMaterials)
            {
                mat.SetColor("_Color1", Color.HSVToRGB(0, 0, 12f));
                mat.SetColor("_Color2", Color.HSVToRGB(0, 0, 71f));
                mat.SetFloat("_Intensity", .001f);
            }

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

        //private void OnNewLevelLoaded()
        //{
        //    GlobalVariables.STARTING_POINT = GameObject.FindGameObjectWithTag("Start").transform.position;
        //}

        public void onNewEnemyKilled(EnemyBehaviour newEnemy)
        {
            if (!m_killedEnemiesInTheLevel.Contains(newEnemy))
                m_killedEnemiesInTheLevel.Add(newEnemy);
        }
        private void _OnNewLevelStarted()
        {
            m_killedEnemiesInTheLevel.Clear();
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
            if (gameState == state) return;
            gameState = state;
        }
        /// <summary>
        /// This Methods gives us the information about the state of the game. <br></br>
        /// 1.Game Started <br></br>
        /// 2.Game Completed<br></br>
        /// 3.MainMenu<br></br>
        /// 4.CutScene<br></br>
        /// </summary>
        /// <returns></returns>
        public GameState GetGameState() => gameState;

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

        }

        #region Callbacks
        public void Callback_On_Player_Dead(object args)
        {
            RespawnPlayer();
        }

        #endregion Callbacks

    }
}