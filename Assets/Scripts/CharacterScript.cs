using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class States
{
    public string type;
    public float minatk,maxatk;
    public float mindef,maxdef;
    public string weakness;
}
    public class CharacterScript : MonoBehaviour
{
    public States State;
    public float Speed;
    public GameObject Target;
    public string TagSearch;
    public float Life = 100;
    public Transform SightStart, SightEnd;
    public bool Heroe;

    private Animator _anim;
    private bool spotted = false;
    private bool _move = true;

    private void Start()
    {
        if (!Heroe)
        {
            _anim = GetComponent<Animator>();
            Target = FindClosestEnemy(TagSearch);
        }
        
    }

    void FixedUpdate()
    {
        if (!Heroe)
        {
            spotted = Physics2D.Linecast(SightStart.position, SightEnd.position, 1 << LayerMask.NameToLayer(tag));
            if (Life > 0)
            {
                Target = FindClosestEnemy(TagSearch);
                if (Target != null)
                {
                    _move = true;
                }
                else
                {
                    _move = false;
                    _anim.SetInteger("State", 0);
                }

                if (_move)
                {
                    if (_anim.GetInteger("State") == 1)
                        _anim.SetInteger("State", 0);
                    var dist = Vector3.Distance(transform.position, Target.transform.position);
                    if (dist > 1.5f)
                    {
                        if (!spotted)
                            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
                    }

                    else
                    {
                        _move = false;
                        _anim.SetInteger("State", 1);
                    }

                }

            }
            else
            {
                _anim.SetInteger("State", 2);
                //ObjectPool.Instance.PoolAgain(gameObject);
                Destroy(gameObject, 1.2f);
            }
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
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    #endregion

    #region Daño a Target
    public void DamageTarget() {
        if (Target != null)
        {
            if (Target.GetComponent<CharacterScript>().State.weakness == State.type)
            {
                Target.GetComponent<CharacterScript>().Life -=( (Random.Range(State.minatk, State.maxatk) * 2) - (Random.Range(Target.GetComponent<CharacterScript>().State.mindef, Target.GetComponent<CharacterScript>().State.maxdef) / 1.25f));
            }
            else {
                Target.GetComponent<CharacterScript>().Life -=( (Random.Range(State.minatk, State.maxatk)) - (Random.Range(Target.GetComponent<CharacterScript>().State.mindef, Target.GetComponent<CharacterScript>().State.maxdef) / 1.25f));
            }
        }
    }
    #endregion
}
