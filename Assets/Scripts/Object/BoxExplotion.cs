using System;
using UnityEngine;
using Assets.Scripts.Core;

namespace Assets.Scripts.Object
{
    public class BoxExplotion : MonoBehaviour
    {
        public event Action OnExplotion;

        public void Explotion()
        {
            Debug.Log("Emito evento de que explota");
            OnExplotion?.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}