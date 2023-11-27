using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 destPos;
    Vector3 dir;
    Quaternion lookTarget;

    public float maxHP;
    public float curHP;
    public float mp;

    float str; //힘
    float dex; //민첩
    float vision; //시야
    float defense; //방어력
    float offense = 10; //공격력

    int tempOffense;


    private void Start()
    {

    }

    private void Update()
    {
        float moveZ = 0f;
        float moveX = 0f;
        if (Input.GetKey(KeyCode.W))
        {
            moveZ += 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveZ -= 1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX -= 1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveX += 1f;
        }

        transform.Translate(new Vector3(moveX, 0f, moveZ) * 0.1f);
    }

    public float CalDamage() //temp는 전투 중 집중을 통한 일시적 공격력 증가량
    {
        float damage;

        //크리티컬 확률
        int critical = Random.Range(0, 100);
        if (critical % 4 == 0)
        {
            damage = (offense + tempOffense) * 1.5f;
            Debug.Log("크리티컬!");
        }
        else
            damage = (offense + tempOffense) * 1.0f;

        return damage;
    }

    public void Attack(Enemy enemy)
    {
        if (enemy.curHP > 0)
        {
            Debug.Log("공격");
            enemy.curHP -= this.CalDamage();
            tempOffense = 0;

            Debug.Log("적 체력: " + enemy.curHP);

            enemy.Attack(this, 1);
        }
        else
        {
            Destroy(enemy.gameObject);
            Debug.Log("전투 종료!");
        }
    }

    public void CounterAttack(Enemy enemy)
    {
        if (enemy.curHP > 0)
        {
            Debug.Log("반격");
            enemy.Attack(this, 2);

            Debug.Log("적 체력: " + enemy.curHP);
        }
        else
        {
            Destroy(enemy.gameObject);
            Debug.Log("전투 종료!");

        }
    }

    public void Focusing(Enemy enemy)
    {
        Debug.Log("집중");
        tempOffense += 5;
        enemy.Attack(this, 1);
    }
}