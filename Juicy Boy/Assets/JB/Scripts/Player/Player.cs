using UnityEngine;

public class Player : Character {

    static public int lifes = 3;

    void Start() {
        init(0);
        updateStatusIndicator();
    }

    void Update() {
        fallDeath();
    }

    public void damagePlayer(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            GameMaster.killPlayer(this);
            lifes--;
        }

        if(lifes <= 0) {
            Debug.Log("GAME OVER");
            return;
        }
        updateStatusIndicator();
    }

    private void fallDeath() {
        if (transform.position.y <= -20) {
            damagePlayer(Mathf.Infinity);
        }
    }
}
