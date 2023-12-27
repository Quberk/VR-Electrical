using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRMultiplayer.Template.Offline.Protocols;

namespace VRMultiplayer.Template.Offline.UI{
    public class ShowingUiPanelManager : ProtocolManager
    {
        [SerializeField] private GameObject uiPanel;
        [SerializeField] private Animator uiAnimator;

        [Header("Jika UI Menghilang dengan jangka waktu tertentu")]
        [SerializeField] private bool useTimer;
        [SerializeField] private float timer;
        private float timerCounter = 0;
        private bool startTimer = false;

        void Start(){

        }

        void Update(){
           
            if (startTimer)
                Timer();
        }
        
        void ShowThePanel(){
            uiPanel.SetActive(true);
            uiAnimator.SetTrigger("OpenPanel");
            startTimer = true;
        }

        void HideThePanel(){
            uiAnimator.SetTrigger("ClosePanel");
        }

        public void DeactivatePanel(){
            uiPanel.SetActive(false);
        }

        void Timer(){

            if (!useTimer)
                return;

            if (timerCounter >= timer){
                StopTheProtocol();
                timerCounter = 0;
                useTimer = false;
            }
                
            timerCounter += Time.deltaTime;
        }

        //===============================OVERRIDES FUNCTION===============================
        public override void StartTheProtocol()
        {
            ShowThePanel();
            protocolStarted = true;
        }

        public override void StopTheProtocol()
        {
            HideThePanel();
            protocolFinished = true;
        }

    }

}



