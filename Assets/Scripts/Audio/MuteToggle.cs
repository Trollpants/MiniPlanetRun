﻿// <copyright file="MuteToggle.cs" company="Jan Ivar Z. Carlsen">
// Copyright (c) 2018 Jan Ivar Z. Carlsen. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace MiniPlanetRun.Audio
{
    using System;
    using Data;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Toggle))]
    public class MuteToggle : MonoBehaviour
    {
        [SerializeField] private AudioType audioType;

        private Toggle toggle;

        private enum AudioType
        {
            Music,
            Sfx
        }

        private void Awake()
        {
            toggle = GetComponent<Toggle>();
        }

        private void Start()
        {
            toggle.isOn = audioType == AudioType.Music
                ? PlayerSettings.MuteMusic
                : PlayerSettings.MuteSfx;

            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnToggleValueChanged(bool isOn)
        {
            switch (audioType)
            {
                case AudioType.Music:
                    AudioClipPlayer.MuteMusic(isOn);
                    break;
                case AudioType.Sfx:
                    AudioClipPlayer.MuteSfx(isOn);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            AudioClipPlayer.PlayButton();
            PlayerSettings.Save();
        }
    }
}
