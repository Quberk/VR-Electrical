using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using VRMultiplayer.Template.Offline.Protocols;

namespace VRMultiplayer.Template.Offline.EquippingItem{
    
    [RequireComponent(typeof(XRSimpleInteractable))]
    [RequireComponent(typeof(ObjectGlowManager))]
    public class GadgetTriggerController : MonoBehaviour
    {
        private bool readyToBeEquipped;
        private bool equipped;
        private XRSimpleInteractable xRSimpleInteractable;
        private ObjectGlowManager objectGlowManager;



        void Start()
        {
            xRSimpleInteractable = gameObject.GetComponent<XRSimpleInteractable>();
            objectGlowManager = gameObject.GetComponent<ObjectGlowManager>();
            xRSimpleInteractable.onSelectEntered.AddListener(OnSelectEnter);
        }

        

        public void BeginTheTrigger(){
            readyToBeEquipped = true;
            objectGlowManager.StartTheProtocol();
        }

        private void OnSelectEnter(XRBaseInteractor interactor){
            if (readyToBeEquipped){
                equipped = true;
            }
                
        }

        public bool Equipped(){
            return equipped;
        }
    }
}

