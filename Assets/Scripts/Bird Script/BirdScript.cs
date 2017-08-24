using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdScript : MonoBehaviour {

    public static BirdScript instance;

    private Rigidbody2D myRigidBody;
    private Animator anim;
    private AudioSource audioSource;

    private float forwardSpeed =3f;
    private float bounceSpeed = 4f;

    private bool didFlap;
    public bool isAlive;

    [HideInInspector]
    public int score;

    private Button flapButton;

    [SerializeField]
    private AudioClip flapClip, pointClip, diedClip;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
        }

        isAlive = true;
        score = 0;

        flapButton = GameObject.FindGameObjectWithTag("FlapButton").GetComponent<Button>();
        flapButton.onClick.AddListener(() => FlapTheBird());

        SetCamerasX();

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isAlive)
        {
            Vector3 temp = transform.position;
            temp.x += forwardSpeed * Time.deltaTime;
            transform.position = temp;

            if (didFlap)
            {
                didFlap = false;
                myRigidBody.velocity = new Vector2(0, bounceSpeed);

                audioSource.PlayOneShot(flapClip);
                anim.SetTrigger("Flap");
            }

            if (myRigidBody.velocity.y >= 0)
            {
                // as the bird flaps more it will rotate up until its level
                if (transform.rotation.z < 0)
                {
                    float angle = Mathf.Lerp(transform.rotation.z, 20f, myRigidBody.velocity.y / 7f);
                    transform.rotation = Quaternion.Euler(0, 0, angle);
                }
                
            } else
            {
                float angle = 0;
                // as the bird flaps more it will rotate down
                angle = Mathf.Lerp(0, -90, -myRigidBody.velocity.y / 7f);
                transform.rotation = Quaternion.Euler(0, 0, angle);

            }


        }
	}

    void SetCamerasX()
    {
        CameraScript.offsetX = (Camera.main.transform.position.x - transform.position.x) - 1f;
    }

    public float GetPositionX()
    {
        return transform.position.x;
    }

    public void FlapTheBird()
    {
        didFlap = true;
    }

    //hit pipe
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "Ground" || target.gameObject.tag == "Pipe")
        {
            if (isAlive)
            {
                isAlive = false;
                anim.SetTrigger("Bird Died");
                audioSource.PlayOneShot(diedClip);
            }
        }
    }

    //pass through pipes (or goodies?)
    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "PipeHolder")
        {
            score++;
            audioSource.PlayOneShot(pointClip);
        }
    }


}
