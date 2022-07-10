using UnityEngine;

namespace Naren_Dev
{
    public class DestroyOverTime : MonoBehaviour
    {
        [SerializeField] private float m_timer;

        private void Start()
        {
            AutoDestroy();
        }

        private void AutoDestroy()
        {
            Destroy(gameObject, m_timer);
        }
    }
}