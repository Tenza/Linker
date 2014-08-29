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
// file:	User\Videos.aspx.cs
//
// summary:	Implements the videos.aspx class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace Linker.User
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     This page presents the videos of the user or the user in the URL. This is the main page
    ///     to share videos.
    /// </summary>
    ///
    /// <remarks>   Filipe, 10 Nov 2011. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class Videos : System.Web.UI.Page
    {
        /// <summary>   The qs user. </summary>
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

        #region Video Load
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Loads the videos and respects permissions. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <returns>   The videos. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public string load_videos()
        {
            string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connection_string);
            string videos = @"<tr>";

            try
            {
                string query = "SELECT link, id FROM Videos WHERE username=@username AND permission=@permission";
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
                    videos +=
                        @"<td style=""padding:10px;"">
                            <iframe width=""200"" height=""165"" src=""https://www.youtube-nocookie.com/embed/" + reader["link"].ToString() + @"?rel=0&amp;hd=1"" frameborder=""0"" allowfullscreen></iframe><br />
                            <center><input type='button' onClick=""location.href='Comments.aspx?ID=" + reader["id"].ToString() + @"&section=videos'"" value='Comment' style=""width:100px; height:20px;""></center>
                        </td>";
                    if (x == 4)
                    {
                        videos += @"</tr><tr>";
                        x = 0;
                    }
                }
                if (y == 0)
                {
                    videos = @"<tr><td><p>There are no videos to be displayed :(</p></td>";
                }

                reader.Close();
                connection.Close();
            }
            catch
            {
                videos = @"<tr><td><p>There are no videos to be displayed :(</p></td>";
                connection.Close();
            }

            return ("<table>" + videos + "</tr></table>");
        }
        #endregion

    }
}