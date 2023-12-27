using UnityEngine;
using VRMultiplayer.Template.Offline.Protocols;
using UnityEngine.XR.Interaction.Toolkit;

///<SUMMARY>
///Script ini adalah Protokol untuk membuat User berinteraksi
///Pada Suatu Objek
///</SUMMARY>

namespace VRMultiplayer.Template.Offline.ObjectInteractions{
    public class OneTimeObjectInteractionManager : ProtocolManager
    {
        
        [SerializeField] private ObjectGlowManager objectGlowManager;
        
        [Header("Jika menggunakan XR Grab Interactable Component")]
        [SerializeField] private bool usingXRGrabInteractable;
        [SerializeField] private XRGrabInteractable xRGrabInteractable;

        [Header("Jika menggunakan XR Simple Interactable Component")]
        [SerializeField] private bool usingXRSimpleInteractable;
        [SerializeField] private XRSimpleInteractable xRSimpleInteractable;


        //==================================XR Interactable================================
        void OnSelectEnterd(XRBaseInteractor interactor){
            objectGlowManager.StopTheProtocol();
            StopTheProtocol();
        }

        void OnSelectExited(XRBaseInteractor interactor){

        }

        //===============================OVERRIDES FUNCTION===============================
        public override void StartTheProtocol()
        {
            protocolStarted = true;
            objectGlowManager.StartTheProtocol();
            
            if (usingXRGrabInteractable){
                xRGrabInteractable.onSelectEntered.AddListener(OnSelectEnterd);
                xRGrabInteractable.onSelectExited.AddListener(OnSelectExited);
            }

            else if (usingXRSimpleInteractable){
                xRSimpleInteractable.onSelectEntered.AddListener(OnSelectEnterd);
                xRSimpleInteractable.onSelectExited.AddListener(OnSelectExited);
            }

        }

        public override void StopTheProtocol()
        {
            protocolFinished = true;
            protocolStarted = false;
        }
    }
}

