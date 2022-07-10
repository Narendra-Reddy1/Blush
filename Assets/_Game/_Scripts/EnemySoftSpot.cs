using UnityEngine;

namespace Naren_Dev
{
    public class EnemySoftSpot : MonoBehaviour
    { 
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
              GameObject go=  Instantiate(GameManager.instance.enemyDeathEffect,transform.parent.position,
                  GameManager.instance.enemyDeathEffect.transform.rotation);
                go.GetComponent<ParticleSystem>().Play();
                transform.parent.parent.gameObject.SetActive(false);
            }
        }
    }
}