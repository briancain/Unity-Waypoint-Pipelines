using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private bool deployBlock = false;

    [SerializeField]
    GameObject star;
    private bool showStar = false;

    // UI
    [Header("UI Objects")]
    [SerializeField]
    GameObject textbg;
    [SerializeField]
    GameObject textBox;
    private bool showTextBox = false;

    [SerializeField]
    GameObject pipelineUp;
    private bool showPipeUp = false;

    [SerializeField]
    GameObject pipelineWs;
    private bool showPipeWs = false;

    [SerializeField]
    GameObject pipelineExec;
    private bool showPipeExec = false;

    [SerializeField]
    GameObject depUI;
    private bool showDepUI = false;

    [SerializeField]
    GameObject pipeRunUI;
    private bool showRunUI = false;

    // Audio
    private AudioSource audioSource;

    [Header("Audio Files")]
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

    // Internal
    // deploy first
    private bool deployAnim = true;
    // slideshow second
    private bool slideshowAnim = false;
    private bool showSlide = true;

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
        if (deployAnim) {
          Deploy();
        } else if (slideshowAnim) {
          Slideshow();
        }
      }
    }

    void Slideshow() {
      if (!showTextBox) {
        showTextBox = true;
        textBox.SetActive(true);
        textbg.SetActive(true);

        showPipeUp = true;
        showSlide = true;
      } else if (showPipeUp && showSlide) {
        pipelineUp.SetActive(true);

        showPipeUp = false;
        showPipeWs = true;
        showSlide = false;
      } else if (showPipeWs && showSlide) {
        pipelineUp.SetActive(false);
        pipelineWs.SetActive(true);
        showPipeWs = false;
        showPipeExec = true;
        showSlide = false;
      } else if (showPipeExec && showSlide) {
        pipelineWs.SetActive(false);
        pipelineExec.SetActive(true);
        showPipeExec = false;
        showDepUI = true;
        showSlide = false;
      } else if (showDepUI && showSlide) {
        pipelineExec.SetActive(false);
        depUI.SetActive(true);
        showDepUI = false;
        showRunUI = true;
        showSlide = false;
      } else if (showRunUI && showSlide) {
        depUI.SetActive(false);
        pipeRunUI.SetActive(true);
        showRunUI = false;
        showSlide = false;
      } else if (!showSlide) {
        pipeRunUI.SetActive(false);
        pipelineUp.SetActive(false);
        pipelineWs.SetActive(false);
        pipelineExec.SetActive(false);
        depUI.SetActive(false);
        pipeRunUI.SetActive(false);

        showSlide = true;
      } else {
        showSlide = true;
        pipeRunUI.SetActive(false);
      }
    }

    void Deploy() {
      // The big linear state machine
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
        audioSource.PlayOneShot(powerMeterRefillClip, 1f);

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
        StartCoroutine(Utilities.DoAfter(1.5f, ()=>blockAnim.SetTrigger("Rise")));
        StartCoroutine(Utilities.DoAfter(2.25f, ()=>audioSource.PlayOneShot(coinClip, 1f)));

        showQuestionBlock = false;
        Debug.Log("Starting Question Block animation...");
        deployBlock = true;
      } else if (deployBlock) {
        Animator anim = questionBlock.GetComponent<Animator>();
        anim.SetTrigger("Deploy");
        audioSource.PlayOneShot(pipeDown, 1f);
        deployBlock = false;
      } else {
        // final state
        waypoint.SetActive(true);
        textbg.SetActive(true);
        textBox.SetActive(true);

        deployAnim = false;
        slideshowAnim = true;
      }
    }
}
