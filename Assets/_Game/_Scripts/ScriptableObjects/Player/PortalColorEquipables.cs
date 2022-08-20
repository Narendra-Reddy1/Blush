using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Naren_Dev
{
    [CreateAssetMenu(fileName = "newPortalColorEquipables", menuName = "ScriptableObjects/Player/PortalColorEquipable")]
    public class PortalColorEquipables : BaseScriptableObject
    {

        [Header("Colors")]
        [Space]
        public Color defaultColor;
        public Color portalColor_1;
        public Color portalColor_2;
        public Color portalColor_3;
    }
}