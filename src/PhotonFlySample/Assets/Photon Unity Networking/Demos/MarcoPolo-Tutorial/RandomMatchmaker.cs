using UnityEngine;

public class RandomMatchmaker : Photon.PunBehaviour
{
    public string prefabName = "monsterprefab";
    public ViewSwitcher viewSwitcher;
    private PhotonView myPhotonView;

    // Use this for initialization
    public void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("JoinRandom");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnConnectedToMaster()
    {
        // when AutoJoinLobby is off, this method gets called when PUN finished the connection (instead of OnJoinedLobby())
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedRoom()
    {
        var player = PhotonNetwork.Instantiate(this.prefabName, new Vector3(0f, 250f + (Random.value * 500f), 0f), Quaternion.identity, 0);
        var meshRenderer = player.GetComponentInChildren<MeshRenderer>();
        meshRenderer.material.color = new Color(Random.value, Random.value, Random.value);
        this.viewSwitcher.myCaracter = player;
        this.viewSwitcher.SwitchCameraToLocal();

        myPhotonView = player.GetComponent<PhotonView>();
    }

    public void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (PhotonNetwork.connectionStateDetailed == PeerState.Joined)
        {
            bool shoutMarco = GameLogic.playerWhoIsIt == PhotonNetwork.player.ID;

            //if (shoutMarco && GUILayout.Button("Marco!"))
            //{
            //    myPhotonView.RPC("Marco", PhotonTargets.All);
            //}
            //if (!shoutMarco && GUILayout.Button("Polo!"))
            //{
            //    myPhotonView.RPC("Polo", PhotonTargets.All);
            //}
        }
    }
}
