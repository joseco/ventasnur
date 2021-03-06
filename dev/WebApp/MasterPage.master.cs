﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidateVersion();


    }

    private void ValidateVersion()
    {
        string msg = null;
        try
        {
            VersionInfo dbVersion = VersionBRL.GetVersionInfo();
            VersionInfo appVersion = Configuracion.GetVersionInfo();

            bool valid = dbVersion.VersionMayor == appVersion.VersionMayor &&
                dbVersion.VersionMenor == appVersion.VersionMenor &&
                dbVersion.Patch == appVersion.Patch;
            if (!valid)
                msg = "La version de la Base de Datos y de la Aplicacion no coinciden";
        }
        catch (Exception e)
        {
            msg = e.Message;
        }

        if (!string.IsNullOrEmpty(msg))
        {
            Session["MSG_ERROR"] = msg;
            Response.Redirect("Error.aspx");
        }
    }
}