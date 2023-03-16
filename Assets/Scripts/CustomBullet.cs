using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    public Rigidbody bulletBody;

    //bullet stat
    [Range(0,1)]
    public float bounciness;
    public bool useGravity;

    int collision;
    PhysicMaterial physicMaterial;

    private void Start() {
        Setup();
    }
    private void Update() {
        Invoke("ClearBullet", 2f);
    }
    private void ClearBullet(){
        Destroy(gameObject); 
    }
    private void Setup(){
        //tao material
        physicMaterial = new PhysicMaterial();
        physicMaterial.bounciness = bounciness;
        physicMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
        physicMaterial.bounceCombine = PhysicMaterialCombine.Maximum;
        //gan material vao collider
        GetComponent<SphereCollider>().material = physicMaterial;
        //độ rơi của đạn/trọng lực lên đạn
        bulletBody.useGravity = useGravity;

    }
private void OnCollisionEnter(Collision other) {
    if(other.gameObject.tag =="MeleEnemy" || other.gameObject.tag =="Ground"||other.gameObject.tag =="RangeEnemy"||other.gameObject.tag =="Player"||other.gameObject.tag =="Boss")
        ClearBullet();
}
}
