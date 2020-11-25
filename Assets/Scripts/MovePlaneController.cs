using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlaneController : MonoBehaviour
{
    float speed = 5.0f;
    float zUpperLimit = 85.0f;
    float zLowerLimit = 55.0f;

    bool isMoveFwd = false;
    bool isMoveBack = true;

    public GameObject PlayerGo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerGo.GetComponent<PlayerController>().SwitchTouched)
        {
            if (isMoveBack && !isMoveFwd)
            {
                if (transform.position.z >= zLowerLimit)
                {
                    transform.Translate(Vector3.back * Time.deltaTime * speed);
                }
                else
                {
                    isMoveBack = !isMoveBack;
                    isMoveFwd = !isMoveFwd;
                }
            }

            if (isMoveFwd && !isMoveBack)
            {
                if (transform.position.z <= zUpperLimit)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * speed);
                }
                else
                {
                    isMoveBack = !isMoveBack;
                    isMoveFwd = !isMoveFwd;
                }
            }
        }
    }

    
    // Make player move w moving plane
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerGo.transform.SetParent(transform);
        }
    }
}
