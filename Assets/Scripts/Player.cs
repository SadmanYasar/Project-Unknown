using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float Cameraspeed = 5f;
    [SerializeField] LayerMask aimLayerMask;

    public static Vector3 Direction;


    public static Animator animator;

    private Vector3 velocity = Vector3.zero;

    int VelocityXHash;
    int VelocityZHash;

    Vector3 movement;

    float velocityZ;
    float velocityX;

    float horizontal;
    float vertical;

    public Rigidbody playerRb;

    public static Player playerInstance;
    
    void Awake() {
        playerInstance = this;
        animator = GetComponent<Animator>();
    }
    void Start() {
        gameObject.name = "Player";
        VelocityXHash = Animator.StringToHash("VelocityX");
        VelocityZHash = Animator.StringToHash("VelocityZ");
    }


    // Update is called once per frame
    void Update()
    {
        bool RightClicked = Input.GetMouseButton(1);
        bool LeftClicked = Input.GetMouseButton(0);
        bool OnePressed = Input.GetKeyDown(KeyCode.Alpha1);
        AimTowardMouse();

        
        
        //reading the input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal, 0f, vertical);

        //animating
        velocityZ = Vector3.Dot(movement.normalized, transform.forward);
        velocityX = Vector3.Dot(movement.normalized, transform.right);



        animator.SetFloat(VelocityZHash, velocityZ, 0.1f, Time.deltaTime);
        animator.SetFloat(VelocityXHash, velocityX, 0.1f, Time.deltaTime);


    }

    void FixedUpdate() {
        CameraZoomOut();
        MovePlayer(movement);
       
    }

    void MovePlayer(Vector3 direction) {
        playerRb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }

    void AimTowardMouse() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimLayerMask) )
        {
            var direction = hitInfo.point - transform.position;
            direction.y = 0f;
            direction.Normalize();
            transform.forward = direction;
            Direction = direction;

        }
    }    

    void CameraZoomOut() {
        if ( Input.GetKey(KeyCode.LeftShift)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, aimLayerMask) )
            {
                var direction = hitInfo.point - transform.position;
                direction.y = 0f;
                direction.Normalize();

                //Zoom Out Camera
                
                direction *= Cameraspeed;
                Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, Camera.main.transform.position + direction * 1.5f, ref velocity, 0.3f);
                }
        }
    }
    
    public void PlayerDie() {
        GameManager.restartText.text = "<i>PRESS R TO RESTART</i>";
        gameObject.SetActive(false);
    }
}

    