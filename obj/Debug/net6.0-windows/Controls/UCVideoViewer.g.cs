#pragma checksum "..\..\..\..\Controls\UCVideoViewer.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "415E57A68C6E3595A07B001E9573F69F7CF1FAFA"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using GHVideoApp;
using GHVideoApp.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace GHVideoApp.Controls {
    
    
    /// <summary>
    /// UCVideoViewer
    /// </summary>
    public partial class UCVideoViewer : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 97 "..\..\..\..\Controls\UCVideoViewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listActors;
        
        #line default
        #line hidden
        
        
        #line 114 "..\..\..\..\Controls\UCVideoViewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listTags;
        
        #line default
        #line hidden
        
        
        #line 132 "..\..\..\..\Controls\UCVideoViewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteVideo;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\..\..\Controls\UCVideoViewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddActor;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\..\..\Controls\UCVideoViewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddTag;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\..\Controls\UCVideoViewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRefresh;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/GHVideoApp;component/controls/ucvideoviewer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Controls\UCVideoViewer.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "6.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\..\Controls\UCVideoViewer.xaml"
            ((GHVideoApp.Controls.UCVideoViewer)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.listActors = ((System.Windows.Controls.ListView)(target));
            
            #line 98 "..\..\..\..\Controls\UCVideoViewer.xaml"
            this.listActors.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.listActors_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.listTags = ((System.Windows.Controls.ListView)(target));
            
            #line 115 "..\..\..\..\Controls\UCVideoViewer.xaml"
            this.listTags.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.listTags_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnDeleteVideo = ((System.Windows.Controls.Button)(target));
            
            #line 135 "..\..\..\..\Controls\UCVideoViewer.xaml"
            this.btnDeleteVideo.Click += new System.Windows.RoutedEventHandler(this.btnDeleteVideo_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnAddActor = ((System.Windows.Controls.Button)(target));
            return;
            case 6:
            this.btnAddTag = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.btnRefresh = ((System.Windows.Controls.Button)(target));
            
            #line 147 "..\..\..\..\Controls\UCVideoViewer.xaml"
            this.btnRefresh.Click += new System.Windows.RoutedEventHandler(this.btnRefresh_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

