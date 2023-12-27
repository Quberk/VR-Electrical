using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VRMultiplayer.Electrical.Equipment.Multimeter{

    [RequireComponent(typeof(XRGrabInteractable))]
    public class KabelController : MonoBehaviour
    {
        [SerializeField] private KabelCollider kabelCollider;

        private MultimeterManager multimeterManager;
        private XRGrabInteractable xRGrabInteractable;
        private Transform initialTransform;

        private bool beingGrabbed = false;

        private string[] namaSumbuPositif; //Nama-nama Gameobject yang sebagai Sumbu Positif
        private string[] namaSumbuNegatif; //Nama-nama Gameobject yang sebagai Sumbu Negatif

        private Sumbu sumbuYangTersambung = Sumbu.None;
        private bool firstFrame = false;

        void Start(){

            xRGrabInteractable = GetComponent<XRGrabInteractable>();
            xRGrabInteractable.onSelectEntered.AddListener(OnSelectGrabEntered);
            xRGrabInteractable.onSelectExited.AddListener(OnSelectGrabExited);


            
        }

        void Update(){

            //Setelah satu kali di Grab maka akan trus diGrab sampai Multimeternya dilepas
            if (!beingGrabbed){
                transform.position = initialTransform.position;
                transform.rotation = initialTransform.rotation;
            }
            
            //Objek hanya bisa di Grab ketika Multimeter di Grab terlebih dahulu
            if (multimeterManager.GetGrabbedStatus()){
                xRGrabInteractable.enabled = true;
            }

            else{
                xRGrabInteractable.enabled = false;
                beingGrabbed = false;
            }


        }

        //==============================================SETTER METHODS===================================================================
        public void SetNamaSumbuPositif(string[] namaSumbuPositif){
            this.namaSumbuPositif = new string[namaSumbuPositif.Length];
            this.namaSumbuPositif = namaSumbuPositif;
            kabelCollider.SetNamaSumbuPositif(this.namaSumbuPositif);
        }
        public void SetNamaSumbuNegatif(string[] namaSumbuNegatif){
            this.namaSumbuNegatif = new string[namaSumbuNegatif.Length];
            this.namaSumbuNegatif = namaSumbuNegatif;
            kabelCollider.SetNamaSumbuNegatif(this.namaSumbuNegatif);
        }

        public void SetMultimeterManager(MultimeterManager multimeterManager){
            this.multimeterManager = multimeterManager;
        }

        public void SetInitialTransform(Transform initialTransform){
            this.initialTransform = initialTransform;
        }

        public void SetSumbuYangTersambung(Sumbu sumbuYangTersambung){
            this.sumbuYangTersambung = sumbuYangTersambung;
            multimeterManager.CheckingKabelControllerSumbu();
        }

        //==============================================GETTER METHODS===================================================================
        public Sumbu GetSumbuYangTersambung(){
            return sumbuYangTersambung;
        }

        //=================================METHOD UNTUK MENGATUR KETIKA OBJEK DIPEGANG=================================================
        void OnSelectGrabEntered(XRBaseInteractor interactor){
            beingGrabbed = true;
        }

        void OnSelectGrabExited(XRBaseInteractor interactor){
            //beingGrabbed = false;
        }

    }
}

