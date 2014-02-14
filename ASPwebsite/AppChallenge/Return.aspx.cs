﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinkedIn.OAuth;
using System.Web.Security;

public partial class ReturnClass : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            var token = OAuthManager.Current.GetTokenInCallback();
            var session = OAuthManager.Current.CompleteAuth(token);
            if (session.IsAuthorized())
            {
                session.StoreInUserSession();
                var p = session.GetProfile();

                var user = Membership.GetUser(p.UserId);
                if (user == null)
                {
                    user = Membership.CreateUser(p.UserId, Guid.NewGuid().ToString());
                }



                FormsAuthentication.SetAuthCookie(p.Firstname + " " + p.Lastname, false);
                Response.Redirect("~");
            }
        
    }
}
