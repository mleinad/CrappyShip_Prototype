using System.Collections;
using System.Collections.Generic;
using QFSW.QC;
using UnityEngine;
using UnityEngine.Scripting;

public class LightManager : MonoBehaviour
{

   [SerializeField]
   Material environmentMaterial;
   [SerializeField]
   List<RoomLights> roomLights;
   public static LightManager Instance { get; private set; }
   private void Start()
   {
      Instance = this;
      EventManager.Instance.onTurnOnLights += TurnOnLights;
   }

   void LoadLights(bool state)
   {
      foreach (var light in roomLights)
      {
         light.TurnOnLights(state);
      }
   }

   public void EnableEnviormentLight(bool state)
   {
      if(state) environmentMaterial.EnableKeyword("_EMISSION");
      else environmentMaterial.DisableKeyword("_EMISSION");
   }

   public void TurnOnLights(bool state)
   { 
      LoadLights(state);
   }


   
   [Preserve]
   [Command]
   void LightsON()
   {
      LoadLights(true);
   }

   [Preserve]
   [Command]
   void LightsOFF()
   {
      LoadLights(false);
   }
   
}