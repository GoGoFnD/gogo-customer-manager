using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GogoRCustomerManager
{
    public partial class RControl : Form
    {
        public RControl()
        {
            InitializeComponent();
            InitializeWebView2();
        }
        private async void InitializeWebView2()
        {
            await webView2.EnsureCoreWebView2Async();

            webView2.CoreWebView2.PermissionRequested += (sender, args) =>
            {
                if (args.PermissionKind == CoreWebView2PermissionKind.Geolocation)
                {
                    args.State = CoreWebView2PermissionState.Allow;
                }
                else
                {
                    args.State = CoreWebView2PermissionState.Deny;
                }
            };

            // 웹 페이지 로드
            webView2.Source = new Uri("https://gb.navers.co.kr:3150/rider");
            //webView2.CoreWebView2.Navigate("https://map.kakao.com/");
        }

    }
}
