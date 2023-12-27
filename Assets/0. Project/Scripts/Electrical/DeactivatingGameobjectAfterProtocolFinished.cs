using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRMultiplayer.Template.Offline.Protocols;

namespace VRMultiplayer.Electrical{
    public class DeactivatingGameobjectAfterProtocolFinished : MonoBehaviour
    {
        [SerializeField] private ProtocolManager targetProtocol;
        [SerializeField] private GameObject targetGameobjectToDeactivate;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (targetProtocol.protocolFinished){
                targetGameobjectToDeactivate.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}


