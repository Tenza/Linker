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
// file:	All\Contact_us.aspx.cs
//
// summary:	Implements the contactus.aspx class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace Linker.All
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     This page so that the user can easily contact the administrator of the site.
    /// </summary>
    ///
    /// <remarks>   Filipe, 10 Nov 2011. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class Contact_us : System.Web.UI.Page
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Send a message to the admin. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void btn_send_Click(object sender, EventArgs e)
        {
            string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connection_string);

            string query = "INSERT INTO Contact (email, name, comment, section, date) Values(@email, @name, @comment, @section, @date)";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@email", txt_email.Text));
            command.Parameters.Add(new SqlParameter("@name", txt_name.Text));
            command.Parameters.Add(new SqlParameter("@comment", txt_comment.Text));
            command.Parameters.Add(new SqlParameter("@section", txt_section.Text));
            command.Parameters.Add(new SqlParameter("@date", DateTime.Now));

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            txt_name.Text = "";
            txt_email.Text = "";
            txt_comment.Text = "";
            txt_section.Text = "";

            message.ForeColor = System.Drawing.Color.LightGreen;
            message.Font.Size = FontUnit.Large;
            message.Text = "Message send successfully.";
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     If the section is "Other", make a textbox showup to describe the problem.
        /// </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_section.Text == "Other:")
            {
                txt_section.Text = "";
                txt_section.Visible = true;
            }
            else
            {
                txt_section.Text = cb_section.Text;
                txt_section.Visible = false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Make the costum textbox not showup at statup. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void Page_Init(object sender, EventArgs e)
        {
            if (cb_section.Text == "Other:")
            {
                txt_section.Text = "";
                txt_section.Visible = true;
            }
            else
            {
                txt_section.Text = cb_section.Text;
                txt_section.Visible = false;
            }
        }
    }
}