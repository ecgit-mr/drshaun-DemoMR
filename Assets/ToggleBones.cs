using Microsoft.MixedReality.Toolkit;
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
	private Interactable Reset;

	[SerializeField]
	private Interactable[] interactables;

	private GameObject _characterView;
    private bool togglePin;
    private Vector3 currentPos = Vector3.zero;
    private Vector3 currentScale = Vector3.one;
	private Quaternion currentRotation = Quaternion.identity;

	private void Start()
    {
		// Disable gaze
		CoreServices.InputSystem.GazeProvider.Enabled = false;

		var inputSystemProfile = CoreServices.InputSystem.InputSystemProfile;
		if (inputSystemProfile == null)
		{
			return;
		}

		// Disabled hand mesh
		var handTrackingProfile = inputSystemProfile.HandTrackingProfile;
		if (handTrackingProfile != null)
		{
			handTrackingProfile.EnableHandMeshVisualization = false;
			handTrackingProfile.EnableHandJointVisualization = false;
		}

		this.togglePin = true;
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
		this.Reset.OnClick.AddListener(this.ResetScale);
	}

	private void ResetScale()
    {
		_characterView.transform.localScale = new Vector3(1, 1, 1);
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

    private void SetCharacterView(GameObject prefab, bool first)
    {
		if (_characterView != null)
		{
			this.currentPos = _characterView.transform.position;
			this.currentScale = _characterView.transform.localScale;
			this.currentRotation = _characterView.transform.localRotation;
			_characterView.transform.parent = null;
			Destroy(_characterView.gameObject);
		}

		_characterView = Instantiate(prefab);
		var bone = _characterView.GetComponent<Bone>();
		if (first)
		{
			this.currentPos = bone.UpdatePosition();
		}
		else
		{
			bone.transform.position = this.currentPos;
			bone.transform.localScale = this.currentScale;
			bone.transform.localRotation = this.currentRotation;
		}

		var boundsControl = _characterView.GetComponent<BoundsControl>();
		var boundsManipulator = _characterView.GetComponent<ObjectManipulator>();
		_characterView.GetComponent<BoxCollider>().enabled = this.togglePin;
		boundsManipulator.enabled = this.togglePin;
		boundsControl.enabled = this.togglePin;
		boundsManipulator.ReleaseBehavior = ObjectManipulator.ReleaseBehaviorType.KeepVelocity;
		boundsManipulator.SmoothingNear = true;
		boundsManipulator.SmoothingFar = true;
		boundsControl.SmoothingActive = true;

		var transparentScript = this.GetComponent<MakeObjectTransparent>();
		transparentScript.currentGameObject = bone.MainObject;
		transparentScript.SetCurrentAlpha();
	}
}
