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
// file:	Admin\VideosEdit.aspx.cs
//
// summary:	Implements the videosedit.aspx class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace Linker.Admin
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     In this page it is possible to view, edit and delete all the video links made.
    /// </summary>
    ///
    /// <remarks>   Filipe, 10 Nov 2011. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class VideosEdit : System.Web.UI.Page
    {
        public string querystring;

        #region Load ListView
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Loads the video list. </summary>
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

                string query = "SELECT * FROM Videos";
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
        /// <summary>
        ///     Deletes a video link. The ID comes from the CommandArgument on the ListView. It does not
        ///     delete the comments attached to it.
        /// </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Command event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void delete_image(object sender, CommandEventArgs e)
        {
            string connection_string = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connection_string);

            string query = "DELETE FROM Videos WHERE id=@id";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.Add(new SqlParameter("@id", e.CommandArgument));

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        #endregion

    }
}