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
// file:	Admin\CommentsEdit.aspx.cs
//
// summary:	Implements the CommentsEdit.aspx class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace Linker.Admin
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     In this page it is possible to view, edit and delete all the comments made on the videos
    ///     and images.
    /// </summary>
    ///
    /// <remarks>   Filipe, 10 Nov 2011. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class CommentsEdit : System.Web.UI.Page
    {

        public string querystring;

        #region Load Form
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detects, tests and sets the ID on the querystring. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void Page_Init(object sender, EventArgs e)
        {
            int test_id;
            if (Int32.TryParse(Request.QueryString["id"], out test_id))
            {
                querystring = test_id.ToString();
            }

            if (querystring != null)
            {
                string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection connection = new SqlConnection(connection_string);

                string query = "SELECT comment FROM Comments WHERE id=@id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add(new SqlParameter("@id", querystring));

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    txt_edit_comment.Text = (string)reader["comment"];
                }

                reader.Close();
                connection.Close();

                if (txt_edit_comment.Text == "")
                {
                    message.ForeColor = System.Drawing.Color.Red;
                    message.Font.Size = FontUnit.Large;
                    message.Text = "User ID[" + querystring + "] is invalid.";
                    btn_edit_comment.Enabled = false;
                }
            }
        }
        #endregion

        #region Load ListView
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Loads the comments list. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (querystring == null)
            {
                string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection connection = new SqlConnection(connection_string);

                string query = "SELECT * FROM Comments";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                ListView1.DataSource = reader;
                ListView1.DataBind();

                reader.Close();
                command.Connection.Close();
            }
        }
        #endregion

        #region ListView Options
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Deletes a comment. The ID comes from the CommandArgument on the ListView. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Command event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void delete_comment(object sender, CommandEventArgs e)
        {
            string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connection_string);

            string query = "DELETE FROM Comments WHERE id=@id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@id", e.CommandArgument));

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        #endregion

        #region Edit Comment
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Updates a comment. The ID comes from the CommandArgument on the ListView. </summary>
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

            string query = "UPDATE Comments SET comment=@comment WHERE id=@id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@id", querystring));
            command.Parameters.Add(new SqlParameter("@comment", txt_edit_comment.Text));

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            Response.Redirect("/Admin/CommentsEdit.aspx");
        }
        #endregion

    }
}