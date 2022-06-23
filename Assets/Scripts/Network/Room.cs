using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;

public class Room : MonoBehaviour
{
    public Text buttonText;
    private RoomInfo info;

    public void RegisterRoomDetails(RoomInfo info)
    {
        this.info = info;

        buttonText.text = this.info.Name;
    }

    // このルームボタンが管理しているルームに参加する
    public void OpenRoom()
    {
        PhotonManager.instance.JoinRoom(info);
    }
}
