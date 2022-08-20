using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Naren_Dev
{
    public class UIManager : MonoBehaviour
    {

        public static UIManager instance { get; private set; }
        [SerializeField] private AddressablesHelper m_addressablesHelper;
        [SerializeField] private UnityEngine.AddressableAssets.AssetReference m_unlockColorPiece;
        [SerializeField] private Image m_backgroundOverlay;
        [SerializeField] private Canvas m_staticCanvas;
        [SerializeField] private Canvas m_dynamicCanvas;

        [Header("Player A")]
        public List<RectTransform> playerA_ColorSection;

        [Header("Player B")]
        public List<RectTransform> playerB_ColorSection;
        [SerializeField] private PortalColorEquipables m_playerBColorEquipables;
        [SerializeField] private Vector3 m_selectionSize = new Vector3(3f, 3f, 3f);
        [SerializeField] private Vector3 m_defaultSize = new Vector3(2.5f, 2.5f, 2.5f);

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
                instance = this;
            /*playerA_ColorSection = new List<RectTransform>();
            playerB_ColorSection = new List<RectTransform>();*/
        }
        private void OnEnable()
        {
            GlobalEventHandler.AddListener(EventID.EVENT_ON_CUTSCENE_STARTED, Callback_On_CutScene_Started);
            GlobalEventHandler.AddListener(EventID.EVENT_ON_CUTSCENE_ENDED, Callback_On_CutScene_Ended);
        }
        private void OnDisable()
        {
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_CUTSCENE_STARTED, Callback_On_CutScene_Started);
            GlobalEventHandler.RemoveListener(EventID.EVENT_ON_CUTSCENE_ENDED, Callback_On_CutScene_Ended);

        }

        private void Update()
        {
            if (playerA_ColorSection != null && playerB_ColorSection != null)
            {
                ColorSelectionIndicator(InputManager.instance.playerAWheelIndex, playerA_ColorSection);
                ColorSelectionIndicator(InputManager.instance.playerBWheelIndex, playerB_ColorSection);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                SetupForCutSceneStart();
            }
            if (Input.GetKeyUp(KeyCode.L))
            {
                SetupForCutSceneEnd();
            }
            //if (Input.GetKeyUp(KeyCode.O))
            //{
            //    ShowUnlockColorAnimation(ColorID.Color_One);
            //}
            //if (Input.GetKeyUp(KeyCode.I))
            //{
            //    ShowUnlockColorAnimation(ColorID.Color_Two);
            //}
            //if (Input.GetKeyUp(KeyCode.U))
            //{
            //    ShowUnlockColorAnimation(ColorID.Color_Three);
            //}

            //  PlayerBColorSelectionIndicator();

            // Debug.Log(InputManager.instance.playerAWheelIndex);
        }
        public void ShowUnlockColorAnimation(ColorID colorID)
        {
            RectTransform colorPiece = default;
            Image colorPieceImage = default;
            m_addressablesHelper.InstantiateAsync<GameObject>(m_unlockColorPiece, m_staticCanvas.transform, OnCompleted: (status, handle) =>
            {
                if (status)
                {
                    colorPiece = handle.Result.GetComponent<RectTransform>();
                    colorPieceImage = colorPiece.transform.GetChild(1).GetComponent<Image>();
                    switch (colorID)
                    {
                        case ColorID.Color_One:
                            colorPieceImage.color = m_playerBColorEquipables.portalColor_1;
                            PlayerResourcesManager.Buy(ResourceID.FIRST_COLOR_ID);
                            break;
                        case ColorID.Color_Two:
                            colorPieceImage.color = m_playerBColorEquipables.portalColor_2;
                            PlayerResourcesManager.Buy(ResourceID.SECOND_COLOR_ID);
                            break;
                        case ColorID.Color_Three:
                            colorPieceImage.color = m_playerBColorEquipables.portalColor_3;
                            PlayerResourcesManager.Buy(ResourceID.THIRD_COLOR_ID);
                            break;
                        default:
                            SovereignUtils.Log($"Invalid ColorID: {colorID}");
                            break;
                    }
                    PlayerDataManager.instance.SaveData();
                    StartCoroutine(DelayedCallback((int)colorID + 1, false, 3f));

                    //colorPiece.anchorMin = playerB_ColorSection[(int)colorID].anchorMin;
                    //colorPiece.anchorMax = playerB_ColorSection[(int)colorID].anchorMax;
                    //colorPiece.DOPunchScale(new Vector2(0.95f, .95f), 1f).OnComplete(() =>
                    //{
                    //    //colorPiece.DOAnchorPosY(-25, 2f).SetSpeedBased();
                    //    //colorPiece.DOAnchorPosY(-25, 2f).SetSpeedBased();
                    //    if ((int)(colorID + 1) < playerB_ColorSection.Count)
                    //        colorPiece.DOMove(playerB_ColorSection[(int)colorID].localPosition, 2f).OnComplete(() =>
                    //        {
                    //            colorPiece.DORotateQuaternion(playerB_ColorSection[(int)colorID].localRotation, 0.75f).OnComplete(() =>
                    //            {
                    //                ShowUnlockColorEffect((int)(colorID + 1), false);
                    //                colorPieceImage.DOFade(0, 0.1f);
                    //            });
                    //        });
                    //});

                }
            });

        }


        private IEnumerator DelayedCallback(int colorID, bool isPlayerA, float delay = 1f)
        {
            yield return new WaitForSeconds(delay);
            ShowUnlockColorEffect(colorID, isPlayerA);
        }
        public void ShowUnlockColorEffect(int colorId, bool playerA)
        {
            if (playerA)
            {
                if (colorId > playerA_ColorSection.Count || colorId < 0)
                    return;
                playerA_ColorSection[colorId].transform.DOShakePosition(1.5f, 5f);
                playerA_ColorSection[colorId].transform.DOShakeRotation(1.5f, 5f);
                playerA_ColorSection[colorId].GetComponent<Image>().DOFillAmount(1, 1.5f);
            }
            else
            {
                if (colorId > playerB_ColorSection.Count || colorId < 0)
                    return;
                playerB_ColorSection[colorId].transform.DOShakePosition(1.5f, 5f);
                playerB_ColorSection[colorId].transform.DOShakeRotation(1.5f, 5f);
                playerB_ColorSection[colorId].GetComponent<Image>().DOFillAmount(1, 1.5f);
            }
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

        private void SetupForCutSceneStart()
        {
            m_dynamicCanvas.enabled = false;
            m_backgroundOverlay.DOFade(0.85f, 0.75f);
        }
        private void SetupForCutSceneEnd()
        {

            m_dynamicCanvas.enabled = true;
            m_backgroundOverlay.DOFade(0f, 0.75f);
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

        #region Callbacks

        public void Callback_On_CutScene_Started(object args)
        {
            SetupForCutSceneStart();
        }
        public void Callback_On_CutScene_Ended(object args)
        {
            SetupForCutSceneEnd();
        }
        #endregion

    }
}