using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<SUMMARY>
/// SCRIPT INI DITARUH PADA TARGET ANIMATION DARI <ObjectInteractionAnimationTriggered>
/// BERFUNGSI UNTUK MENGETAHUI APAKAH ANIMASI YANG DITARGETKAN TELAH SELESAI
///</SUMMARY>
namespace VRMultiplayer.Template.Offline.objectInteractionWithAnimations{
    public class AnimationTriggeredFinishStatus : MonoBehaviour
    {
        private bool animationFinishedStatus = false;

        public void AnimationFinished(){
            animationFinishedStatus = true;
        }

        //=======================================GET METHODS===============================================
        public bool GetAnimationFinishedStatus(){
            return animationFinishedStatus;
        }
    } 
}

