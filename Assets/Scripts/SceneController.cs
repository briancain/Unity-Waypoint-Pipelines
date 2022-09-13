using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The Idea:
// Have the pipe come out from under the ground
// On space bar:
// - Have waypoint logo come up and spin
// - Make waypoint logo disappear and Question Block Shows up and spins
// - Block disappears, then Star shows up

public class SceneController : MonoBehaviour {
    [SerializeField]
    GameObject waypoint;

    [SerializeField]
    GameObject questionBlock;

    [SerializeField]
    GameObject star;

    // Start is called before the first frame update
    void Start() {
      //waypoint.SetActive(false);
      questionBlock.SetActive(false);
      star.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
      if (Input.GetKeyDown(KeyCode.Space)) {
        Debug.Log("Space was pressed!");
      }
    }
}
