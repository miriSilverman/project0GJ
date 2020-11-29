using System;
using System.Collections;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
   private static BackGroundMusic bgmInstance;
   private void Awake()
   {
      DontDestroyOnLoad(transform.gameObject);

      // onlu one instance of background sound
      if (bgmInstance == null)
      {
         bgmInstance = this;
      }
      else
      {
         Destroy(gameObject);
      }
   }
}
