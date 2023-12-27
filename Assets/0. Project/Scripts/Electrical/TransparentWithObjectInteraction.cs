using UnityEngine;
using VRMultiplayer.Template.Offline.ObjectInteractions;

namespace VRMultiplayer.Electrical{
    public class TransparentWithObjectInteraction : MonoBehaviour
    {
        [SerializeField] private ObjectInteractionManager objectInteractionManager;
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
            if (objectInteractionManager.IsProtocolStarted() && !alradyStarted)
            {
                alradyStarted = true;
                for (int i = 0; i < objectToBeTransparentMaterials.Length; i++){
                    objectToBeTransparentRenderers[i].material = transparentMaterial;
                }
            }

            else if (objectInteractionManager.IsProtocolFinished()){
                for (int i = 0; i < objectToBeTransparentMaterials.Length; i++){
                    objectToBeTransparentRenderers[i].material = objectToBeTransparentMaterials[i];
                }

                Destroy(gameObject);
            }
        }
    }
}


