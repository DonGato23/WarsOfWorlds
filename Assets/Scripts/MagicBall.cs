using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{

    public GameObject _target;
    public float Speed;
    public float _damage;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_target != null)
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, Speed * Time.deltaTime);
        else
            Destroy(gameObject);
    }

    public void SetTarget(GameObject Target, float Damage)
    {
        _target = Target;
        _damage = Damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == _target.transform.name)
        {
            if (_target != null)
            {
                _target.GetComponentInChildren<CharacterScript>().Life -= _damage - (Random.Range(_target.GetComponentInChildren<CharacterScript>().State.mindef, _target.GetComponentInChildren<CharacterScript>().State.maxdef) / 1.25f);
                Destroy(gameObject);
            }
        }
        
    }
}




