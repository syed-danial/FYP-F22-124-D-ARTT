using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PaddleController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, 0, vertical);
            transform.position += direction * speed * Time.deltaTime;
        }
    }
}

