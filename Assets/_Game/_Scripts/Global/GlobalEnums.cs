using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Naren_Dev
{
    public enum AudioStatus
    {
        MusicStatus = 0,
        SFXStatus = 1
    }
    public enum AudioId
    {
        GamePlayBGM = 0,
        JumpSFX = 1,
        CollectableSFX = 2,
        JumpPadSFX = 3,
        BubblePopSFX = 4,
        MerchantTransitionSFX = 5,
        //UI
        UIButtonClickSFX = 6,
        UIButtonHoverSFX = 7,
    }

    public enum ControlScheme
    {
        Touch,
        JoyStick,
        Keyboard

    }

    public enum ColorID
    {
        Color_One,
        Color_Two,
        Color_Three
    }

    public enum GameMode
    {
        SinglePlayer,
        MultiPlayer
    }

    public enum GameState
    {
        Started,
        Completed,
        GamePlay,
        CutScene
    }
    public enum PlayerState
    {
        Alive,
        Dead,
    }

    public enum PlayerPrefKeys
    {
        MusicStatus,
        SFXStatus,
        MainMenuSessionCounter,
        SessionNumber

    }
}

