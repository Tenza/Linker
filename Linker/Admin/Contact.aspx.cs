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
// file:	Admin\Contact.aspx.cs
//
// summary:	Implements the contact.aspx class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;

namespace Linker.Admin
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     In this page it is possible to view, reply and delete all the contacts made. To reply to
    ///     a contact, the user must have a Gmail account.
    /// </summary>
    ///
    /// <remarks>   Filipe, 10 Nov 2011. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class Contact : System.Web.UI.Page
    {

        public bool querystring;

        #region Load ListView
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Detects, tests and sets the querystring in order to view or reply. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["email"]) && (!string.IsNullOrEmpty(Request.QueryString["section"])))
            {
                querystring = true;
                txt_to.Text = Request.QueryString["email"];
                txt_subject.Text = "RE: " + Request.QueryString["section"];
            }

            if (!querystring)
            {
                string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                SqlConnection connection = new SqlConnection(connection_string);

                string query = "SELECT * FROM Contact";
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
        /// <summary>   Deletes a contact. The ID comes from the CommandArgument on the ListView. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Command event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void delete_item(object sender, CommandEventArgs e)
        {
            string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connection_string);

            string query = "DELETE FROM Contact WHERE id=@id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@id", e.CommandArgument));

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        #endregion


        #region Send Email
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Sends an email using the Gmail servers. </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void btn_send_click(object sender, EventArgs e)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true; //Dependendo do anti-virus (se verifica email), pode ser necessario desligar o SSL. Pois o verificador, vai verificar e então ligar com SSL.
            client.Host = "smtp.gmail.com";
            client.Port = 587;

            //Smtp authentication
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(txt_account.Text, txt_password.Text);
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(txt_account.Text);
            msg.To.Add(new MailAddress(txt_to.Text));

            msg.Subject = txt_subject.Text;
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head></head><body><b>" + txt_message.Text + "</b></body></html>");

            try
            {
                client.Send(msg);
                message.ForeColor = System.Drawing.Color.LightGreen;
                message.Font.Size = FontUnit.Large;
                message.Text = "Message successfully sent.";
            }
            catch (Exception ex)
            {
                message.ForeColor = System.Drawing.Color.Red;
                message.Font.Size = FontUnit.Large;
                message.Text = "Error occured while sending your message.<br />" + ex.Message;
            }
        }
        #endregion

    }
}