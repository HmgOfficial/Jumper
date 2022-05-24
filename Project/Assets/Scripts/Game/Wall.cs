using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    void Update()
    {
		transform.position = new Vector3(transform.position.x, Player.Instance.transform.position.y, transform.position.z);
    }
}
