using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }
        public GameState gameState;
        //[SerializeField]private 
        public GameObject enemyDeathEffect;
        public GameObject wheelSelectionHighlight;
        [SerializeField] private Camera m_camera;
        // [SerializeField] private Material m_bgMaterial;
        [SerializeField] private List<Material> m_gradientMaterials;



        public float MinScreenBound => minScreenBounds.x;
        public float MaxScreenBound => maxScreenBounds.x;

        private Vector3 minScreenBounds;
        private Vector3 maxScreenBounds;
        private float m_intensity = 0.1f;
        // [SerializeField]private float m_intensitySpeed = 2f;

        private void Awake()
        {
            instance = this;
            CheckDependencies();
            SetScreenBounds();
          
        }
        private void Start()
        {
            AudioManager.instance?.PlayMusic(AudioId.GamePlayBGM);
        }

        private void Update()
        {
            SetScreenBounds();
            if (!InputManager.instance.hasControlAcces)
                ApplyColorsToLevel();

        }
      

        private void CheckDependencies()
        {
            if (m_camera == null) m_camera = Camera.main;
            /* if (enemyDeathEffect == null) { Resources.Load("Assets/_Game/Prefabs/Effects/Death Effect.prefab"); }
             if (wheelSelectionHighlight == null)
                 Resources.Load("Assets/_Game/Prefabs/Wheel Sec_Highlight.prefab");*/


            foreach (Material mat in m_gradientMaterials)
            {
                mat.SetColor("_Color1", Color.HSVToRGB(0, 0, 12f));
                mat.SetColor("_Color2", Color.HSVToRGB(0, 0, 71f));
                mat.SetFloat("_Intensity", .001f);
            }

        }


        private void SetScreenBounds()
        {
            minScreenBounds = m_camera.ScreenToWorldPoint(new Vector3(0, Screen.height, m_camera.transform.position.z));
            maxScreenBounds = m_camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, m_camera.transform.position.z));
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
    }
    public enum GameState
    {
        MainMenu = 0,
        Started = 1,
        Completed = 2,
        CutScene = 3
    }
}