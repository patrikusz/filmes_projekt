using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject gameManager;
    public Transform player;
    public float lookSpeed = 2.0f;
    public float followSpeed = 5.0f;
    public float distance = 3.0f;
    public Vector3 offset;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;


    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    void Update()
    {
        if (gameManager.GetComponent<GameManager>().cursorLocked)
        {
            rotationX += Input.GetAxis("Mouse X") * lookSpeed;
            rotationY -= Input.GetAxis("Mouse Y") * lookSpeed;
            rotationY = Mathf.Clamp(rotationY, -90f, 90f);
            transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0);
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 direction = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(rotationY, rotationX, 0);
            Vector3 desiredPosition = player.position + rotation * direction;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed + Time.deltaTime);
            transform.LookAt(player.position + Vector3.up * 2.5f);
        }
    }

    public void SetPlayer(GameObject newPlayer)
    {
        if (newPlayer != null)
        {
            player = newPlayer.transform;
            offset = transform.position - player.position;
            Debug.Log("�j j�t�kos be�ll�tva: " + player.name);
        }
        else
        {
            Debug.LogWarning("A megadott j�t�kos null!");
        }
    }
}
