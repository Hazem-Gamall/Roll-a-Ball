using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CubeBehaviour : MonoBehaviour
{
    public static Action CubeCollided;
    [SerializeField] private float rotationSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate() {
        transform.Rotate(Vector3.up * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other) {
            
            if(other.gameObject.name == "Player")
            {
                CubeCollided?.Invoke();
                GameObject.Destroy(this.gameObject);
            }
    }
}
