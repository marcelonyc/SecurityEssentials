﻿using System;
using System.Web;

namespace SecurityEssentials.Core
{
	/// <summary>
	/// Http handler that ensures the site can never be loaded in an iFrame and any request to the site is made over SSL and removes server identity header
	/// </summary>
	public class HttpHeaders : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			context.PreSendRequestHeaders += Context_PreSendRequestHeaders;
		}

		private void Context_PreSendRequestHeaders(object sender, EventArgs e)
		{
			// SECURE: Prevent site being viewed in an iFrame
			HttpContext.Current.Response.Headers.Add("X-Frame-Options", "Deny");
			// SECURE: Can effectively prevent information being inadvertently leaked to other websites
			HttpContext.Current.Response.Headers.Add("Referrer-Policy", "origin");
            // SECURE Turn off features that can be used in the browser by default
            HttpContext.Current.Response.Headers.Add("Permissions-Policy",
                "geolocation=(), midi=(), camera=(),usb=(), magnetometer=(), sync-xhr=(), microphone=(), camera=(), gyroscope=(), speaker=(), payment=()");
#if DEBUG
               // SECURE: Enable Content security policy with reporting
               HttpContext.Current.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; style-src 'self' 'unsafe-inline' https://unpkg.com/gridjs/; img-src * data:; font-src 'self' https: data:; script-src 'self' https://unpkg.com/gridjs-jquery/; connect-src 'self'; frame-ancestors 'self'; form-action 'self'; base-uri 'self'; object-src 'none'; report-uri /Security/CspReporting");
#else
			HttpContext.Current.Response.Headers.Add("Content-Security-Policy", "default-src https:; style-src https: 'unsafe-inline' https://unpkg.com/gridjs/; img-src https: data:; font-src https: data:; script-src https: https://unpkg.com/gridjs-jquery/; connect-src https:; frame-ancestors https:; form-action https:; base-uri https:; object-src 'none'; report-uri /Security/CspReporting");
#endif
            // SECURE: Enable Expect-CT header with reporting
            HttpContext.Current.Response.Headers.Add("Expect-CT", "max-age=0, report-uri /Security/CtReporting");
            HttpContext.Current.Response.Headers.Add("X-Content-Type-Options", "nosniff"); // SECURE: Prevent site being displayed in a different format in older browsers c.f. https://www.owasp.org/index.php/List_of_useful_HTTP_headers
			HttpContext.Current.Response.Headers.Add("X-XSS-Protection", "1; mode=block; report=/Security/CspReporting"); // SECURE: Enable browsers anti-XSS protection and report any violations  https://www.owasp.org/index.php/XSS_(Cross_Site_Scripting)_Prevention_Cheat_Sheet
			// SECURE: Public key pinning to be deprecated, do not use: http://www.zdnet.com/article/google-chrome-is-backing-away-from-public-key-pinning-and-heres-why/
			HttpContext.Current.Response.Headers.Add("Public-Key-Pins", "max-age=0; includeSubDomains"); // Remove public key pinning 
#if DEBUG
			HttpContext.Current.Response.Headers.Add("Strict-Transport-Security", "max-age=0; includeSubDomains"); // Remove HSTS header for debug
#else
// SECURE: Enforce any further requests after initial request to be made over TLS, register for HSTS pre-load
			HttpContext.Current.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");
#endif
			HttpContext.Current.Response.Headers.Remove("Server"); // SECURE: Remove server information disclosure
		}

		public void Dispose()
		{
		}
	}
}
