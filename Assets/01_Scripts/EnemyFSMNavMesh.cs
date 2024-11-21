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

    public float stateTime;
    public float idleStateTime;
    public Animator enemyAnim;
    public Transform target;

    public float speed = 2f;
    public float rotationSpeed = 10f;
    public float attackRange = 2.5f;
    public float attackStateMaxTime = 1f;

    public CharacterController enemyCharacterController;
    //public PlayerState playerState;
    public GameObject explosionEffect;

    public NavMeshAgent nvAgent;

    public int hp = 5;
    public AudioClip attackSfx;
    public AudioClip deadSfx;


    void Start()
    {
        enemyState = ENEMYSTATE.IDLE;
        target = GameObject.FindWithTag("Player").transform;
        enemyCharacterController = GetComponent<CharacterController>();
        enemyAnim = GetComponentInChildren<Animator>();
        //playerState = target.GetComponent<PlayerState>();
        nvAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //if (playerState.isDead)
        //{
        //    enemyAnim.enabled = false;
        //    return;
        //}

        switch (enemyState)
        {
            case ENEMYSTATE.NONE:
                //Debug.Log("�ƹ��͵� ����!!!!!");
                break;
            case ENEMYSTATE.IDLE:
                //Debug.Log("#### IDLE ####");
                enemyAnim.SetInteger("ENEMYSTATE", (int)enemyState);
                nvAgent.speed = 0;
                stateTime += Time.deltaTime;
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
                }
                break;
            case ENEMYSTATE.ATTACK:
                //Debug.Log("#### ATTACK ####");
                enemyAnim.SetInteger("ENEMYSTATE", (int)enemyState);
                nvAgent.speed = 0;
                stateTime += Time.deltaTime;
                if (stateTime > attackStateMaxTime)
                {
                    // �÷��̾� ����!!!!!!!!!!!!!!!!!!!
                    stateTime = 0;
                    //Debug.Log("#### ATTACK ####");
                    //AudioManager.Instance().PlaySfx(attackSfx);
                    //target.GetComponent<PlayerState>().DamageByEnemy();
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
                //Debug.Log("���� ü�� :::: " + hp);
                stateTime += Time.deltaTime;
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
                enemyAnim.SetTrigger("DEAD");
                Instantiate(explosionEffect, transform.position, transform.rotation);
                Destroy(gameObject, 3f);
                //StartCoroutine(DeadProcess(3f));
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
        yield return new WaitForSeconds(t);
        while (transform.position.y > -t)
        {
            Vector3 temp = transform.position;
            temp.y -= Time.deltaTime * 0.5f;
            transform.position = temp;
            yield return new WaitForEndOfFrame();
        }
        //Destroy(gameObject);
        InitEnemy();
        gameObject.SetActive(false);
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