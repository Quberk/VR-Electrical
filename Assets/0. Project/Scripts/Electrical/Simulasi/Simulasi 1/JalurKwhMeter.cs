using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRMultiplayer.Template.Offline.Protocols;

namespace VRMultiplayer.Electrical.Simulasi.Simulasi1{
    public class JalurKwhMeter : MonoBehaviour
    {
        [Header("Glow Managers")]
        [SerializeField] private ObjectGlowManager kabelPlnGlowManager;
        [SerializeField] private ObjectGlowManager kabelPanelMcbGlowManager;
        [SerializeField] private ObjectGlowManager groundGlowManager;

        [Header("UI")]
        [SerializeField] private GameObject kabelPlnUi;
        [SerializeField] private GameObject panelMcbUi;
        [SerializeField] private GameObject groundUi;

        // Start is called before the first frame update
        void Start()
        {
            kabelPlnUi.SetActive(false);
            panelMcbUi.SetActive(false);
            groundUi.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            //Mengecek apakah semua Protokol sudah selesai
            if (kabelPlnGlowManager.IsProtocolFinished() == true 
                && kabelPanelMcbGlowManager.IsProtocolFinished() == true
                && groundGlowManager.IsProtocolFinished() == true)
                Destroy(gameObject);

            //Mengecek apakah sudah Mulai Protokolnya
            if (kabelPlnGlowManager.IsProtocolStarted() == true)
                kabelPlnUi.SetActive(true);

            if (kabelPanelMcbGlowManager.IsProtocolStarted() == true)
                panelMcbUi.SetActive(true);

            if (groundGlowManager.IsProtocolStarted() == true)
                groundUi.SetActive(true);

            //Mengecek apakah sudah Selesai Protokolnya
            if (kabelPlnGlowManager.IsProtocolFinished() == true)
                kabelPlnUi.SetActive(false);

            if (kabelPanelMcbGlowManager.IsProtocolFinished() == true)
                panelMcbUi.SetActive(false);

            if (groundGlowManager.IsProtocolFinished() == true)
                groundUi.SetActive(false);
        }   
    }

}
