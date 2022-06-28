using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Satchel;
using HutongGames.PlayMaker.Actions;
using HutongGames;
using GlobalEnums;


namespace BA_tets
{
    public class Xero : AbilityChanger.Ability
    {
        static new string name = "Xero";
        static new string title = "Xero";
        static new string description = "weeeeeeeeeeeeeeeeeee";
        public static GameObject sword1 = null;
        public static GameObject sword2 = null;
        public static GameObject home1 = null;
        public static GameObject home2 = null;
        private GameObject target=null;

        static Sprite getActiveSprite() { return Satchel.AssemblyUtils.GetSpriteFromResources("burrow.png"); }


        public Xero():base(Xero.name, Xero.title, Xero.description, Xero.getActiveSprite(), Xero.getActiveSprite(), ()=>true, true)
        {
            On.HeroController.Start += AddHome;
            On.EnemyDreamnailReaction.RecieveDreamImpact += Att;
            
        }

        private void Att(On.EnemyDreamnailReaction.orig_RecieveDreamImpact orig, EnemyDreamnailReaction self)
        {
            target = self.gameObject;
            if (sword1 != null)
            {
                sword1.LocateMyFSM("xero_nail").GetState("Antic Spin").GetAction<GetAngleToTarget2D>(1).target = target;
                sword1.LocateMyFSM("xero_nail").GetState("Antic Point").GetAction<GetAngleToTarget2D>(0).target = target;

                var d = sword1.AddComponent<DamageEnemies>();
                d.attackType = AttackTypes.Spell;
                d.damageDealt = 50;

                sword1.LocateMyFSM("xero_nail").SendEvent("ATTACK");
                if (sword2 != null)
                {
                    sword2.LocateMyFSM("xero_nail").GetState("Antic Spin").GetAction<GetAngleToTarget2D>(1).target = target;
                    sword2.LocateMyFSM("xero_nail").GetState("Antic Point").GetAction<GetAngleToTarget2D>(0).target = target;
                    var de = sword2.AddComponent<DamageEnemies>();
                    de.attackType = AttackTypes.Spell;
                    de.damageDealt = 50;


                    sword2.LocateMyFSM("xero_nail").SendEvent("ATTACK");
                }
            }
            orig(self);
        }

        private void AddHome(On.HeroController.orig_Start orig, HeroController self)
        {

            home1 = GameObject.Instantiate(BA_tets.Preloads["GG_Ghost_Xero"]["Warrior/Ghost Warrior Xero/S1 Home"], HeroController.instance.transform);
            home1.SetActive(true);
            home1.name=home1.name.Replace("(Clone)", "");

            home2 = GameObject.Instantiate(BA_tets.Preloads["GG_Ghost_Xero"]["Warrior/Ghost Warrior Xero/S2 Home"], HeroController.instance.transform);
            home2.SetActive(true);
            home2.name=home2.name.Replace("(Clone)", "");

           

            
            orig(self);


        }

        public override void handleAbilityUse(string interceptedState, string interceptedEvent)
        {
            if (sword1 == null)
            { 
                sword1 = GameObject.Instantiate(BA_tets.Preloads["GG_Ghost_Xero"]["Warrior/Ghost Warrior Xero/Sword 1"], HeroController.instance.transform);
                sword1.SetActive(true);
                sword1.GetAddComponent<Collider2D>().isTrigger = true;
                sword1.layer = ((int)PhysLayers.ENEMY_DETECTOR);



                return;
            }
            
            
            if (sword2 == null)
            { sword2=GameObject.Instantiate(BA_tets.Preloads["GG_Ghost_Xero"]["Warrior/Ghost Warrior Xero/Sword 2"], HeroController.instance.transform);
                sword2.SetActive(true);
                sword2.GetAddComponent<Collider2D>().isTrigger = true;
                sword2.layer = ((int)PhysLayers.ENEMY_DETECTOR);

                return; 
            }
        }



    }
}
