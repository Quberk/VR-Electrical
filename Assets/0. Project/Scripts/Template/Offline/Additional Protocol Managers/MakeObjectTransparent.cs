using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRMultiplayer.Template.Offline.Protocols;

namespace VRMultiplayer.Template.Offline.Additional{
    ///<SUMMARY>
    /// Class ini sebagai Protocol Additional yang berfungsi untuk membuat Object Transparan
    ///</SUMMARY>
    public class MakeObjectTransparent : ProtocolManager
    {
        [SerializeField] private Material transparentMaterial;
        [SerializeField] private Renderer[] objectToBeTransparentRenderers;
        private Material[] objectToBeTransparentMaterials;
            
        private bool alradyStarted = false;

        // Start is called before the first frame update
        void Start()
        {
            objectToBeTransparentMaterials = new Material[objectToBeTransparentRenderers.Length];
            for (int i = 0; i < objectToBeTransparentMaterials.Length; i++){
                objectToBeTransparentMaterials[i] = objectToBeTransparentRenderers[i].material;
            }
        }
        // Update is called once per frame
        void Update()
        {
            if (protocolStarted && !alradyStarted)
            {
                alradyStarted = true;
                for (int i = 0; i < objectToBeTransparentMaterials.Length; i++){
                    objectToBeTransparentRenderers[i].material = transparentMaterial;
                }
            }

            else if (protocolFinished){
                for (int i = 0; i < objectToBeTransparentMaterials.Length; i++){
                    objectToBeTransparentRenderers[i].material = objectToBeTransparentMaterials[i];
                }

                Destroy(gameObject);
            }

        }

        //===============================OVERRIDES FUNCTION===============================
        public override void StartTheProtocol()
        {
            protocolStarted = true;
        }

        public override void StopTheProtocol()
        {
            protocolStarted = false;
            protocolFinished = true;
        }
    }

}
