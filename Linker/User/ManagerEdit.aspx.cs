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
// file:	User\ManagerEdit.aspx.cs
//
// summary:	Implements the manager edit.aspx class
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
    /// <summary>   This page allows the user to edit the information regarding the link. </summary>
    ///
    /// <remarks>   Filipe, 10 Nov 2011. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class ManagerEdit : System.Web.UI.Page
    {
        public string qs_id;
        public string qs_sec;
        public string qs_link;

        #region Load Form
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Loads the data for edition. </summary>
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
                    //Fail ID
                    Response.Redirect("Manager.aspx");
                }
            }
            else
            {
                Response.Redirect("Manager.aspx");
            }

            if (qs_id != null)
            {
                bool check_sec = false;
                string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection connection = new SqlConnection(connection_string);
                string query = "";

                if (qs_sec == "images")
                {
                    query = "SELECT link, description, permission FROM Images WHERE id=@id AND username=@username";
                    check_sec = true;
                }
                else if (qs_sec == "videos")
                {
                    query = "SELECT link, description, permission FROM Videos WHERE id=@id AND username=@username";
                    check_sec = true;
                }
                else
                {
                    //Fail Section
                    Response.Redirect("Manager.aspx");
                }

                if (check_sec)
                {
                    try
                    {
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.Add(new SqlParameter("@id", qs_id));
                        command.Parameters.Add(new SqlParameter("@username", User.Identity.Name));

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        reader.Read();
                        qs_link = (string)reader["link"];
                        txt_description.Text = (string)reader["description"];
                        cb_permission.Text = (string)reader["permission"];

                        reader.Close();
                        connection.Close();
                    }
                    catch
                    {
                        //Fail Read
                        connection.Close();
                        Response.Redirect("Manager.aspx");
                    }
                    
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Loads the image. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <returns>   The image. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public string load_img()
        {
            return qs_link;
        }

        #endregion

        #region Edit Properties
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Updates the video or image link. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void btn_edit_click(object sender, EventArgs e)
        {
            string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connection_string);
            string query = "";

            if (qs_sec == "images")
            {
                query = "UPDATE Images SET description=@description, permission=@permission WHERE id=@id AND username=@username";
            }
            else if (qs_sec == "videos")
            {
                query = "UPDATE Videos SET description=@description, permission=@permission WHERE id=@id AND username=@username";
            }

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@id", qs_id));
            command.Parameters.Add(new SqlParameter("@username", User.Identity.Name));
            command.Parameters.Add(new SqlParameter("@description", txt_description.Text));
            command.Parameters.Add(new SqlParameter("@permission", cb_permission.Text));

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            if (qs_sec == "images")
            {
                Response.Redirect("Manager.aspx?ID=1");
            }
            else if (qs_sec == "videos")
            {
                Response.Redirect("Manager.aspx?ID=2");
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes this link. Comments are preserved. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void btn_delete_click(object sender, EventArgs e)
        {
            string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connection_string);
            string query = "";

            if (qs_sec == "images")
            {
                query = "DELETE FROM Images WHERE id=@id AND username=@username";
            }
            else if (qs_sec == "videos")
            {
                query = "DELETE FROM Videos WHERE id=@id AND username=@username";
            }

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@id", qs_id));
            command.Parameters.Add(new SqlParameter("@username", User.Identity.Name));

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            if (qs_sec == "images")
            {
                Response.Redirect("Manager.aspx?ID=1");
            }
            else if (qs_sec == "videos")
            {
                Response.Redirect("Manager.aspx?ID=2");
            }
        }
        #endregion

    }
}