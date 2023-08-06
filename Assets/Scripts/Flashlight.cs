using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private Light flashlight;
    [SerializeField] private float flashlightDistance;
    [SerializeField] private float overlapSphereRadius;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float batteryDuration;
    [SerializeField, Range(.1f, .5f)] private float batteryBarWidthPercent;
    [SerializeField, Range(.001f, .025f)] private float batteryBarHeightPercent;
    [SerializeField] private Image batteryBarBG; 
    [SerializeField] private Image batteryBar;
    [SerializeField] private float flashlightDamage;


    public float batteryRemaining;

    private bool isLightOn = false;
    private bool canFlashlight = true;
    private float batteryBarWidth;
    private float batteryBarHeight;
    private CanvasGroup batteryBarCG;


    private void Start()
    {
        batteryBarCG = GetComponentInChildren<CanvasGroup>();
        ShowBatteryBar(false);

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        batteryBarWidth = screenWidth * batteryBarWidthPercent;
        batteryBarHeight = screenHeight * batteryBarHeightPercent;

        batteryBarBG.rectTransform.sizeDelta = new Vector3(batteryBarWidth, batteryBarHeight, 0f);
        batteryBar.rectTransform.sizeDelta = new Vector3(batteryBarWidth - 2, batteryBarHeight - 2, 0f);

        flashlight.enabled = isLightOn;
        batteryRemaining = batteryDuration;
    }

    private void Update()
    {
        if (canFlashlight)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                isLightOn = !isLightOn;
                flashlight.enabled = isLightOn;
                ShowBatteryBar(true);

                //play sound
            }

            if (isLightOn)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                Debug.DrawRay(transform.position, transform.forward * flashlightDistance, Color.blue, .2f);
                if(Physics.Raycast(ray, out RaycastHit hitInfo, flashlightDistance))
                {
                    Collider[] colliders = Physics.OverlapSphere(hitInfo.point, overlapSphereRadius);
                    foreach (Collider collider in colliders)
                    {
                        if (collider.CompareTag("Enemy"))
                        {
                            collider.GetComponent<Health>().TakeDamage(flashlightDamage);
                        }
                    }
                }

                batteryRemaining -= 1 * Time.deltaTime;
                if (batteryRemaining <= 0f)
                {
                    batteryRemaining = 0f;
                    isLightOn = false;
                    canFlashlight = false;
                    ShowBatteryBar(false);
                }
            }
        }
        else
        {
            flashlight.enabled = isLightOn;
        }

        if(batteryRemaining > 0f)
        {
            canFlashlight = true;
            if (batteryRemaining > batteryDuration)
            {
                batteryRemaining = batteryDuration;
                ShowBatteryBar(false);
            }
        }

        float batteryRemainingPercent = batteryRemaining / batteryDuration;
        batteryBar.transform.localScale = new Vector3(batteryRemainingPercent, 1f, 1f);

    }

    private void ShowBatteryBar(bool condition)
    {
        batteryBarCG.alpha = condition ? 1f : 0f;
        batteryBarBG.gameObject.SetActive(condition);
        batteryBar.gameObject.SetActive(condition);
    }

}
