using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class ShootSetting
{
    public float startAngle;
    public float speed;
    public float damage;
    public float bulletsPerTime;
    public float timeMin;
    public float timeMax;
}
public class EnemyShoot : MonoBehaviour
{
    [SerializeField]
    public List<ShootSetting> shootSettings = new List<ShootSetting>();
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
            StartCoroutine(ShootCoroutine());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator ShootCoroutine()
    {
        int i = 0;
        while (true)
        {
            Shoot(this.shootSettings[i]);
            yield return new WaitForSeconds(shootSettings[i].timeMin + UnityEngine.Random.Range(0, shootSettings[i].timeMax));
            i++;
            if (i >= shootSettings.Count) i = 0;
        }
    }
    public void Shoot(ShootSetting setting)
    {
        for (int i = 0; i < setting.bulletsPerTime; i++)
        {
            float angle = setting.startAngle + (i * 360 / setting.bulletsPerTime);
            Vector3 dir = Quaternion.Euler(0, 0, angle) * Vector3.up;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<BulletController>().SetDirection(dir, setting.speed);
            bullet.GetComponent<BulletController>().damage = setting.damage;
        }
    }
}
