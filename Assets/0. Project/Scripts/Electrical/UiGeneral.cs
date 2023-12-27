using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRMultiplayer.Electrical{
    public class UiGeneral : MonoBehaviour
    {
        public void DeactivateGameobject(GameObject target){
            target.SetActive(false);
        }    
    }
}

