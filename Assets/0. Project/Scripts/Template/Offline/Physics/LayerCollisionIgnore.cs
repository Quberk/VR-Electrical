using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VRMultiplayer.Template.Offline.Physic{
    public class LayerCollisionIgnore : MonoBehaviour
    {

        void Start()
        {
            //Masukkan Layer yang anda tidak ingin bersinggungan

            Physics.IgnoreLayerCollision(6,7); //Ignore Collision antara Player & Interactable Object
        }
    }

}
