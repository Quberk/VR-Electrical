using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using VRMultiplayer.Template.Offline.Protocols;

namespace VRMultiplayer.Template.Offline.Installation{

    [RequireComponent(typeof(XRSimpleInteractable))]
    public class ObjectAnimationTriggerManager : ProtocolManager
    {
      ///<param name = "targetObject">Sebagai Objek yang ingin dijadikan Trigger</param>
        ///<param name = "targetAnimator">Animator dari Objek yang ingin animasinya diTrigger</param>
        ///<param name = "simpleInteractable">Referensi Komponen SimpleInteractable yg berfungsi sbg penerima Trigger klik dari User</param>
        ///<param name = "objectGlowManager">Referensi Komponen ObjectGlowManager yg berfungsi utk membuat Objek Glow</param>
        ///<param name = "deactivateColliderAfterTrigger">Variable untuk mengetahui apakah setelah animasi terTrigger Collider dinonaktifkan atau tidak</param>

        [SerializeField] private GameObject targetObject;
        [SerializeField] private Animator targetAnimator;

        [SerializeField] private XRSimpleInteractable simpleInteractable;
        [SerializeField] private ObjectGlowManager objectGlowManager;
        [SerializeField] private bool deactivateColliderAfterTrigger;

        void Start(){

        }

        void OnSelectEnter(XRBaseInteractor interactor){
            Debug.Log("Dia Terselect Kok");
            if (protocolStarted)
                BeginAnimation();
        }

        void BeginAnimation(){
            
            targetAnimator.SetTrigger("ActionStart");
            objectGlowManager.StopToGlow();

            //Menonaktifkan Collider setelah animasi dimainkan
            if (deactivateColliderAfterTrigger)
                targetObject.GetComponent<Collider>().enabled = false;
        }

        
        //===============================OVERRIDES FUNCTION===============================
        public override void StartTheProtocol()
        {
            Debug.Log("Object Animation Trigger : Sampai sini dia");
            protocolStarted = true;
            simpleInteractable.onSelectEntered.AddListener(OnSelectEnter);
            objectGlowManager.StartTheProtocol();
            targetObject.GetComponent<Collider>().enabled = true;
        }

        public override void StopTheProtocol()
        {
            protocolFinished = true;
            protocolStarted = false;
            
        }

    }

}
