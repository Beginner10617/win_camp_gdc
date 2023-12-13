using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spawn : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate("JohnLemon", new Vector3(Random.Range(-12,-7),0, Random.Range(-6,2)), Quaternion.identity);   
    }
}
