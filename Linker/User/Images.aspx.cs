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
// file:	User\Images.aspx.cs
//
// summary:	Implements the images.aspx class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace Linker.User
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     This page presents the images of the user or the user in the URL. This is the main page
    ///     to share images.
    /// </summary>
    ///
    /// <remarks>   Filipe, 10 Nov 2011. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class Images : System.Web.UI.Page
    {
        public string qs_user;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Load the user, or other user content? </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["user"]))
            {
                qs_user = Request.QueryString["user"];
            }
        }

        #region Image Load
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Loads the images, apply the highslide and respect permissions. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <returns>   The images. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public string load_images()
        {
            string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connection_string);
            string images = @"<tr>";

            try
            {
                string query = "SELECT link, id FROM Images WHERE username=@username AND permission=@permission";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add(new SqlParameter("@permission", "Public"));

                if (qs_user == null)
                {
                    command.Parameters.Add(new SqlParameter("@username", User.Identity.Name));
                }else{
                    command.Parameters.Add(new SqlParameter("@username", qs_user));
                }            

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                int x = 0, y = 0;
                while (reader.Read())
                {
                    x++;
                    y++;
                    images +=
                        @"<td style=""padding:10px;"">
                            <a href=" + reader["link"].ToString() + @" class=""highslide"" onclick=""return hs.expand(this)""><img src=" + reader["link"].ToString() + @" alt="""" width=""100px"" height=""100px""/></a><br />
                            <center><input type='button' onClick=""location.href='Comments.aspx?ID=" + reader["id"].ToString() + @"&section=images'"" value='Comment' style=""width:100px; height:20px;""></center>
                        </td>";
                    if (x == 7)
                    {
                        images += @"</tr><tr>";
                        x = 0;
                    }
                }
                if (y == 0)
                {
                    images = @"<tr><td><p>There are no images to be displayed :(</p></td>";
                }

                reader.Close();
                connection.Close();
            }
            catch
            {
                images = @"<tr><td><p>There are no images to be displayed :(</p></td>";
                connection.Close();
            }

            return ("<table>" + images + "</tr></table>");
        }
        #endregion

    }
}