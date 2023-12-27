using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRMultiplayer.Template.Offline.Protocols;
using VRMultiplayer.Template.Offline.UI;

namespace VRMultiplayer.Template.Offline.EquippingItem{
    public class PlayerGadgetManager : ProtocolManager
    {
        [SerializeField] private Gadget[] gadgets;
        [Header("Jika Menggunakan Checklist Panel")]
        [SerializeField] private bool useCheckPanel;
        [SerializeField] private ShowingUiPanelManager showingUiPanelManager;

        private bool gadgetProtocolEquipBegin;
        private bool triggerTheGadgetTrigger;

        private int gadgetEquippedAmount;

        void Start()
        {
            for(int i = 0; i < gadgets.Length; i++){

                if (gadgets[i].gadgetEquipmentController != null)
                    gadgets[i].gadgetPrefab = gadgets[i].gadgetEquipmentController.gameObject;

                gadgets[i].gadgetTrigger = gadgets[i].gadgetTriggerController.gameObject;
            }
        }


        void Update()
        {
            //Jika Protokol belum mulai bisa diabaikan saja
            if (!gadgetProtocolEquipBegin)
                return;

            //Jika Gadget yang sudah dipakai sudah lengkap maka Protokol selesai
            if (gadgetEquippedAmount >= gadgets.Length){
                StopTheProtocol();
            }

            //Membuat Objek Trigger agar dapat diTrigger oleh User
            if (!triggerTheGadgetTrigger){
                triggerTheGadgetTrigger = true;
                for (int i = 0; i < gadgets.Length; i++){
                    gadgets[i].gadgetTriggerController.BeginTheTrigger();
                }
            }

            //Mengecek Status Gadget Apakah sudah diPakai atau belum
            CheckingGadgetEquippedStatus();
        }

        //Fungsi untuk mengecek apakah Gadget sudah di Trigger
        void CheckingGadgetEquippedStatus(){

            for(int i = 0; i < gadgets.Length; i++){

                //Jika  tidak ada Trigger Controller ya diabaikan sja
                if (gadgets[i].gadgetTriggerController == null)
                    continue;

                if (gadgets[i].gadgetTriggerController.Equipped()){
                    gadgetEquippedAmount++;
                    PutGadgetInPlayer(gadgets[i]);

                    //Mengaktifkan Checklist jika menggunakan Checklist Panel
                    if (useCheckPanel)
                        gadgets[i].checlistUi.SetActive(true);
                    
                    Destroy(gadgets[i].gadgetTrigger);
                }
                    
            }
        }

        //Fungsi untuk memasukkan Gadget pada Badan Player
        void PutGadgetInPlayer(Gadget gadget){

            if (gadget.defaultPositionInPlayer == null)
                return;

            GameObject gadgetInPlayer = Instantiate(gadget.gadgetPrefab, transform.position, Quaternion.identity);
            gadgetInPlayer.transform.SetParent(gadget.defaultPositionInPlayer.transform);
            gadgetInPlayer.transform.localPosition = Vector3.zero;
            gadgetInPlayer.transform.localRotation = Quaternion.Euler(Vector3.zero);
            gadgetInPlayer.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        //===============================OVERRIDES FUNCTION===============================
        public override void StartTheProtocol()
        {
            gadgetProtocolEquipBegin = true;

            if (useCheckPanel)
                showingUiPanelManager.StartTheProtocol();

            protocolStarted = true;
        }

        public override void StopTheProtocol()
        {
            gadgetProtocolEquipBegin = false;
            protocolFinished = true;

            if (useCheckPanel)
            {
                showingUiPanelManager.StopTheProtocol();
            }
        }


    }

    [System.Serializable]
    public class Gadget{
        public string name;
        public GameObject defaultPositionInPlayer;
        public GadgetTriggerController gadgetTriggerController;
        public GadgetEquipmentController gadgetEquipmentController;
        public GameObject checlistUi;
        [HideInInspector] public GameObject gadgetPrefab;
        [HideInInspector] public GameObject gadgetTrigger;
        [HideInInspector] public bool equpped;
    }
}

