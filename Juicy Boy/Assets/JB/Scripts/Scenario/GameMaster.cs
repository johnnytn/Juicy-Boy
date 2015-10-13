using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm;
    public Transform playerPreFab;
    public Transform spawnPoint;
    public Transform spawnPreFab;
    public int spawnDelay = 2;

    void Start() {
        if(gm == null) {
           gm = GameObject.FindGameObjectWithTag("GM").GetComponent< GameMaster>();
        }
    }

    // IEnumerator to work with yield
    public IEnumerator respawnPlayer() {
        yield return new WaitForSeconds(spawnDelay);

        GetComponent<AudioSource>().Play();
        Instantiate(playerPreFab, spawnPoint.position, spawnPoint.rotation);
        GameObject spawnParticles = Instantiate(spawnPreFab, spawnPoint.position, spawnPoint.rotation) as GameObject;
        Destroy(spawnParticles, 3f);
    }

    public static void killPlayer(Player player) {
        Destroy(player.gameObject);
        //To start an IEnumarator method
        gm.StartCoroutine(gm.respawnPlayer());
    }


}
