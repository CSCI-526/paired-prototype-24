using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    private GameObject target;

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            PlayerController controller = FindObjectOfType<PlayerController>();
            if(controller != null)
            {
                target = controller.gameObject;
            }
        }

        if(target != null)
        {
            transform.position = target.transform.position + offset;
        }

    }
}
