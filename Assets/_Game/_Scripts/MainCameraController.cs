
using UnityEngine;
using Cinemachine;
using Naren_Dev;
using DG.Tweening;

public class MainCameraController : MonoBehaviour
{
    #region Variables

    [SerializeField] private CinemachineVirtualCamera m_camera;
    [SerializeField] private Vector3 m_shakeThreshold;
    [SerializeField] private float m_shakeDuration;

    [SerializeField] private float m_minCamSize = 8, m_maxCamSize = 15;

    #endregion Variables

    #region Unity Methods

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        GlobalEventHandler.AddListener(EventID.EVENT_ON_PLAYER_DEAD, Callback_On_Player_Dead);
        GlobalEventHandler.AddListener(EventID.EVENT_ON_PLAYER_RESPAWN, Callback_On_Player_Respawned);
    }
    private void OnDisable()
    {
        GlobalEventHandler.RemoveListener(EventID.EVENT_ON_PLAYER_DEAD, Callback_On_Player_Dead);
        GlobalEventHandler.RemoveListener(EventID.EVENT_ON_PLAYER_RESPAWN, Callback_On_Player_Respawned);
    }

    #endregion Unity Methods

    #region Custom Methods
    public void Init()
    {

    }
    private void ShakeCameraPosition()
    {

        if (!DOTween.IsTweening(transform))
            transform.DOPunchRotation(m_shakeThreshold, m_shakeDuration);
    }
    //
    // Summary:
    //     Tweens a Camera's
    //     orthographicSize
    //     to the given value. Also stores the camera as the tween's target so it can be
    //     used for filtered operations
    //
    // Parameters:
    //   endValue:
    //     The end value to reach
    //
    //   duration:
    //     The duration of the tween


    public void AdjustCameraView(float orthoSize, float duration)
    {

    }


    public static void DoMoveOrthoSize()
    {

    }

    #endregion Custom methods

    #region Callbacks
    public void Callback_On_Player_Dead(object args)
    {
        ShakeCameraPosition();
    }
    public void Callback_On_Player_Respawned(object args)
    {
        ShakeCameraPosition();
    }

    #endregion Callbacks
}
