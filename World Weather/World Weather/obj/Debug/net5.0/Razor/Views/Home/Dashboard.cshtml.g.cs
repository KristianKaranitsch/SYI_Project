#pragma checksum "C:\Users\Kristian\Documents\MWI\SystemIntegration\SYI_Project\World Weather\World Weather\Views\Home\Dashboard.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "15eaa20c4d84154a004c0199accd94126f542734"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Dashboard), @"mvc.1.0.view", @"/Views/Home/Dashboard.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Kristian\Documents\MWI\SystemIntegration\SYI_Project\World Weather\World Weather\Views\_ViewImports.cshtml"
using World_Weather;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Kristian\Documents\MWI\SystemIntegration\SYI_Project\World Weather\World Weather\Views\_ViewImports.cshtml"
using World_Weather.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"15eaa20c4d84154a004c0199accd94126f542734", @"/Views/Home/Dashboard.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5cc0c14385d3a664986c621acf86dc118bb19ec3", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Dashboard : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<DashboardModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\Kristian\Documents\MWI\SystemIntegration\SYI_Project\World Weather\World Weather\Views\Home\Dashboard.cshtml"
  
    ViewData["Title"] = "Dashboard";
    Layout = "_Layout";

#line default
#line hidden
#nullable disable
            WriteLiteral("<h1>Dashboard</h1>\r\n\r\n<div class=\"Dashboard\">\r\n    <div class=\"tile\">\r\n        <h2>Wien</h2>\r\n        <p>Temperatur: ");
#nullable restore
#line 12 "C:\Users\Kristian\Documents\MWI\SystemIntegration\SYI_Project\World Weather\World Weather\Views\Home\Dashboard.cshtml"
                  Write(Model.Wien);

#line default
#line hidden
#nullable disable
            WriteLiteral(" &deg;C</p>\r\n    </div>\r\n    <div class=\"tile\">\r\n        <h2>New York</h2>\r\n        <p>Temperatur: ");
#nullable restore
#line 16 "C:\Users\Kristian\Documents\MWI\SystemIntegration\SYI_Project\World Weather\World Weather\Views\Home\Dashboard.cshtml"
                  Write(Model.NewYork);

#line default
#line hidden
#nullable disable
            WriteLiteral(" &deg;C</p>\r\n    </div>\r\n    <div class=\"tile\">\r\n        <h2>Sydney</h2>\r\n        <p>Temperatur: ");
#nullable restore
#line 20 "C:\Users\Kristian\Documents\MWI\SystemIntegration\SYI_Project\World Weather\World Weather\Views\Home\Dashboard.cshtml"
                  Write(Model.Sydney);

#line default
#line hidden
#nullable disable
            WriteLiteral(" &deg;C</p>\r\n    </div>\r\n    <div class=\"tile\">\r\n        <h2>Tokio</h2>\r\n        <p>Temperatur: ");
#nullable restore
#line 24 "C:\Users\Kristian\Documents\MWI\SystemIntegration\SYI_Project\World Weather\World Weather\Views\Home\Dashboard.cshtml"
                  Write(Model.Tokio);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" &deg;C</p>
    </div>
</div>
<style>
    .dashboard {
        display: flex;
        flex-wrap: wrap;
    }

    .tile {
        flex: 1 0 50%; /* Jede Kachel soll 50% der Breite einnehmen, damit zwei Kacheln in einer Reihe angezeigt werden */
        padding: 20px;
        box-sizing: border-box;
    }
</style>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<DashboardModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
