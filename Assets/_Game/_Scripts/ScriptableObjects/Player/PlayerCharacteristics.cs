using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    [CreateAssetMenu(fileName = "newPlayerCharacteristics", menuName = "ScriptableObjects/Player/Characteristics")]
    public class PlayerCharacteristics : BaseScriptableObject
    {
        public float m_moveSpeed = 25f;
        public float m_jumpForce = 25f;
        public float m_maxVelocity = 20f;


        [Tooltip("Layer to check with ground i.e to to bring collision check only with desired layer.")]
        public LayerMask layerMask;

        [Tooltip("Distance to check from the player to ground.")]
        public float m_checkDistance = 0.1f;

        [Tooltip("Box Size to check isPlayerOnHead or not.")]
        public Vector2 m_boxSize = new Vector2(0.1f, 0.1f);

        [Tooltip("Ground Check Box Size")]
        public Vector2 gcBoxSize = new Vector2(0.1f, 0.1f);

        public AudioCueEventChannelSO m_audioEventChannel;
    }
}