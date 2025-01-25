using HyperQuest.EasyPooling;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float _force;
    [SerializeField] GameObject _bulletPrefab;

    private void Awake()
    {
        InvokeRepeating("Shoot", 1f, 1f);
    }

    public void Shoot()
    {
        GameObject newBullet = _bulletPrefab.PooledInstantiate(pos: transform.position);
        newBullet.transform.up = transform.up;

        Rigidbody2D bulletBody = newBullet.GetComponent<Rigidbody2D>();
        bulletBody.AddForce(transform.up * _force, ForceMode2D.Impulse);
    }
}
