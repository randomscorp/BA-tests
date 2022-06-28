using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using GlobalEnums;
using Satchel;

using HutongGames.Extensions;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Satchel.Futils;
using static Satchel.FsmUtil;
using static Satchel.GameObjectUtils;

//("GG_God_Tamer", "Lobster"),


namespace BA_tets
{
    public class GodTamer: AbilityChanger.Ability
    {
        static new string name = "LOBSTER";
        static new string title = "LOBSTER";
        static new string description = "SPAWNS A FKING LOBSTER";
        public static GameObject lobster = null;

        static Sprite getActiveSprite() { return Satchel.AssemblyUtils.GetSpriteFromResources("burrow.png"); }



        public GodTamer(): base(name, title, description, getActiveSprite(), getActiveSprite(), () => true, true)
        {

            //GodTamer.lobster = BA_tets.Preloads["GG_God_Tamer"]["Lobster"];
           



        }

        public override void handleAbilityUse(string interceptedState, string interceptedEvent)
        {

            GodTamer.lobster = GameObject.Instantiate(BA_tets.Preloads["GG_God_Tamer"]["Entry Object/Lobster"]);


            GodTamer.lobster.transform.localScale /= 2;
            GodTamer.lobster.layer = ((int)PhysLayers.HERO_ATTACK);

            for (int child = 0; child < lobster.transform.childCount; child++)
            {
                GameObject.Destroy(lobster.transform.GetChild(child).GetComponent<DamageHero>());
                lobster.transform.GetChild(child).gameObject.AddComponent<DamageEnemies>();

            }
            lobster.AddComponent<DamageEnemies>();
            lobster.AddComponent<Lobster>();
            GameObject.Destroy(lobster.GetComponent<DamageHero>());
            GameObject.Destroy(lobster.GetComponent<HealthManager>());

            var control = lobster.LocateMyFSM("Control");

            control.GetState("Move Choice").GetAction<SendRandomEvent>(0).weights[0].Value = 0f;
            control.GetState("Move Choice").GetAction<SendRandomEvent>(0).delay = 1f;

            control.Intercept(new TransitionInterceptor()
            {
                fromState="Scuttle End",
                eventName="FINISHED",
                toStateDefault="Attack Choice",
                toStateCustom= "Move Choice"
            });


            GodTamer.lobster.transform.position = HeroController.instance.transform.position;
            GodTamer.lobster.SetActive(true);
            GodTamer.lobster.LocateMyFSM("Control").SendEvent("WAKE");



        }
            

        private class Lobster: MonoBehaviour
        {

            void OnTriggerEnter2D(Collider2D col)
            {
                if(col.gameObject.tag=="Nail Attack")
                {
                    GodTamer.lobster.LocateMyFSM("Control").SendEvent("ATTACK");

                }

            }
        }

    }





}
