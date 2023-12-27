using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace VRMultiplayer.Template.Online.Network{
    public class NetworkPlayerSpawner : MonoBehaviourPunCallbacks
    {
        [SerializeField] private GameObject playerPrefab;
        private GameObject spawnedPlayerPrefab;

        public override void OnJoinedRoom()
        {
            spawnedPlayerPrefab = PhotonNetwork.Instantiate(playerPrefab.name, transform.position, transform.rotation);
        }

        public override void OnLeftRoom()
        {
            PhotonNetwork.Destroy(spawnedPlayerPrefab);
        }
    }
}

