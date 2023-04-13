using Photon.Pun;
using Photon.Realtime;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Google.XR.ARCoreExtensions;
using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject arSessionOriginPrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject ballPrefab;
    
    public ARAnchorManager arAnchorManager;
    public ARAnchorManager arCloudAnchorManager;
    private ARCloudAnchor arCloudAnchor;

    private GameObject localPlayer;
    private GameObject localBall;

    void Start()
    {
        // Connect to Photon
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        // Join a random room
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // Create a room if joining a random room failed
        PhotonNetwork.CreateRoom(null, new RoomOptions());
    }

    public override void OnJoinedRoom()
    {
        // Instantiate AR Session Origin
        GameObject arSessionOrigin = Instantiate(arSessionOriginPrefab);
        arAnchorManager = arSessionOrigin.GetComponent<ARAnchorManager>();
        arCloudAnchorManager = arSessionOrigin.GetComponent<ARAnchorManager>();

        // Instantiate player and ball prefabs
        localPlayer = PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        localBall = PhotonNetwork.Instantiate(ballPrefab.name, Vector3.zero, Quaternion.identity);

        // Set up AR Cloud Anchor
        StartCoroutine(SetupARCloudAnchor());
    }

    private IEnumerator SetupARCloudAnchor()
    {
        // Wait for AR tracking to be initialized
        yield return new WaitUntil(() => arAnchorManager != null && arAnchorManager.subsystem != null && arAnchorManager.subsystem.running);
        
        // Add a Cloud Anchor to the AR session
        ARAnchor anchor = arAnchorManager.AddAnchor(new Pose(Vector3.zero, Quaternion.identity));
        ARCloudAnchor cloudAnchor = arCloudAnchorManager.HostCloudAnchor(anchor);

        yield return new WaitUntil(() => cloudAnchor.cloudAnchorState == CloudAnchorState.Success || cloudAnchor.cloudAnchorState == CloudAnchorState.ErrorInternal);

        if (cloudAnchor.cloudAnchorState == CloudAnchorState.Success)
        {
            // Share the Cloud Anchor ID with other players via Photon
            photonView.RPC("ShareCloudAnchorId", RpcTarget.Others, cloudAnchor.cloudAnchorId);

            // Set the position of the table, rackets, and ball
            Transform tableTransform = cloudAnchor.transform.GetChild(0);
            localPlayer.transform.SetParent(tableTransform);
            localBall.transform.SetParent(tableTransform);

            // Set local player's racket active and other player's racket inactive
            if (PhotonNetwork.IsMasterClient)
            {
                localPlayer.transform.Find("Racket1").gameObject.SetActive(true);
                localPlayer.transform.Find("Racket2").gameObject.SetActive(false);
            }
            else
            {
                localPlayer.transform.Find("Racket1").gameObject.SetActive(false);
                localPlayer.transform.Find("Racket2").gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Failed to create AR Cloud Anchor.");
        }
    }

    [PunRPC]
    private void ShareCloudAnchorId(string cloudAnchorId)
    {
        StartCoroutine(ResolveARCloudAnchor(cloudAnchorId));
    }

    private IEnumerator ResolveARCloudAnchor(string cloudAnchorId)
    {
        // Resolve the Cloud Anchor using the received Cloud Anchor ID
        arCloudAnchor = arCloudAnchorManager.ResolveCloudAnchorId(cloudAnchorId);
    yield return new WaitUntil(() => arCloudAnchor.cloudAnchorState == CloudAnchorState.Success || arCloudAnchor.cloudAnchorState == CloudAnchorState.ErrorInternal);

    if (arCloudAnchor.cloudAnchorState == CloudAnchorState.Success)
    {
        // Set the position of the table, rackets, and ball
        Transform tableTransform = arCloudAnchor.transform.GetChild(0);
        localPlayer.transform.SetParent(tableTransform);
        localBall.transform.SetParent(tableTransform);

        // Set local player's racket active and other player's racket inactive
        if (PhotonNetwork.IsMasterClient)
        {
            localPlayer.transform.Find("Racket1").gameObject.SetActive(true);
            localPlayer.transform.Find("Racket2").gameObject.SetActive(false);
        }
        else
        {
            localPlayer.transform.Find("Racket1").gameObject.SetActive(false);
            localPlayer.transform.Find("Racket2").gameObject.SetActive(true);
        }
    }
    else
    {
        Debug.LogError("Failed to resolve AR Cloud Anchor.");
    }
}

}
