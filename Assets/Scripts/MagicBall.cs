using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{

    public GameObject _target;
    public float Speed;
    public float _damage;
    public int _layerParent;
    private SpriteRenderer _spriteRender;

    private void Start()
    {
        _spriteRender = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Physics2D.IgnoreLayerCollision(12, _layerParent);
        if (_target != null)
        {
            if (transform.position != _target.transform.position)
            {
                
                Vector3 diff = _target.transform.position - transform.position;
                diff.Normalize();

                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

                transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, Speed * Time.deltaTime);

            }
            else
            {
                if (_target.gameObject.layer != 11)
                    _target.GetComponentInChildren<CharacterScript>().Life -= _damage - (Random.Range(_target.GetComponentInChildren<CharacterScript>().State.mindef, _target.GetComponentInChildren<CharacterScript>().State.maxdef) / 1.25f);
                else
                    _target.GetComponentInChildren<HeroeControl>().Life -= _damage - (Random.Range(_target.GetComponentInChildren<HeroeControl>().state.mindef, _target.GetComponentInChildren<HeroeControl>().state.maxdef) / 1.25f);

                Destroy(gameObject);
            }
        }
        else
            Destroy(gameObject);
    }

    private void LateUpdate()
    {
        _spriteRender.sortingOrder = (int)Camera.main.WorldToScreenPoint(_spriteRender.bounds.min).y * -1;
    }

    public void SetTarget(GameObject Target, float Damage,int layer)
    {
        _target = Target;
        _damage = Damage;
        _layerParent = layer;
    }

   /* private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == _target.transform.name)
        {
            if (_target != null)
            {
                _target.GetComponentInChildren<CharacterScript>().Life -= _damage - (Random.Range(_target.GetComponentInChildren<CharacterScript>().State.mindef, _target.GetComponentInChildren<CharacterScript>().State.maxdef) / 1.25f);
                Destroy(gameObject);
            }
        }
        
    }*/
}




