using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class AnimationEventTrigger : MonoBehaviour {
	public UnityEvent event1;
	public UnityEvent event2;
	public UnityEvent event3;
	public UnityEvent event4;
	public UnityEvent event5;

	public void TriggerEvent1 ()
	{
		if (event1 != null)
			event1.Invoke ();
	}

	public void TriggerEvent2 ()
	{
		if (event2 != null)
			event2.Invoke ();
	}

	public void TriggerEvent3 ()
	{
		if (event3 != null)
			event3.Invoke ();
	}

	public void TriggerEvent4 ()
	{
		if (event4 != null)
			event4.Invoke ();
	}

	public void TriggerEvent5 ()
	{
		if (event5 != null)
			event5.Invoke ();
	}
}
