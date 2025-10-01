using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float dashDistance = 10f;
    [SerializeField] private MeshRenderer characterColor;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material penaltyMaterial;

    [Header("Movement Boundaries")]
    [SerializeField] private float minX = -20f;
    [SerializeField] private float maxX = 20f;
    [SerializeField] private float minZ = -20f;
    [SerializeField] private float maxZ = 20f;

    [Header("Prefabs")]
    [SerializeField] private GameObject projectilePrefab;

    [Header("Weapons")]
    [SerializeField] private Image red;
    [SerializeField] private Image green;
    [SerializeField] private Image blue;
    private int[] activeWeapon = { 1, 1, 1 };

    [Header("Penalty")]
    [SerializeField] private int penalty = 3;
    private int cd = 0;

    // Start is called before the first frame update
    void Start()
    {
        RefreshActiveWeapon();
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

        // Input
        if (Input.GetKeyDown(KeyCode.J))
        {
            inputCheck(0);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            inputCheck(1);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            inputCheck(2);
        }

        // Penalty Cooldown
        if (cd > 0)
        {
            cd--;
            if (cd == 0)
            {
                RefreshActiveWeapon();
                characterColor.material = normalMaterial;
            }
        }
    }

    private void inputCheck(int inputNum)
    {
        if (cd > 0)
            return;
        else
        {
            if (activeWeapon[inputNum] == 1)
            {
                ShootProjectile();
                RefreshActiveWeapon();
            } 
            else
            {
                for (int i = 0; i < 3; i++)
                    activeWeapon[i] = 0;
                UpdateWeaponUI();
                cd = penalty * 60;
                characterColor.material = penaltyMaterial;
            }
        }
    }

    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.transform.rotation = transform.rotation;
    }

    private void RefreshActiveWeapon()
    { 
        int light = Random.Range(0, 3);
        for (int i = 0; i < 3; i++)
        {
            if (i == light)
            {
                activeWeapon[i] = 1;
            } 
            else
            {
                activeWeapon[i] = Random.Range(0, 2);
            }
        }
        UpdateWeaponUI();
    }

    private void UpdateWeaponUI()
    {
        if(red == null || green == null || blue == null)
        {
            red = GameObject.Find("Red").GetComponent<Image>();
            green = GameObject.Find("Green").GetComponent<Image>();
            blue = GameObject.Find("Blue").GetComponent<Image>();
        }

        red.color = new Color(1, 0, 0, 0.3f + activeWeapon[0] * 0.7f);
        green.color = new Color(0, 1, 0, 0.3f + activeWeapon[1] * 0.7f);
        blue.color = new Color(0, 0, 1, 0.3f + activeWeapon[2] * 0.7f);
    }
}
