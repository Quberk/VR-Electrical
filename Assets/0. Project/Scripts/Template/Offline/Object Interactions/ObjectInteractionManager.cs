using UnityEngine;
using VRMultiplayer.Template.Offline.Protocols;
using VRMultiplayer.Template.Offline.UI;
using UnityEngine.UI;

///<SUMMARY>
/// SCRIPT INI ADALAH PROTOKOL UNTUK MENDETEKSI 2 OBJEK COLLIDED LALU PROTOKOL SELESAI SETELAH 2 OBJEK TARGET SALING BERTEMU
///</SUMMARY>
namespace VRMultiplayer.Template.Offline.ObjectInteractions{
    public class ObjectInteractionManager : ProtocolManager
    {
        [SerializeField] private ObjectInteraction[] objectInteractions;

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

        void Start()
        {
            //Menonaktifkan Objek yang ingin diaktifkan
            for (int i = 0; i < objectInteractions.Length; i++){
                if (objectInteractions[i].activatedObject != null){
                    objectInteractions[i].activatedObject.SetActive(false);
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
            if (step > objectInteractions.Length){
                StopTheProtocol();
                step -= 1;//Kembalikan jumlah Step
                return;
            }

            startCheckingCollision = true;
                
            objectInteractions[step - 1].firstObjectGlowManager.StartTheProtocol();
            objectInteractions[step - 1].secondObjectGlowManager.StartTheProtocol();

            
        }
        
        //Method untuk mengecek Collider
        void CheckingColliderCollision(){

            if (!startCheckingCollision)
                return;

            //Mengambil Collider yang ingin dicek sesuai dengan step yang sedang berjalan
            Collider collider1 = objectInteractions[step - 1].firstObjectCollider;
            Collider collider2 = objectInteractions[step - 1].secondObjectCollider;

            int firstObjectLayerMask = objectInteractions[step - 1].firstObjectLayerMask;
            int secondObjectLayerMask = objectInteractions[step - 1].secondObjectLayerMask;

            //Objek yang ingin dicek haruslah berbentuk Kotak
            if (Physics.OverlapBox(collider1.bounds.center, collider1.bounds.extents, collider1.transform.rotation, secondObjectLayerMask).Length > 0 &&
                Physics.OverlapBox(collider2.bounds.center, collider2.bounds.extents, collider2.transform.rotation, firstObjectLayerMask).Length > 0)
            {

                objectInteractions[step - 1].firstObjectGlowManager.StopTheProtocol();
                objectInteractions[step - 1].secondObjectGlowManager.StopTheProtocol();

                //Melanjutkan pada Step selanjutnya
                startNextSteepCooldown = true;
                startCheckingCollision = false;

                //Menonaktifkan Objek yang ingin diaktifkan
                if (objectInteractions[step - 1].activatedObject != null){
                    objectInteractions[step - 1].activatedObject.SetActive(true);
                }

                collided = true;
            }

        }

        void CheckingColliderReference(){

            for(int i = 0; i < objectInteractions.Length; i++){

                //Mengecek First Collider
                if (objectInteractions[i].firstObjectCollider == null){
                    objectInteractions[i].firstObjectCollider = GameObject.FindGameObjectWithTag(objectInteractions[i].firstColliderTag).GetComponent<Collider>();
                    objectInteractions[i].firstObjectGlowManager = GameObject.FindGameObjectWithTag(objectInteractions[i].firstGlowTag).GetComponent<ObjectGlowManager>();
                }

                //Mengecek Second Collider
                if (objectInteractions[i].secondObjectCollider == null){
                    objectInteractions[i].secondObjectCollider = GameObject.FindGameObjectWithTag(objectInteractions[i].secondColliderTag).GetComponent<Collider>();
                    objectInteractions[i].secondObjectGlowManager = GameObject.FindGameObjectWithTag(objectInteractions[i].secondGlowTag).GetComponent<ObjectGlowManager>();
                }

                
            }

            StartTheNextStep();
            beginTheProtocol = true;
        }

        //===================================GET METHOD=======================================
        public ObjectInteraction[] GetObjectInteractions(){
            return objectInteractions;
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
    public class ObjectInteraction{
        public string JudulObjectInteraction;

        [Header("Pastikan Collider berbentuk Kotak")]
        public Collider firstObjectCollider;
        public ObjectGlowManager firstObjectGlowManager;
        public LayerMask firstObjectLayerMask;

        public Collider secondObjectCollider;
        public ObjectGlowManager secondObjectGlowManager;
        public LayerMask secondObjectLayerMask;

        [Header("Gunakan Tag jika Object akan diinstantiate saat Aplikasi Running")]
        public string firstColliderTag;
        public string firstGlowTag;
        public string secondColliderTag;
        public string secondGlowTag;

        [Header("Objek yang diaktifkan")]
        public GameObject activatedObject;
    }
}
