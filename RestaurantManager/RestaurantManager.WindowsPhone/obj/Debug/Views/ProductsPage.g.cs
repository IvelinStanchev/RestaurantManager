﻿

#pragma checksum "C:\Users\Ivelin\Desktop\WindowsPhoneProject\RestaurantManager\RestaurantManager\RestaurantManager.WindowsPhone\Views\ProductsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B878A955FE3987A4CB99E4C449309138"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RestaurantManager.Views
{
    partial class ProductsPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 30 "..\..\Views\ProductsPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Holding += this.ListView_Holding;
                 #line default
                 #line hidden
                #line 31 "..\..\Views\ProductsPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).DoubleTapped += this.ListView_DoubleTapped;
                 #line default
                 #line hidden
                #line 32 "..\..\Views\ProductsPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.ListView_ItemClick;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


