using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRMultiplayer.Template.Offline.ObjectInteractions;
using VRMultiplayer.Template.Offline.Protocols;

namespace VRMultiplayer.Electrical.Equipment.TestPen{
    public class TestPenController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem tesPenLight;
        [SerializeField] private ObjectGlowManager testPenLightGlowManager;
        [SerializeField] private string[] namaKabelMenyala;

        private Collider[] kabelMenyala;

        void Start(){

            tesPenLight.Stop();

            kabelMenyala = new Collider[namaKabelMenyala.Length];

            for (int i = 0; i < kabelMenyala.Length; i++){
                kabelMenyala[i] = GameObject.Find(namaKabelMenyala[i]).GetComponent<Collider>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            for (int i = 0; i < kabelMenyala.Length; i++){
                if (other == kabelMenyala[i]){
                    tesPenLight.Play();
                    testPenLightGlowManager.StartTheProtocol();
                }
            } 
        }

        void OnTriggerExit(Collider other)
        {
            for (int i = 0; i < kabelMenyala.Length; i++){
                if (other == kabelMenyala[i]){
                    tesPenLight.Stop();
                    testPenLightGlowManager.StopTheProtocol();
                }
            } 
        }


    }

}
 