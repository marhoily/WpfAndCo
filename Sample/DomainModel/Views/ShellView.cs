﻿using System;
using System.Windows;
using Newtonsoft.Json;
using Configurator.Properties;

namespace Configurator
{
    public partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
        }

        private void RestorePosition(object sender, EventArgs e)
        {
            WindowStartupLocation = WindowStartupLocation.Manual;
            var jp = Settings.Default.WindowPosition;
            if (string.IsNullOrWhiteSpace(jp)) return;
            var p = JsonConvert.DeserializeObject<WindowPosition>(jp);
            WindowState = p.WindowState;
            Top = p.Top;
            Left = p.Left;
            Width = p.Width;
            Height = p.Height;
        }

        private void SavePosition(object sender, EventArgs e)
        {
            Settings.Default.WindowPosition = JsonConvert.SerializeObject(
                new WindowPosition
                {
                    WindowState = WindowState,
                    Top = Top,
                    Left = Left,
                    Width = Width,
                    Height = Height,
                    RestoreBounds = RestoreBounds
                });
            Settings.Default.Save();
        }

        private sealed class WindowPosition
        {
            public WindowState WindowState { get; set; }
            public double Top { get; set; }
            public double Left { get; set; }
            public double Width { get; set; }
            public double Height { get; set; }
            public Rect RestoreBounds { get; set; }
        }
    }
}