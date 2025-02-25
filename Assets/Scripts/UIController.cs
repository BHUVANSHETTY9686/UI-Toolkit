using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class UIController : MonoBehaviour
{
    private VisualElement _bottomContainer;
    private Button _openButton;
    private Button _closeButton;
    private VisualElement _bottomSheet;
    private VisualElement _scrim;
    private VisualElement _boy;
    private VisualElement _girl;
    private Label _message;
    



    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _bottomContainer = root.Q<VisualElement>(UIConstants.CONTAINER_BOTTOM);
        _openButton = root.Q<Button>(UIConstants.BUTTON_OPEN);
        _closeButton = root.Q<Button>(UIConstants.BUTTON_CLOSE);
        _bottomContainer.style.display = DisplayStyle.None;
        _openButton.RegisterCallback<ClickEvent>(OnOpenButtonClick);
        _closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClick);
        _bottomSheet = root.Q<VisualElement>(UIConstants.BOTTOM_SHEET);
        _scrim = root.Q<VisualElement>(UIConstants.SCRIM);
        _boy = root.Q<VisualElement>(UIConstants.BOY);
        _girl = root.Q<VisualElement>(UIConstants.IMAGE_GIRL);
        _message = root.Q<Label>(UIConstants.MESSAGE);
        StartCoroutine(nameof(AnimateBoy));
    }

    private IEnumerator AnimateBoy()
    {
        yield return new WaitForEndOfFrame();
        _boy.RemoveFromClassList(UIConstants.IMAGE_BOY_INAIR);
    }
    private void OnOpenButtonClick(ClickEvent evt)
    {
        AnimateGirl();
        _bottomContainer.style.display = DisplayStyle.Flex;
        _bottomSheet.AddToClassList(UIConstants.BUTTOMSHEET__UP);
        _scrim.AddToClassList(UIConstants.SCRIM__FADEIN);

    }

    private void AnimateGirl()
    {
        _girl.ToggleInClassList(UIConstants.IMAGE_GIRL_UP);
        _girl.RegisterCallback<TransitionEndEvent>((evt) => {
            Debug.Log("Transition Ended!");
            
                _girl.ToggleInClassList(UIConstants.IMAGE_GIRL_UP);
           
        });
        _message.text = string.Empty;
        string fullText = "hello hello hello hello hello hello hello";

        _message.experimental.animation.Start(0f, 1f, 3000, (element, value) =>
        {
            int length = Mathf.FloorToInt(value * fullText.Length);
            _message.text = fullText.Substring(0, length);
        });




    }
    

    private void OnCloseButtonClick(ClickEvent evt)
    {
        Debug.Log("Open Called");
        _bottomContainer.style.display = DisplayStyle.None;
        _bottomSheet.RemoveFromClassList(UIConstants.BUTTOMSHEET__UP);
        _scrim.RemoveFromClassList(UIConstants.SCRIM__FADEIN);
    }

    
}
