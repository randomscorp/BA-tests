using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using HutongGames.PlayMaker.Actions;
using Modding;
using UnityEngine;
using Satchel;
using AbilityChanger;
using static AbilityChanger.AbilityChanger;
namespace BA_tets
{
    public class BA_tets : Mod
    {
        internal static BA_tets Instance;
        public static Dictionary<string, Dictionary<string, GameObject>> Preloads;
        public override List<ValueTuple<string, string>> GetPreloadNames()=>new List<ValueTuple<string, string>>
            {
               ("sharedassets355", "Dung Ball Small"),
               ("GG_Dung_Defender", "Dung Defender/Burrow Effect"),
               ("GG_Brooding_Mawlek", "Battle Scene/Mawlek Body/Shot Mawlek NoDrip"),
               ("GG_Nosk", "Battle Scene/Mawlek Body/Shot Mawlek NoDrip"),
               ("GG_Ghost_Xero","Warrior/Ghost Warrior Xero/Sword 1"),
               ("GG_Ghost_Xero","Warrior/Ghost Warrior Xero/S1 Home"),
               ("GG_Ghost_Xero","Warrior/Ghost Warrior Xero/Sword 2"),
               ("GG_Ghost_Xero","Warrior/Ghost Warrior Xero/S2 Home"),
               ("GG_Broken_Vessel","Infected Knight/Dstab Burst"),
               ("GG_Ghost_Galien","Warrior/Galien Hammer"),
               ("GG_Collector","Spawn Jar"),
               //("sharedassets209.assets","Shot Traitor Lord"),
               //("GG_Dung_Defender", "Dung Defender"),
               ("GG_God_Tamer", "Entry Object/Lobster"),
               //("GG_Dung_Defender", "_GameManager/GlobalPool/Dung Ball Small"),
              // ("GG_Dung_Defender/Dung Defender/", "Splash Out"),
            };
     

        //public BA_tets() : base("BA_tets")
        //{
        //    Instance = this;
        //}

        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects)
        {



            Preloads = preloadedObjects;
            foreach(var name in Preloads.Values)
            {
                foreach(var go in name.Values)
                {
                    //GameObject.DontDestroyOnLoad(go);
                }

            }
            
            
            //Log(preloadedObjects.Keys);
            Log("Initializing");

            Instance = this;

            Log("Initialized");

            AbilityMap[Quake.abilityName].addAbility(new Burrow());
            //AbilityMap[Scream.abilityName].addAbility(new Brood());
            AbilityMap[Dreamgate.abilityName].addAbility(new Xero());
            AbilityMap[Quake.abilityName].addAbility(new BV());
            AbilityMap[Dreamgate.abilityName].addAbility(new Galien());
            AbilityMap[Focus.abilityName].addAbility(new Jar());
           AbilityMap[Fireball.abilityName].addAbility(new GodTamer());
            AbilityMap[DoubleJump.abilityName].addAbility(new test());



        }
    }
}