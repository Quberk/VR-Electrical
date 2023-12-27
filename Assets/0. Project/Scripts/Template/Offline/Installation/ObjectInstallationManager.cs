using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using VRMultiplayer.Template.Offline.UI;
using VRMultiplayer.Template.Offline.Protocols;

namespace VRMultiplayer.Template.Offline.Installation{

    /// <summary>
    /// Class ini berfungsi mengatur cara kerja Pemasangan Objek pada Objek lainnya
    /// Class ini harus dipasangkan hanya pada Object yang ingin dipasang
    /// </summary>

    [RequireComponent(typeof(XRGrabInteractable))]
    public class ObjectInstallationManager : ProtocolManager
    {
        ///<param name = "objectToInstall">Objek yang ingin di pasang</param>
        ///<param name = "installedObjectPosition">Trigger Posisi dari Objek yang ingin dipasang</param>
        ///<param name = "objectInstalledTo">Objek yang ingin dipasangkan nantinya ObjectToInstall dijadikan Child dari Objek ini</param>
        ///<param name = "protocolStarted">Variabel untuk mengetahui apakah Simulasi Instalasi sudah berkalan atau belum</param>
        ///<param name = "grabInteractable">XRGrabInteractable Component pada objek yang ingin dipasang</param>
        ///<param name = "objectIsGrabbed">Info apakah objek yang ingin dipasang sedang di Grab atau tidak</param>
        [SerializeField] private GameObject objectToInstall;
        [SerializeField] private ObjectGlowManager objectToInstallGlowManager;
        [SerializeField] private GameObject installedObjectPosition;
        [SerializeField] private GameObject objectInstalledTo;
        
        private bool protocolStarted = false;

        private XRGrabInteractable grabInteractable;
        private bool objectIsGrabbed = false; //Variable untuk mengetahui apakah ObjectToInstall sedang di grab atau tidak

        [Header("Jika Menggunakan Checklist Panel")]
        [SerializeField] private bool useCheckPanel;
        [SerializeField] private ShowingUiPanelManager showingUiPanelManager;

        [SerializeField] private bool useNextSystem;
        [SerializeField] private Button nextButton;
        [SerializeField] private GameObject checklistPanel;

        /// <summary>
        /// Pada awal permainan maka Posisi objek yang ingin dipasang akan di nonaktifkan
        /// Juga mengambil referensi dari XRGrabInteractable pada objek yang ingin dipasang
        /// Memasukkan juga Method yang berfungsi mengetahui apakah objek yang ingin dipasang sedang di Grab atau tidak
        /// </summary>
        void Start(){

            installedObjectPosition.SetActive(false);
            grabInteractable = objectToInstall.GetComponent<XRGrabInteractable>();
            grabInteractable.onSelectExited.AddListener(OnSelectExit);
            grabInteractable.onSelectEntered.AddListener(OnSelectEnter);

        }

        /// <summary>
        /// Method yang berfungsi untuk mengaktifkan Installation Protocol tandanya Objek akan Glow dan Pemasangan dapat dilakukan
        /// Menyalakan Glow Effect pada Objek yang ingin dipasang
        /// </summary>
        private void StartTheInstallationProtocol(){

            installedObjectPosition.SetActive(true);
            protocolStarted = true;

            //Glow Effect
            objectToInstallGlowManager.StartTheProtocol();
        }

        /// <summary>
        /// Mengecek apakah Objek yang ingin diinstall sudah berada pada Trigger Posisinya
        /// Dicek juga apakah Protocol Instalasi sudah dimulai
        /// Dicek juga apakah Objek sudah berhenti diGrab
        /// Setelah semua kondisi benar barulah Objek dianggap terpasang
        /// Objek yang ingin diinstall ditaruh pada Posisi yang seharusnya dan dimasukkan sbg Child dari Objek yang ingin dipasangkan
        /// Objek yang dipasang juga dibuat agar tdk dapat di Grab lagi
        /// Objek yang dijadikan referensi posisi juga akan dinonaktifkan
        /// Rigidbody dari Objek yang dipasang akan dinonaktfikan
        /// Posisi & Rotasi juga disesuaikan
        /// Mematikan glow effect
        /// Menyelesaikan Protokol
        /// </summary>
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Target_Object") && protocolStarted && !objectIsGrabbed){

                objectToInstall.transform.position = installedObjectPosition.transform.position;
                objectToInstall.transform.rotation = installedObjectPosition.transform.rotation;
                objectToInstall.transform.localScale = installedObjectPosition.transform.localScale;
                objectToInstall.transform.SetParent(objectInstalledTo.transform);

                grabInteractable.enabled = false;

                installedObjectPosition.SetActive(false);

                Rigidbody objectToInstallRb = GetComponent<Rigidbody>();
                objectToInstallRb.isKinematic = true;

                //Glow Effect
                objectToInstallGlowManager.StopTheProtocol();

                //Finish The Protocol
                StopTheProtocol();

            }
        }

        /// OnSelectExit sbg Method yang dipanggil ketika objek berhenti diGrab
        void OnSelectExit(XRBaseInteractor interactor){
            objectIsGrabbed = false;
        }

        /// OnSelectExit sbg Method yang dipanggil ketika objek mulai diGrab
        void OnSelectEnter(XRBaseInteractor interactor){
            objectIsGrabbed = true;
        }

        //===============================OVERRIDES FUNCTION===============================
        
        ///Method yang berfungsi menjalankan Protokol
        public override void StartTheProtocol()
        {
            if (useNextSystem){
                nextButton.onClick.AddListener(StopTheProtocol);
                nextButton.gameObject.SetActive(false);
            }

            if (useCheckPanel)
                showingUiPanelManager.StartTheProtocol();
            
            StartTheInstallationProtocol();
            protocolStarted = true;
        }

        ///Method yang berfungsi menghentikan Protokol
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
        }
    }
}

