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
// file:	Admin\Users.aspx.cs
//
// summary:	Implements the users.aspx class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;

namespace Linker.Admin
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An users. </summary>
    ///
    /// <remarks>   Filipe, 10 Nov 2011. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class Users : System.Web.UI.Page
    {
        public string querystring;

        #region Load Form
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detects, tests and sets the querystring in order to view or edit. </summary>
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

                string query = "SELECT * FROM Users WHERE id=@id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add(new SqlParameter("@id", querystring));

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    txt_email.Text = (string)reader["email"];
                    txt_name.Text = (string)reader["name"];
                    cb_permission.Text = (string)reader["permission"];
                    txt_visits.Text = reader["visits"].ToString();
                }

                reader.Close();
                connection.Close();

                if (txt_email.Text == "")
                {
                    message.ForeColor = System.Drawing.Color.Red;
                    message.Font.Size = FontUnit.Large;
                    message.Text = "User ID[" + querystring + "] is invalid.";
                    btn_edit.Enabled = false;
                }
            }
        }

        #endregion

        #region Load ListView
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Loads the user list. </summary>
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

                string query = "SELECT * FROM Users";
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
        /// <summary>   Gets the role for user. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="user"> The user. </param>
        ///
        /// <returns>   The role. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected string get_role(object user)
        {
            string[] a1 = Roles.GetRolesForUser((string)user);
            return a1[0];
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Changes the role of user. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Command event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void promote_user(object sender, CommandEventArgs e)
        {
            string user = (string)e.CommandArgument;
            string[] a1 = Roles.GetRolesForUser(user);
            string[] a2 = {"User"};

            if (a1[0] == a2[0])
            {
                Roles.RemoveUserFromRole(user, "User");
                Roles.AddUserToRole(user, "Admin");
            }
            else
            {
                Roles.RemoveUserFromRole(user, "Admin");
                Roles.AddUserToRole(user, "User");
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Set user status to banned or active. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Command event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void delete_user(object sender, CommandEventArgs e)
        {
            string arg = Convert.ToString(e.CommandArgument);
            string[] split = arg.Split(new Char [] {'|'});

            string user_id = split[0];
            string status = split[1];

            if (status == "Banned")
            {
                status = "Active";
            }
            else if (status == "Active")
            {
                status = "Banned";
            }

            string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connection_string);

            string query = "UPDATE Users SET status=@status WHERE id=@id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@id", user_id));
            command.Parameters.Add(new SqlParameter("@status", status));

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        #endregion

        #region Edit User
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Edits the current user information. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void btn_edit_click(object sender, EventArgs e)
        {
            if (check_email())
            {
                string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection connection = new SqlConnection(connection_string);

                string query = "UPDATE Users SET email=@email, name=@name, permission=@permission, visits=@visits WHERE id=@id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add(new SqlParameter("@id", querystring));
                command.Parameters.Add(new SqlParameter("@email", txt_email.Text));
                command.Parameters.Add(new SqlParameter("@name", txt_name.Text));
                command.Parameters.Add(new SqlParameter("@permission", cb_permission.Text));
                command.Parameters.Add(new SqlParameter("@visits", txt_visits.Text));

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                Response.Redirect("/Admin/Users.aspx");
            }
            else
            {
                message.ForeColor = System.Drawing.Color.Red;
                message.Font.Size = FontUnit.Large;
                message.Text = "Email already in use.";
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Determines if we can check email. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool check_email()
        {
            string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connection_string);

            string query2 = "SELECT email FROM Users where id=@id";
            SqlCommand command2 = new SqlCommand(query2, connection);
            command2.Parameters.Add(new SqlParameter("@id", querystring));

            connection.Open();
            SqlDataReader reader2 = command2.ExecuteReader();
            reader2.Read();
            string current_email = (string)reader2["email"];

            reader2.Close();
            command2.Connection.Close();

            if (txt_email.Text != current_email)
            {
                string query = "SELECT COUNT(*) AS ext FROM Users WHERE email=@email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add(new SqlParameter("@email", txt_email.Text));

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                bool exists = false;
                if ((int)reader["ext"] == 0)
                {
                    exists = true;
                }
                reader.Close();
                connection.Close();

                return exists;
            }
            else
            {
                return true;
            }
        }
        #endregion

    }
}