using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform playerTransform;
    [SerializeField] private float distanceFromPlayer = 5f;
    [SerializeField] private float yDistance = 4.5f;
    [SerializeField] private float xAngle = 35f;
    private Vector2 turn;
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        transform.localRotation = Quaternion.Euler(xAngle, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + new Vector3(0, yDistance, -distanceFromPlayer);
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log("You hit something");
        if(other.gameObject.tag == "Wall")
        {
            Debug.Log("You hit a wall");
        }
    }
}
