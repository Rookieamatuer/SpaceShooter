using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidRotation : MonoBehaviour
{
    public float moveSpeed = 3.0f;
    public float minAngularSpeed = 1.0f;
    public float maxAngularSpeed = 5.0f;

    private Rigidbody rb;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        // ��������Ľ��ٶ�
        float randomAngularSpeed = Random.Range(minAngularSpeed, maxAngularSpeed);

        // ���ȷ����ת����
        int randomDirection = Random.Range(0, 3);
        int isClockWise = Random.Range(0, 2) == 0? -1 : 1;

        switch(randomDirection)
        {
            case 0:
                // ���ø���Ľ��ٶ�
                rb.angularVelocity = new Vector3(randomAngularSpeed * isClockWise, 0, 0);
                break;
            case 1:
                // ���ø���Ľ��ٶ�
                rb.angularVelocity = new Vector3(0, randomAngularSpeed * isClockWise, 0);
                break;
            case 2:
                // ���ø���Ľ��ٶ�
                rb.angularVelocity = new Vector3(0, 0, randomAngularSpeed * isClockWise);
                break;
        }

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // ���㴹ֱ���µ��ƶ�����
        Vector3 movement = -1 * Vector3.forward * moveSpeed * Time.deltaTime;

        // Ӧ���ƶ�
        transform.Translate(movement, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bolts")
        {
            GameObject.Destroy(other.gameObject);

            GameObject.Destroy(this.gameObject);
        }
    }
}
