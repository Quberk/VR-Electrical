using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VRMultiplayer.Template.Offline.UI{
    public class GeneralUiManager : MonoBehaviour
    {
        public void MoveToScene(string sceneName){
            SceneManager.LoadScene(sceneName);
        }
    }

}
