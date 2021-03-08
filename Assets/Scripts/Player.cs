using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("移動速度")]
    public float MoveSpeed;
    [Header("最大速度")]
    public float MaxSpeed;
    Rigidbody2D Rig;
    float X;
    // Start is called before the first frame update
    void Start()
    {
        Rig=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        X = Input.GetAxisRaw("Horizontal");
        Vector3 MoveV=Vector3.zero;
        if (X>0)
        {
            MoveV = new Vector3(1,0,0) * Time.deltaTime*MoveSpeed;
        }
        else if (X < 0)
        {
            MoveV = new Vector3(-1, 0, 0)*Time.deltaTime * MoveSpeed;
        }
        else
        {
            Rig.velocity = Vector3.zero;
        }

        if (Rig.velocity.magnitude < MaxSpeed) Rig.AddForce(MoveV);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
