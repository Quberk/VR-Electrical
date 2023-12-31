using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace VRMultiplayer.Template.Online.Network{
    public class NetworkManager : MonoBehaviourPunCallbacks 
    {
        // Start is called before the first frame update
        void Start()
        {
            ConnectToServer();
        }

        void ConnectToServer(){
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Try to Connect To Server...");
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected To Server!");
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 10;
            roomOptions.IsVisible = true;
            roomOptions.IsOpen = true;

            PhotonNetwork.JoinOrCreateRoom("Room 1", roomOptions, TypedLobby.Default);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Joined a Room!");

        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            Debug.Log("A new Player Joined the room!");
        }


    }
}

