using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    void Update()
    {
		transform.position = new Vector3(transform.position.x, Player.Instance.transform.position.y + 6, transform.position.z);
    }
}
