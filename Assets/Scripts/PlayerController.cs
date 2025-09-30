using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashDistance = 10f;

    [Header("Movement Boundaries")]
    [SerializeField] private float minX = -20f;
    [SerializeField] private float maxX = 20f;
    [SerializeField] private float minZ = -20f;
    [SerializeField] private float maxZ = 20f;

    [Header("Prefabs")]
    [SerializeField] private GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        if(Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if(Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if(Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if(Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }

        bool movePressed = true;

        if (direction == Vector3.zero)
        {
            movePressed = false;
        }
        transform.rotation = direction != Vector3.zero ? Quaternion.LookRotation(direction) : transform.rotation;

        Vector3 movement = Vector3.forward;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement *= dashDistance;
        }
        else if (movePressed)
        {
            movement *= moveSpeed * Time.deltaTime;
        }
        else
        {
            movement = Vector3.zero;
        }

        transform.Translate(movement);

        Vector3 clampedPosition = transform.position; ;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, minZ, maxZ);
        transform.position = clampedPosition;

        if (Input.GetKeyDown(KeyCode.J))
        {
            ShootProjectile();
        }
    }

    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.transform.rotation = transform.rotation;
    }
}
