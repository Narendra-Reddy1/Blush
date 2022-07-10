using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev {
    public class MerchantScript : MonoBehaviour
    {
       [SerializeField] private Animator m_merchantAnim;

        private void Awake()
        {
            if (m_merchantAnim == null)
                m_merchantAnim = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                InputManager.instance.hasControlAcces = false;
                m_merchantAnim.SetTrigger("canAppear");
            }

        }
    }
}