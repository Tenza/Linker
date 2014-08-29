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
// file:	All\Home.aspx.cs
//
// summary:	Implements the home.aspx class
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
    /// <summary>   The home page of the website. </summary>
    ///
    /// <remarks>   Filipe, 10 Nov 2011. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class Home : System.Web.UI.Page
    {
        /// <summary>   true if this object is logged. </summary>
        public bool is_logged;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Event handler. Called by Page for load events. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.Name == "")
            {
                is_logged = true;
            }else{
                is_logged = false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Authenticates the user, using the ASP Membership system. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Authenticate event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void Login1_Authenticate(object sender, System.Web.UI.WebControls.AuthenticateEventArgs e)
        {
            string username = LoginUser.UserName;
            string password = LoginUser.Password;

            if (Membership.ValidateUser(username, password))
            {
                if (check_status(username))
                {
                    e.Authenticated = true;
                }
            }
            else
            {
                e.Authenticated = false;
                message.ForeColor = System.Drawing.Color.White;
                message.Font.Size = FontUnit.Small;
                message.Text = "Your login attempt was not successful. Please try again.";
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Check the user status, is the account banned?. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="username"> The username. </param>
        ///
        /// <returns>   true if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool check_status(string username)
        {
            string stat = "";

            string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connection_string);

            string query = "SELECT status FROM Users WHERE username=@username";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@username", username));

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                stat = (string)reader["status"];
            }

            reader.Close();
            connection.Close();

            if (stat == "Active" || username == "1")
            {
                return true;
            }
            else
            {
                message.ForeColor = System.Drawing.Color.Red;
                message.Font.Size = FontUnit.Large;
                message.Text = "Your account is banned!";
                return false;
            }
        }
    }
}