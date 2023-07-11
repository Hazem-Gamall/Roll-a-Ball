using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 200f;
    private float jumpForce = 7f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private AudioSource jump;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * speed * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse );
            isGrounded = false;
            jump.Play();
        }
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("other" + other.gameObject.name);
        if(other.gameObject.tag == "Step" || other.gameObject.name == "Ground")
        {
            isGrounded = true;
        }
        if(other.gameObject.name == "Lava"){
            if(isActiveAndEnabled){
                var ind = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(ind);
                Debug.Log("Lost");   

            }
        }
    }

    private void OnCollisionExit(Collision other) {
        
    }
}
