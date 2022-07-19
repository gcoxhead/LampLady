using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sfx : MonoBehaviour
{
 public AudioSource Bell;
 public AudioSource Explode;

 public void playBell()
 {
    Bell.Play();
 }

 public void playExplode()
 {
    Explode.Play();
 }

}
