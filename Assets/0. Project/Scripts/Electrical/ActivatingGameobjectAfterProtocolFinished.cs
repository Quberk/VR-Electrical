using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRMultiplayer.Template.Offline.Protocols;

namespace VRMultiplayer.Electrical{
    public class ActivatingGameobjectAfterProtocolFinished : MonoBehaviour
    {
        [SerializeField] private ProtocolManager targetProtocol;
        [SerializeField] private GameObject targetGameobjectToActivate;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if (targetProtocol.protocolFinished){
                targetGameobjectToActivate.SetActive(true);
                Destroy(gameObject);
            }
        }
    }


}
