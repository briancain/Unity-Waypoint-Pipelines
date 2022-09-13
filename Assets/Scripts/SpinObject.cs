using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObject : MonoBehaviour {
  public int rotationSpeed = 50;
  public bool disableSpin = false;

  // Start is called before the first frame update
  void Start() {
  }

  // Update is called once per frame
  void Update() {
    if (!disableSpin) {
      transform.Rotate (0,rotationSpeed*Time.deltaTime, 0);
    }
  }
}
