﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StackExchange.Exceptional.Email
{
    
    #line 2 "..\..\Email\ErrorEmail.cshtml"
    using System;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Email\ErrorEmail.cshtml"
    using System.Collections.Generic;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Email\ErrorEmail.cshtml"
    using System.Collections.Specialized;
    
    #line default
    #line hidden
    
    #line 5 "..\..\Email\ErrorEmail.cshtml"
    using System.Linq;
    
    #line default
    #line hidden
    
    #line 6 "..\..\Email\ErrorEmail.cshtml"
    using System.Text;
    
    #line default
    #line hidden
    
    #line 7 "..\..\Email\ErrorEmail.cshtml"
    using System.Text.RegularExpressions;
    
    #line default
    #line hidden
    
    #line 8 "..\..\Email\ErrorEmail.cshtml"
    using System.Web;
    
    #line default
    #line hidden
    
    #line 9 "..\..\Email\ErrorEmail.cshtml"
    using StackExchange.Exceptional;
    
    #line default
    #line hidden
    
    #line 11 "..\..\Email\ErrorEmail.cshtml"
    using StackExchange.Exceptional.Extensions;
    
    #line default
    #line hidden
    
    #line 10 "..\..\Email\ErrorEmail.cshtml"
    using StackExchange.Exceptional.Pages;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "1.5.0.0")]
    internal partial class ErrorEmail : RazorPageBase
    {
#line hidden

        #line 16 "..\..\Email\ErrorEmail.cshtml"

    public Error error { get; set; }
    private static HashSet<string> hiddenHttpKeys = new HashSet<string>
                                                        {
                                                            "ALL_HTTP",
                                                            "ALL_RAW",
                                                            "HTTP_CONTENT_LENGTH",
                                                            "HTTP_CONTENT_TYPE",
                                                            "HTTP_COOKIE",
                                                            "QUERY_STRING"
                                                        };

    private static HashSet<string> defaultHttpKeys = new HashSet<string>
                                                     {
                                                         "APPL_MD_PATH",
                                                         "APPL_PHYSICAL_PATH",
                                                         "GATEWAY_INTERFACE",
                                                         "HTTP_ACCEPT",
                                                         "HTTP_ACCEPT_CHARSET",
                                                         "HTTP_ACCEPT_ENCODING",
                                                         "HTTP_ACCEPT_LANGUAGE",
                                                         "HTTP_CONNECTION",
                                                         "HTTP_HOST",
                                                         "HTTP_KEEP_ALIVE",
                                                         "HTTPS",
                                                         "INSTANCE_ID",
                                                         "INSTANCE_META_PATH",
                                                         "PATH_INFO",
                                                         "PATH_TRANSLATED",
                                                         "REMOTE_PORT",
                                                         "SCRIPT_NAME",
                                                         "SERVER_NAME",
                                                         "SERVER_PORT",
                                                         "SERVER_PORT_SECURE",
                                                         "SERVER_PROTOCOL",
                                                         "SERVER_SOFTWARE"
                                                     };

    public IHtmlString RenderVariableTable(string title, string className, NameValueCollection vars)
    {
        if (vars == null || vars.Count == 0) return Html("");
        var result = new StringBuilder();

        var fetchError = vars[Error.CollectionErrorKey];
        var errored = fetchError.HasValue();
        var keys = vars.AllKeys.Where(key => !hiddenHttpKeys.Contains(key) && key != Error.CollectionErrorKey).OrderBy(k => k);

        result.AppendFormat("    <div>").AppendLine();
        result.AppendFormat("        <h3 style=\"color: #224C00; font-family: Verdana, Tahoma, Arial, 'Helvetica Neue', Helvetica, sans-serif; font-size: 14px; margin: 10px 0 5px 0;\">{0}{1}</h3>", title, errored ? " - Error while gathering data" : "").AppendLine();
        if(keys.Any())
        {
            result.AppendFormat("        <table style=\"font-family: Verdana, Tahoma, Arial, 'Helvetica Neue', Helvetica, sans-serif; font-size: 12px; width: 100%; border-collapse: collapse; border: 0;\">").AppendLine();
            var i = 0;
            foreach (var k in keys)
            {
                // If this has no value, skip it
                if (vars[k].IsNullOrEmpty() || defaultHttpKeys.Contains(k))
                {
                    continue;
                }
                result.AppendFormat("          <tr{2}><td style=\"padding: 0.4em; width: 200px;\">{0}</td><td style=\"padding: 0.4em;\">{1}</td></tr>", k, Linkify(vars[k]), i % 2 == 0 ? " style=\"background-color: #F2F2F2;\"" : "").AppendLine();
                i++;
            }
            if (vars["HTTP_HOST"].HasValue() && vars["URL"].HasValue())
            {
                var url = string.Format("http://{0}{1}{2}", vars["HTTP_HOST"], vars["URL"], vars["QUERY_STRING"].HasValue() ? "?" + vars["QUERY_STRING"] : "");
                result.AppendFormat("          <tr><td style=\"padding: 0.4em; width: 200px;\">URL and Query</td><td style=\"padding: 0.4em;\">{0}</td></tr>", vars["REQUEST_METHOD"] == "GET" ? Linkify(url).ToString() : Server.HtmlEncode(url)).AppendLine();
            }
            result.AppendFormat("        </table>").AppendLine();
        }
        if(errored)
        {
            result.AppendFormat("        <span style=\"color: maroon;\">Get {0} threw an exception:</span>", title).AppendLine();
            result.AppendFormat("        <pre  style=\"background-color: #EEE; font-family: Consolas, Monaco, monospace; padding: 8px;\">{0}</pre>", Server.HtmlEncode(fetchError)).AppendLine();
        }
        result.AppendFormat("    </div>").AppendLine();
        return Html(result.ToString());
    }


    private IHtmlString Linkify(string s)
    {
        if (Regex.IsMatch(s, @"%[A-Z0-9][A-Z0-9]"))
        {
            s = Server.UrlDecode(s);
        }

        if (Regex.IsMatch(s, "^(https?|ftp|file)://"))
        {
            //@* || (Regex.IsMatch(s, "/[^ /,]+/") && !s.Contains("/LM"))*@ // block special case of "/LM/W3SVC/1"
            var sane = SanitizeUrl(s);
            if (sane == s) // only link if it's not suspicious
                return Html(string.Format(@"<a style=""color: #3D85B0;"" href=""{0}"">{1}</a>", sane, Server.HtmlEncode(s)));
        }

        return Html(Server.HtmlEncode(s));
    }

    private static readonly Regex _sanitizeUrl = new Regex(@"[^-a-z0-9+&@#/%?=~_|!:,.;\(\)\{\}]", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    public static string SanitizeUrl(string url)
    {
        return url.IsNullOrEmpty() ? url : _sanitizeUrl.Replace(url, "");
    }


        #line default
        #line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");













            
            #line 13 "..\..\Email\ErrorEmail.cshtml"
  


            
            #line default
            #line hidden

WriteLiteral("\r\n<div style=\"font-family: Arial, \'Helvetica Neue\', Helvetica, sans-serif;\">\r\n");


            
            #line 122 "..\..\Email\ErrorEmail.cshtml"
 if (error == null)
{

            
            #line default
            #line hidden
WriteLiteral("    <h1 style=\"color: maroon; font-size: 16px;\">Error not found.</h1>\r\n");


            
            #line 125 "..\..\Email\ErrorEmail.cshtml"
}
else
{

            
            #line default
            #line hidden
WriteLiteral("    <h1 style=\"color: maroon; font-size: 16px; padding: 0; margin: 0;\">");


            
            #line 128 "..\..\Email\ErrorEmail.cshtml"
                                                                  Write(error.Message);

            
            #line default
            #line hidden
WriteLiteral("</h1>\r\n");



WriteLiteral("    <div style=\"font-size: 12px; color: #444; padding: 0; margin: 2px 0;\">");


            
            #line 129 "..\..\Email\ErrorEmail.cshtml"
                                                                     Write(error.Type);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");



WriteLiteral("    <pre style=\"background-color: #FFFFCC; font-family: Consolas, Monaco, monospa" +
"ce; font-size: 12px; margin: 2px 0; padding: 12px;\">");


            
            #line 130 "..\..\Email\ErrorEmail.cshtml"
                                                                                                                                Write(error.Detail);

            
            #line default
            #line hidden
WriteLiteral("\r\n    </pre>\r\n");



WriteLiteral("    <p class=\"error-time\" style=\"font-size: 13px; color: #555; margin: 5px 0;\">oc" +
"curred at <b title=\"");


            
            #line 132 "..\..\Email\ErrorEmail.cshtml"
                                                                                                Write(error.CreationDate.ToLongDateString());

            
            #line default
            #line hidden
WriteLiteral(" at ");


            
            #line 132 "..\..\Email\ErrorEmail.cshtml"
                                                                                                                                          Write(error.CreationDate.ToLongTimeString());

            
            #line default
            #line hidden
WriteLiteral("\">");


            
            #line 132 "..\..\Email\ErrorEmail.cshtml"
                                                                                                                                                                                  Write(error.CreationDate.ToUniversalTime());

            
            #line default
            #line hidden
WriteLiteral(" UTC</b> on ");


            
            #line 132 "..\..\Email\ErrorEmail.cshtml"
                                                                                                                                                                                                                               Write(error.MachineName);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n");


            
            #line 133 "..\..\Email\ErrorEmail.cshtml"
    if (!string.IsNullOrEmpty(error.SQL))
    {

            
            #line default
            #line hidden
WriteLiteral("        <h3 style=\"color: #224C00; font-family: Verdana, Tahoma, Arial, \'Helvetic" +
"a Neue\', Helvetica, sans-serif; font-size: 14px; margin: 10px 0 5px 0;\">SQL</h3>" +
"\r\n");



WriteLiteral("        <pre style=\"background-color: #EEE; font-family: Consolas, Monaco, monosp" +
"ace; padding: 8px 8px 8px 8px; margin: 2px 0;\">");


            
            #line 136 "..\..\Email\ErrorEmail.cshtml"
                                                                                                                           Write(error.SQL);

            
            #line default
            #line hidden
WriteLiteral("</pre>\r\n");



WriteLiteral("        <br/>\r\n");


            
            #line 138 "..\..\Email\ErrorEmail.cshtml"
    }
    
            
            #line default
            #line hidden
            
            #line 139 "..\..\Email\ErrorEmail.cshtml"
Write(RenderVariableTable("Server Variables", "server-variables", error.ServerVariables));

            
            #line default
            #line hidden
            
            #line 139 "..\..\Email\ErrorEmail.cshtml"
                                                                                       
    if (error.CustomData != null && error.CustomData.Count > 0)
    {
        var errored = error.CustomData.ContainsKey(ErrorStore.CustomDataErrorKey);
        var cdKeys = error.CustomData.Keys.Where(k => k != ErrorStore.CustomDataErrorKey);

            
            #line default
            #line hidden
WriteLiteral("        <div class=\"custom-data\">\r\n");


            
            #line 145 "..\..\Email\ErrorEmail.cshtml"
             if (errored)
            {

            
            #line default
            #line hidden
WriteLiteral("                <h3 style=\"color: maroon; font-family: Verdana, Tahoma, Arial, \'H" +
"elvetica Neue\', Helvetica, sans-serif; font-size: 14px; margin: 10px 0 5px 0;\">C" +
"ustom - Error while gathering custom data</h3>\r\n");


            
            #line 148 "..\..\Email\ErrorEmail.cshtml"
            } else
            {

            
            #line default
            #line hidden
WriteLiteral("                <h3 style=\"color: #224C00; font-family: Verdana, Tahoma, Arial, \'" +
"Helvetica Neue\', Helvetica, sans-serif; font-size: 14px; margin: 10px 0 5px 0;\">" +
"Custom</h3>\r\n");


            
            #line 151 "..\..\Email\ErrorEmail.cshtml"
            }

            
            #line default
            #line hidden

            
            #line 152 "..\..\Email\ErrorEmail.cshtml"
             if(cdKeys.Any(k => k != ErrorStore.CustomDataErrorKey))
            {
                var i = -1;

            
            #line default
            #line hidden
WriteLiteral("                <table style=\"font-family: Verdana, Tahoma, Arial, \'Helvetica Neu" +
"e\', Helvetica, sans-serif; font-size: 12px; width: 100%; border-collapse: collap" +
"se; border: 0;\">\r\n");


            
            #line 156 "..\..\Email\ErrorEmail.cshtml"
                     foreach (var cd in cdKeys)
                    {
                        i++;

            
            #line default
            #line hidden
WriteLiteral("                        <tr");


            
            #line 159 "..\..\Email\ErrorEmail.cshtml"
                      Write(Html(i % 2 == 0 ? " style=\"background-color: #F2F2F2;\"" : ""));

            
            #line default
            #line hidden
WriteLiteral(">\r\n                            <td style=\"padding: 0.4em; width: 200px;\">");


            
            #line 160 "..\..\Email\ErrorEmail.cshtml"
                                                                 Write(cd);

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                            <td style=\"padding: 0.4em;\">");


            
            #line 161 "..\..\Email\ErrorEmail.cshtml"
                                                   Write(Linkify(error.CustomData[cd]));

            
            #line default
            #line hidden
WriteLiteral("</td>\r\n                        </tr>\r\n");


            
            #line 163 "..\..\Email\ErrorEmail.cshtml"
                    }

            
            #line default
            #line hidden
WriteLiteral("                </table>\r\n");


            
            #line 165 "..\..\Email\ErrorEmail.cshtml"
            }

            
            #line default
            #line hidden

            
            #line 166 "..\..\Email\ErrorEmail.cshtml"
             if(errored)
            {

            
            #line default
            #line hidden
WriteLiteral("                <span style=\"color: maroon;\">GetCustomData threw an exception:</s" +
"pan>\r\n");



WriteLiteral("                <pre style=\"background-color: #EEE; font-family: Consolas, Monaco" +
", monospace; padding: 8px;\">");


            
            #line 169 "..\..\Email\ErrorEmail.cshtml"
                                                                                                        Write(error.CustomData[ErrorStore.CustomDataErrorKey]);

            
            #line default
            #line hidden
WriteLiteral("</pre>\r\n");


            
            #line 170 "..\..\Email\ErrorEmail.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </div>\r\n");


            
            #line 172 "..\..\Email\ErrorEmail.cshtml"
    }
    
            
            #line default
            #line hidden
            
            #line 173 "..\..\Email\ErrorEmail.cshtml"
Write(RenderVariableTable("QueryString", "querystring", error.QueryString));

            
            #line default
            #line hidden
            
            #line 173 "..\..\Email\ErrorEmail.cshtml"
                                                                         
    
            
            #line default
            #line hidden
            
            #line 174 "..\..\Email\ErrorEmail.cshtml"
Write(RenderVariableTable("Form", "form", error.Form));

            
            #line default
            #line hidden
            
            #line 174 "..\..\Email\ErrorEmail.cshtml"
                                                    
    
            
            #line default
            #line hidden
            
            #line 175 "..\..\Email\ErrorEmail.cshtml"
Write(RenderVariableTable("Cookies", "cookies", error.Cookies));

            
            #line default
            #line hidden
            
            #line 175 "..\..\Email\ErrorEmail.cshtml"
                                                             
}

            
            #line default
            #line hidden
WriteLiteral("</div>");


        }
    }
}
#pragma warning restore 1591
