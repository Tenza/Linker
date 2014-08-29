/*
 * Linker
 * Copyright (C) 2011 Filipe Carvalho
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	User\Thumbnail.ashx.cs
//
// summary:	Implements the thumbnail.ashx class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;

namespace Linker.User
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     This code is incomplete, and it is vulnerable to DDOS attacks. The attacker could easily
    ///     overload the server by continually pulling images or videos.
    /// </summary>
    ///
    /// <remarks>   Filipe, 10 Nov 2011. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public class Thumbnail : IHttpHandler
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Enables processing of HTTP Web requests by a custom HttpHandler that implements the
        ///     <see cref="T:System.Web.IHttpHandler" /> interface.
        /// </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="context">  The context. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void ProcessRequest(HttpContext context)
        {
            int height = 100;
            int width = 100;
            string image_URL = context.Request.QueryString["image"];

            if (!string.IsNullOrEmpty(context.Request.QueryString["image"]))
            {
                try
                {
                    byte[] image_data = download_image(image_URL);
                    MemoryStream stream = new MemoryStream(image_data);
                    System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                    stream.Close();

                    System.Drawing.Image thumbnail_image = image.GetThumbnailImage(width, height, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);

                    MemoryStream image_stream = new MemoryStream();
                    thumbnail_image.Save(image_stream, System.Drawing.Imaging.ImageFormat.Png);

                    byte[] image_content = new Byte[image_stream.Length];
                    image_stream.Position = 0;

                    image_stream.Read(image_content, 0, (int)image_stream.Length);

                    context.Response.ContentType = "image/png";
                    context.Response.BinaryWrite(image_content);
                }
                catch (Exception)
                {

                }
            }
            else
            {
                context.Response.Redirect("/All/Home.aspx");
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Downloads the image described by URL. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="url">  URL of the document. </param>
        ///
        /// <returns>   A byte[]. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private byte[] download_image(string url)
        {
            byte[] downloaded_data = new byte[0];
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();

                byte[] buffer = new byte[1024];

                MemoryStream memory_stream = new MemoryStream();
                while (true)
                {
                    int read_bytes = stream.Read(buffer, 0, buffer.Length);

                    if (read_bytes == 0)
                    {
                        break;
                    }
                    else
                    {
                        memory_stream.Write(buffer, 0, read_bytes);
                    }
                }
                downloaded_data = memory_stream.ToArray();

                stream.Close();
                memory_stream.Close();

                return downloaded_data;
            }
            catch (Exception)
            {
                //May not be connected to the internet
                //Or the URL might not exist
            }

            return null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Callback, called when the thumbnail. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool ThumbnailCallback()
        {
            return true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Gets a value indicating whether another request can use the
        ///     <see cref="T:System.Web.IHttpHandler" /> instance.
        /// </summary>
        ///
        /// <value>
        ///     true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise,
        ///     false.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}