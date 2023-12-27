using System.Collections;
using System.Collections.Generic;
using UnityEngine;using VRMultiplayer.Template.Offline.Protocols;

namespace VRMultiplayer.Template.Offline.UI{
    public class ShowingUiPanelAdditional : ShowingUiPanelManager
    {
        [SerializeField] private SimulationManager simulationManager;
        [SerializeField] private int startStep;
        [SerializeField] private int stopStep;

        private bool alreadyStartedProtocol = false;
        

        void Update(){

            if (protocolFinished)
                return;
            
            //Simulasi dimulai ketika Simulation Manager sampai pada Step tertentu
            if (simulationManager.GetStep() == startStep + 1 && alreadyStartedProtocol == false){
                StartTheProtocol();
                alreadyStartedProtocol = true;
            }

            if (simulationManager.GetStep() == stopStep + 1){
                StopTheProtocol();
            }
        }


    }
}


