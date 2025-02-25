using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PanelManager : MonoBehaviour
{
    
    [SerializeField]  private UIDocument _menuScreen;
    [SerializeField]  private UIDocument _settingsScreen;
    [SerializeField] private UIDocument _gameScreen;
    private VisualElement _menuScreenRoot;
    private VisualElement _settingsScreenRoot;
    private VisualElement _gameScreenRoot;
    private Button _startButton;
    private Button _settingsButton;
    private Button _backToMenuButton;
    private Button _backButton;
    private Button _exitButton;
    private Button _shotButton;
    private Button _reloadButton;
    private Label _ammoLabel;
    private int _maxAmmo = 10;
    private int _currentAmmo = 10;

    // Start is called before the first frame update
    void OnEnable()
    {
       InitilizeElements();
    }

    private void InitilizeElements()
    {
        InitMenuScreenElements();
        InitSettingsScreenElements();
        InitGameScreenElements();
    }

    private void InitGameScreenElements()
    {
        _gameScreenRoot = _gameScreen.rootVisualElement;
        _ammoLabel = _gameScreenRoot.Q<Label>("ammo-count");
        _backToMenuButton= _gameScreenRoot.Q<Button>("back-to-menu-button");
        _shotButton = _gameScreenRoot.Q<Button>("shot-button");
        _reloadButton = _gameScreenRoot.Q<Button>("reload-button");
        
        RegisterGameScreenElements();
    }

    private void RegisterGameScreenElements()
    {
        _backToMenuButton.RegisterCallback<ClickEvent>(OnBackButtonClick);
        _shotButton.RegisterCallback<ClickEvent>(OnShotButtonClick);
        _reloadButton.RegisterCallback<ClickEvent>(OnReloadButtonClick);
    }

    private void OnReloadButtonClick(ClickEvent evt)
    {
        _currentAmmo = _maxAmmo;
        _ammoLabel.text = $"Ammo: {_currentAmmo}/{_maxAmmo}";

    }

    private void OnShotButtonClick(ClickEvent evt)
    {
        if(_currentAmmo <= 0)
            ResetGame();
        else
            _currentAmmo--;
       
        _ammoLabel.text = $"Ammo: {_currentAmmo}/{_maxAmmo}";
    }

    private void InitMenuScreenElements()
    {
        _menuScreenRoot = _menuScreen.rootVisualElement;
        _startButton = _menuScreenRoot.Q<Button>("start-button");
        _settingsButton = _menuScreenRoot.Q<Button>("setting-button");
        _exitButton = _menuScreenRoot.Q<Button>("exit-button");
        
        RegisterMenuScreenElements();
        
    }
    
    private void InitSettingsScreenElements()
    {
        _settingsScreenRoot = _settingsScreen.rootVisualElement;
        _backButton = _settingsScreenRoot.Q<Button>("back-to-menu-button");
        RegisterSettingsScreenElements();
    }

    private void RegisterMenuScreenElements()
    {
        _startButton.RegisterCallback<ClickEvent>(OnStartButtonClick);
        _startButton.RegisterCallback<MouseEnterEvent>(OnStartButtonHover);
        _settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClick);
        _exitButton.RegisterCallback<ClickEvent>(OnExitClick);
    }

    private void OnExitClick(ClickEvent evt)
    {
        Application.Quit();
    }

    private void RegisterSettingsScreenElements()
    {
        _backButton.RegisterCallback<ClickEvent>(OnBackButtonClick);
    }


    private void Start()
    {
        EnableMenuScreen();
    }
    
    private void OnBackButtonClick(ClickEvent evt)
    {
        EnableMenuScreen();
    }

    private void OnSettingsButtonClick(ClickEvent evt)
    {
       EnableSettingsScreen();
    }

   
    private void OnStartButtonHover(MouseEnterEvent evt)
    {
        Debug.Log($"Start Button Hover");
    }

    private void OnDisable()
    {
        _startButton.UnregisterCallback<ClickEvent>(OnStartButtonClick);
    }

    private void ResetGame()
    {
        _currentAmmo = _maxAmmo;
        _ammoLabel.text = $"Ammo: {_currentAmmo}/{_maxAmmo}";
    }

    private void OnStartButtonClick(ClickEvent evt)
    {
        EnableGameScreen();
    }
    
    private void EnableMenuScreen()
    {
        _menuScreenRoot.style.display = DisplayStyle.Flex;
        _settingsScreenRoot.style.display = DisplayStyle.None;
        _gameScreenRoot.style.display = DisplayStyle.None;
    }
    private void EnableSettingsScreen()
    {
        Debug.Log($"Display status of setting screen { _settingsScreenRoot.style.display}");
        _menuScreenRoot.style.display = DisplayStyle.None;
        _settingsScreenRoot.style.display = DisplayStyle.Flex;
        _gameScreenRoot.style.display = DisplayStyle.None;
    }
    
    private void EnableGameScreen()
    {
        ResetGame();
        _gameScreenRoot.style.display = DisplayStyle.Flex;
        _menuScreenRoot.style.display = DisplayStyle.None;
        _settingsScreenRoot.style.display = DisplayStyle.None;
    }
}
