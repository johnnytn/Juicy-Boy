using UnityEngine;
using System.Collections;

public class Enemy : Character {

    public Transform deathParticles;
    public float shakeAmt = 0.1f;
    public float shakeLenght = 0.1f;

    public float damage = 40;

    void Start() {
        init(1);
        updateStatusIndicator();

        if(deathParticles == null) {
            Debug.Log("NO DEAD PARTICLES ON ENEMY");
        }
    }

    void Update() {
        //getStatusIndicator().
        //Debug.Log(currentHealth + " - " + maxHealth);
    }

    public void damageEnemy(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            GameMaster.killEnemy(this);
        }
        updateStatusIndicator();
    }

    void OnCollisionEnter2D(Collision2D colInfo) {

        Player player = colInfo.collider.GetComponent<Player>();
        if(player != null) {
            player.damagePlayer(damage);
            damageEnemy(999);
        }
    }
}
