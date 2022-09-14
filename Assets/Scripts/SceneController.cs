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
    // Game Objects
    [Header("Game Objects")]
    [SerializeField]
    GameObject pipe;
    private bool showPipe = false;

    [SerializeField]
    GameObject waypoint;
    private bool showWaypoint = false;

    [SerializeField]
    GameObject questionBlock;
    private bool showQuestionBlock = false;

    [SerializeField]
    GameObject star;
    private bool showStar = false;

    // Audio
    [Header("Audio Files")]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip oneUpClip;
    [SerializeField]
    private AudioClip coinClip;
    [SerializeField]
    private AudioClip pipeDown;
    [SerializeField]
    private AudioClip pipeUp;

    // Start is called before the first frame update
    void Start() {
      audioSource = GetComponent<AudioSource>();

      waypoint.SetActive(false);
      questionBlock.SetActive(false);
      star.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
      if (Input.GetKeyDown(KeyCode.Space)) {
        Debug.Log("Space was pressed!");
        // Use this as a linear bool state machine to trigger animations
        if (!showPipe) {
          showPipe = true;
          showWaypoint = true;
          Debug.Log("Starting pipe animation...");
        } else if (showWaypoint) {
          waypoint.SetActive(true);
          showWaypoint = false;
          showStar = true;

          Debug.Log("Starting waypoint animation...");
        } else if (showStar) {
          waypoint.SetActive(false);
          star.SetActive(true);

          showQuestionBlock = true;
          showStar = false;

          Debug.Log("Starting Star animation...");
        } else if (showQuestionBlock) {
          star.SetActive(false);
          questionBlock.SetActive(true);

          showQuestionBlock = false;
          Debug.Log("Starting Question Block animation...");
        }
      }
    }
}
