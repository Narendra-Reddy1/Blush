using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace Naren_Dev
{
    [CreateAssetMenu(fileName = "newAddressablesHelper", menuName = "ScriptablesObjects/AddressableHelper")]
    public class AddressablesHelper : BaseScriptableObject
    {
        public void LoadAssetAsync<T>(string key, Action<AsyncOperationHandle<T>> OnCompleted) where T : UnityEngine.Object
        {
            AsyncOperationHandle operationHanlde = Addressables.LoadResourceLocationsAsync(key);
            if (operationHanlde.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"Invalid Addressables Key: {key}");
                return;
            }
            Addressables.LoadAssetAsync<T>(key).Completed += (handle) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                    OnCompleted?.Invoke(handle);
            };
        }
        public async void LoadAssetAsync<T>(string name, Action<bool, AsyncOperationHandle<T>> OnCompleted) where T : class
        {
            var async = Addressables.LoadAssetAsync<T>(name);
            await async.Task;
            if (async.Status == AsyncOperationStatus.Succeeded)
            {
                OnCompleted.Invoke(true, async);
            }
            else
            {
                OnCompleted.Invoke(false, default);
            }

        }
        public async void Instantiate(AssetReferenceGameObject obj, Transform parent, bool instantiateInWorldSpace, Action<bool, AsyncOperationHandle<GameObject>> OnCompleted)
        {

            if (obj.RuntimeKeyIsValid())
            {
                var async = Addressables.InstantiateAsync(obj, parent, instantiateInWorldSpace);
                await async.Task;
                // Debug.Log("Instantiate  valid key");
                if (async.Status == AsyncOperationStatus.Succeeded)
                {
                    OnCompleted.Invoke(true, async);
                    return;
                }
            }
            else
            {
                Debug.Log("Instantiate not valid key");
            }
            OnCompleted.Invoke(false, default);

        }
        public void Instantiate<T>(string key, Transform parent, bool instantiateInWorldSpace, Action OnCompleted) where T : AssetReference
        {
            AsyncOperationHandle operationHanlde = Addressables.LoadResourceLocationsAsync(key);
            if (operationHanlde.Status != AsyncOperationStatus.Succeeded)
                return;
            Addressables.InstantiateAsync(key, parent, instantiateInWorldSpace).Completed += (handle) =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    OnCompleted?.Invoke();
                }
            };
        }
        public void Release(AsyncOperationHandle handle)
        {
            if (handle.IsValid())
                Addressables.Release(handle);
        }
        public void Release<TObj>(AsyncOperationHandle<TObj> asyncObj) where TObj : UnityEngine.Object
        {
            if (asyncObj.IsValid())
            {
                Addressables.Release(asyncObj);
            }
        }
        public void Release<T>(T asyncObj) where T : class
        {
            if (asyncObj != null)
            {
                Addressables.Release(asyncObj);
            }
        }

    }
}