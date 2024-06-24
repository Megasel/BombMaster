using DG.Tweening;
using UnityEngine;
using System.Collections;


public class Chunk : MonoBehaviour
{
    public int id;

    private Collider _collider;

    public VoxelChunkManager _manager;

    public void Setup(int id, VoxelChunkManager manager)
    {
        this.id = id;
        _manager = manager;
    }


    private void Start()
    {
        _collider = gameObject.AddComponent<BoxCollider>();
    }

    public void Explode()
    {
        _collider.isTrigger = false;
        var rb = gameObject.AddComponent<Rigidbody>(); //INFO: better add it in editor and control the rigidbody with isKenimatic
        rb.freezeRotation = true;
        if (rb == null)
            return;

        transform.SetParent(null);
        rb.AddExplosionForce(35, transform.position, 3.5f, 20);

        //add the chunk to the destroyed list
        _manager.AddChunkToDamagedList(this, id);

        //add chunk to layer not colliding with future missiles
        gameObject.layer = LayerMask.NameToLayer("OutFromMissile");

        //Destroy(this,3); //INFO: "this" means you are only destroying the script component .. and you must destroy the whole gameobject=> Destroy(gameobject)
        //Destroy(gameObject, 10);

        //shake to ensure the chunk fall from the voxel
        transform.DOPunchRotation(new Vector3(90, 90, 90), .5f, 2);

        StartCoroutine(waitToDestroy());
        IEnumerator waitToDestroy()
        {
            yield return new WaitForSeconds(Random.Range(2,6));

            transform.DOScale(Vector3.zero, Random.Range(.5f,2f)).OnComplete(()=> {
                Destroy(gameObject);
            });
        }
    }

    public void AlreadyDamaged()
    {
        //only remove this from chunk list
        _manager.AddChunkToDamagedList(this);

        //Immediate explode
        Destroy(gameObject);
    }
}
