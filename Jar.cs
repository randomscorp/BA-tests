using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using GlobalEnums;



namespace BA_tets
{

    public class SpawnClone : MonoBehaviour
    {

        private SpriteRenderer sprite;
        private Vector3 pos;
        private Rigidbody2D body;
        private int state = 0;
        private void Start()
        {
            pos = HeroController.instance.transform.position;
            sprite = base.GetComponent<SpriteRenderer>();

        }

        private void Update()
        {
            switch (state){
                case 0:
                    if (sprite.enabled == true) { state = 1; }
                    break;
                case 1:
                    if (sprite.enabled == false) 
                    {
                        GameObject.Instantiate(Jar.clone, transform.position, Quaternion.identity).SetActive(true);
                        GameObject.Destroy(this);
                    }
                    break;

            }



        }
    }



    public class Jar: AbilityChanger.Ability
    {
        static new string name = "Galien";
        static new string title = "Galien";
        static new string description = "weeeeeeeeeeeeeeeeeee";
        public static GameObject jar = null;
        public static GameObject clone = null;

        static Sprite getActiveSprite() { return Satchel.AssemblyUtils.GetSpriteFromResources("burrow.png"); }

        public Jar() : base(Jar.name, Jar.title, Jar.description, getActiveSprite(), getActiveSprite(), () => true, true) 
        {

            On.EnemyDreamnailReaction.RecieveDreamImpact += FindClone;
            On.SetSpawnJarContents.OnEnter += SetSpawnJarContents_OnEnter;
            foreach (var go in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                //if (go.hideFlags != HideFlags.HideAndDontSave) return;
                if (go.name == "Spawn Jar")
                {
                    Jar.jar= go;
                    GameObject.DontDestroyOnLoad(Jar.jar);
                    GameObject.Destroy(Jar.jar.GetComponent<DamageHero>());
                    Jar.jar.AddComponent<SpawnClone>();
                   /* BV.infec.transform.localScale *= 10f;

                    var damageE = BV.infec.AddComponent<DamageEnemies>();
                    damageE.attackType = AttackTypes.Spell;
                    damageE.damageDealt = 50;
                    damageE.ignoreInvuln = false;
                    damageE.magnitudeMult = 0.001f;
                    damageE.moveDirection = false;
                    damageE.specialType = 0;
                    damageE.circleDirection = false;*/

                    //Jar.jar.layer = ((int)PhysLayers.ENEMY_DETECTOR);
                    //Jar.jar.GetComponent<SpawnJarControl>().SetEnemySpawn()

                }
            }

        }

        private void SetSpawnJarContents_OnEnter(On.SetSpawnJarContents.orig_OnEnter orig, SetSpawnJarContents self)
        {
            self.enemyPrefab = Jar.clone;
            orig(self);

        }

        private void FindClone(On.EnemyDreamnailReaction.orig_RecieveDreamImpact orig, EnemyDreamnailReaction self)
        {

            Jar.clone = GameObject.Instantiate(self.gameObject);
            Jar.clone.SetActive(false);
            Jar.clone.GetComponent<HealthManager>().hp = 1;
            orig(self);
        }

        public override void handleAbilityUse(string interceptedState, string interceptedEvent)
        {
            
            Jar.jar.GetComponent<SpawnJarControl>().breakY = HeroController.instance.transform.position.y;
            Jar.jar.GetComponent<SpawnJarControl>().spawnY = HeroController.instance.transform.position.y + 20;
            Jar.jar.GetComponent<SpawnJarControl>().SetEnemySpawn(Jar.clone, 10);

            GameObject.Instantiate(Jar.jar,HeroController.instance.transform.position, Quaternion.identity).SetActive(true);


        }




    }
}
