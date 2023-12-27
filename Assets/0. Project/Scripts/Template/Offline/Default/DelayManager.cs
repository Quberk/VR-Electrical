using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRMultiplayer.Template.Offline.Protocols;

namespace VRMultiplayer.Template.Offline.Default{

    /// <summary>
    /// Class ini berfungsi mengatur Delay sebelum melanjutkan ke Protokol selanjutnya
    /// Setelah delay mencapai waktu yang ditentukan maka akan StopProtocol akan seara
    /// Otomatis terpanggil
    /// </summary>

    public class DelayManager : ProtocolManager
    {
        ///<param name = "delayTime">Waktu delay yang diinginkan</param>
        ///<param name = "delayCounter">Sebagai waktu yang dihitung</param>
        ///<param name = "startCounting">Variable yang menandakan mulainya untuk menghitung delayCounter</param>
        [SerializeField] private float delayTime;
        private float delayCounter = 0;

        private bool startCounting = false;

        void Update(){

            if (!startCounting)
                return;

            if (delayCounter >= delayTime){
                StopTheProtocol();
                Debug.Log("Delay Selesai");
                return;
            }
                

            delayCounter += Time.deltaTime;

        }


        //===============================OVERRIDES FUNCTION===============================
        
        ///Method yang berfungsi menjalankan Protokol
        public override void StartTheProtocol()
        {
            Debug.Log("Delay dimulai");
            startCounting = true;
            protocolStarted = true;
        }

        ///Method yang berfungsi menghentikan Protokol
        public override void StopTheProtocol()
        {
            protocolFinished = true;
        }
    }

}
