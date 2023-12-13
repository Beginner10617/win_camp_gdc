using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;

public class RoomListItem : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text text;
    
    public RoomInfo Info;

    public void SetUp(RoomInfo _info)
    {
        Info = _info;
        text.text = _info.Name;
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(Info);
    }
}
