using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using GlobalEnums;


namespace BA_tets
{
    public class TraitorLord:AbilityChanger.Ability
    {
        static new string name = "TL";
        static new string title = "TL";
        static new string description = "adasdsa";
        public static GameObject sickle = null;
        static Sprite getActiveSprite() { return Satchel.AssemblyUtils.GetSpriteFromResources("burrow.png"); }

        public TraitorLord() : base(TraitorLord.name, TraitorLord.title, TraitorLord.description, getActiveSprite(), getActiveSprite(), () => true, true)
        {
                    TraitorLord.sickle = BA_tets.Preloads["sharedassets209.assets"]["Shot Traitor Lord"];
                    GameObject.DontDestroyOnLoad(TraitorLord.sickle);
                    GameObject.Destroy(TraitorLord.sickle.GetComponent<DamageHero>());
        }
        public override void handleAbilityUse(string interceptedState, string interceptedEvent)
        {
            var go = GameObject.Instantiate(TraitorLord.sickle);
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 0);

            go = GameObject.Instantiate(TraitorLord.sickle);
            go.GetComponent<Rigidbody2D>().velocity = new Vector2(22.5f, 0);


        }



    }
}
