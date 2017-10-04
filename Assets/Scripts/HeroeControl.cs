using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroeControl : MonoBehaviour {

    public States state;
    public GameObject Target;
    public string TagSearch;
    public float Life;
    private Animator _anim;
    public GameObject MagicBall;
    private SpriteRenderer _spriteRender;

    // Use this for initialization
    void Start () {
        _anim = GetComponent<Animator>();
        _spriteRender = GetComponent<SpriteRenderer>();
	}

    private void FixedUpdate()
    {
        Target=FindClosestEnemy(TagSearch);
        if (Target != null)
        {
            if (Target.tag == "Player")
            {
                if (Target.transform.position.x > 3f)
                {
                    _anim.SetBool("Attack", true);
                }
                else
                {
                    _anim.SetBool("Attack", false);
                }
            }
            else if (Target.tag == "Enemy")
            {
                if (Target.transform.position.x < -3f)
                {
                    _anim.SetBool("Attack", true);
                }
                else
                {
                    _anim.SetBool("Attack", false);
                }
            }

        }
        else {
            _anim.SetBool("Attack", false);
        }
            
    }

    private void LateUpdate()
    {
        _spriteRender.sortingOrder = (int)Camera.main.WorldToScreenPoint(_spriteRender.bounds.min).y * -1;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == TagSearch)
        {
            _anim.SetBool("Attack", true);
            Target = collision.gameObject;
        }
        else if(collision.tag == "Player") {
            Physics2D.IgnoreCollision(collision,GetComponent<Collider2D>());
        }
    }

    #region Busca al enemigo mas cercano y lo asigna como target
    private GameObject FindClosestEnemy(string TagSearch)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(TagSearch);

        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                if (LayerMask.LayerToName(go.layer) != "Hero")
                {
                    closest = go;
                    distance = curDistance;
                }
            }
        }
        return closest;
    }
    #endregion

    public void MagicBallAttack() {
        float damage = Random.Range(state.minatk, state.maxatk);
        GameObject ball = Instantiate(MagicBall,transform.position,Quaternion.Euler(0,0,0));
        ball.GetComponent<MagicBall>().SetTarget(Target, damage,gameObject.layer);
    }

}
