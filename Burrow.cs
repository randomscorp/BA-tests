using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbilityChanger;
using UnityEngine;
using Satchel;
using GlobalEnums;
namespace BA_tets
{
    public class Burrow : Ability
    {
        static new string name = "Burrow";
        static new string title = "Burrow";
        static new string description = "DIVES IN AND DIVES OUT ";
        public static GameObject ball = null;
        public static GameObject burrow = null;




        public Burrow(): base(Burrow.name, Burrow.title, Burrow.description, Burrow.getActiveSprite(), Burrow.getActiveSprite(), ()=> true , true)
        {
            /*//Physics2D.IgnoreLayerCollision(((int)PhysLayers.HERO_ATTACK), ((int)PhysLayers.HERO_ATTACK));
            //Physics2D.IgnoreLayerCollision(((int)PhysLayers.HERO_ATTACK), ((int)PhysLayers.HERO_BOX));
            foreach (var go in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                //if (go.hideFlags != HideFlags.HideAndDontSave) return;
                if (go.name == "Dung Ball Small")
                {
                    Burrow.ball = GameManager.Instantiate(go);
                    GameObject.DontDestroyOnLoad(Burrow.ball);
                    GameObject.Destroy(Burrow.ball.GetComponent<DamageHero>());
     
                    var damageE=Burrow.ball.AddComponent<DamageEnemies>();
                    damageE.attackType = AttackTypes.Spell;
                    damageE.damageDealt = 50;
                    damageE.ignoreInvuln = false;
                    damageE.magnitudeMult = 0.001f;
                    damageE.moveDirection = false;
                    damageE.specialType = 0;
                    damageE.circleDirection = false;

                    Burrow.ball.layer = ((int)PhysLayers.HERO_ATTACK);


                }
            }*/

                

            




        }

        static Sprite getActiveSprite() { return Satchel.AssemblyUtils.GetSpriteFromResources("burrow.png"); }



        public override void handleAbilityUse(string interceptedState, string interceptedEvent)
        {
           if(HeroController.instance.gameObject.GetComponent<BurrowBehaviour>() == null){
                HeroController.instance.gameObject.AddComponent<BurrowBehaviour>().gameObject.SetActive(true);
                //GameObject.Instantiate(BA_tets.Preloads["GG_Dung_Defender/Dung Defender/"]["Splash Out"]).transform.position=HeroController.instance.transform.position;
                //Modding.Logger.Log(GameManager.instance.gameObject.transform.parent.Find("Dung Ball Small") is null);
                //ball.LocateMyFSM("Control").enabled = false;
                //Modding.Logger.Log(ball == null);
                //GameObject.Instantiate(ball,HeroController.instance.transform);
            }

            //GameObject.Destroy((HeroController.instance.gameObject.GetComponent<MonoBehaviour>()));

        }
    }

    internal class BurrowBehaviour : MonoBehaviour
    {
        internal bool onGround = true;
        internal float time = 0;
        internal float upTime = 0.3f;

        private void Start()
        {



            var go = BA_tets.Preloads["sharedassets355"]["Dung Ball Small"];
            Burrow.ball = GameManager.Instantiate(go);
            GameObject.DontDestroyOnLoad(Burrow.ball);
            GameObject.Destroy(Burrow.ball.GetComponent<DamageHero>());

            var damageE = Burrow.ball.AddComponent<DamageEnemies>();
            damageE.attackType = AttackTypes.Spell;
            damageE.damageDealt = 50;
            damageE.ignoreInvuln = false;
            damageE.magnitudeMult = 0.001f;
            damageE.moveDirection = false;
            damageE.specialType = 0;
            damageE.circleDirection = false;

            Burrow.ball.layer = ((int)PhysLayers.ENEMY_DETECTOR);

            PlayerData.instance.isInvincible= true;
           
            tk2dSprite component = HeroController.instance.GetComponent<tk2dSprite>();
            Color color = component.color;
                color.a = 0f;
                component.color = color;
            Burrow.burrow = GameObject.Instantiate(BA_tets.Preloads["GG_Dung_Defender"]["Dung Defender/Burrow Effect"],
                HeroController.instance.transform.position, Quaternion.identity);
            Destroy(Burrow.burrow.LocateMyFSM("Keep Y"));

            Burrow.burrow.SetActive(true);
            Burrow.burrow.LocateMyFSM("Burrow Effect").SendEvent("BURROW START");
            Burrow.burrow.transform.localScale *=0.7f;

            //Burrow.burrow.GetComponent<MeshRenderer>().enabled = true;

        }

        private void FixedUpdate()
        {
            Burrow.burrow.transform.position = HeroController.instance.transform.position +new Vector3(0,0.5f,0);
            if (onGround) return;
            time += Time.fixedDeltaTime;
            //HeroController.instance.acceptingInput = false;
            if (time < upTime) return;
            //HeroController.instance.acceptingInput = true;
            //HeroController.instance.AffectedByGravity(true);
            PlayerData.instance.isInvincible= false;
            Destroy(Burrow.burrow);
            Destroy(this);

        }

        private void Update()
        {
            if (!HeroController.instance.CheckTouchingGround() && onGround)
            {
                onGround = false;
                //HeroController.instance.CancelHeroJump();// = false;
                //HeroController.instance.AffectedByGravity(false);
                HeroController.instance.QuakeInvuln();

                Burrow.burrow.LocateMyFSM("Burrow Effect").SetState("Inactive");
                
               // HeroController.instance.AffectedByGravity(false);
                tk2dSprite component = HeroController.instance.GetComponent<tk2dSprite>();
                Color color = component.color;

                    color.a = 1f;
                    component.color = color;
                //Destroy(Burrow.burrow);
                GameObject.Instantiate(Burrow.ball, HeroController.instance.transform.position, Quaternion.identity)
                    .GetComponent<Rigidbody2D>().velocity = new Vector2(6f, 35f);
                GameObject.Instantiate(Burrow.ball, HeroController.instance.transform.position, Quaternion.identity)
                    .GetComponent<Rigidbody2D>().velocity = new Vector2(12f, 35f);
                GameObject.Instantiate(Burrow.ball, HeroController.instance.transform.position, Quaternion.identity)
                    .GetComponent<Rigidbody2D>().velocity = new Vector2(-6f, 35f);
                GameObject.Instantiate(Burrow.ball, HeroController.instance.transform.position, Quaternion.identity)
                    .GetComponent<Rigidbody2D>().velocity = new Vector2(-12f, 35f);

            }
        }



    }


}