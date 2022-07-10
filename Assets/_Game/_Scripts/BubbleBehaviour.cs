using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{


    public class BubbleBehaviour : MonoBehaviour
    {
        [SerializeField] private float m_gravityValue = 9.81f;
        private SpriteRenderer m_sRenderer;
        private CircleCollider2D m_collider2D;

        private void Awake()
        {
            TryGetComponent(out m_sRenderer);
            TryGetComponent(out m_collider2D);
        }
        private void EnableSprite()
        {
            m_sRenderer.enabled = true;
            m_collider2D.enabled = true;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player"))
                return;
            collision.attachedRigidbody.gravityScale =-m_gravityValue;
        }
        private void OnTriggerExit2D(Collider2D collision)
        {

            if (!collision.CompareTag("Player"))
                return;
            collision.attachedRigidbody.gravityScale = m_gravityValue;
            m_sRenderer.enabled = false;
            m_collider2D.enabled = false;

            Invoke(nameof(EnableSprite), 2f);
        }

    }
}