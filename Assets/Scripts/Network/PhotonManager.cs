using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager instance;
    public GameObject loadingPanel;
    public Text loadingText;
    public GameObject buttons;

    public GameObject createRoomPanel;
    public Text enterRoomName;

    public GameObject roomPanel;
    public Text roomName;

    public GameObject errorPanel;
    public Text errorText;

    public GameObject roomListPanel;

    public Room originalRoomButton;
    public GameObject roomButtonContent;

    Dictionary<string,RoomInfo> roomsList = new Dictionary<string,RoomInfo>();
    private List<Room> allRoomButtons = new List<Room>();

    public Text playerNameText;
    private List<Text> allPlayerNames = new List<Text>();
    public GameObject playerNameContent;

    public GameObject nameInputPanel;
    public Text placeholderText;
    public InputField nameInput;
    private bool setName;

    public GameObject startButton;

    public string levelToPlay;

    // Start is called before the first frame update
    private void Start()
    {
        CloseMenuUI();

        // パネルとテキストを更新
        loadingPanel.SetActive(true);
        loadingText.text = "ネットワークに接続中...";

        // ネットワーク接続
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        instance = this;
    }

    // メニューUIを全て閉じる
    public void CloseMenuUI()
    {
        loadingPanel.SetActive(false);

        buttons.SetActive(false);

        createRoomPanel.SetActive(false);

        roomPanel.SetActive(false);

        errorPanel.SetActive(false);

        roomListPanel.SetActive(false);

        nameInputPanel.SetActive(false);
    }

    // ロビーUIを表示
    public void LobbyMenuDisplay()
    {
        CloseMenuUI();
        buttons.SetActive(true);
    }

    // マスターサーバーに接続されたときに呼ばれる関数
    public override void OnConnectedToMaster()
    {
        // ロビー接続
        PhotonNetwork.JoinLobby();

        // テキスト更新
        loadingText.text = "ロビーに参加中...";

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    // ロビー接続時に呼ばれる関数
    public override void OnJoinedLobby()
    {
        LobbyMenuDisplay();

        // 辞書の初期化
        roomsList.Clear();

        //名前ランダム
        PhotonNetwork.NickName = Random.Range(0, 1000).ToString();

        // 名前が入力済みか確認してUI更新
        ConfirmationName();
    }

    // ルームを作るボタン用の関数
    public void OpenCreatePanel()
    {
        CloseMenuUI();
        createRoomPanel.SetActive(true);
    }

    // ルームを作成ボタン用の関数
    public void CreateRoomButton()
    {
        if(!string.IsNullOrEmpty(enterRoomName.text))
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 2;

            // ルーム作成
            PhotonNetwork.CreateRoom(enterRoomName.text, options);

            CloseMenuUI();

            // ロードパネル表示
            loadingText.text = "ルーム作成中...";
            loadingPanel.SetActive(true);
        }
    }

    // ルームに参加時に呼ばれる関数
    public override void OnJoinedRoom()
    {
        CloseMenuUI();
        roomPanel.SetActive(true);

        // ルームの名前に反映する
        roomName.text = PhotonNetwork.CurrentRoom.Name;

        GetAllPlayer();

        CheckRoomMaster();
        
    }

    // ルーム退出関数
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();

        CloseMenuUI();

        loadingText.text = "退出中...";
        loadingPanel.SetActive(true);
    }

    // ルーム退出時に呼ばれる関数
    public override void OnLeftRoom()
    {
        // ロビーUI表示
        LobbyMenuDisplay();
    }

    // ルーム作成できなかった時に呼ばれる関数
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CloseMenuUI();
        errorText.text = "ルームの作成に失敗しました" + message;

        errorPanel.SetActive(true);
    }

    // ルーム一覧を開く関数
    public void FindRoom()
    {
        CloseMenuUI();
        roomListPanel.SetActive(true);
    }

    // ルームリストに更新があったときに呼ばれる
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // ルームボタンUIの初期化
        RoomUIinitialization();
        // 辞書に登録
        UpdateRoomList(roomList);
    }

    // ルーム情報を辞書に登録
    public void UpdateRoomList(List<RoomInfo> roomList)
    {
        // 辞書にルーム登録
        for (int i = 0; i < roomList.Count; i++ )
        {
            RoomInfo info = roomList[i];

            if(info.RemovedFromList)
            {
                roomsList.Remove(info.Name);
            }
            else
            {
                roomsList[info.Name] = info;
            }
        }
        // ルームボタン表示
        RoomListDisplay(roomsList);
    }
    // ルームボタン作成して表示
    public void RoomListDisplay(Dictionary<string,RoomInfo> cacheRoomList)
    {
        foreach(var roomInfo in cacheRoomList)
        {
            // ボタン作成
            Room newButton = Instantiate(originalRoomButton);

            // 生成したボタンにルーム情報設定
            newButton.RegisterRoomDetails(roomInfo.Value);

            // 親の設定
            newButton.transform.SetParent(roomButtonContent.transform);

            allRoomButtons.Add(newButton);
        }
    }

    public void RoomUIinitialization()
    {
        foreach (Room rm in allRoomButtons)
        {
            // 削除
            Destroy(rm.gameObject);
        }
        // リストの初期化
        allRoomButtons.Clear();
    }

    // 引数のルームに入る
    public void JoinRoom(RoomInfo roomInfo)
    {
        // ルームに参加
        PhotonNetwork.JoinRoom(roomInfo.Name);

        // UI
        CloseMenuUI();  
        
        loadingText.text = "ルームに参加中...";
        loadingPanel.SetActive(true);
    }

    public void GetAllPlayer()
    {
        // 名前テキストUI初期化
        InitializePlayerList();

        // プレイヤー表示
        PlayerDisplay();
    }

    public void InitializePlayerList()
    {
        foreach (var rm in allPlayerNames)
        {
            Destroy(rm.gameObject);
        }
        allPlayerNames.Clear();
    }

    public void PlayerDisplay()
    {
        // ルームに参加している人数分UI作成
        foreach (var players in PhotonNetwork.PlayerList)
        {
            PlayerTextGeneration(players);
        }
    }
    public void PlayerTextGeneration(Player players)
    {
        Text newPlayerText = Instantiate(playerNameText);

        newPlayerText.text = players.NickName;

        newPlayerText.transform.SetParent(playerNameContent.transform);

        allPlayerNames.Add(newPlayerText);
    }

    // 名前が入力済みか確認してUI更新
    public void ConfirmationName()
    {
        if(!setName)
        {
            CloseMenuUI();
            nameInputPanel.SetActive(true);

            if(PlayerPrefs.HasKey("PlayerName"))
            {
                placeholderText.text = PlayerPrefs.GetString("playerName");
                nameInput.text = PlayerPrefs.GetString("playerName");
            }
        }
        else
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("playerName");
        }
    }

    // 名前登録
    public void SetName()
    {
        // 入力フィールドに文字が入力されているかどうか
        if(!string.IsNullOrEmpty(nameInput.text))
        {
            // ユーザー名登録
            PhotonNetwork.NickName = nameInput.text;

            // 保存
            PlayerPrefs.SetString("playerName", nameInput.text);

            // UI
            LobbyMenuDisplay();

            setName = true;
        }
    }
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerTextGeneration(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        GetAllPlayer();
    }

    public void CheckRoomMaster()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
        else
        {
            startButton.SetActive(false);
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            startButton.SetActive(true);
        }
    }

    // 遷移
    public void PlayGame()
    {
        PhotonNetwork.LoadLevel(levelToPlay);
    }

    // 遷移とネットワーク切断
    public void LeaveLobby()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("TitleScene");
    }
}
