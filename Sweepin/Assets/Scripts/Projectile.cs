using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int projectileDamage;
    public bool isProjectilePlayersOriginal;
    private AudioManager audioManager;
    public Sprite projectileStuckInWallSprite;
    private SpriteRenderer currentProjectileSpriteRenderer;

    List<string> collisionTagsToIgnore = new List<string>()
    {
        "MyPlayer",
        "Tile",
    };
    public List<AudioClip> projectileHitStoneAudioList = new List<AudioClip>() { };
    public List<AudioClip> projectileHitWoodAudioList = new List<AudioClip>() { };
    public List<AudioClip> projectileHitMetalAudioList = new List<AudioClip>() { };
    public List<AudioClip> projectileHitEnemyAudioList = new List<AudioClip>() { };
    void Start()
    {
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        currentProjectileSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }


    private void OnCollisionEnter2D(Collision2D objectWeCollidedWith)
    {
        //print("-------Projetile COLLIDED WITH: " + objectWeCollidedWith.gameObject.name);
        if (isProjectilePlayersOriginal || collisionTagsToIgnore.Contains(objectWeCollidedWith.gameObject.tag)) {
            print("THIS PROJECTILE collided with list of ignored tags, we will not acceppt cllision");
            return; 
        }


        FreezeProjectileAndMakeColliderObjectParent(objectWeCollidedWith);


        StartCoroutine(WaitBeforeDestroyingProjectile());
    }

    private void FreezeProjectileAndMakeColliderObjectParent(Collision2D objectWeCollidedWith)
    {
        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        currentProjectileSpriteRenderer.sprite = projectileStuckInWallSprite;
        this.gameObject.transform.parent = objectWeCollidedWith.transform;
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;

        if (objectWeCollidedWith.gameObject.tag == "Enemy")
        {
            objectWeCollidedWith.gameObject.GetComponent<EnemyHealth>().TakeDamage(projectileDamage);
            audioManager.PlayRandomSoundEffectFromList(projectileHitEnemyAudioList, 0.2f, audioManager.soundEffectsSource);
        }
        else
        {
            audioManager.PlayRandomSoundEffectFromList(projectileHitWoodAudioList, 0.2f, audioManager.soundEffectsSource);

        }
    }

    private IEnumerator WaitBeforeDestroyingProjectile()
    {
        yield return new WaitForSeconds(1f);
       Destroy(this.gameObject);
    }
}
