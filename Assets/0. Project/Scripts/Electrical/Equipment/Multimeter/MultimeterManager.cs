using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace VRMultiplayer.Electrical.Equipment.Multimeter{

    [RequireComponent(typeof(XRGrabInteractable))]
    public class MultimeterManager : MonoBehaviour
    {
        private XRGrabInteractable xRGrabInteractable;
        [SerializeField] private Animator multimeterAnimator;
        [SerializeField] private GameObject batangMerahPrefab;
        [SerializeField] private GameObject batangHitamPrefab;

        [SerializeField] private Transform batangMerahInitialTransform;
        [SerializeField] private Transform batangHitamInitialTransform;

        [SerializeField] private string[] namaSumbuPositif; //Nama-nama Gameobject yang sebagai Sumbu Positif
        [SerializeField] private string[] namaSumbuNegatif; //Nama-nama Gameobject yang sebagai Sumbu Negatif
        
        private bool beingGrabbed = false;

        private KabelController hitamKabelController;
        private KabelController merahKabelController;

        private Sumbu hitamSumbuYangTersambung;
        private Sumbu merahSumbuYangTersambung;

        // Start is called before the first frame update
        void Start()
        {
            xRGrabInteractable = GetComponent<XRGrabInteractable>();
            xRGrabInteractable.onSelectEntered.AddListener(OnSelectGrabEntered);
            xRGrabInteractable.onSelectExited.AddListener(OnSelectGrabExited);

            GameObject batangHitam = Instantiate(batangHitamPrefab, transform.position, Quaternion.identity);
            GameObject batangMerah = Instantiate(batangMerahPrefab, transform.position, Quaternion.identity);

            hitamKabelController =  batangHitam.GetComponent<KabelController>();
            merahKabelController = batangMerah.GetComponent<KabelController>();

            hitamKabelController.SetInitialTransform(batangHitamInitialTransform);
            merahKabelController.SetInitialTransform(batangMerahInitialTransform);

            hitamKabelController.SetMultimeterManager(this);
            merahKabelController.SetMultimeterManager(this);

            hitamKabelController.SetNamaSumbuPositif(namaSumbuPositif);
            hitamKabelController.SetNamaSumbuNegatif(namaSumbuNegatif);
            merahKabelController.SetNamaSumbuPositif(namaSumbuPositif);
            merahKabelController.SetNamaSumbuNegatif(namaSumbuNegatif);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CheckingKabelControllerSumbu(){

            if((hitamKabelController.GetSumbuYangTersambung() == Sumbu.SumbuPositif && merahKabelController.GetSumbuYangTersambung() == Sumbu.SumbuNegatif) || 
                (hitamKabelController.GetSumbuYangTersambung() == Sumbu.SumbuNegatif && merahKabelController.GetSumbuYangTersambung() == Sumbu.SumbuPositif)){
                multimeterAnimator.SetTrigger("220V");
            }
    
            else
                multimeterAnimator.SetTrigger("On");
        }

        //============================================GETTER METHODS=======================================================================
        public bool GetGrabbedStatus(){
            return beingGrabbed;
        }

         //=================================METHOD UNTUK MENGATUR KETIKA OBJEK DIPEGANG=================================================
        void OnSelectGrabEntered(XRBaseInteractor interactor){
            multimeterAnimator.SetTrigger("On");
            beingGrabbed = true;
        }

        void OnSelectGrabExited(XRBaseInteractor interactor){
            multimeterAnimator.SetTrigger("Off");
            beingGrabbed = false;
        }

    }

}
