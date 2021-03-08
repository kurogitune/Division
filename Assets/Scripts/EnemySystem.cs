using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    [Header("移動速度")]
    public float MoveSpeed;
    [Header("最大速度")]
    public float MaxSpeed;
    [Header("地面LayerMaskを設定せよ")]
    public LayerMask GroundMask;
    [Header("壁判定長さ")]
    public float Judgment_Length;
    [Header("true:反転 false:そのまま進む")]
    [Header("目の前に足場が無い場合")]
    public bool ScaffoldingJudgment;
    [Header("足場判定長さ")]
    public float Scaffold_Length;
    [Header("足場判定開始場所")]
    public float ScaffoldStart_Langth;
    Rigidbody2D Rig;

    int direction;//方向判定用
    bool Des;//死
    // Start is called before the first frame update
    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
        direction = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Des)//死の処理
        {
            return;
        }

        Vector3 MoveV=Vector3.zero;//移動用
        Vector3 ScaffoldStartPoz = new Vector3(transform.position.x + ScaffoldStart_Langth*direction, transform.position.y, 0);//足場判定用

        Debug.DrawRay(ScaffoldStartPoz, transform.up * -1);
        if (Physics2D.Raycast(transform.position, transform.right, Judgment_Length, GroundMask)& Scaffold ()||
            !Physics2D.Raycast(ScaffoldStartPoz, transform.up * -1, Scaffold_Length, GroundMask)& Scaffold()& ScaffoldingJudgment)//目の前壁判定 足場判定
        {
            direction *= -1;
            transform.Rotate(0,180,0);         
            Rig.velocity = Vector3.zero;
            return;
        }

        if (Rig.velocity.magnitude < MaxSpeed)//速度制限
        {
            MoveV = transform.right * Time.deltaTime * MoveSpeed;
            Rig.AddForce(MoveV);
        }
    }

    bool Scaffold()//足場に乗っているか判定
    {
        bool Retunbool = false;

        if (Physics2D.BoxCast(transform.position,new Vector2(0.4f,0.05f), 0, transform.up*-1, Scaffold_Length, GroundMask)) Retunbool = true;
        return Retunbool;
    }
}
