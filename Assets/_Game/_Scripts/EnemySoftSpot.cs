using UnityEngine;
using DG.Tweening;

namespace Naren_Dev
{
    public class EnemySoftSpot : MonoBehaviour
    {
        [SerializeField] private AddressablesHelper m_addressableHelper = default;
        [SerializeField]
        private UnityEngine.AddressableAssets.AssetReference enemyDeathEffect;
        [SerializeField] private EnemyBehaviour m_enemyBehaviour = default;
        private GameObject m_enemyDeathEffect;
        private void Awake()
        {
            m_addressableHelper.LoadAssetAsync<GameObject>(enemyDeathEffect, (status, handle) =>
            {
                if (status)
                    m_enemyDeathEffect = handle.Result;
            }
            );
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (m_enemyBehaviour.enemyState == EnemyState.Dead) return;
            if (collision.CompareTag("Player"))
            {
                GameObject go = Instantiate(m_enemyDeathEffect, transform.position, Quaternion.identity, null);
                //go = (GameObject)handle.Result;
                //  go.transform.position = transform.position;
                go.GetComponent<ParticleSystem>().Play();
                m_enemyBehaviour.UpdateEnemyState(EnemyState.Dead);
                GameManager.instance.onNewEnemyKilled(m_enemyBehaviour);
                //transform.parent.parent.gameObject.SetActive(false);


                //GameObject go = Instantiate(GameManager.instance.enemyDeathEffect, transform.parent.position,
                //    GameManager.instance.enemyDeathEffect.transform.rotation);
            }
        }
    }
}