using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRMultiplayer.Template.Offline.Protocols;

namespace VRMultiplayer.Electrical.Simulasi.Simulasi1{
    public class TembokTransparan : MonoBehaviour
    {
        [SerializeField] private ObjectGlowManager tiangListrikGlowManager;
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
            if (tiangListrikGlowManager.IsProtocolStarted() && !alradyStarted)
            {
                alradyStarted = true;
                for (int i = 0; i < objectToBeTransparentMaterials.Length; i++){
                    objectToBeTransparentRenderers[i].material = transparentMaterial;
                }
            }

            else if (tiangListrikGlowManager.IsProtocolFinished()){
                for (int i = 0; i < objectToBeTransparentMaterials.Length; i++){
                    objectToBeTransparentRenderers[i].material = objectToBeTransparentMaterials[i];
                }

                Destroy(gameObject);
            }

        }
    }

}
