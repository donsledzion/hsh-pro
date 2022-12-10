using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class ClosetopencloseDoor : MonoBehaviour
	{

		public Animator Closetopenandclose;
		public bool open;

		void Start()
		{
			open = false;
		}


		[ContextMenu("Toggle")]
		public void Toggle()
		{
			if (open)
				Close();
			else
				Open();
		}


        [ContextMenu("Open")]
        public void Open()
        {
            StartCoroutine(opening());
        }

        [ContextMenu("Close")]
        public void Close()
        {
            StartCoroutine(closing());
        }
        IEnumerator opening()
		{
			print("you are opening the door");
			Closetopenandclose.Play("ClosetOpening");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			Closetopenandclose.Play("ClosetClosing");
			open = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}