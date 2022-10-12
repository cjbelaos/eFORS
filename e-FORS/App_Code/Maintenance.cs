﻿using Microsoft.ApplicationBlocks.Data;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Maintenance
/// </summary>
public class Maintenance
{
    private readonly string EPPIIIP;
    readonly SqlConnection SqlConEPPIIIP;

    private readonly string EFORS;
    readonly SqlConnection conn;

    private readonly string EPPIIIP_New;
    readonly SqlConnection SqlConEPPIIIP_New;

    public Maintenance()
    {
        this.EPPIIIP = System.Configuration.ConfigurationManager.AppSettings["EPPIIIP"];
        SqlConEPPIIIP = new SqlConnection(EPPIIIP);

        this.EPPIIIP_New = System.Configuration.ConfigurationManager.AppSettings["EPPIIIP_New"];
        SqlConEPPIIIP_New = new SqlConnection(EPPIIIP_New);

        this.EFORS = System.Configuration.ConfigurationManager.AppSettings["EFORS"];
        conn = new SqlConnection(EFORS);
    }

    public DataView GetUser(string strSystemName, string strUsername)
    {
        //using (var cmd = new SqlCommand("LOGIN_GetUser", SqlConEPPIIIP) { CommandType = CommandType.StoredProcedure })
        //{
        //    cmd.Parameters.AddWithValue("@system_name", strSystemName);
        //    cmd.Parameters.AddWithValue("@user_id", strUsername);
        //    cmd.Parameters.AddWithValue("@password", "");
        //    cmd.Parameters.AddWithValue("@LDAP", true);

        //    cmd.CommandTimeout = 360000;

        //    SqlDataAdapter da = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();

        //    conn.Open();

        //    da.Fill(ds);
        //    cmd.ExecuteNonQuery();

        //    conn.Close();

        //    return ds;
        //}

        return SqlHelper.ExecuteDataset(this.EPPIIIP, "LOGIN_GetUser", strSystemName, strUsername, "", true).Tables[0].DefaultView;
    }

    public DataTable GetUser(LoginDetails ld)
    {
        using (var cmd = new SqlCommand("LOGIN_GetUser", SqlConEPPIIIP) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@system_name", ld.system);
            cmd.Parameters.AddWithValue("@user_id", ld.username);
            cmd.Parameters.AddWithValue("@password", ld.password);
            cmd.Parameters.AddWithValue("@LDAP", ld.ldap);

            cmd.CommandTimeout = 300;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (SqlConEPPIIIP.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlConEPPIIIP.Open();
                    da.Fill(dt);
                    cmd.ExecuteNonQuery();
                } 
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                SqlConEPPIIIP.Close();
            }
            return dt;
        }
    }

    public DataTable GetAllTasks()
    {
        using (var cmd = new SqlCommand("GET_TASKLIST_ALL", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.CommandTimeout = 360000;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);

                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
    }

    public DataTable GetPendingTasks(string UserID)
    {
        using (var cmd = new SqlCommand("GET_TASKLIST_SECTION_REQUESTOR", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@USERID", UserID);
            cmd.CommandTimeout = 360000;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);

                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
    }

    public DataTable GetMyTasks(string UserID)
    {
        using (var cmd = new SqlCommand("GET_TASKLIST_USER", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@USERID", UserID);
            cmd.CommandTimeout = 360000;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if(conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
    }

    public DataTable GetFinishedTasks()
    {
        using (SqlCommand cmd = new SqlCommand("GetFinishedTasks", conn) { CommandType = CommandType.StoredProcedure })
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    sqlDataAdapter.Fill(dataTable);
                }
                else
                {
                    conn.Open();
                    sqlDataAdapter.Fill(dataTable);
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
            return dataTable;
        }
    }

    public DataTable GetFinishedTasksPerSection(string UserID)
    {
        using (SqlCommand cmd = new SqlCommand("GetFinishedTasksPerSection", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@USERID", UserID);

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    sqlDataAdapter.Fill(dataTable);
                }
                else
                {
                    conn.Open();
                    sqlDataAdapter.Fill(dataTable);
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
            return dataTable;
        }
    }

    public DataTable GetCancelledTasks()
    {
        using (SqlCommand cmd = new SqlCommand("GetCancelledTasks", conn) { CommandType = CommandType.StoredProcedure })
        {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    sqlDataAdapter.Fill(dataTable);
                }
                else
                {
                    conn.Open();
                    sqlDataAdapter.Fill(dataTable);
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
            return dataTable;
        }
    }

    public DataTable GetFinished8112Tasks(string DIVISION, string LOANO, string SUPPLIER, string PURPOSE)
    {
        using (SqlCommand cmd = new SqlCommand("GetFinished8112Tasks", conn) { CommandType = CommandType.StoredProcedure })
        {
            if (DIVISION == null)
            {
                DIVISION = "";
            }
            if (LOANO == null)
            {
                LOANO = "";
            }
            if (SUPPLIER == null)
            {
                SUPPLIER = "";
            }
            if (PURPOSE == null)
            {
                PURPOSE = "";
            }
            cmd.Parameters.AddWithValue("@DIVISION", DIVISION);
            cmd.Parameters.AddWithValue("@LOANO", LOANO);
            cmd.Parameters.AddWithValue("@SUPPLIER", SUPPLIER);
            cmd.Parameters.AddWithValue("@PURPOSE", PURPOSE);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
    }

    public DataTable GetFinished8112LOANOs()
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetFinished8112LOANOs", conn) { CommandType = CommandType.StoredProcedure })
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
    }

    public DataTable GetFinished8112Purpose()
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetFinished8112Purpose", conn) { CommandType = CommandType.StoredProcedure })
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
    }

    public DataTable GetFinished8112Suppliers()
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetFinished8112Suppliers", conn) { CommandType = CommandType.StoredProcedure })
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
    }

    public Boolean FinishTaskChecking(string ControlNo)
    {
        SqlCommand cmd = new SqlCommand("FinishTaskChecking", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ControlNo", ControlNo);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        try
        {
            if (conn.State == ConnectionState.Open)
            {
                da.Fill(dt);
            }
            else
            {
                conn.Open();
                da.Fill(dt);
            }
        }
        catch (SqlException sqlex)
        {
            throw sqlex;
        }
        finally
        {
            conn.Close();
        }

        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Boolean CheckIfCancelled(string ControlNo)
    {
        SqlCommand cmd = new SqlCommand("CheckIfCancelled", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CONTROLNO", ControlNo);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        try
        {
            if (conn.State == ConnectionState.Open)
            {
                da.Fill(dt);
            }
            else
            {
                conn.Open();
                da.Fill(dt);
            }
        }
        catch (SqlException sqlex)
        {
            throw sqlex;
        }
        finally
        {
            conn.Close();
        }

        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Boolean CheckIfFinishedRequestor(string ControlNo)
    {
        SqlCommand cmd = new SqlCommand("CheckIfFinishedRequestor", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@CONTROLNO", ControlNo);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        try
        {
            if (conn.State == ConnectionState.Open)
            {
                da.Fill(dt);
            }
            else
            {
                conn.Open();
                da.Fill(dt);
            }
        }
        catch (SqlException sqlex)
        {
            throw sqlex;
        }
        finally
        {
            conn.Close();
        }

        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public DataTable GetAssignedForRequestChange(FarmOutDocumentDetails fod)
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetAssignedForRequestChange", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", fod.CONTROLNO);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
    }

    public DataSet SaveFarmOutRequestForm(FarmOutDetails fo, string strUsername)
    {
        using (var cmd = new SqlCommand("SaveFarmOutRequestForm", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@ControlNo", fo.ControlNo);
            cmd.Parameters.AddWithValue("@Division", fo.Division);
            cmd.Parameters.AddWithValue("@NatureOfItem", fo.NatureOfItem);
            cmd.Parameters.AddWithValue("@TransferTo", fo.TransferTo);
            cmd.Parameters.AddWithValue("@TypeOfItem", fo.TypeOfItem);
            cmd.Parameters.AddWithValue("@ClassificationOfItem", fo.ClassificationOfItem);
            cmd.Parameters.AddWithValue("@PurposeOfItem", fo.PurposeOfItem);
            cmd.Parameters.AddWithValue("@BearerEmployeeNo", fo.BearerEmployeeNo);
            cmd.Parameters.AddWithValue("@BearerEmployeeName", fo.BearerEmployeeName);
            cmd.Parameters.AddWithValue("@RequestorEmployeeNo", fo.RequestorEmployeeNo);
            cmd.Parameters.AddWithValue("@RequestorEmployeeName", fo.RequestorEmployeeName);
            cmd.Parameters.AddWithValue("@Section", fo.Section);
            cmd.Parameters.AddWithValue("@LocalNo", fo.LocalNo);
            cmd.Parameters.AddWithValue("@DateRequested", fo.DateRequested);
            cmd.Parameters.AddWithValue("@ActualDateOfTransfer", fo.ActualDateOfTransfer);
            cmd.Parameters.AddWithValue("@TargetDateOfReturn", fo.TargetDateOfReturn);
            cmd.Parameters.AddWithValue("@PackagingUsed", fo.PackagingUsed);
            cmd.Parameters.AddWithValue("@SupplierID", fo.SupplierID);
            cmd.Parameters.AddWithValue("@SupplierName", fo.SupplierName);
            cmd.Parameters.AddWithValue("@DestinationAddress", fo.DestinationAddress);
            cmd.Parameters.AddWithValue("@OriginOfItem", fo.OriginOfItem);
            cmd.Parameters.AddWithValue("@DeliveryReceiptNo", fo.DeliveryReceiptNo);
            cmd.Parameters.AddWithValue("@InvoiceNo", fo.InvoiceNo);
            cmd.Parameters.AddWithValue("@ContactPerson", fo.ContactPerson);
            cmd.Parameters.AddWithValue("@ContactNo", fo.ContactNo);
            cmd.Parameters.AddWithValue("@TelephoneNo", fo.TelephoneNo);
            cmd.Parameters.AddWithValue("@FaxNo", fo.FaxNo);
            cmd.Parameters.AddWithValue("@ModeOfTransfer", fo.ModeOfTransfer);
            cmd.Parameters.AddWithValue("@TypeOfTransfer", fo.TypeOfTransfer);
            cmd.Parameters.AddWithValue("@CreatedBy", strUsername);
            cmd.Parameters.AddWithValue("@UpdatedBy", strUsername);
            cmd.CommandTimeout = 360000;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(ds);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(ds);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            return ds;
        }
    }

    public DataSet GetUserInformation(string UserID)
    {
        SqlCommand cmd = new SqlCommand("GetUserInformation", SqlConEPPIIIP_New);
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.AddWithValue("@APOAccount", UserID);
        cmd.CommandTimeout = 360000;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();

        if (SqlConEPPIIIP_New.State == ConnectionState.Open)
        {
            da.Fill(ds);
            cmd.ExecuteNonQuery();

            SqlConEPPIIIP_New.Close();
        }
        else
        {
            SqlConEPPIIIP_New.Open();

            da.Fill(ds);
            cmd.ExecuteNonQuery();

            SqlConEPPIIIP_New.Close();
        }
        return ds;
    }

    public DataTable GetAllItems(string strControlNo)
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(conn, "GetAllItems", strControlNo).Tables[0];
        return dt;
    }

    public void SaveSupplier(string UserName, string Supplier, string LOAType, string LOANo, string ExpirationDate1, string LOAAmountLimit, string LOAQuantityLimit, string SuretyBondNo, string ExpirationDate2)
    {
        SqlCommand cmd = new SqlCommand("SaveSupplier", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Username", UserName);
        cmd.Parameters.AddWithValue("@SupplierName", Supplier);
        cmd.Parameters.AddWithValue("@LOAType", LOAType);
        cmd.Parameters.AddWithValue("@LOANo", LOANo);
        cmd.Parameters.AddWithValue("@LOAExpirationDate", ExpirationDate1);
        cmd.Parameters.AddWithValue("@AmountLimit", LOAAmountLimit);
        cmd.Parameters.AddWithValue("@QuantityLimit", LOAQuantityLimit);
        cmd.Parameters.AddWithValue("@SuretyBondNo", SuretyBondNo);
        cmd.Parameters.AddWithValue("@SuretyExpirationDate", ExpirationDate2);
        cmd.CommandTimeout = 36000;

        try
        {
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            conn.Close();
        }

    }
    public string SaveItem(Items i, string strUsername)
    {
        string Message = "";
        try
        {
            using (var cmd = new SqlCommand("SaveItem", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@ControlNo", i.ControlNo);
                cmd.Parameters.AddWithValue("@TypeOfItem", i.TypeOfItem);
                cmd.Parameters.AddWithValue("@ItemCode", i.ItemCode);
                cmd.Parameters.AddWithValue("@ItemDescription", i.ItemDescription);
                cmd.Parameters.AddWithValue("@Quantity", i.Quantity);
                cmd.Parameters.AddWithValue("@UnitOfMeasurement", i.UnitOfMeasurement);
                cmd.Parameters.AddWithValue("@Amount", i.Amount.ToString());
                cmd.Parameters.AddWithValue("@AssetNo", i.AssetNo);
                cmd.Parameters.AddWithValue("@ODNo", i.ODNo);
                cmd.Parameters.AddWithValue("@ContainerNo", i.ContainerNo);
                cmd.Parameters.AddWithValue("@PEZASeal", i.PEZASeal);
                cmd.Parameters.AddWithValue("@DSRDRNo", i.DSRDRNo);
                cmd.Parameters.AddWithValue("@CreatedBy", strUsername);

                try
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    else
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                catch (SqlException sqlex)
                {
                    Message = sqlex.Message.ToString();
                }
                return Message;
            }
        }
        catch (Exception ex)
        {
            Message = ex.Message.ToString();
            conn.Close();
        }
        return Message;
    }

    public void UpdateItem(Items i, string strUsername)
    {
        using (var cmd = new SqlCommand("UpdateItem", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@ID", i.ID.ToString());
            cmd.Parameters.AddWithValue("@ControlNo", i.ControlNo);
            cmd.Parameters.AddWithValue("@TypeOfItem", i.TypeOfItem);
            cmd.Parameters.AddWithValue("@ItemCode", i.ItemCode);
            cmd.Parameters.AddWithValue("@ItemDescription", i.ItemDescription);
            cmd.Parameters.AddWithValue("@Quantity", i.Quantity);
            cmd.Parameters.AddWithValue("@UnitOfMeasurement", i.UnitOfMeasurement);
            cmd.Parameters.AddWithValue("@Amount", i.Amount);
            cmd.Parameters.AddWithValue("@AssetNo", i.AssetNo);
            cmd.Parameters.AddWithValue("@ODNo", i.ODNo);
            cmd.Parameters.AddWithValue("@ContainerNo", i.ContainerNo);
            cmd.Parameters.AddWithValue("@PEZASeal", i.PEZASeal);
            cmd.Parameters.AddWithValue("@DSRDRNo", i.DSRDRNo);
            cmd.Parameters.AddWithValue("@UpdatedBy", strUsername);

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                conn.Close();
            }
        }
    }

    public void DeleteItem(Items i)
    {
        using (var cmd = new SqlCommand("DeleteItem", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@ID", i.ID.ToString());

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
           finally
            {
                conn.Close();
            }
        }
    }

    public void Approval(Approval a)
    {
        using (var cmd = new SqlCommand("APPROVE_WORKFLOW", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", a.ControlNo);
            cmd.Parameters.AddWithValue("@WORKFLOWID", a.WorkFlowID);
            cmd.Parameters.AddWithValue("@APPROVERID", a.ApproverID);
            cmd.Parameters.AddWithValue("@APPROVALCOMMENTS", a.Comment);
            cmd.Parameters.AddWithValue("@UID", a.UserID);

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
        }
    }

    public string GetRequestCreator(string ControlNo)
    {
        using (var cmd = new SqlCommand("sp_GetRequestCreator", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", ControlNo);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            return dt.Rows[0]["ASSIGNEDUSERID"].ToString();
        }
    }

    public void RequestChange(Approval a)
    {
        using (var cmd = new SqlCommand("REQUEST_CHANGE_WORKFLOW", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", a.ControlNo);
            cmd.Parameters.AddWithValue("@WORKFLOWID", a.WorkFlowID);
            cmd.Parameters.AddWithValue("@APPROVERID", a.ApproverID);
            cmd.Parameters.AddWithValue("@REQUESTCHANGECOMMENT", a.Comment);
            cmd.Parameters.AddWithValue("@UID", a.UserID);

            conn.Open();
            cmd.ExecuteNonQuery();

            conn.Close();
        }
    }

    public void ReassignTask(Approval a, string strReassignto)
    {
        using (var cmd = new SqlCommand("REASSIGN_TASK_WORKFLOW", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", a.ControlNo.ToString());
            cmd.Parameters.AddWithValue("@WORKFLOWID", a.WorkFlowID.ToString());
            cmd.Parameters.AddWithValue("@APPROVERID", a.ApproverID.ToString());
            cmd.Parameters.AddWithValue("@REASSIGNEDCOMMENTS", a.Comment.ToString());
            cmd.Parameters.AddWithValue("@REASSIGNEDTO", strReassignto);
            cmd.Parameters.AddWithValue("@UID", a.UserID.ToString());

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
        }
    }

    public void SaveMirrorApproval(Approval a)
    {
        using (var cmd = new SqlCommand("SaveMirrorApproval", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@ControlNo", a.ControlNo);
            cmd.Parameters.AddWithValue("@Createdby", a.UserID);

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    public void SaveApproval(Approval a)
    {
        using (var cmd = new SqlCommand("SaveApproval", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@ControlNo", a.ControlNo);
            cmd.Parameters.AddWithValue("@Requestedby", a.Requestedby);
            cmd.Parameters.AddWithValue("@Checkedby", a.Checkedby);
            cmd.Parameters.AddWithValue("@Approvedby", a.Approvedby);
            cmd.Parameters.AddWithValue("@UserID", a.UserID);

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    public void UpdateApproval(string ControlNo, string ApproverID, string ApproverUserID, string UserID)
    {
        using (var cmd = new SqlCommand("UpdateApproval", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", ControlNo);
            cmd.Parameters.AddWithValue("@APPROVERID", ApproverID);
            cmd.Parameters.AddWithValue("@APPROVERUSERID", ApproverUserID);
            cmd.Parameters.AddWithValue("@USERID", UserID);

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    public DataSet GetApproval(Approval a)
    {
        using (var cmd = new SqlCommand("GetApproval", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@ControlNo", a.ControlNo);
            cmd.CommandTimeout = 360000;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            conn.Open();

            da.Fill(ds);
            cmd.ExecuteNonQuery();

            conn.Close();

            return ds;
        }
    }

    public DataTable GetDivision()
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(EFORS, "GetDivision").Tables[0];
        return dt;
    }

    public DataTable GetNatureOfItem()
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(EFORS, "GetNatureOfItem").Tables[0];
        return dt;
    }

    public DataTable GetTransferTo()
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(EFORS, "GetTransferTo").Tables[0];
        return dt;
    }

    public DataTable GetTypeOfItem()
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(EFORS, "GetTypeOfItem").Tables[0];
        return dt;
    }

    public DataTable GetClassificationOfItem()
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(EFORS, "GetClassificationOfItem").Tables[0];
        return dt;
    }

    public DataTable GetPurposeOfItem()
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(EFORS, "GetPurposeOfItem").Tables[0];
        return dt;
    }

    public DataTable GetPackagingUsed()
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(EFORS, "GetPackagingUsed").Tables[0];
        return dt;
    }

    public DataTable GetModeOfTransfer()
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(EFORS, "GetModeOfTransfer").Tables[0];
        return dt;
    }

    public DataTable GetTypeOfTransfer()
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(EFORS, "GetTypeOfTransfer").Tables[0];
        return dt;
    }

    public DataTable GetRequestedby(UserInfo ui)
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(EFORS, "GetRequestedby", ui.Section).Tables[0];
        return dt;
    }

    public DataTable GetCheckedby(UserInfo ui)
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(EFORS, "GetCheckedby", ui.Section).Tables[0];
        return dt;
    }

    public DataTable GetApprovedby(UserInfo ui)
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(EFORS, "GetApprovedby", ui.Section).Tables[0];
        return dt;
    }

    public DataTable GetReassignto(UserInfo ui, Approval a)
    {
        DataTable dt = new DataTable();
        dt = SqlHelper.ExecuteDataset(EFORS, "GetReassignto", ui.Section, a.Approvedby, a.ApproverID).Tables[0];
        return dt;
    }

    public DataTable GetGatepass(ReportDetails rd)
    {
        SqlCommand cmd = new SqlCommand("GetGatepass", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Section", rd.Section);
        cmd.Parameters.AddWithValue("@Supplier", rd.Supplier);
        cmd.Parameters.AddWithValue("@DateFrom", rd.DateFrom);
        cmd.Parameters.AddWithValue("@DateTo", rd.DateTo);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        conn.Open();

        da.Fill(dt);

        conn.Close();

        return dt;
    }

    public DataTable GetFarmOut(ReportDetails rd)
    {
        SqlCommand cmd = new SqlCommand("GetFarmOut", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Section", rd.Section);
        cmd.Parameters.AddWithValue("@Supplier", rd.Supplier);
        cmd.Parameters.AddWithValue("@DateFrom", rd.DateFrom);
        cmd.Parameters.AddWithValue("@DateTo", rd.DateTo);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        conn.Open();

        da.Fill(dt);

        conn.Close();

        return dt;
    }

    public string GetTasksCount()
    {
        SqlCommand cmd = new SqlCommand("GetTasksCount", conn);
        cmd.CommandType = CommandType.StoredProcedure;


        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();

        conn.Open();

        da.Fill(ds);

        conn.Close();

        return JsonConvert.SerializeObject(ds.Tables[1]);
    }


    public DataTable GetMyTasksCount(string APOAccount)
    {
        using (var cmd = new SqlCommand("GetMyTasksCount", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@APOAccount", APOAccount);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(ds);
                }
                else
                {
                    conn.Open();
                    da.Fill(ds);
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
            return ds.Tables[1];
        }
    }
    public Boolean CheckAuthorization(string APO)
    {
        SqlCommand cmd = new SqlCommand("CheckAuthorization", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@APO", APO);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        if (conn.State == ConnectionState.Open)
        {
            da.Fill(dt);
            conn.Close();
        }
        else
        {
            conn.Open();
            da.Fill(dt);
            conn.Close();
        }

        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Boolean CheckIfBypassAccount(LoginDetails ld)
    {
        SqlCommand cmd = new SqlCommand("CheckIfBypassAccount", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@username", ld.username);

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();

        try
        {
            if (conn.State == ConnectionState.Open)
            {
                da.Fill(dt);
            }
            else
            {
                conn.Open();
                da.Fill(dt);
            }
        }
        catch (SqlException sqlex)
        {
            throw sqlex;
        }
        finally
        {
            conn.Close();
        }

        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public DataTable GetFarmOutDetailsCreatorandApprover(FarmOutDetails fo)
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetFarmOutDetailsCreatorandApprover", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@ControlNo", fo.ControlNo);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }

            return dt;
        }
    }

    public DataTable GetTotalQuantityWithUnitOfMeasurement(FarmOutDetails fo)
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetTotalQuantityWithUnitOfMeasurement", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@ControlNo", fo.ControlNo);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }

            return dt;
        }
    }

    public DataTable GetLOAofSupplierInControlNo(FarmOutDetails fo)
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetLOAofSupplierInControlNo", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", fo.ControlNo);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }

            return dt;
        }
    }

    public DataTable GetControlNoOf8112WithSameLOA(string LOA, string CTRLNO)
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetControlNoOf8112WithSameLOA", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@LOA", LOA);
            cmd.Parameters.AddWithValue("@CTRLNO", CTRLNO);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
    }

    public string Get8112Dates(string CTRLNOS)
    {
        using (SqlCommand cmd = new SqlCommand("sp_Get8112Dates", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CTRLNOS", CTRLNOS);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                }   
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["ACTUALDELIVERYDATES"].ToString();
            }
            else
            {
                return null;
            }
        }
    }

        public DataTable GetSuppliers()
        {
            using (SqlCommand cmd = new SqlCommand("sp_GetSuppliers", conn) { CommandType = CommandType.StoredProcedure })
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                try
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        da.Fill(dt);
                        conn.Close();
                    }
                    else
                    {
                        conn.Open();
                        da.Fill(dt);
                        conn.Close();
                    }
                }
                catch (SqlException sqlex)
                {
                    throw sqlex;
                }
                return dt;
            }
        }

        public void AddSupplier(SupplierDetails sd, string USERID)
        {
            using (SqlCommand cmd = new SqlCommand("sp_AddSupplier", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@SUPPLIERNAME", sd.Supplier);
                cmd.Parameters.AddWithValue("@SUPPLIERADDRESS", sd.Address);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (SqlException sqlex)
                {
                    throw sqlex;
                }
            }
        }

        public void UpdateSupplier(SupplierDetails sd, string USERID)
        {
            using (SqlCommand cmd = new SqlCommand("sp_UpdateSupplier", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@SUPPLIERID", sd.ID);
                cmd.Parameters.AddWithValue("@SUPPLIERNAME", sd.Supplier);
                cmd.Parameters.AddWithValue("@SUPPLIERADDRESS", sd.Address);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (SqlException sqlex)
                {
                    throw sqlex;
                }
            }
        }

        public void DeleteSupplier(SupplierDetails sd, string USERID)
        {
            using (SqlCommand cmd = new SqlCommand("sp_DeleteSupplier", conn) { CommandType = CommandType.StoredProcedure })
            {
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@SUPPLIERID", sd.ID);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                catch (SqlException sqlex)
                {
                    throw sqlex;
                }
            }
        }

    public DataTable GetLOA()
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetLOA", conn) { CommandType = CommandType.StoredProcedure })
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            return dt;
        }
    }

    public void AddLOA(LOADetails ld, string USERID)
    {
        using (SqlCommand cmd = new SqlCommand("sp_AddLOA", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@USERID", USERID);
            cmd.Parameters.AddWithValue("@SUPPLIERID", ld.SUPPLIERID);
            cmd.Parameters.AddWithValue("@DIVISION", ld.DIVISION);
            cmd.Parameters.AddWithValue("@LOANO", ld.LOANO);
            cmd.Parameters.AddWithValue("@LOAEXP", ld.LOAEXP);
            cmd.Parameters.AddWithValue("@SBNO", ld.SBNO);
            cmd.Parameters.AddWithValue("@SBEXP", ld.SBEXP);
            cmd.Parameters.AddWithValue("@DESCRIPTION", ld.DESCRIPTION);
            cmd.Parameters.AddWithValue("@QTYLIMIT", ld.QTYLIMIT);
            cmd.Parameters.AddWithValue("@UM", ld.UM);
            cmd.Parameters.AddWithValue("@AMTLIMIT", ld.AMTLIMIT);

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
        }
    }

    public void UpdateLOA(LOADetails ld, string USERID)
    {
        using (SqlCommand cmd = new SqlCommand("sp_UpdateLOA", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@USERID", USERID);
            cmd.Parameters.AddWithValue("@SUPPLIERID", ld.SUPPLIERID);
            cmd.Parameters.AddWithValue("@LOAID", ld.LOAID);
            cmd.Parameters.AddWithValue("@DIVISION", ld.DIVISION);
            cmd.Parameters.AddWithValue("@LOANO", ld.LOANO);
            cmd.Parameters.AddWithValue("@LOAEXP", ld.LOAEXP);
            cmd.Parameters.AddWithValue("@SBNO", ld.SBNO);
            cmd.Parameters.AddWithValue("@SBEXP", ld.SBEXP);
            cmd.Parameters.AddWithValue("@DESCRIPTION", ld.DESCRIPTION);
            cmd.Parameters.AddWithValue("@QTYLIMIT", ld.QTYLIMIT);
            cmd.Parameters.AddWithValue("@UM", ld.UM);
            cmd.Parameters.AddWithValue("@AMTLIMIT", ld.AMTLIMIT);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
        }
    }

    public void DeleteLOA(LOADetails ld, string USERID)
    {
        using (SqlCommand cmd = new SqlCommand("sp_DeleteLOA", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@USERID", USERID);
            cmd.Parameters.AddWithValue("@LOAID", ld.LOAID);

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
        }
    }

    public DataTable GetLOADetails(LOADetails ld)
    {
        using (SqlCommand cmd = new SqlCommand("GetLOADetails", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@LOAID", ld.LOAID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            return dt;
        }
    }

    public DataTable GetItemType(SupplierDetails sd, LOADetails ld)
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetItemType", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@SUPPLIERID", sd.ID);
            cmd.Parameters.AddWithValue("@DIVISION", ld.DIVISION);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            return dt;
        }
    }

    public DataTable GetFarmOutDocument(FarmOutDetails fd)
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetFarmOutDocument", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", fd.ControlNo);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            return dt;
        }
    }

    public DataTable GetDocumentFormat()
    {
        using (SqlCommand cmd = new SqlCommand("GetDocumentFormattobeUsed", conn) { CommandType = CommandType.StoredProcedure })
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            return dt;
        }
    }

    public DataTable GetEPPIAuthorizedSignatory()
    {
        using (SqlCommand cmd = new SqlCommand("GetEPPIAuthorizedSignatory", conn) { CommandType = CommandType.StoredProcedure })
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            return dt;
        }
    }

    public DataTable GetPreparedby()
    {
        using (SqlCommand cmd = new SqlCommand("GetPreparedby", conn) { CommandType = CommandType.StoredProcedure })
        {
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            return dt;
        }
    }

    public void SaveFarmOutDocuments(FarmOutDocumentDetails fdd)
    {
        using (SqlCommand cmd = new SqlCommand("SaveFarmOutDocuments", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ControlNo", fdd.CONTROLNO);
            cmd.Parameters.AddWithValue("@DocumentFormat", fdd.DOCFORMAT);
            cmd.Parameters.AddWithValue("@PEZADocumentNo", fdd.PEZADOCNO);
            cmd.Parameters.AddWithValue("@GatepassNo", fdd.GPNO);
            cmd.Parameters.AddWithValue("@LOANo", fdd.LOANO);
            cmd.Parameters.AddWithValue("@LOAExpiryDate", fdd.LOAEXP);
            cmd.Parameters.AddWithValue("@SuretyBondNo", fdd.SBNO);
            cmd.Parameters.AddWithValue("@SuretyExpiryDate", fdd.SBEXP);
            cmd.Parameters.AddWithValue("@ContainerNo", fdd.CONTNO);
            cmd.Parameters.AddWithValue("@PEZASeal", fdd.PEZASEAL);
            cmd.Parameters.AddWithValue("@PlateNo", fdd.PLATENO);
            cmd.Parameters.AddWithValue("@ControlNo8105", fdd.CN8105);
            cmd.Parameters.AddWithValue("@EPPIAuthorizedSignatory", fdd.EPPIAS);
            cmd.Parameters.AddWithValue("@PEZAExaminerSignatory", fdd.PEZAES);
            cmd.Parameters.AddWithValue("@PEZAOICSignatory", fdd.PEZAOIC);
            cmd.Parameters.AddWithValue("@CreatedBy", fdd.USERID);
            cmd.Parameters.AddWithValue("@UpdatedBy", fdd.USERID);

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
        }
    }

    public void SaveFarmOutDocumentsApproval(FarmOutDocumentDetails fdd)
    {
        using (var cmd = new SqlCommand("SaveFarmOutDocumentsApproval", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", fdd.CONTROLNO);
            cmd.Parameters.AddWithValue("@Preparedby", fdd.PREPARED);
            cmd.Parameters.AddWithValue("@Approvedby", fdd.APPROVED);

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
        }
    }

    public void DeleteFile(FileDetails fd)
    {
        using (var cmd = new SqlCommand("sp_DeleteFile", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", fd.ControlNo);
            cmd.Parameters.AddWithValue("@FILENAME", fd.FileName);

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
        }
    }

    public Boolean FarmOutDocumentsControlNoChecking(FarmOutDocumentDetails fdd)
    {
        using (var cmd = new SqlCommand("FarmOutDocumentsControlNoChecking", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", fdd.CONTROLNO);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public DataSet GetPEZASignatory()
    {
        SqlCommand cmd = new SqlCommand("GetPEZASignatory", conn);
        cmd.CommandType = CommandType.StoredProcedure;

        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();

        if (conn.State == ConnectionState.Open)
        {
            da.Fill(ds);
            conn.Close();
        }
        else
        {
            conn.Open();
            da.Fill(ds);
            conn.Close();
        }
        return ds;
    }

    public Boolean CheckPurposeOfItemIfWithLOA(FarmOutDocumentDetails fdd)
    {
        using (var cmd = new SqlCommand("CheckPurposeOfItemIfWithLOA", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", fdd.CONTROLNO);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void SendEmail(EmailDetails ed)
    {
        using (SqlCommand cmd = new SqlCommand("SEND_EMAIL", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", ed.CONTROLNO);
            cmd.Parameters.AddWithValue("@FROM", ed.FROM_EMAIL);
            cmd.Parameters.AddWithValue("@TO", ed.TO_EMAIL);
            cmd.Parameters.AddWithValue("@EMAILTYPE", ed.EMAILTYPE);
            cmd.Parameters.AddWithValue("@COMMENT", ed.COMMENT);

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
        }
    }

    public void SendEmailRequestApproved(EmailDetails ed)
    {
        using (SqlCommand cmd = new SqlCommand("SendEmailRequestApproved", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", ed.CONTROLNO);
            cmd.Parameters.AddWithValue("@FROM", ed.FROM_EMAIL);
            cmd.Parameters.AddWithValue("@TO", ed.TO_EMAIL);

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
        }
    }

    public void AddPrinted8112(Printed8112 p8112)
    {
        using (SqlCommand cmd = new SqlCommand("sp_AddPrinted8112", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", p8112.CONTROLNO);
            cmd.Parameters.AddWithValue("@SUBCONTROLNO", p8112.SUBCONTROLNO);
            cmd.Parameters.AddWithValue("@USERID", p8112.USERID);

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
        }
    }

    public DataTable GetCtrlNoPrinted8112(Printed8112 p8112)
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetCtrlNoPrinted8112", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", p8112.CONTROLNO);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            return dt;
        }
    }

    public DataTable GetSumofQty8112Items(Printed8112 p8112)
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetSumofQty8112Items", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", p8112.CONTROLNO);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            return dt;
        }
    }

    public DataTable GetMultipleUOM(FarmOutDocumentDetails fdd)
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetMultipleUOM", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", fdd.CONTROLNO);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            return dt;
        }
    }

    public DataSet GetLOAReport(ReportDetails rd)
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetLOAReport", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@SECTION", rd.Section);
            cmd.Parameters.AddWithValue("@SUPPLIER", rd.Supplier);
            cmd.Parameters.AddWithValue("@DATEFROM", rd.DateFrom);
            cmd.Parameters.AddWithValue("@DATETO", rd.DateTo);
            cmd.Parameters.AddWithValue("@LOANO", rd.LOANo);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(ds);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(ds);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            return ds;
        }
    }

    public DataSet GetLiquidationLedger(ReportDetails rd)
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetLiquidationLedger", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@LOANO", rd.LOANo);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(ds);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(ds);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            return ds;
        }
    }

    public DataTable GetLiquidationLedgerInfo (LiquidationLedgerDetails ll)
    {
        using (SqlCommand cmd = new SqlCommand("sp_GetLiquidationLedgerInfo", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@LOANO", ll.LOANO);
            cmd.Parameters.AddWithValue("@SUPPLIERID", ll.SUPPLIERID);
            cmd.Parameters.AddWithValue("@TYPEOFITEM", ll.TYPEOFITEM);
            cmd.Parameters.AddWithValue("@PEZADOCUMENTNO", ll.PEZADOCUMENTNO);
            cmd.Parameters.AddWithValue("@DATEOFTRANSFER", ll.DATEOFTRANSFER);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            return dt;
        }
    }

    public DataTable GetLOALimitPercentage(LOADetails ld)
    {
        using (SqlCommand cmd = new SqlCommand("GetLOALimitPercentage", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@SUPPLIERID", ld.SUPPLIERID);
            cmd.Parameters.AddWithValue("@DIVISION", ld.DIVISION);
            cmd.Parameters.AddWithValue("@DESCRIPTION", ld.DESCRIPTION);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    da.Fill(dt);
                    conn.Close();
                }
                else
                {
                    conn.Open();
                    da.Fill(dt);
                    conn.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            return dt;
        }
    }

    public void SendEmailLOALimit(FarmOutDetails fod, Approval a)
    {
        using (SqlCommand cmd = new SqlCommand("SendEmailLOALimit", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@FROM", a.Requestedby);
            cmd.Parameters.AddWithValue("@SUPPLIERID", fod.SupplierID);
            cmd.Parameters.AddWithValue("@DIVISION", fod.Division);

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    public void CancelRequest(Approval a)
    {
        using (SqlCommand cmd = new SqlCommand("sp_CancelRequest", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", a.ControlNo);
            cmd.Parameters.AddWithValue("@COMMENT", a.Comment);
            cmd.Parameters.AddWithValue("@USERID", a.UserID);

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    public void MarkAsPrinted(string ControlNo, string UserId)
    {
        using (var cmd = new SqlCommand("sp_MarkAsPrinted", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", ControlNo);
            cmd.Parameters.AddWithValue("@USERID", UserId);

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    public void RemoveMarkAsPrinted(string ControlNo, string UserId)
    {
        using (var cmd = new SqlCommand("sp_RemoveMarkAsPrinted", conn) { CommandType = CommandType.StoredProcedure })
        {
            cmd.Parameters.AddWithValue("@CONTROLNO", ControlNo);
            cmd.Parameters.AddWithValue("@USERID", UserId);

            try
            {
                if (conn.State == ConnectionState.Open)
                {
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            finally
            {
                conn.Close();
            }
        }
    }

    //public Boolean CheckIfPrinted(FarmOutDocumentDetails fdd)
    //{
    //    using (var cmd = new SqlCommand("CheckIfPrinted", conn) { CommandType = CommandType.StoredProcedure })
    //    {
    //        cmd.Parameters.AddWithValue("@CONTROLNO", fdd.CONTROLNO);

    //        SqlDataAdapter da = new SqlDataAdapter(cmd);
    //        DataTable dt = new DataTable();

    //        try
    //        {
    //            if (conn.State == ConnectionState.Open)
    //            {
    //                da.Fill(dt);
    //            }
    //            else
    //            {
    //                conn.Open();
    //                da.Fill(dt);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            ex.Message.ToString();
    //        }

    //        if (dt.Rows.Count > 0)
    //        {
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //}

    //public void DeletePrinted8112(FarmOutDocumentDetails fd)
    //{
    //    using (var cmd = new SqlCommand("sp_DeletePrinted8112", conn) { CommandType = CommandType.StoredProcedure })
    //    {
    //        cmd.Parameters.AddWithValue("@CONTROLNO", fd.CONTROLNO);
    //        cmd.Parameters.AddWithValue("@USERID", fd.USERID);

    //        try
    //        {
    //            if (conn.State == ConnectionState.Open)
    //            {

    //                cmd.ExecuteNonQuery();
    //            }
    //            else
    //            {
    //                conn.Open();
    //                cmd.ExecuteNonQuery();
    //            }
    //        }
    //        catch (SqlException sqlex)
    //        {
    //            throw sqlex;
    //        }
    //        finally
    //        {
    //            conn.Close();
    //        }
    //    }
    //}
}