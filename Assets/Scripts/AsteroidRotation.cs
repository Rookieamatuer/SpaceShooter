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

        // 生成随机的角速度
        float randomAngularSpeed = Random.Range(minAngularSpeed, maxAngularSpeed);

        // 随机确定旋转方向
        int randomDirection = Random.Range(0, 3);
        int isClockWise = Random.Range(0, 2) == 0? -1 : 1;

        switch(randomDirection)
        {
            case 0:
                // 设置刚体的角速度
                rb.angularVelocity = new Vector3(randomAngularSpeed * isClockWise, 0, 0);
                break;
            case 1:
                // 设置刚体的角速度
                rb.angularVelocity = new Vector3(0, randomAngularSpeed * isClockWise, 0);
                break;
            case 2:
                // 设置刚体的角速度
                rb.angularVelocity = new Vector3(0, 0, randomAngularSpeed * isClockWise);
                break;
        }

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 计算垂直向下的移动方向
        Vector3 movement = -1 * Vector3.forward * moveSpeed * Time.deltaTime;

        // 应用移动
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
