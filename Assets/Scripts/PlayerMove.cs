using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODUnity;
public class PlayerMove : MonoBehaviour
{
    public List<GameObject> obstaculos = new List<GameObject>();
    [SerializeField] private Vector2 input;
    [SerializeField] private Rigidbody2D rb;
    
   // [SerializeField] private StudioEventEmitter explotion;
    public float speed = 5f, force = 300f;
    public bool pause = false;
    public GameObject pauseMenu, spawnP1, spwnP2;
    public float xmeters = 5;
    [SerializeField] private StudioEventEmitter pasos;
    [SerializeField] private StudioEventEmitter salto;
    [SerializeField] private StudioEventEmitter land;
     [SerializeField]  bool sonando;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        InputManager.OnPlayerMovement += Move;
        InputManager.OnJump += Jump;
        InputManager.OnPause += Pause;
    }

    private void OnDisable()
    {
        InputManager.OnPlayerMovement -= Move;
        InputManager.OnJump -= Jump;
        InputManager.OnPause -= Pause;
    }
    private void Move(Vector2 input)
    {
        this.input = input;
        
        if (input.x == 0)
        {
            pasos.Stop();
            sonando = false;

        }
        else if(input.x == 1 || input.x == -1 && !sonando)
        {
            sonando = true;
            pasos.Play();
        }
    }
    private void FixedUpdate()
    {
        Vector2 velocity = new Vector3(input.x, 0);
        rb.position += (velocity * speed * Time.fixedDeltaTime);


        
        
    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * force);
        salto.Play();
    }

    public void Pause()
    {
        if (!pause)
        {
            pauseMenu.SetActive(true);
            pause = true;
        }
        else
        {
            pauseMenu.SetActive(false);
            pause = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("suelo"))
        {
            land.Play();
        }
    }
    private void Update()
    {

        if(transform.position.x > spawnP1.transform.position.x + 10)
        {
            spawnP1.transform.position = new Vector3(transform.position.x + 10, 0, 1);
            spwnP2.transform.position = new Vector3(transform.position.x + 10, 2, 1);
        }

        if (transform.position.x > xmeters)
        {
            int random = Random.Range(1, 2);
            if(random == 1)
            {
                Instantiate(obstaculos[Random.Range(0, 2)], spawnP1.transform);
                xmeters = xmeters + 5;
            }else if(random == 2)
            {
                Instantiate(obstaculos[Random.Range(0, 2)], spwnP2.transform);
                xmeters = xmeters + 5;
            }


        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstaculo"))
        {
            SceneManager.LoadScene(0);
        }
    }

}
