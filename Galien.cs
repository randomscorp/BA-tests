using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using GlobalEnums;

namespace BA_tets
{
    public  class Galien: AbilityChanger.Ability
    {
        static new string name = "Galien";
        static new string title = "Galien";
        static new string description = "weeeeeeeeeeeeeeeeeee";
        public static GameObject hammer1 = null;
        static Sprite getActiveSprite() { return Satchel.AssemblyUtils.GetSpriteFromResources("burrow.png"); }

        public Galien():base(Galien.name, Galien.title, Galien.description, getActiveSprite(), getActiveSprite(), () => true, true) 
        {
            hammer1 = BA_tets.Preloads["GG_Ghost_Galien"]["Warrior/Galien Hammer"];
        }

        public override void handleAbilityUse(string interceptedState, string interceptedEvent)
        {

            GameObject.Instantiate(hammer1,HeroController.instance.transform.position,Quaternion.identity).SetActive(true);
            hammer1.LocateMyFSM("Control").SendEvent("READY");
            hammer1.LocateMyFSM("Attack").SendEvent("HAMMER ATTACK");

        }

    }
}
