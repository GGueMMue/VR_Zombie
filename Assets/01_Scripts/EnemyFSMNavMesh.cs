using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSMNavMesh : MonoBehaviour
{
    public enum ENEMYSTATE
    {
        NONE = -1,
        IDLE = 0,
        MOVE,
        ATTACK,
        DAMAGE,
        DEAD
    }

    public ENEMYSTATE enemyState;

    public float stateTime = 0;
    public float idleStateTime = 2;
    public Animator enemyAnim;
    public Transform target;
    bool isdead = false;

    public float speed = 5f;
    public float rotationSpeed = 10f;
    public float attackRange = 3.5f;
    public float attackStateMaxTime = 1f;

    public CapsuleCollider enemyCharacterController;
    public PlayerState playerState;
    public GameObject explosionEffect;

    public NavMeshAgent nvAgent;

    public int hp = 2;
    public AudioClip attackSfx;
    public AudioClip deadSfx;


    void Start()
    {
        enemyState = ENEMYSTATE.IDLE;
        target = GameObject.FindWithTag("Player").transform;
        enemyCharacterController = GetComponent<CapsuleCollider>();
        enemyAnim = GetComponentInChildren<Animator>();
        playerState = target.GetComponent<PlayerState>();
        nvAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (playerState.isDead)
        {
            enemyAnim.enabled = false;
            return;
        }
        else stateTime += Time.deltaTime;

        switch (enemyState)
        {
            case ENEMYSTATE.NONE:
                //Debug.Log("아무것도 안함!!!!!");
                break;
            case ENEMYSTATE.IDLE:
                //Debug.Log("#### IDLE ####");
                enemyAnim.SetInteger("ENEMYSTATE", (int)enemyState);
                nvAgent.speed = 0;
                if (stateTime > idleStateTime)
                {
                    stateTime = 0;
                    enemyState = ENEMYSTATE.MOVE;
                }
                break;
            case ENEMYSTATE.MOVE:
                //Debug.Log("#### MOVE ####");
                //float distance = (target.position - transform.position).magnitude;
                enemyAnim.SetInteger("ENEMYSTATE", (int)enemyState);
                nvAgent.speed = this.speed;
                float distance = Vector3.Distance(target.position, transform.position);
                if (distance < attackRange)
                {
                    stateTime = 0;
                    enemyState = ENEMYSTATE.ATTACK;
                }
                else
                {
                    nvAgent.SetDestination(target.position);
                    Debug.Log(nvAgent.destination);

                }
                break;
            case ENEMYSTATE.ATTACK:
                //Debug.Log("#### ATTACK ####");
                enemyAnim.SetInteger("ENEMYSTATE", (int)enemyState);
                nvAgent.speed = 0;
                if (stateTime > attackStateMaxTime)
                {
                    // 플레이어 공격!!!!!!!!!!!!!!!!!!!
                    stateTime = 0;
                    //Debug.Log("#### ATTACK ####");
                    //AudioManager.Instance().PlaySfx(attackSfx);
                    target.GetComponent<PlayerState>().DamageByEnemy();
                }

                float dist = Vector3.Distance(target.position, transform.position);
                if (dist > attackRange)
                {
                    stateTime = 0;
                    enemyState = ENEMYSTATE.MOVE;
                }
                break;
            case ENEMYSTATE.DAMAGE:
                //Debug.Log("#### DAMAGE ####");
                enemyAnim.SetInteger("ENEMYSTATE", (int)enemyState);
                nvAgent.speed = 0;
                //Debug.Log("현재 체력 :::: " + hp);                
                
                if (stateTime > 1.042f)
                {
                    stateTime = 0;
                    enemyState = ENEMYSTATE.MOVE;
                }

                break;
            case ENEMYSTATE.DEAD:
                //Debug.Log("#### DEAD ####");
                //enemyAnim.SetInteger("ENEMYSTATE", (int)enemyState);
                //AudioManager.Instance().PlaySfx(deadSfx);
                
                //Instantiate(explosionEffect, transform.position, transform.rotation);
                //Destroy(gameObject, 3f);
                StartCoroutine(DeadProcess(3f));
                //enemyState = ENEMYSTATE.NONE;
                //enemyCharacterController.enabled = false;
                //nvAgent.enabled = false;
                //ScoreManager.Instance().myScore += 10;
                break;
            default:
                break;
        }
    }

    IEnumerator DeadProcess(float t)
    {
        if(isdead == false)
            enemyAnim.SetTrigger("DEAD");

        isdead = true;

        yield return new WaitForSeconds(t);
        while (transform.position.y > -t)
        {
            Vector3 temp = transform.position;
            temp.y -= Time.deltaTime * 0.5f;
            transform.position = temp;
            yield return new WaitForEndOfFrame();
        }
        Destroy(gameObject);
        //InitEnemy();
        //gameObject.SetActive(false);
    }

    void InitEnemy()
    {
        hp = 5;
        enemyState = ENEMYSTATE.IDLE;
        enemyCharacterController.enabled = true;
        nvAgent.enabled = true;
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
    }

    public void Damaged()
    {
        --hp;
        if (hp <= 0)
            enemyState = ENEMYSTATE.DEAD;
        else
            enemyState = ENEMYSTATE.DAMAGE;
    }
}