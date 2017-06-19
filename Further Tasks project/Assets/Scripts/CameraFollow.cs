using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Vector3 myPos;
    public Transform myPlay;

    public void Update()
    {
        transform.position = myPlay.position + myPos;
    }
}
