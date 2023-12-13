using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] TMP_InputField username;

    private void Start()
    {
        if(PlayerPrefs.HasKey("username"))
        {
            username.text = PlayerPrefs.GetString("username");
            PhotonNetwork.NickName = PlayerPrefs.GetString("username");
        }
        else
        {
            username.text="Player" + Random.Range(0,100000).ToString("0000");
            OnUsernameInputValueChanged();
        }
    }
    public void OnUsernameInputValueChanged()
    {
        
        PhotonNetwork.NickName=username.text;
        PlayerPrefs.SetString("username", username.text);
        
    }
}