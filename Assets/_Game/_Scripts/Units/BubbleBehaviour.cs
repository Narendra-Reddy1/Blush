using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{


    public class BubbleBehaviour : MonoBehaviour
    {
        #region Variables 

        [Range(0.75f, 4f)]
        [SerializeField] private float m_bubbleRespawnTime = 1.75f;
        [SerializeField] private AudioCueEventChannelSO m_audioEventChannel;
        //  [SerializeField] private float m_gravityValue = 9.81f;
        private SpriteRenderer m_sRenderer;
        private CircleCollider2D m_collider2D;

        #endregion

        #region Built-In Methods
        private void Awake()
        {
            TryGetComponent(out m_sRenderer);
            TryGetComponent(out m_collider2D);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;
            collision.attachedRigidbody.gravityScale *= -1;
            Debug.Log($"Collided: {collision.name} : {collision.attachedRigidbody.gravityScale}");

        }
        private void OnTriggerExit2D(Collider2D collision)
        {

            if (!collision.CompareTag("Player"))
                return;
            // AudioManager.instance?.PlaySFX(AudioId.BubblePopSFX);
            m_audioEventChannel.RaiseSFXPlayEvent(AudioId.BubblePopSFX);
            collision.attachedRigidbody.gravityScale *= -1;
            m_sRenderer.enabled = false;
            m_collider2D.enabled = false;

            Invoke(nameof(RespawnBuble), m_bubbleRespawnTime);
        }

        #endregion

        #region Custom Methods

        private void RespawnBuble()
        {
            m_sRenderer.enabled = true;
            m_collider2D.enabled = true;
        }
        #endregion
    }
}