using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //MoveBehindPlayer();
    }

    void MoveBehindPlayer()
    {
        Vector3 newPosition = player.transform.position;
        newPosition.y = transform.position.y;
        newPosition.z -= 10;
        transform.position = newPosition;
    }
}
