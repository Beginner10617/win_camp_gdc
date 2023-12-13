using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEditor;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
    private bool isAllPlayersReady = false;
    public static Launcher Instance;
    public TMP_InputField RoomNameInput;
    public TMP_InputField username;
    public TMP_Text RoomName;
    public TMP_Text errorText;
    public Transform PlayerListContent;
    public GameObject PlayerListPrefab;
    public Transform roomListContent;
    public GameObject roomListItemPrefab;
    public GameObject startGameButton;
    public bool x = true;
    public bool joinedroom = false;
    public Transform ReadyContainer;
    
    RoomListItem room;
    public TMP_InputField roomname;
    List<RoomListItem> rooms = new List<RoomListItem>();
    

    private void Awake()
    {
        Instance = this;
    }


    void Start()
    {
        MenuManager.Instance.OpenMenu("Loading Menu");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("ConnectedToMaster");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("LobbyJoined");
        if (x){
            MenuManager.Instance.OpenMenu("Username");
            x = false;
        }
    }

    public void EnterUsername()
    {
        if(!string.IsNullOrEmpty(username.text))
        {
            MenuManager.Instance.OpenMenu("title");
        }
    }

    public void Create_Room()
    {
        MenuManager.Instance.OpenMenu("Create Room");
        
    }


    public void Create()
    {
        if(!string.IsNullOrEmpty(RoomNameInput.text))
        {
            
            MenuManager.Instance.OpenMenu("Loading Menu");
            PhotonNetwork.CreateRoom(RoomNameInput.text);
        
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        MenuManager.Instance.OpenMenu("Error");
        errorText.text = "Room Creation Failed" + message;
    }

    public void Back()
    {
        MenuManager.Instance.OpenMenu("title");
    }
    public override void OnJoinedRoom()
    {
        joinedroom = true;
        PhotonNetwork.Instantiate("ReadyBox", new Vector3(0,-400,0), Quaternion.identity);
        PhotonNetwork.AutomaticallySyncScene = true;
        Debug.Log("JoinedRoom");
        MenuManager.Instance.OpenMenu("Room");
        RoomName.text = PhotonNetwork.CurrentRoom.Name;

        Player[] player = PhotonNetwork.PlayerList;
        foreach(Transform child in PlayerListContent)
        {
            Destroy(child.gameObject);
        }
        for(int i =0; i< player.Count(); i++)
        {
            Instantiate(PlayerListPrefab, PlayerListContent).GetComponent<PlayerListItem>().SetUp(player[i]);
        }


//            startGameButton.SetActive(PhotonNetwork.IsMasterClient); 
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient && joinedroom)
        {
            CheckAllPlayersReady();
        }
    }

    private void CheckAllPlayersReady()
    {
        GameObject[] players_ready = GameObject.FindGameObjectsWithTag("Ready");
        isAllPlayersReady = true;

        foreach (GameObject player in players_ready)
        {
            Ready readyScript = player.GetComponent<Ready>();

            if (readyScript != null && !readyScript.IsReady())
            {
                isAllPlayersReady = false;
                break;
            }
        }

        // Activate the button if all players are ready
        startGameButton.SetActive(isAllPlayersReady);
    }
    public override void OnPlayerEnteredRoom(Player player)
    {
        Instantiate(PlayerListPrefab,PlayerListContent).GetComponent<PlayerListItem>().SetUp(player);     
    }

    public void JoinRoom(RoomInfo info)
 {
     PhotonNetwork.JoinRoom(info.Name);
     MenuManager.Instance.OpenMenu("Loading Menu");
 }

 public override void OnLeftRoom()
 {
     MenuManager.Instance.OpenMenu("title");
 }

 public override void OnRoomListUpdate(List<RoomInfo> roomList)
 {
      foreach(Transform trans in roomListContent)
      {
      	Destroy(trans.gameObject);
      }

     for (int i = 0; i < roomList.Count; i++)
     {
         if (roomList[i].RemovedFromList)
         {
            continue;
         }    
         room=Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>();
         room.SetUp(roomList[i]);
         rooms.Add(room);
     }
 }


public void get_into()
{
    for (int i = 0; i < rooms.Count; i++)
    {
        if(rooms[i].text.text == roomname.text){
            JoinRoom(rooms[i].Info);
        }
    }
}
 public void StartGame()
 {
    PhotonNetwork.LoadLevel(1);
    
 }

 public void LeaveRoom()
 {
     PhotonNetwork.LeaveRoom();
     MenuManager.Instance.OpenMenu("Loading Menu");
 }

 public override void OnMasterClientSwitched(Player newMasterClient)
 {
    startGameButton.SetActive(PhotonNetwork.IsMasterClient);
 }

}