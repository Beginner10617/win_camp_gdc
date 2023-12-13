using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ready : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private bool is_ready;

    private void Start()
    {
        is_ready = false;
        Debug.Log("Press space to ready");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Space pressed");
            ReadyUp();
        }
    }

    private void ReadyUp()
    {
        if (photonView.IsMine)
        {
            Debug.Log("Ready executed");
            is_ready = true;
            
            // Call an RPC to inform other players about the readiness
            photonView.RPC("SetReadyState", RpcTarget.AllBuffered, is_ready);
        }
    }

    public bool IsReady()
    {
        return is_ready;
    }

    // RPC method to set the ready state on all clients
    [PunRPC]
    private void SetReadyState(bool readyState)
    {
        is_ready = readyState;
        Debug.Log("Ready state updated on all clients: " + is_ready);
    }
}