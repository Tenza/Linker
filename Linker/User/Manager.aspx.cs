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
// file:	User\Manager.aspx.cs
//
// summary:	Implements the manager.aspx class
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
    ///     This page is where the user will upload their links. It is also possible to view all the
    ///     content here, private and public.
    /// </summary>
    ///
    /// <remarks>   Filipe, 10 Nov 2011. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class Manager : System.Web.UI.Page
    {

        #region Dropdown Select
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Restore page on postback. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                txt_option.Value = (ViewState["opt"] != null) ? (string)ViewState["opt"] : string.Empty;
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    int test_id;
                    if (Int32.TryParse(Request.QueryString["id"], out test_id))
                    {
                        if (test_id == 1){
                            txt_option.Value = "Images";
                        }else if(test_id == 2){
                            txt_option.Value = "Videos";
                        }
                    }        
                }
            }
        }
                
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Load the viewstate for the section of the page.
        /// </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_section.Text == "Choose a section:")
            {
                txt_option.Value = "";
            }else if (cb_section.Text == "Images")
            {
                txt_option.Value = "Images";
            }
            else if (cb_section.Text == "Videos")
            {
                txt_option.Value = "Videos";
            }

            ViewState["opt"] = txt_option.Value;
            message.Text = "";
        }
        #endregion


        #region Add image
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Uploads a image. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void btn_send_images_click(object sender, EventArgs e)
        {
            string[] allowed_img = {".png", ".jpg", ".bmp", "gif", "tif"};
            int f = 0;

            for(int i=0;i<allowed_img.Length; i++)
            {
                f = txt_link_images.Text.LastIndexOf(allowed_img[i]);

                if (f != -1)
                {
                    f = i;
                    break;
                }
            }

            if (f == -1)
            {
                message.ForeColor = System.Drawing.Color.Red;
                message.Font.Size = FontUnit.Large;
                message.Text = "Image format not recognized.";
            }
            else
            {
                string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection connection = new SqlConnection(connection_string);

                string query = "INSERT INTO Images (username, link, description, permission, date, visits) Values(@username, @link, @description, @permission, @date, @visits)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add(new SqlParameter("@username", (string)User.Identity.Name));
                command.Parameters.Add(new SqlParameter("@link", txt_link_images.Text));
                command.Parameters.Add(new SqlParameter("@description", txt_description_images.Text));
                command.Parameters.Add(new SqlParameter("@permission", cb_permission_images.Text));
                command.Parameters.Add(new SqlParameter("@date", DateTime.Now));
                command.Parameters.Add(new SqlParameter("@visits", "0"));

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                txt_link_images.Text = "";
                txt_description_images.Text = "";

                message.ForeColor = System.Drawing.Color.LightGreen;
                message.Font.Size = FontUnit.Large;
                message.Text = "Image send successfully.";
            }
        }
        #endregion

        #region Image Load
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Loads all images that the user has uploaded and applys highslide. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <returns>   The images to be parsed in the HTML. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public string load_images()
         {
                string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection connection = new SqlConnection(connection_string);

                string query = "SELECT link, id FROM Images WHERE username=@username";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add(new SqlParameter("@username", User.Identity.Name));

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                string images = @"<tr>";
                int x = 0, y = 0;
                while (reader.Read())
                {
                    x++;
                    y++;
                    images +=
                        @"<td style=""padding:10px;"">
                            <a href=" + reader["link"].ToString() + @" class=""highslide"" onclick=""return hs.expand(this)""><img src=Thumbnail.ashx?image=" + reader["link"].ToString() + @" alt="""" width=""100px"" height=""100px""/></a><br />
                            <center><input type='button' onClick=""location.href='ManagerEdit.aspx?ID=" + reader["id"].ToString() + @"&section=images'"" value='Edit' style=""width:100px; height:20px;""></center>
                        </td>";
                    if (x == 6)
                    {
                        images += @"</tr><tr>";
                        x = 0;
                    }
                }
                if (y == 0)
                {
                    images += @"<tr><td><p>You haven't sent any image :(</p><br /></td>";
                }

                reader.Close();
                connection.Close();

                return ("<table>" + images + "</tr></table>");
         }
        #endregion


        #region Add video
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Uploads a video. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void btn_send_videos_click(object sender, EventArgs e)
        {
            int a1 = 0;
            int a2 = 0;
            string vid = "";

            if (txt_link_videos.Text.IndexOf("youtube") != -1)
            {
                a1 = txt_link_videos.Text.IndexOf("watch?v=");
                a2 = txt_link_videos.Text.IndexOf("&v=");
                if (a1 != -1)
                {
                    vid = txt_link_videos.Text.Substring((a1 + 8), 11);
                }
                else if (a2 != -1)
                {
                    vid = txt_link_videos.Text.Substring((a2 + 3), 11);
                }

                if(vid != "")
                {
                    string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    SqlConnection connection = new SqlConnection(connection_string);

                    string query = "INSERT INTO Videos (username, link, description, permission, date, visits) Values(@username, @link, @description, @permission, @date, @visits)";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.Add(new SqlParameter("@username", (string)User.Identity.Name));
                    command.Parameters.Add(new SqlParameter("@link", vid));
                    command.Parameters.Add(new SqlParameter("@description", txt_description_videos.Text));
                    command.Parameters.Add(new SqlParameter("@permission", cb_permission_videos.Text));
                    command.Parameters.Add(new SqlParameter("@date", DateTime.Now));
                    command.Parameters.Add(new SqlParameter("@visits", "0"));

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                    txt_link_videos.Text = "";
                    txt_description_videos.Text = "";

                    message.ForeColor = System.Drawing.Color.LightGreen;
                    message.Font.Size = FontUnit.Large;
                    message.Text = "Video send successfully.";
                }
                else
                {
                    message.ForeColor = System.Drawing.Color.Red;
                    message.Font.Size = FontUnit.Large;
                    message.Text = "Could't find the video ID.";
                }
            }
            else
            {
                message.ForeColor = System.Drawing.Color.Red;
                message.Font.Size = FontUnit.Large;
                message.Text = "Only YouTube is supported.";
            }
        }
        #endregion

        #region Video Load
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Loads all videos that the user has uploaded. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <returns>   The videos. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public string load_videos()
        {
            string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connection_string);

            string query = "SELECT link, id FROM Videos WHERE username=@username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@username", User.Identity.Name));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            string videos = @"<tr>";
            int x = 0, y = 0;
            while (reader.Read())
            {
                x++;
                y++;
                videos +=
                    @"<td style=""padding:10px;"">
                            <iframe width=""200"" height=""165"" src=""https://www.youtube-nocookie.com/embed/" + reader["link"].ToString() + @"?rel=0&amp;hd=1"" frameborder=""0"" allowfullscreen></iframe><br />
                            <center><input type='button' onClick=""location.href='ManagerEdit.aspx?ID=" + reader["id"].ToString() + @"&section=videos'"" value='Edit' style=""width:100px; height:20px;""></center>
                        </td>";
                if (x == 4)
                {
                    videos += @"</tr><tr>";
                    x = 0;
                }
            }
            if (y == 0)
            {
                videos += @"<tr><td><p>You haven't sent any video :(</p><br /></td>";
            }

            reader.Close();
            connection.Close();

            return ("<table>" + videos + "</tr></table>");
        }
        #endregion

    }
}