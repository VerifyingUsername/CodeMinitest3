using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    float MoveSpeed = 5.0f;
    public Animator playerAnim;
    public GameObject CountdownText;
    public GameObject PowerUpLeft;
    public GameObject PlayPlaneB;

    //public GameObject MovePlane;
    float Countdown = 10f;

    bool isTimerStart = false;
    float RoundedUp;
    bool AllPowerUp = false;
    // bool for timer
    bool CollidedWithCone = false;

    bool TimerIsZero = false;

    public bool SwitchTouched;

    private int powerupCount;
    private int totalPowerUp;
    // Start is called before the first frame update
    void Start()
    {
        totalPowerUp = GameObject.FindGameObjectsWithTag("PowerUp").Length;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            StartRun();          
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.SetBool("isRun", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            StartRun();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.SetBool("isRun", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            StartRun();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.SetBool("isRun", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            StartRun();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            playerAnim.SetBool("isRun", false);
        }

        if (powerupCount == totalPowerUp)
        {
            AllPowerUp = true;
        }

        //if (CollidedWithCone == true)
        //{
        //    Countdown-= Time.deltaTime; 
        //    Debug.Log("Countdown started");
        //    CountdownText.GetComponent<Text>().text = "Timer Countdown: " + Countdown;
        //}

        //if(Countdown <= 0)
        //{
        //    TimerIsZero = true;
        //}

        //if (TimerIsZero == true)
        //{
        //    CountdownText.GetComponent<Text>().text = "Timer Countdown: 0";           
        //}

        if(transform.position.y < -5)
        {
            SceneManager.LoadScene("LoseScene");
        }

        CountDownController();
       
    }

    void StartRun()
    {
        playerAnim.SetBool("isRun", true);
        playerAnim.SetFloat("StartRun", 0.26f);
        transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {      
        if (other.gameObject.CompareTag("PowerUp"))
        {
            powerupCount++;
            PowerUpLeft.GetComponent<Text>().text = "PowerUp Collected: " + powerupCount;
            Destroy(other.gameObject);

        }
        // if all pUp collected, touch cone to activate
        if (powerupCount == totalPowerUp)
        {
            Debug.Log("Cone Activated");

            if (other.gameObject.CompareTag("TagCone"))
            {
                //CollidedWithCone = true;
                if (AllPowerUp == true)
                {
                    isTimerStart = true;
                    CollidedWithCone = true;
                    Debug.Log("Activated PlaneB 90 deg rotation");
                    PlayPlaneB.transform.Rotate(0f, 90f, 0f);
                    //PlayPlaneB.transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed);
                }
            }           
        }
        if (other.gameObject.CompareTag("Switch"))
        {
            Debug.Log("Switch Activated");
            SwitchTouched = true;          
        }

    }
    
    private void CountDownController()
    {
        
        if (CollidedWithCone == true)
        {
            if (Countdown > 0 && isTimerStart == true)
            {
                Countdown -= Time.deltaTime;
                RoundedUp = Mathf.RoundToInt(Countdown);
                CountdownText.GetComponent<Text>().text = "Timer Countdown: " + RoundedUp;
            }
            else if (Countdown <= 0 && isTimerStart == true)
            {
                //Countdown = 10f;
                //RoundedUp = 10;
                isTimerStart = false;
                CountdownText.GetComponent<Text>().text = "Timer Countdown: 0";
                PlayPlaneB.transform.Rotate(0f, 90f, 0f);
            }
        }
    }
}
