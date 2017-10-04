using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class States
{
    public string type;
    public float minatk, maxatk;
    public float mindef, maxdef;
    public string weakness;
    public Sprite Portrait;
    public int GoldCost;
}
public class CharacterScript : MonoBehaviour
{
    public States State;
    public float Speed;
    public GameObject Target;
    public string TagSearch;
    public float Life = 100;
    public GameObject SightStart;//, SightEnd;
    public GameObject Projectile;

    private Animator _anim;
    private bool spotted = false;
    private bool _move = true;
    private SpriteRenderer _spriteRender;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        Target = FindClosestEnemy(TagSearch);
        _spriteRender = GetComponent<SpriteRenderer>();
        //SightStart=Instantiate(SightStart, new Vector3(transform.position.x+ 0.642f,0f,0f),Quaternion.Euler(0f,0f,0f),transform);
        //SightEnd=Instantiate(SightEnd, new Vector3(transform.position.x + 0.642f, 0f, 0f), Quaternion.Euler(0f, 0f, 0f), transform);
    }

    void FixedUpdate()
    {
        spotted = Physics2D.Linecast(SightStart.transform.position, SightStart.transform.position, 1 << LayerMask.NameToLayer(LayerMask.LayerToName(gameObject.layer)));
       
        //if(tag=="Player")
        //  spotted = Physics2D.Linecast(new Vector3(transform.position.x + 0.7f, 0f, 0f), new Vector3(transform.position.x + 1f, 0f, 0f), 1 << LayerMask.NameToLayer(tag));
        //else if(tag=="Enemy")
        //  spotted = Physics2D.Linecast(new Vector3(transform.position.x - 0.7f, 0f, 0f), new Vector3(transform.position.x - 1f, 0f, 0f), 1 << LayerMask.NameToLayer(tag));
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
                if (!Projectile)
                {
                    if (dist > 1.5f)
                    {
                        if (!spotted)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
                            if (_anim.GetInteger("State") == 4)
                                _anim.SetInteger("State", 0);
                        }
                        else
                            _anim.SetInteger("State", 4);
                    }

                    else
                    {
                        _move = false;
                        _anim.SetInteger("State", 1);
                    }
                }
                else {
                    if (dist > 3.5f)
                    {
                        if (!spotted)
                        {
                            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Speed * Time.deltaTime);
                            if (_anim.GetInteger("State") == 4)
                                _anim.SetInteger("State", 0);
                        }
                        else
                            _anim.SetInteger("State", 4);
                    }

                    else
                    {
                        _move = false;
                        _anim.SetInteger("State", 1);
                    }
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

    private void LateUpdate()
    {
        _spriteRender.sortingOrder = (int)Camera.main.WorldToScreenPoint(_spriteRender.bounds.min).y * -1;
    }

    GameObject[] FindGameObjectsWithLayer(int layer) {
        GameObject[] goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<GameObject> goList = new List<GameObject>();
        for (int i = 0; i < goArray.Length; i++) {
            if (goArray[i].layer == layer) {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0) {
            return null;
        }
        return goList.ToArray();
    }


    #region Busca al enemigo mas cercano y lo asigna como target
    private GameObject FindClosestEnemy(string TagSearch)
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(TagSearch);
        
        //gos = FindGameObjectsWithLayer(LayerMask.NameToLayer(TagSearch));
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
    public void DamageTarget()
    {
        if (Target != null)
        {
            if (Target.GetComponent<CharacterScript>()!=null)
            {
                if (Target.GetComponentInChildren<CharacterScript>().State.weakness == State.type)
                {
                    Target.GetComponentInChildren<CharacterScript>().Life -= ((Random.Range(State.minatk, State.maxatk) * 4) - (Random.Range(Target.GetComponentInChildren<CharacterScript>().State.mindef, Target.GetComponentInChildren<CharacterScript>().State.maxdef) / 1.25f));
                }
                else
                {
                    Target.GetComponentInChildren<CharacterScript>().Life -= ((Random.Range(State.minatk, State.maxatk)*2) - (Random.Range(Target.GetComponentInChildren<CharacterScript>().State.mindef, Target.GetComponentInChildren<CharacterScript>().State.maxdef) / 1.25f));
                }
            }
            else
            {
                Target.GetComponentInChildren<HeroeControl>().Life -= ((Random.Range(State.minatk, State.maxatk)*2) - (Random.Range(Target.GetComponentInChildren<HeroeControl>().state.mindef, Target.GetComponentInChildren<HeroeControl>().state.maxdef) / 1.25f));
            }
        }
    }
    #endregion

    public void ProjectileAttack()
    {
        float damage = Random.Range(State.minatk, State.maxatk);
        GameObject ball = Instantiate(Projectile, transform.position, Quaternion.Euler(0, 0, 0));
        ball.GetComponent<MagicBall>().SetTarget(Target, damage,gameObject.layer);
    }
}
