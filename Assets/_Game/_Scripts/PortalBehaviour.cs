using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalBehaviour : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "PushableBox":
                other.attachedRigidbody.AddForce(Vector2.up * 100f);
                break;
        }
    }
}
