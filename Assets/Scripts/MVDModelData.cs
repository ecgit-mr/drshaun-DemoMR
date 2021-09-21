using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ModelData",
		menuName = "ECGiT/ModelData",
		order = 0)]
public class MVDModelData : ScriptableObject
{
	//  Fields ----------------------------------------------
	public List<GameObject> CharacterViews { get { return _characterViews; } }

	[SerializeField]
	private List<GameObject> _characterViews = null;

}