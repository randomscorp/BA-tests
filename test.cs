using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BA_tets
{
    public class test:AbilityChanger.Ability
    {
        static new string name = "test";
        static new string title = "we are testing stuff";
        static new string description = "weeeeeeeeeeeeeeee";

        static Sprite getActiveSprite() { return Satchel.AssemblyUtils.GetSpriteFromResources("burrow.png"); }


        public test() : base(test.name, test.title, test.description, getActiveSprite(), getActiveSprite(), () => true, true)
        {

        }



        public override void handleAbilityUse(string interceptedState, string interceptedEvent)
        {
            Modding.Logger.Log("test");

        }


    }
}
