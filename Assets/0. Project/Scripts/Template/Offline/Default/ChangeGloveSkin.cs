using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VRMultiplayer.Template.Offline.Default{

    [RequireComponent(typeof(XRSimpleInteractable))]
    public class ChangeGloveSkin : MonoBehaviour
    {
        [SerializeField] private Material targetGloveMaterial;
        [SerializeField] private string targetGloveTag;

        private SkinnedMeshRenderer[] targetGloveRenderer;
        private XRSimpleInteractable xRSimpleInteractable;

        void Start(){

            xRSimpleInteractable = GetComponent<XRSimpleInteractable>();
            xRSimpleInteractable.onSelectEntered.AddListener(ChangeTheGloveSkin);
            
        }

        private void ChangeTheGloveSkin(XRBaseInteractor interactor){

            GameObject[] targetGlove = GameObject.FindGameObjectsWithTag(targetGloveTag);
            targetGloveRenderer = new SkinnedMeshRenderer[targetGlove.Length];
            Debug.Log("Jumlah Target Glove : " + targetGlove.Length);
            for (int i = 0; i < targetGlove.Length; i++){
                
                targetGloveRenderer[i] = targetGlove[i].GetComponent<SkinnedMeshRenderer>();
                
            }


            for (int i = 0; i < targetGloveRenderer.Length; i++){
                targetGloveRenderer[i].material = targetGloveMaterial;
                Debug.Log("Mengganti Glove harusnya sudah bisa");
            }
        }

        
    }
}

