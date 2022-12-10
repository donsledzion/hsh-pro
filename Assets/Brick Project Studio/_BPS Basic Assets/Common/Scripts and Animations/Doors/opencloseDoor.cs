using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour
	{

		public Animator openandclose;
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
			openandclose.Play("Opening");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			openandclose.Play("Closing");
			open = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}