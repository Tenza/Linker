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
// file:	All\Register.aspx.cs
//
// summary:	Implements the register.aspx class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;

namespace Linker.All
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   This page is used to create new users in the application. </summary>
    ///
    /// <remarks>   Filipe, 10 Nov 2011. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class Register : System.Web.UI.Page
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Redirect if already logged. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (Context.User.Identity.Name != "")
            {
                Response.Redirect("/All/Home.aspx");
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Creates a new user. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void btn_register_click(object sender, EventArgs e)
        {
            if (check_login_email() && txt_username.Text != "1") //"1" global admin, just exists on the membership.
            {
                string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection connection = new SqlConnection(connection_string);

                string query = "INSERT INTO Users (username, email, name, status, permission, date, visits) Values(@username, @email, @name, @status, @permission, @date, @visits)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Add(new SqlParameter("@username", txt_username.Text));
                command.Parameters.Add(new SqlParameter("@email", txt_email.Text));
                command.Parameters.Add(new SqlParameter("@name", txt_name.Text));
                command.Parameters.Add(new SqlParameter("@status", "Active"));
                command.Parameters.Add(new SqlParameter("@permission", "Public"));
                command.Parameters.Add(new SqlParameter("@date", DateTime.Now));
                command.Parameters.Add(new SqlParameter("@visits", "0"));

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                Membership.CreateUser(txt_username.Text, txt_password1.Text);
                Roles.AddUserToRole(txt_username.Text, "User");

                txt_username.Text = "";
                txt_email.Text = "";
                txt_name.Text = "";
                txt_password1.Text = "";
                txt_password2.Text = "";

                message.ForeColor = System.Drawing.Color.LightGreen;
                message.Font.Size = FontUnit.Large;
                message.Text = "Registered successfully.";
            }
            else
            {
                message.ForeColor = System.Drawing.Color.Red;
                message.Font.Size = FontUnit.Large;
                message.Text = "Username or Email already in use.";
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Determines the email or username are already in use. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool check_login_email()
        {
            string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connection_string);

            string query = "SELECT COUNT(*) AS ext FROM Users WHERE username=@username OR email=@email";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@username", txt_username.Text));
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
    }
}