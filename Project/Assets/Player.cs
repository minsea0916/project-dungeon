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

    bool move = false;

    private void Start()
    {

    }

    private void Update()
    {
        // 왼쪽 마우스 버튼을 눌렀을 때
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            // 메인 카메라를 통해 마우스 클릭한 곳의 ray 정보를 가져옴
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // ray와 닿은 물체가 있는지 검사
            if (Physics.Raycast(ray, out hit, 100f))
            {
                print(hit.transform.name);

                // hit.point 는 마우스 클릭한 곳의 월드 좌표.
                // 이 예제에서 캐릭터의 y 값(높이) 은 변하지 않기 때문에 
                // 아래와 같이 목표위치를 재설정합니다.
                destPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                // 현재 위치와 목표 위치의 방향 벡터
                dir = destPos - transform.position;

                // 바라 보아야 할 곳의 Quaternion
                lookTarget = Quaternion.LookRotation(dir);
                move = true;
            }
        }

        Move();
    }

    void Move()
    {
        if (move)
        {
            // 이동할 방향으로 Time.deltaTime * 2f 의 속도로 움직임.
            transform.position += dir.normalized * Time.deltaTime * 2f;
            // 현재 방향에서 움직여야할 방향으로 부드럽게 회전
            transform.rotation = Quaternion.Lerp(transform.rotation, lookTarget, 0.25f);

            // 캐릭터의 위치와 목표 위치의 거리가 0.05f 보다 큰 동안만 이동
            move = (transform.position - destPos).magnitude > 0.05f;
        }
    }

    public float CalDamage() //temp는 전투 중 집중을 통한 일시적 공격력 증가량
    {
        float damage;

        //크리티컬 확률
        int critical = Random.Range(0, 100);
        if (critical % 4 == 0)
        {
            damage = (offense + tempOffense) * 0.5f;
            Debug.Log("크리티컬!");
        }
        else
            damage = (offense + tempOffense) * 0.3f;

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