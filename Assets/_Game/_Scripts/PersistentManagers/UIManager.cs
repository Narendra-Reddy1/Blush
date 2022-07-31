using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Naren_Dev
{
    public class UIManager : MonoBehaviour
    {

        public static UIManager instance { get; private set; }


        [Header("Player A")]
        public List<RectTransform> playerA_ColorSection;

        [Header("Player B")]
        public List<RectTransform> playerB_ColorSection;
        [SerializeField] private Vector3 m_selectionSize = new Vector3(3f, 3f, 3f);
        [SerializeField] private Vector3 m_defaultSize = new Vector3(2.5f, 2.5f, 2.5f);

        private void Awake()
        {
            /*playerA_ColorSection = new List<RectTransform>();
            playerB_ColorSection = new List<RectTransform>();*/
        }

        private void Update()
        {
            if (playerA_ColorSection != null && playerB_ColorSection != null)
            {
                ColorSelectionIndicator(InputManager.instance.playerAWheelIndex, playerA_ColorSection);
                ColorSelectionIndicator(InputManager.instance.playerBWheelIndex, playerB_ColorSection);

            }
            //  PlayerBColorSelectionIndicator();

            // Debug.Log(InputManager.instance.playerAWheelIndex);
        }


        private void ColorSelectionIndicator(Vector2 playerWheelIndex, List<RectTransform> playerColorSection)
        {
            if (playerWheelIndex.x > .75f)
                SetObjectScale(playerColorSection[1], m_selectionSize);
            else
                SetObjectScale(playerColorSection[1], m_defaultSize);

            if (playerWheelIndex.y > .75f)
                SetObjectScale(playerColorSection[0], m_selectionSize);
            else
                SetObjectScale(playerColorSection[0], m_defaultSize);

            if (playerWheelIndex.x < -.75f)
                SetObjectScale(playerColorSection[3], m_selectionSize);
            else
                SetObjectScale(playerColorSection[3], m_defaultSize);

            if (playerWheelIndex.y < -.75f)
                SetObjectScale(playerColorSection[2], m_selectionSize);
            else
                SetObjectScale(playerColorSection[2], m_defaultSize);


        }

        private void SetObjectScale(RectTransform rect, Vector3 size)
        {
            rect.localScale = size;
        }

        /*
                private void PlayerBColorSelectionIndicator()
                {

                    if (InputManager.instance.playerBWheelIndex.x > .75f)
                        playerB_ColorSection[1].localScale = new Vector3(3, 3, 3);
                    else
                        playerB_ColorSection[1].localScale = new Vector3(2.5f, 2.5f, 2.5f);

                    if (InputManager.instance.playerBWheelIndex.y > .75f)
                        playerB_ColorSection[0].localScale = new Vector3(3, 3, 3);
                    else
                        playerB_ColorSection[0].localScale = new Vector3(2.5f, 2.5f, 2.5f);

                    if (InputManager.instance.playerBWheelIndex.x < -.75f)
                        playerB_ColorSection[3].localScale = new Vector3(3, 3, 3);
                    else
                        playerB_ColorSection[3].localScale = new Vector3(2.5f, 2.5f, 2.5f);

                    if (InputManager.instance.playerAWheelIndex.y < -.75f)
                        playerB_ColorSection[2].localScale = new Vector3(3, 3, 3);
                    else
                        playerB_ColorSection[2].localScale = new Vector3(2.5f, 2.5f, 2.5f);
                }*/



    }
}