﻿using System.Collections;
using UnityEngine;

namespace ChobiAssets.PTM
{

	public class Cannon_Fire_Input_02_For_Sticks_Drive_CS : Cannon_Fire_Input_00_Base_CS
	{

		public override void Get_Input()
		{
			if (Input.GetButton ("Bumper R")) {
				cannonFireScript.Fire ();
			}
		}

	}

}
