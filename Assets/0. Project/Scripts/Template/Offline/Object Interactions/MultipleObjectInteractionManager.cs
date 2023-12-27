using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRMultiplayer.Template.Offline.Protocols;
using VRMultiplayer.Template.Offline.UI;
using UnityEngine.UI;


///<SUMMARY> 
///SCRIPT INI MENGATUR COLLISION ANTARA 2 OBJEK
///SCRIPT INI MERUPAKAN PROTOKOL YANG MENDETEKSI JIKA 2 BUAH OBJEK SUDAH BERSENTUHAN
///MAKA PROTOKOL AKAN SELESAI
///</SUMMARY>

namespace VRMultiplayer.Template.Offline.ObjectInteractions{
    public class MultipleObjectInteractionManager : ProtocolManager
    {
        
        [SerializeField] private MultipleObjectInteraction[] multipleObjectInteractions;

        [Header("Jika Menggunakan Checklist Panel")]
        [SerializeField] private bool useCheckPanel;
        [SerializeField] private ShowingUiPanelManager showingUiPanelManager;
        [SerializeField] private GameObject checklistPanel;
        [SerializeField] private bool useNextSystem;
        [SerializeField] private Button nextButton;

        private bool beginTheProtocol = false;

        [Header("Steps")]
        [SerializeField] private float stepCoolDownTime;
        private float stepCoolDownCounter = 0;
        private int step = 0;
        private bool startNextSteepCooldown = false;

        private bool startCheckingCollision = false;


        void Start()
        {
            //Menonaktifkan Objek yang ingin diaktifkan
            for (int i = 0; i < multipleObjectInteractions.Length; i++){
                if (multipleObjectInteractions[i].activatedObject != null){
                    multipleObjectInteractions[i].activatedObject.SetActive(false);
                }
            }
        }

        void Update()
        {
            //Jika Protokol belum dimulai maka Tidak melakukan apa-apa
            if (!beginTheProtocol)
                return;

            NextStepCoolDown();

            CheckingColliderCollision();
        }

        //Method untuk mencari Referensi Collider dan Glow Manager berdasarkan Tag
        void CheckingColliderReference(){

            for(int i = 0; i < multipleObjectInteractions.Length; i++){

                //Mengecek First Collider
                if (multipleObjectInteractions[i].firstObjectCollider.Length == 0){
                    
                    //Mencari Colliders berdasarkan Tag
                    GameObject[] firstObjectColliders = GameObject.FindGameObjectsWithTag(multipleObjectInteractions[i].firstColliderTag);
                    multipleObjectInteractions[i].firstObjectCollider = new Collider[firstObjectColliders.Length];
                    for(int j = 0; j < firstObjectColliders.Length; j++){
                        multipleObjectInteractions[i].firstObjectCollider[j] = firstObjectColliders[j].GetComponent<Collider>();
                    }

                    //Mencari First Glow Manager
                    GameObject[] firstObjectGlowManagers = GameObject.FindGameObjectsWithTag(multipleObjectInteractions[i].firstGlowTag);
                    multipleObjectInteractions[i].firstObjectGlowManager = new ObjectGlowManager[firstObjectGlowManagers.Length];
                    for (int j = 0; j < firstObjectGlowManagers.Length; j++){
                        multipleObjectInteractions[i].firstObjectGlowManager[j] = firstObjectGlowManagers[j].GetComponent<ObjectGlowManager>();
                    }

                }

                //Mengecek Second Collider
                if (multipleObjectInteractions[i].secondObjectCollider.Length == 0){

                    //Mencari Colliders berdasarkan Tag
                    GameObject[] secondObjectColliders = GameObject.FindGameObjectsWithTag(multipleObjectInteractions[i].secondColliderTag);
                    multipleObjectInteractions[i].secondObjectCollider = new Collider[secondObjectColliders.Length];
                    for(int j = 0; j < secondObjectColliders.Length; j++){
                        multipleObjectInteractions[i].secondObjectCollider[j] = secondObjectColliders[j].GetComponent<Collider>();
                    }

                    //Mencari Glow Manager berdasarkan Tag
                    GameObject[] secondObjectGlowManagers = GameObject.FindGameObjectsWithTag(multipleObjectInteractions[i].secondGlowTag);
                    multipleObjectInteractions[i].secondObjectGlowManager = new ObjectGlowManager[secondObjectGlowManagers.Length];
                    for (int j = 0; j < secondObjectGlowManagers.Length; j++){
                        multipleObjectInteractions[i].secondObjectGlowManager[j] = secondObjectGlowManagers[j].GetComponent<ObjectGlowManager>();
                    }
                }

                
            }

            StartTheNextStep();
            beginTheProtocol = true;
        }

        void NextStepCoolDown(){
            
            if (!startNextSteepCooldown)
                return;

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
            if (step > multipleObjectInteractions.Length){
                StopTheProtocol();
                step -= 1;//Kembalikan jumlah Step
                return;
            }

            startCheckingCollision = true;

            //Menyalakan Glow Manager
            for (int i = 0; i <  multipleObjectInteractions[step - 1].firstObjectGlowManager.Length; i++){
                 multipleObjectInteractions[step - 1].firstObjectGlowManager[i].StartTheProtocol();
            }

            for (int i = 0; i <  multipleObjectInteractions[step - 1].secondObjectGlowManager.Length; i++){
                 multipleObjectInteractions[step - 1].secondObjectGlowManager[i].StartTheProtocol();
            }

            
        }

        //Method untuk mengecek Collider
        void CheckingColliderCollision(){

            if (!startCheckingCollision)
                return;

            //Mengambil Collider yang ingin dicek sesuai dengan step yang sedang berjalan
            Collider[] collider1 = multipleObjectInteractions[step - 1].firstObjectCollider;
            Collider[] collider2 = multipleObjectInteractions[step - 1].secondObjectCollider;

            int firstObjectLayerMask = multipleObjectInteractions[step - 1].firstObjectLayerMask;
            int secondObjectLayerMask = multipleObjectInteractions[step - 1].secondObjectLayerMask;

            int colliderAmountBeingCollided = 0; //Variable untuk menghitung apakah semua Collider FirstCollider dan SecondCollider sudah Collide atau belum

            for (int i = 0; i < collider1.Length; i++){
                
                //Objek yang ingin dicek haruslah 
                if (Physics.OverlapBox(collider1[i].bounds.center, collider1[i].bounds.extents, collider1[i].transform.rotation, secondObjectLayerMask).Length > 0 &&
                    Physics.OverlapBox(collider2[i].bounds.center, collider2[i].bounds.extents, collider2[i].transform.rotation, firstObjectLayerMask).Length > 0)
                {
                    colliderAmountBeingCollided++;

                }
            }


            //Jika Semua Collider First dan Second bersinggungan barulah kita bisa ke Next Step
            if (colliderAmountBeingCollided == collider1.Length){
                //Melanjutkan pada Step selanjutnya
                startNextSteepCooldown = true;
                startCheckingCollision = false;

                //Mematikan Glow Object
                for(int j = 0; j < multipleObjectInteractions[step - 1].firstObjectGlowManager.Length; j++){
                    multipleObjectInteractions[step - 1].firstObjectGlowManager[j].StopTheProtocol();
                }
                for (int j = 0; j < multipleObjectInteractions[step - 1].secondObjectGlowManager.Length; j++){
                    multipleObjectInteractions[step - 1].secondObjectGlowManager[j].StopTheProtocol();
                }

                //Mengaktifkan Objek yang ingin diaktifkan
                if (multipleObjectInteractions[step - 1].activatedObject != null){
                    multipleObjectInteractions[step - 1].activatedObject.SetActive(true);
                }
            }
        }
        

        //===================================GET METHOD=======================================
        public MultipleObjectInteraction[] GetObjectInteractions(){
            return multipleObjectInteractions;
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
                useNextSystem = false;
                return;
            }

            protocolFinished = true;
            beginTheProtocol = false;
            checklistPanel.SetActive(true);
        }
    }

    [System.Serializable]
    public class MultipleObjectInteraction{
        public string JudulObjectInteraction;

        [Header("Pastikan Collider berbentuk Kotak")]
        public Collider[] firstObjectCollider;
        public ObjectGlowManager[] firstObjectGlowManager;
        public LayerMask firstObjectLayerMask;

        public Collider[] secondObjectCollider;
        public ObjectGlowManager[] secondObjectGlowManager;
        public LayerMask secondObjectLayerMask;

        [Header("Gunakan Tag jika Objectnya Prefab")]
        public string firstColliderTag;
        public string firstGlowTag;
        public string secondColliderTag;
        public string secondGlowTag;
        
        [Header("Objek yang diaktifkan")]
        public GameObject activatedObject;
    }
}

