using UnityEngine;
using System.Collections;

public class IPopupController : MonoBehaviour {
	public System.Action onClose;

	public virtual void Close(){}

	public virtual void SelfDestruction(){
		if(gameObject == null || !gameObject.activeSelf){
			return;
		}
	}

	protected void OnSpawned(){
		
	}

	protected void OnDespawned(){
		onClose = null;
	}
}
