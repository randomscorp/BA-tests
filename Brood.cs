using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Satchel;
using GlobalEnums;
using System.Timers;
using System.Collections;
using System.Collections.Generic;
namespace BA_tets
{
    public class Brood:AbilityChanger.Ability
    {
        static new string name = "Brood";
        static new string title = "Brood";
        static new string description = "DIVES IN AND DIVES OUT ";
        public static GameObject ball = null;
        public static GameObject brood = null;




        public Brood() : base(Brood.name, Brood.title, Brood.description, Brood.getActiveSprite(), Brood.getActiveSprite(), () => true, true)
        {
            Physics2D.IgnoreLayerCollision(((int)PhysLayers.HERO_ATTACK), ((int)PhysLayers.HERO_ATTACK));
            Physics2D.IgnoreLayerCollision(((int)PhysLayers.HERO_ATTACK), ((int)PhysLayers.HERO_BOX));
            foreach (var go in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                //if (go.hideFlags != HideFlags.HideAndDontSave) return;
                if (go.name == "Shot Mawlek")//Vomit Glob Nosk
                {
                    Brood.ball = go;
                    GameObject.Destroy(Brood.ball.GetComponent<DamageHero>());
                    var damageE = Brood.ball.AddComponent<DamageEnemies>();
                    damageE.attackType = AttackTypes.Spell;
                    damageE.damageDealt = 50;
                    damageE.ignoreInvuln = false;
                    damageE.magnitudeMult = 0.001f;
                    damageE.moveDirection = false;
                    damageE.specialType = 0;
                    damageE.circleDirection = false;

                    Brood.ball.layer = ((int)PhysLayers.HERO_ATTACK);


                }
            }

        }

        static Sprite getActiveSprite() { return Satchel.AssemblyUtils.GetSpriteFromResources("burrow.png"); }

        public override void handleAbilityUse(string interceptedState, string interceptedEvent)
        {

            GameManager.instance.StartCoroutine(Spawn());
        }

        public IEnumerator Spawn()
        {
            for(int num=0; num<50;num++)
            {
                InputHandler.Instance.acceptingInput = false;
                var go = GameObject.Instantiate(Brood.ball, HeroController.instance.transform.position, Quaternion.identity);
                var angle = UnityEngine.Random.Range(-45, 45)*2*3.14f;
                go.GetComponent<Rigidbody2D>().velocity = new Vector2(((float)Math.Cos(angle)),((float)Math.Sin(angle)))*30;
                // go.AddComponent<resetter>();
                GameObject.Destroy(go.GetComponent<DamageHero>());

                var damageE = go.AddComponent<DamageEnemies>();
                damageE.attackType = AttackTypes.Spell;
                damageE.damageDealt = 50;
                damageE.ignoreInvuln = false;
                damageE.magnitudeMult = 0.001f;
                damageE.moveDirection = false;
                damageE.specialType = 0;
                damageE.circleDirection = false;

                go.layer = ((int)PhysLayers.HERO_ATTACK);
                yield return new WaitForSeconds(0.1f);
            }
            InputHandler.Instance.acceptingInput = true;

        }


    }
    public class resetter:MonoBehaviour
    {
        void OnCollisionEnter2D()
        {
            Destroy(this.gameObject);
        }
    }
}
