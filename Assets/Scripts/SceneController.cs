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
    [SerializeField]
    private AudioClip powerMeterRefillClip;
    [SerializeField]
    private AudioClip highscoreClip;

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
          pipe.SetActive(true);
          showPipe = true;
          showWaypoint = true;

          Debug.Log("Starting pipe animation...");
          Animator anim = pipe.GetComponent<Animator>();

          anim.SetTrigger("Rise");
          audioSource.PlayOneShot(pipeUp, 1f);
        } else if (showWaypoint) {
          waypoint.SetActive(true);
          showWaypoint = false;
          showStar = true;

          Debug.Log("Starting waypoint animation...");
          audioSource.PlayOneShot(oneUpClip, 1f);
        } else if (showStar) {
          waypoint.SetActive(false);
          star.SetActive(true);

          showQuestionBlock = true;
          showStar = false;

          Debug.Log("Starting Star animation...");
        } else if (showQuestionBlock) {
          Animator anim = star.GetComponent<Animator>();
          anim.SetTrigger("Lower");
          audioSource.PlayOneShot(pipeDown, 1f);

          //star.SetActive(false);
          questionBlock.SetActive(true);
          Animator blockAnim = questionBlock.GetComponent<Animator>();
          StartCoroutine(Utilities.DoAfter(2f, ()=>blockAnim.SetTrigger("Rise")));
          StartCoroutine(Utilities.DoAfter(2f, ()=>audioSource.PlayOneShot(coinClip, 1f)));

          showQuestionBlock = false;
          Debug.Log("Starting Question Block animation...");
        }
      }
    }
}
