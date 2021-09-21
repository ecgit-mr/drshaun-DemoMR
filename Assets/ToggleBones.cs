using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleBones : MonoBehaviour
{
	//  Fields ----------------------------------------------
	public MVDModelData MVDModelData { get { return _mvdModelData; } }

	//  Fields ----------------------------------------------
	[SerializeField]
	private MVDModelData _mvdModelData = null;

	[SerializeField]
	private Interactable[] interactables;

	private GameObject _characterView;


	private void Start()
    {
		for (int i = 0; i < interactables.Length; i++)
		{
			var patientButton = interactables[i];

			int index = i;
			patientButton.OnClick.AddListener(() =>
			{
				this.SetCharacterView(this.MVDModelData.CharacterViews[index]);
			});
		}

		this.SetCharacterView(this.MVDModelData.CharacterViews[0]);
	}

	private void SetCharacterView(GameObject prefab)
    {
		if (_characterView != null)
		{
			_characterView.transform.parent = null;
			Destroy(_characterView.gameObject);
		}

		_characterView = Instantiate(prefab);
		var bone = _characterView.AddComponent<Bone>();
		bone.UpdatePosition();
	}
}
