using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;
    public Transform playerPreFab;
    public Transform spawnPoint;
    public Transform spawnPreFab;
    public CameraShake camerashake;
    public int spawnDelay = 2;

    void Awake() {
        if(gm == null) {
           gm = GameObject.FindGameObjectWithTag("GM").GetComponent< GameMaster>();
        }
    }

    void Start() {
        if(camerashake == null) {
            Debug.Log("NO camera shake on GM");
        }
    }

    // IEnumerator to work with yield
    public IEnumerator respawnPlayer() {
        yield return new WaitForSeconds(spawnDelay);

        GetComponent<AudioSource>().Play();
        Instantiate(playerPreFab, spawnPoint.position, spawnPoint.rotation);
        Transform spawnParticles = Instantiate(spawnPreFab, spawnPoint.position, spawnPoint.rotation) as Transform;
        Destroy(spawnParticles.gameObject, 3f);
    }

    public static void killCharacter(Character charracter) {
        Destroy(charracter.gameObject);
    }

    public static void killPlayer(Player player) {
        Destroy(player.gameObject);
        //To start an IEnumarator method
        gm.StartCoroutine(gm.respawnPlayer());
    }

    public static void killEnemy(Enemy enemy) {
        gm.killEnemyPlusEffect(enemy);
    }

    // TODO : rename
    private void killEnemyPlusEffect(Enemy enemy) {
        Transform deathParticles = Instantiate(enemy.deathParticles, enemy.transform.position, Quaternion.identity) as Transform;
        Destroy(deathParticles.gameObject, 2f);
        camerashake.shake(enemy.shakeAmt, enemy.shakeLenght);
        Destroy(enemy.gameObject);
    }
}
