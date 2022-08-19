using Naren_Dev;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartPoint : MonoBehaviour
{
    public bool hasInverseGravity;
    private void OnEnable()
    {
        GlobalVariables.STARTING_POINT = transform.position;
        GlobalVariables.START_POINT_HAS_INVERSE_GRAVITY = hasInverseGravity;
    }
}
