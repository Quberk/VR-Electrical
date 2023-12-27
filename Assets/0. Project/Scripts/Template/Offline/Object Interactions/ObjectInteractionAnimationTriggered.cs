using UnityEngine;
using VRMultiplayer.Template.Offline.Installation;
using VRMultiplayer.Template.Offline.UI;
using UnityEngine.UI;
using VRMultiplayer.Template.Offline.Protocols;

///<SUMMARY>
/// SCRIPT INI ADALAH PROTOKOL UNTUK MENDETEKSI 2 OBJEK COLLIDED
/// SETELAH 2 OBJEK SALING BERTEMU MAKA AKAN MENJALANKAN ANIMASI
/// ANIMASI AKAN BERHENTI KETIKA OBJEK TIDAK SALING BERTEMU
///</SUMMARY>
namespace VRMultiplayer.Template.Offline.objectInteractionWithAnimations{
    public class ObjectInteractionAnimationTriggered : ProtocolManager
    {
        [SerializeField] private ObjectInteractionWithAnimation[] objectInteractionWithAnimations;

        [Header("Jika Menggunakan Checklist Panel")]
        [SerializeField] private bool useCheckPanel;
        [SerializeField] private ShowingUiPanelManager showingUiPanelManager;

        [SerializeField] private bool useNextSystem;
        [SerializeField] private Button nextButton;
        [SerializeField] private GameObject checklistPanel;

        private bool beginTheProtocol = false;

        [Header("Steps")]
        [SerializeField] private float stepCoolDownTime;
        private float stepCoolDownCounter = 0;
        private int step = 0;
        private bool startNextSteepCooldown = false;

        private bool startCheckingCollision = false;

        private bool collided = false;

        private bool animationAlreadyTriggered = false;

        void Start()
        {
            //Menonaktifkan Objek yang ingin diaktifkan
            for (int i = 0; i < objectInteractionWithAnimations.Length; i++){
                if (objectInteractionWithAnimations[i].activatedObject != null){
                    objectInteractionWithAnimations[i].activatedObject.SetActive(false);
                }
            }
        }

        void Update(){

            //Jika Protokol belum dimulai maka Tidak melakukan apa-apa
            if (!beginTheProtocol)
                return;

            NextStepCoolDown();

            CheckingColliderCollision();
        }

        void NextStepCoolDown(){
            
            if (!startNextSteepCooldown)
                return;

            collided = false;

            stepCoolDownCounter += Time.deltaTime;

            if (stepCoolDownCounter >= stepCoolDownTime){
                stepCoolDownCounter = 0;
                StartTheNextStep();
                startNextSteepCooldown = false;
            }
        }

        void StartTheNextStep(){

            step++;

            //Jika Step sudah selesai maka Protokol selesai
            if (step > objectInteractionWithAnimations.Length){
                StopTheProtocol();
                step -= 1;//Kembalikan jumlah Step
                return;
            }

            startCheckingCollision = true; //Memperbolehkan untuk Check Collision lagi
            animationAlreadyTriggered = false; //Memperbolehkan untuk mentrigger Animasi lagi
                
            objectInteractionWithAnimations[step - 1].firstObjectGlowManager.StartTheProtocol();
            objectInteractionWithAnimations[step - 1].secondObjectGlowManager.StartTheProtocol();
            
        }
        
        //Method untuk mengecek Collider
        void CheckingColliderCollision(){

            if (!startCheckingCollision)
                return;

            //Mengambil Collider yang ingin dicek sesuai dengan step yang sedang berjalan
            Collider collider1 = objectInteractionWithAnimations[step - 1].firstObjectCollider;
            Collider collider2 = objectInteractionWithAnimations[step - 1].secondObjectCollider;

            int firstObjectLayerMask = objectInteractionWithAnimations[step - 1].firstObjectLayerMask;
            int secondObjectLayerMask = objectInteractionWithAnimations[step - 1].secondObjectLayerMask;

            //Objek yang ingin dicek haruslah berbentuk Kotak
            if (Physics.OverlapBox(collider1.bounds.center, collider1.bounds.extents, collider1.transform.rotation, secondObjectLayerMask).Length > 0 &&
                Physics.OverlapBox(collider2.bounds.center, collider2.bounds.extents, collider2.transform.rotation, firstObjectLayerMask).Length > 0)
            {
                Debug.Log("(Object Interaction Animation Triggered) Collider1 dan Collider2 bersinggungan.");


                //Menonaktifkan Objek yang ingin diaktifkan
                if (objectInteractionWithAnimations[step - 1].activatedObject != null){
                    objectInteractionWithAnimations[step - 1].activatedObject.SetActive(true);
                }

                collided = true;

                //Menyalakan Target Animasi
                if (!animationAlreadyTriggered)
                    objectInteractionWithAnimations[step - 1].targetAnimator.SetTrigger(objectInteractionWithAnimations[step - 1].animationActionTriggerName);

                objectInteractionWithAnimations[step - 1].targetAnimator.SetFloat(objectInteractionWithAnimations[step - 1].animationActionSpeedName, 1);

                //Memastikan bahwa animasi di Trigger hanya satu kali ketik objek saling bertemu
                animationAlreadyTriggered = true;

                //Mengecek apakah animasi telah selesai
                if(objectInteractionWithAnimations[step - 1].animationTriggeredFinishStatus.GetAnimationFinishedStatus()){
                    
                    //Melanjutkan pada Step selanjutnya
                    startNextSteepCooldown = true;
                    startCheckingCollision = false;

                    //Mematikan Glow Manager
                    objectInteractionWithAnimations[step - 1].firstObjectGlowManager.StopTheProtocol();
                    objectInteractionWithAnimations[step - 1].secondObjectGlowManager.StopTheProtocol();
                }
            }

            else{
                //Mempause Target Animasi
                objectInteractionWithAnimations[step - 1].targetAnimator.SetFloat(objectInteractionWithAnimations[step - 1].animationActionSpeedName, 0);
            }

        }

        void CheckingColliderReference(){

            for(int i = 0; i < objectInteractionWithAnimations.Length; i++){

                //Mengecek First Collider
                if (objectInteractionWithAnimations[i].firstObjectCollider == null){
                    objectInteractionWithAnimations[i].firstObjectCollider = GameObject.FindGameObjectWithTag(objectInteractionWithAnimations[i].firstColliderTag).GetComponent<Collider>();
                    objectInteractionWithAnimations[i].firstObjectGlowManager = GameObject.FindGameObjectWithTag(objectInteractionWithAnimations[i].firstGlowTag).GetComponent<ObjectGlowManager>();
                }

                //Mengecek Second Collider
                if (objectInteractionWithAnimations[i].secondObjectCollider == null){
                    objectInteractionWithAnimations[i].secondObjectCollider = GameObject.FindGameObjectWithTag(objectInteractionWithAnimations[i].secondColliderTag).GetComponent<Collider>();
                    objectInteractionWithAnimations[i].secondObjectGlowManager = GameObject.FindGameObjectWithTag(objectInteractionWithAnimations[i].secondGlowTag).GetComponent<ObjectGlowManager>();
                }

                
            }

            StartTheNextStep();
            beginTheProtocol = true;
        }

        //===================================GET METHOD=======================================
        public ObjectInteractionWithAnimation[] GetobjectInteractionWithAnimations(){
            return objectInteractionWithAnimations;
        }

        public int GetStep(){
            return step;
        }

        public bool GetCollided(){
            return collided;
        }


        //===============================OVERRIDES FUNCTION===============================
        public override void StartTheProtocol()
        {
            if (useNextSystem){
                nextButton.onClick.AddListener(StopTheProtocol);
                nextButton.gameObject.SetActive(false);
            }

            if (useCheckPanel)
                showingUiPanelManager.StartTheProtocol();

            CheckingColliderReference();

            protocolStarted = true;

        }

        public override void StopTheProtocol()
        {
            //Jika Menggunakan NextSystem maka Protocol tidak langsung selesai melainkan memunculkan Next Button terlebih dahulu
            if (useNextSystem){
                nextButton.gameObject.SetActive(true);
                checklistPanel.SetActive(true);
                useNextSystem = false;
                return;
            }

            protocolFinished = true;
            beginTheProtocol = false;
        }

    }

    [System.Serializable]
    public class ObjectInteractionWithAnimation{

        public string Judul;

        [Header("Pastikan Collider berbentuk Kotak")]
        public Collider firstObjectCollider;
        public ObjectGlowManager firstObjectGlowManager;
        public LayerMask firstObjectLayerMask;

        public Collider secondObjectCollider;
        public ObjectGlowManager secondObjectGlowManager;
        public LayerMask secondObjectLayerMask;

        [Header("Gunakan Tag jika Object adalah Prefab")]
        public string firstColliderTag;
        public string firstGlowTag;
        public string secondColliderTag;
        public string secondGlowTag;

        [Header("Objek yang diaktifkan")]
        public GameObject activatedObject;

        [Header("Animasi yang akan tertrigger")]
        public Animator targetAnimator;
        public string animationActionTriggerName;
        public string animationActionSpeedName;
        public AnimationTriggeredFinishStatus animationTriggeredFinishStatus;
    }

}

