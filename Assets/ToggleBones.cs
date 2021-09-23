using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using System;
using UnityEngine;

public class ToggleBones : MonoBehaviour
{
	//  Fields ----------------------------------------------
	public MVDModelData MVDModelData { get { return _mvdModelData; } }

	//  Fields ----------------------------------------------
	[SerializeField]
	private MVDModelData _mvdModelData = null;

	[SerializeField]
	private Interactable LockOrUnlock;

	[SerializeField]
	private Interactable[] interactables;

	private GameObject _characterView;
    private bool togglePin;

    private void Start()
    {
		for (int i = 0; i < interactables.Length; i++)
		{
			var patientButton = interactables[i];

			int index = i;
			patientButton.OnClick.AddListener(() =>
			{
				this.SetCharacterView(this.MVDModelData.CharacterViews[index], false);
			});
		}

		this.SetCharacterView(this.MVDModelData.CharacterViews[0], true);

		this.LockOrUnlock.OnClick.AddListener(this.TogglePin);
	}

    private void TogglePin()
   	{
		this.togglePin = !this.togglePin;
		if (_characterView != null)
		{
			_characterView.GetComponent<BoxCollider>().enabled = this.togglePin;
			_characterView.GetComponent<ObjectManipulator>().enabled = this.togglePin;
			_characterView.GetComponent<BoundsControl>().enabled = this.togglePin;
		}
	}

    private void SetCharacterView(GameObject prefab, bool enforce)
    {
		if (_characterView != null)
		{
			_characterView.transform.parent = null;
			Destroy(_characterView.gameObject);
		}

		_characterView = Instantiate(prefab);
		var bone = _characterView.AddComponent<Bone>();
		bone.UpdatePosition();

		if (!enforce)
		{
			_characterView.GetComponent<BoxCollider>().enabled = this.togglePin;
			_characterView.GetComponent<ObjectManipulator>().enabled = this.togglePin;
			_characterView.GetComponent<BoundsControl>().enabled = this.togglePin;
		}
	}
}
