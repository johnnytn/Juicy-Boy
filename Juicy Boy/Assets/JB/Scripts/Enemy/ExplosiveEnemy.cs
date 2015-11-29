using UnityEngine;
using System.Collections;

public class ExplosiveEnemy : Enemy {

    void OnCollisionEnter2D(Collision2D colInfo) {
        Player player = colInfo.collider.GetComponent<Player>();
        if (player != null) {
            player.damagePlayer(damage);
            damageEnemy(999);
        }
    }
}
