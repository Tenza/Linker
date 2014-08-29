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
// file:	User\Comments.aspx.cs
//
// summary:	Implements the comments.aspx class
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
    ///     This page is the comment zone. Besides showing the content, it shows the comments made on
    ///     it.
    /// </summary>
    ///
    /// <remarks>   Filipe, 10 Nov 2011. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class Comments : System.Web.UI.Page
    {
        public string qs_id;
        public string qs_sec;
        public string qs_link;

        #region Load Comments
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Loads the comments on the list. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void Page_PreRender(object sender, EventArgs e)
        {
            string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connection_string);

            try
            {
                string query = "SELECT username, comment, date FROM Comments WHERE link_id=@link_id AND section=@section";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add(new SqlParameter("@link_id", qs_id));
                command.Parameters.Add(new SqlParameter("@section", qs_sec));

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                ListView1.DataSource = reader;
                ListView1.DataBind();

                reader.Close();
                command.Connection.Close();
            }
            catch (Exception)
            {
                connection.Close();
                Response.Redirect("/All/Home.aspx");
            }
        }
        #endregion

        #region Load Link and Description
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Loads the content depending on the section and ID. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]) && (!string.IsNullOrEmpty(Request.QueryString["section"])))
            {
                int test_id;
                if (Int32.TryParse(Request.QueryString["id"], out test_id))
                {
                    qs_id = test_id.ToString();
                    qs_sec = Request.QueryString["section"];
                }
                else
                {
                    Response.Redirect("/All/Home.aspx");
                }
            }
            else
            {
                Response.Redirect("/All/Home.aspx");
            }

            if (qs_id != null)
            {
                bool check_sec = false;
                string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection connection = new SqlConnection(connection_string);
                string query = "";

                if (qs_sec == "images")
                {
                    query = "SELECT link, description FROM Images WHERE id=@id AND permission=@permission";
                    check_sec = true;
                }
                else if (qs_sec == "videos")
                {
                    query = "SELECT link, description FROM Videos WHERE id=@id AND permission=@permission";
                    check_sec = true;
                }
                else
                {
                    Response.Redirect("/All/Home.aspx");
                }

                if (check_sec)
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.Add(new SqlParameter("@id", qs_id));
                        command.Parameters.Add(new SqlParameter("@permission", "Public"));

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        qs_link = (string)reader["link"];
                        txt_description.Text = (string)reader["description"];

                        reader.Close();
                        connection.Close();
                    }
                    catch
                    {
                        connection.Close();
                        Response.Redirect("/All/Home.aspx");
                    }

                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Returns the information to be parsed on the HTML. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <returns>   The content. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public string load_content()
        {
            return qs_link;
        }
        #endregion

        #region Add Comment
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Add a new comment to this image or video. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void btn_add_click(object sender, EventArgs e)
        {
            string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connection_string);

            string query = "INSERT INTO Comments (link_id, section, username, comment, date) Values(@link_id, @section, @username, @comment, @date)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@link_id", qs_id));
            command.Parameters.Add(new SqlParameter("@section", qs_sec));
            command.Parameters.Add(new SqlParameter("@username", User.Identity.Name));
            command.Parameters.Add(new SqlParameter("@comment", txt_add_comment.Text));
            command.Parameters.Add(new SqlParameter("@date", DateTime.Now));

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            txt_add_comment.Text = "";

            message.ForeColor = System.Drawing.Color.LightGreen;
            message.Font.Size = FontUnit.Large;
            message.Text = "Comment posted successfully.";
        }
        #endregion

    }
}