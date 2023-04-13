using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RemoteLoad : MonoBehaviour
{
    private GameObject _mMyGameObject;

    [SerializeField] TextMeshProUGUI text_Load_Percent;
    [SerializeField] Slider progresseSlider;
    [SerializeField] string key;

    [SerializeField] GameObject panel_Managment;
    [SerializeField] GameObject panel_Loading;
    [SerializeField] GameObject Load_Button;

    [SerializeField] GameObject loadImage;
    bool loading;
    CurrentManagment management;

    private void Awake() => management = GameObject.Find("---------").GetComponent<CurrentManagment>();
    

    private void Update(){
        if (loading){
             
            loadImage.transform.Rotate(Vector3.forward, 20 * Time.deltaTime);
        }
    }
    
    private void Load_Lobby_Button(){
       panel_Managment.SetActive(true);
        Load_Button.SetActive(false);

    }
    public void PC(){
        // Logic
        management.IsMobile = false;
        panel_Managment.SetActive(false);
        panel_Loading.SetActive(true);
        StartCoroutine(InstantiateGameobjectUsingAssetReference(key));
    }
    public void MOBILE(){
        // Logic
        management.IsMobile = true;
        panel_Managment.SetActive(false);
        panel_Loading.SetActive(true);
        StartCoroutine(InstantiateGameobjectUsingAssetReference(key));
    }
    AsyncOperationHandle<SceneInstance> scenInfo;
    [System.ComponentModel.ToolboxItem(false)]
    private IEnumerator InstantiateGameobjectUsingAssetReference(string key){

        var downloadScene = Addressables.LoadSceneAsync(key,LoadSceneMode.Single);
         downloadScene.Completed += SceneDownloded;
        
        while (!downloadScene.IsDone){
            loading = true;
            var status = downloadScene.GetDownloadStatus();
            float progress = status.Percent;
            progresseSlider.value = progress * 100;
            if(progress != 0)
            text_Load_Percent.text = "Первичная установка: " + Convert.ToInt32((progress * 100)) .ToString() + " %";
            else text_Load_Percent.text = "3агрузка... Пожалуйста подождите";
            yield return null;
        }
        if(downloadScene.IsDone) text_Load_Percent.text = "Настройка сцены.. "; 
        text_Load_Percent.text = "Настройка сцены..";
         yield return null;
    }
  
    private void OnLoadDone(AsyncOperationHandle<GameObject> obj)
    {
        _mMyGameObject = obj.Result;
    }
    private void SceneDownloded(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> _handle)
    {
        if(_handle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Сцена готова");
        }
    }
    public void ReleaseGameobjectUsingAssetReference()
    {
        Addressables.Release(_mMyGameObject);
    }
}
