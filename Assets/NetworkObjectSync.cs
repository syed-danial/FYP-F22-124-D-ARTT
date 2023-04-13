using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class NetworkObjectSync : MonoBehaviourPun, IPunObservable
{
    private Vector3 latestPosition;
    private Quaternion latestRotation;

    private void Awake()
    {
        latestPosition = transform.position;
        latestRotation = transform.rotation;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            latestPosition = (Vector3)stream.ReceiveNext();
            latestRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, latestPosition, Time.deltaTime * 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, latestRotation, Time.deltaTime * 10);
        }
    }
}

