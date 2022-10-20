using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviour
{
    public Transform _playerTransform;

    private Vector3 playerVector;
    private int speed = 15;

    void Update()
    {
        if (_playerTransform != null)
        {
            playerVector = _playerTransform.position;
            playerVector.z = -10;
            transform.position = Vector3.Lerp(transform.position, playerVector, speed * Time.deltaTime); 
        }
    }
}
