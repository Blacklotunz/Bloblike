using UnityEngine;

public class SpitWeapon : Weapon
{
    public GameObject ammo;
    public float projectileSpeed;
    private Vector2 direction;

    public override void Hit(Vector2 direction)
    {
        this.direction = direction;
        StartCoroutine(Shoot(direction));

       
    }

    private System.Collections.IEnumerator Shoot(Vector3 direction)
    {
        yield return new WaitForSeconds(0.25f);
        GameObject projectile = Instantiate(ammo, transform.position + direction, Quaternion.identity);
        //give it a speed
        projectile.GetComponent<Rigidbody2D>().velocity = this.direction * projectileSpeed;
    }

}
