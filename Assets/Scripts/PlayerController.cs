using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum Direction{
    Forward,
    Back,
    Left,
    Right,
    Jump,
    None,
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 200f;
    private float jumpForce = 7f;
    [SerializeField] private bool isGrounded;
    [SerializeField] private AudioSource jump;
    Direction direction = Direction.None;
    

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = GetComponent<AudioSource>();
    }

    public void Forward(){
        direction = Direction.Forward;
    }
    
    public void Back(){
        direction = Direction.Back;
    }
    public void Left(){
        direction = Direction.Left;
    }
    public void Right(){
        direction = Direction.Right;
    }
    public void Jump(){
        direction = Direction.Jump;
    }
    
    void Update()
    {
        if(Input.GetKey(KeyCode.W) || direction == Direction.Forward)
        {
            rb.AddForce(Vector3.forward * speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.S) || direction == Direction.Back)
        {
            rb.AddForce(Vector3.back * speed * Time.deltaTime);
        }
        
        if(Input.GetKey(KeyCode.A) || direction == Direction.Left)
        {
            rb.AddForce(Vector3.left * speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D) || direction == Direction.Right)
        {
            rb.AddForce(Vector3.right * speed * Time.deltaTime);
        }
        if((Input.GetKeyDown(KeyCode.Space) || direction == Direction.Jump) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse );
            isGrounded = false;
            jump.Play();
        }
        direction = Direction.None;
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
