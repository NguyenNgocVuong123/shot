using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UseGun : MonoBehaviour
{
    public GameObject bullet;
    public float shootForce, upwardForce;
    //Thông tin của súng
    public float timeBetweenShooting, spread, reLoadTime, timeBetweenShoots;
    public int magazinSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletLeft, bulletShot;
    //recoil
    public Rigidbody playerBodys;
    public float recoilForce;

    bool shooting, readyToShoot, reLoading;
    //tia súng
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammo;//hiển thị đạn
    //cam
    public Camera cam;
    public Transform attackPoint;

    public bool allowInvoke = true;

    private void Awake() {
        bulletLeft = magazinSize; //phải đảm bảo băng đạn đầy
        readyToShoot = true;
        
    }

     private void Update() {
        MyInput();
        if (ammo != null)
            ammo.SetText(bulletLeft /bulletsPerTap + " / " + magazinSize/bulletsPerTap);
     }
    private void MyInput(){
        //kiểm tra xem có đang nhấn giữ bắn không và chỉnh phím cho phù hợp
        if(allowButtonHold)
            shooting = Input.GetKey(KeyCode.Mouse0);//nếu đang giữ chuột trái thì shooting = true
        else
            shooting = Input.GetKeyDown(KeyCode.Mouse0);//ngược lại thì là tap từng viên
        //Nạp đạn
        if(Input.GetKeyDown(KeyCode.R) && bulletLeft < magazinSize && !reLoading)
            Reload();
        //tự động nạp khi cố bắn lúc hết đạn
        if(readyToShoot && shooting && !reLoading && bulletLeft <=0)
            Reload();
        //kiểm tra xem có đang trong trạng thái sẵn sàng và bắn hay không và trạng thái nạp đạn
        //cùng với số đạn trong băng phải vẫn còn
        if(readyToShoot && shooting && !reLoading && bulletLeft >0){
            bulletShot = 0; //đặt trạng thái đạn dần về 0 gọi hàm bắn
            Shoot();
        }
    }
    private void Shoot(){
        readyToShoot = false;
        
        //Xác định vị trí đạn sẽ bay tới bằng raycast
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0f)); //tìm điểm ray giữa màn hình
        RaycastHit hit;
        //kiểm tra xem ray có chạm gì không, nếu có thì set vị trí của target = vị trí của rayhit
        Vector3 targetPoint;
        if(Physics.Raycast(ray , out hit))
            targetPoint = hit.point;
        else //nếu không thì bắn vào hư vô
            targetPoint = ray.GetPoint(75); //vị trí cách xa player

        //tính vị trí từ điểm nhả đạn đến vị trí target
        Vector3 dirWithNoSpeard = targetPoint - attackPoint.position;
        //tính độ giật
        float x  = Random.Range(-spread, spread);
        float y  = Random.Range(-spread, spread);
        //tính vị trí đạn tới sau khi giật
        Vector3 DirWithSpeard = dirWithNoSpeard + new Vector3(x,y,0); 
        
        //tạo đạn
        //tại đây sau khi tạo đạn sẽ chứa trong CurrentBullet
        GameObject CurrentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        //xoay đạn về hướng muốn bắn
        CurrentBullet.transform.forward = DirWithSpeard.normalized;
        //làm đạn bay
        CurrentBullet.GetComponent<Rigidbody>().AddForce(DirWithSpeard.normalized * shootForce, ForceMode.Impulse);//bay ngang
        CurrentBullet.GetComponent<Rigidbody>().AddForce(cam.transform.up * upwardForce, ForceMode.Impulse);//bay doc
        

        //tạo tia lửa
        if(muzzleFlash != null){
            Instantiate(muzzleFlash,attackPoint.position, Quaternion.identity);
        }
        bulletLeft--;
        bulletShot++;
        //tạo khoảng dừng giữa mỗi lần bắn bằng cách gọi  hàm ResetShoot
        if(allowInvoke){//chỉ gọi 1 lần mỗi lượt bắn
            Invoke("ResetShoot", timeBetweenShooting);
            allowInvoke = false; 
            //tạo độ giật
            playerBodys.AddForce(-DirWithSpeard.normalized *recoilForce, ForceMode.Impulse);
        }
        //nếu chơi shotgun
        if(bulletShot < bulletsPerTap && bulletLeft >0){
            Invoke("Shoot", timeBetweenShoots);
        }
    }
    private void ResetShoot(){
        readyToShoot = true;
        allowInvoke = true;   
    }
    private void Reload(){
        reLoading = true;
        Invoke("ReloadFinished", reLoadTime);
    }
    private void ReloadFinished(){
        bulletLeft = magazinSize;
        reLoading = false;
    }
}
