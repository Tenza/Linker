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
// file:	Linker.Master.cs
//
// summary:	Implements the linker master class
////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Linker
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///     This master page holds the page layout as well as the links to navigate within the
    ///     website.
    /// </summary>
    ///
    /// <remarks>   Filipe, 10 Nov 2011. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public partial class Linker : System.Web.UI.MasterPage
    {
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

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     Removes sepecific links from the naviagtion bar acording to the state of the user.
        /// </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Menu event information. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected void Menu_MenuItemDataBound(object sender, MenuEventArgs e)
        {  
            if (Context.User.Identity.IsAuthenticated)
            {
                if (e.Item.Text == "Login" || e.Item.Text == "Register")
                {
                    e.Item.Parent.ChildItems.Remove(e.Item);
                }
            }
            else
            {
                if (e.Item.Text == "Logout")
                {
                    e.Item.Parent.ChildItems.Remove(e.Item);
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///     This is the only way to give give access to our public stuff. For example: If I'm user 1,
        ///     and I want to see user 2 stuff the link is:
        ///     http://localhost:51359/User/Videos.aspx?user=2.
        ///     This makes sure that we can navigate the links that belong to the same user.
        /// </summary>
        ///
        /// <remarks>   Filipe, 10 Nov 2011. </remarks>
        ///
        /// <returns>   The URL. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        protected string get_url()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["user"]))
            {
                return ("?user=" + Request.QueryString["user"]);
            }

            return null; ;
        }
    }
}