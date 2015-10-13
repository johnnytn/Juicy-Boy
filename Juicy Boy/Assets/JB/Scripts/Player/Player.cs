using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public PlayerStats playerStats = new PlayerStats();
    public int fallBondary = -20;

    [System.Serializable] //To make a class definition visible on unity
    public class PlayerStats {
        public float health = 100f;
    }


    void Update() {
        if (transform.position.y <= fallBondary) {
            damagePlayer(Mathf.Infinity);
        }
    }

    public void damagePlayer(float damage) {
        playerStats.health -= damage;
        if (playerStats.health <= 0) {
            GameMaster.killPlayer(this);
        }
    }
}
