﻿using System;
using System.Data;
using System.Globalization;
using System.Web.Services;

public partial class Home : System.Web.UI.Page
{
    private static Maintenance maint = new Maintenance();
    public static string UserID;
    public static string UserName;

    public static string ToTitleCase(string title)
    {
        
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(title.ToLower());
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] == null && Session["UserName"] == null)
        {
            Response.Redirect("Login.aspx");
        }

        UserID = Session["UserID"].ToString();
        UserName = Session["UserName"].ToString();
    }


    //[WebMethod]
    //public static string GetMyTasksCount()
    //{
    //    return maint.GetMyTasksCount(UserID);
    //}

}