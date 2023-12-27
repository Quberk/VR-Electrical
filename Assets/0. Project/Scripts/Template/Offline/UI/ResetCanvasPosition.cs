using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VRMultiplayer.Template.Offline.UI{
    public class ResetCanvasPosition : MonoBehaviour
    {
        [SerializeField] private CanvasInFrontOfCameraPos canvasInFrontOfCameraPos;

        public void ResetCavnasPos(){
            canvasInFrontOfCameraPos.ResetCanvasPosition();
        }
    }

}
