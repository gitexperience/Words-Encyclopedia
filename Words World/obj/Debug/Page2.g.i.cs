﻿#pragma checksum "C:\Users\Anubhav\Documents\Visual Studio 2010\Projects\dictionary\Words World\Page2.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A72B9F59962F16EA57389B708E270E5B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Words_World {
    
    
    public partial class Page2 : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Media.TranslateTransform translateletter;
        
        internal System.Windows.Media.RotateTransform rotateletter;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Shapes.Rectangle OuterEdge;
        
        internal System.Windows.Controls.TextBlock Letter;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Words%20World;component/Page2.xaml", System.UriKind.Relative));
            this.translateletter = ((System.Windows.Media.TranslateTransform)(this.FindName("translateletter")));
            this.rotateletter = ((System.Windows.Media.RotateTransform)(this.FindName("rotateletter")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.OuterEdge = ((System.Windows.Shapes.Rectangle)(this.FindName("OuterEdge")));
            this.Letter = ((System.Windows.Controls.TextBlock)(this.FindName("Letter")));
        }
    }
}

