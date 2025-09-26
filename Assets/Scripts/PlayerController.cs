using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashDistance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;

        if(Input.GetKey(KeyCode.W))
        {
            movement += Vector3.forward;
        }
        if(Input.GetKey(KeyCode.S))
        {
            movement += Vector3.back;
        }
        if(Input.GetKey(KeyCode.A))
        {
            movement += Vector3.left;
        }
        if(Input.GetKey(KeyCode.D))
        {
            movement += Vector3.right;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            movement *= dashDistance;
        }
        else
        {
            movement *= moveSpeed * Time.deltaTime;
        }

        transform.Translate(movement);
    }
}
