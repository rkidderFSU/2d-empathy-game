using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;
    public float leftBoundary;
    public float rightBoundary;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player.transform.position.x < leftBoundary)
        {
            transform.position = new Vector3(leftBoundary, player.transform.position.y + 2, -10);
        }
        else if (player.transform.position.x > leftBoundary && player.transform.position.x < rightBoundary)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, -10);
        }
        else if (player.transform.position.x > rightBoundary)
        {
            transform.position = new Vector3(rightBoundary, player.transform.position.y + 2, -10);
        }
    }
}
