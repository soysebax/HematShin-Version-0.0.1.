using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    public Transform player;
    public float separacion = 2f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + separacion, transform.position.y, transform.position.z);
    }
}
