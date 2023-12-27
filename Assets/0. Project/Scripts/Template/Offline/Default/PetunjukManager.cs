using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRMultiplayer.Template.Offline.Protocols;

namespace VRMultiplayer.Template.Offline.Default{

    /// <summary>
    /// Class ini berfungsi untuk membuat Petunjuk bagi Player kemudian hilang ketika Player berada pada Area yang ditentukan
    /// </summary>

    public class PetunjukManager : ProtocolManager
    {
        [SerializeField] private GameObject petunjukGameobject;
        [SerializeField] private string playerTag;

        void Start(){
            petunjukGameobject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other){
            if (other.transform.CompareTag(playerTag)){
                StopTheProtocol();
            }
        }

        //===============================OVERRIDES FUNCTION===============================
        public override void StartTheProtocol()
        {
            petunjukGameobject.SetActive(true);
            protocolStarted = true;
        }

        public override void StopTheProtocol()
        {
            protocolFinished = true;
            Destroy(petunjukGameobject);
        }
    }
}

