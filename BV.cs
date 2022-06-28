using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using GlobalEnums;
namespace BA_tets
{
    public class BV: AbilityChanger.Ability
    {
        static new string name = "Xero";
        static new string title = "Xero";
        static new string description = "weeeeeeeeeeeeeeeeeee";
        static GameObject infec = null;
        static GameObject slam = null;

        static Sprite getActiveSprite() { return Satchel.AssemblyUtils.GetSpriteFromResources("burrow.png"); }


        public BV() : base(BV.name, BV.title, BV.description, getActiveSprite(), getActiveSprite(), () => true, true)
        {

            slam = BA_tets.Preloads["GG_Broken_Vessel"]["Infected Knight/Dstab Burst"];
            GameObject.DontDestroyOnLoad(slam);

            foreach (var go in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                //if (go.hideFlags != HideFlags.HideAndDontSave) return;
                if (go.name == "IK Projectile DS")
                {
                    BV.infec = go;
                    GameObject.DontDestroyOnLoad(BV.infec);
                    GameObject.Destroy(BV.infec.GetComponent<DamageHero>());
                    BV.infec.transform.localScale *= 10f;
                    
                    var damageE = BV.infec.AddComponent<DamageEnemies>();
                    damageE.attackType = AttackTypes.Spell;
                    damageE.damageDealt = 50;
                    damageE.ignoreInvuln = false;
                    damageE.magnitudeMult = 0.001f;
                    damageE.moveDirection = false;
                    damageE.specialType = 0;
                    damageE.circleDirection = false;

                    BV.infec.layer = ((int)PhysLayers.HERO_ATTACK);


                }
            }
        }




        public override void handleAbilityUse(string interceptedState, string interceptedEvent)
        {

            slam.transform.position = HeroController.instance.transform.position;
            slam.SetActive(true);

            foreach (float pos in new List<float>() { -8, -4, 4, 8 })
            {
                GameObject.Instantiate(BV.infec, HeroController.instance.transform.position, Quaternion.identity)
                    .GetComponent<Rigidbody2D>().velocity = new Vector2(pos, 0);

            }


        }


    }
}
