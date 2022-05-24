using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
    public float playerDistance;

    private void Start()
    {
        transform.position = Player.Instance.transform.position + Vector3.down * playerDistance;
    }

    void Update()
    {
        if (transform.position.y + 7.5f <= Player.Instance.transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, Player.Instance.transform.position.y - playerDistance, transform.position.z);
        }
    }
}