using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
public class GunAimController : MonoBehaviour
{
    [SerializeField] private Transform gunPivotTransform;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform hitDummy;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject LauncherParticle;
    [SerializeField] private Animator anim;
    [SerializeField] private float delay;
    [SerializeField] Image reloadProgress;
    private float currTimeBtwShots = 0;
    private Vector3 inputStartPos;
    private void Start()
    {
        delay = 0.5f;
        currTimeBtwShots = delay;
    }
    private void Update()
    {
        anim.SetBool("Fire", false);
        currTimeBtwShots -= Time.deltaTime;
        reloadProgress.fillAmount +=  Time.deltaTime/ delay;
        if (!GameManager.instance.endLevel)
        {
            var crosshairPosition = Camera.main.WorldToScreenPoint(hitDummy.transform.position);
            GameManager.instance.uiLevel.crossHair.transform.position = crosshairPosition;
            gunPivotTransform.transform.LookAt(hitDummy.transform.position);
            
            
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 800, _layerMask))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    hitDummy.transform.position = hit.point;
                    


                }
                if (currTimeBtwShots < 0 && Input.GetKey(KeyCode.Mouse0))
                {
                    Fire();
                    anim.SetBool("Fire", true);
                    LauncherParticle.GetComponent<ParticleSystem>().Play();
                    currTimeBtwShots = delay;
                    reloadProgress.fillAmount = 0;
                }
            
        }
        
    }
    

    public void Fire()
    {
        if (GameManager.instance.ammoCount <= 0) return;

        var projectile = Instantiate(projectilePrefab, firepoint.position, Quaternion.identity);
        projectile.transform.LookAt(hitDummy.transform.position);
        GameManager.instance.ammoCount--;
        GameManager.instance.uiLevel.ammoText.text = GameManager.instance.ammoCount.ToString();

        if (GameManager.instance.ammoCount == 0)
        {
            anim.SetBool("Fire", false);
            LauncherParticle.SetActive(false);
            StartCoroutine(waitToGameEnd());
            IEnumerator waitToGameEnd()
            
            {
                //wait maybe this shot will win the game
                yield return new WaitForSeconds(3f);
                GameManager.instance.LevelEnds();
            }
        }
    }


}