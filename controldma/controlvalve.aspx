﻿<%@ Page Title="" Language="C#" MasterPageFile="~/master.Master" AutoEventWireup="true" CodeBehind="controlvalve.aspx.cs" Inherits="controldma.controlvalve" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        th {
            background-color: #E2F0FF;
        }
    </style>
    <!-- Content Wrapper. Contains page content -->

    <div class="content-wrapper">
        <!-- Content Header (Page header) -->
        <section class="content-header">
            <div class="container-fluid">
                <div class="row mb-2">
                    <div class="col-sm-6">
                        <h1>Control Valve (ควบคุมประตูน้ำ)</h1>
                    </div>
                    <div class="col-sm-6">

                        <%-- <ol class="breadcrumb float-sm-right">
              <li class="breadcrumb-item"><a href="#">Home</a></li>
              <li class="breadcrumb-item active">Icons</li>
            </ol>--%>
                    </div>
                </div>
            </div>
            <!-- /.container-fluid -->
        </section>

        <!-- Main content -->
        <div id="loader_content"></div>
        <section id="div_content_load" class="content">
            <div class="container-fluid">
                <div class="card card-primary card-outline">
                    <div class="card-header">
                        <div class="row">
                            <div class="col-6">
                                <h3 class="card-title">รายการจุดตั้ง Control ประตูน้ำ</h3>
                            </div>
                            <div class="col-6">
                                <button type="button" id="refresh_table" class="btn btn-block btn-success col-md-2" style="float: right;"><i class="fas fa-redo"></i>refresh</button>
                            </div>
                        </div>


                    </div>
                    <!-- /.card-body -->
                    <div class="card-body">
                        <div style="width: 100%;">
                            <div class="table-responsive">
                                <table id="dt_controlvalve" class="table table-striped table-bordered dt-responsive clear-center" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <th>จุดติดตั้ง DMA</th>
                                            <th>Remote Name</th>
                                            <th>Device Type</th>
                                            <th>Control Mode</th>
                                            <th>Failure Mode</th>
                                            <th>Flow (m³)</th>
                                            <th>Pressure (bar)</th>
                                            <th>Last Update</th>
                                            <th style="width: 90px;"></th>
                                            <th style="width: 90px;"></th>
                                            <% if (Convert.ToBoolean(user.UserAdmin))
                                                {  %>
                                            <th style="width: 90px;"></th>
                                            <% }  %>
                                        </tr>
                                    </thead>
                                </table>
                            </div>
                        </div>
                    </div>
                    <!-- /.card-body -->
                </div>
            </div>
            <!-- /.container-fluid -->
        </section>
        <!-- /.content -->
    </div>
    <!-- /.content-wrapper -->
    <input type="hidden" id="txtdvtypeid" value="" />
    <input type="hidden" id="typepopup" value="" />
    <input type="hidden" id="txtRow" name="txtRow" value="" />
    <input type="hidden" id="auto_wwcode" value="<%=this.wwcode %>" />
    <input type="hidden" id="auto_dmacode" value="<%=this.dmacode %>" />

    <div class="modal fade" id="Modal_info_valva">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title modal_title_ml" dmacode="">xx</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card card-primary card-outline card-outline-tabs">
                                <div class="card-header p-0 border-bottom-0">
                                    <ul class="nav nav-tabs" id="custom-tabs-five-tab" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" id="custom-tabs-five-overlay-tab" data-toggle="pill" href="#custom-tabs-five-overlay" role="tab" aria-controls="custom-tabs-five-overlay" aria-selected="true">รายละเอียดจุดติดตั้ง : <span class="modal_title_ml"></span></a>
                                        </li>
                                        <%--                  <li class="nav-item">
                    <a class="nav-link" id="custom-tabs-five-overlay-dark-tab" data-toggle="pill" href="#custom-tabs-five-overlay-dark" role="tab" aria-controls="custom-tabs-five-overlay-dark" aria-selected="false">Overlay Dark</a>
                  </li>
                  <li class="nav-item">
                    <a class="nav-link" id="custom-tabs-five-normal-tab" data-toggle="pill" href="#custom-tabs-five-normal" role="tab" aria-controls="custom-tabs-five-normal" aria-selected="false">Normal Tab</a>
                  </li>--%>
                                    </ul>
                                </div>
                                <div class="card-body">
                                    <div class="tab-content" id="custom-tabs-five-tabContent">
                                        <div class="tab-pane fade show active" id="custom-tabs-five-overlay" role="tabpanel" aria-labelledby="custom-tabs-five-overlay-tab">
                                            <div class="overlay-wrapper">
                                                <div class="overlay" id="overlay_wrapper">
                                                    <i class="fas fa-3x fa-sync-alt fa-spin"></i>
                                                    <div class="text-bold pt-2">Loading...</div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div style="width: 100%;">
                                                            <div class="table-responsive">
                                                                <table id="tempinfovalva" class="table table-striped table-bordered table-condensed" width="100%">
                                                                    <tbody>
                                                                        <tr>
                                                                            <th><i class="fa fa-tags"></i>DMA :</th>
                                                                            <td><span id="valve_info_dma">xx</span></td>
                                                                            <th><i class="fa fa-tags"></i>Remote Name :</th>
                                                                            <td><span id="valve_info_remotename">xx</span></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <th><i class="fa fa-tags"></i>Device type :</th>
                                                                            <td><span id="valve_info_devicetype">xx</span></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <th><i class="fa fa-tags"></i>Door Status :</th>
                                                                            <%--<td><input type="checkbox" id="valve_info_door" checked data-bootstrap-switch data-off-color="danger" 
                                                                        data-off-text="ปิด" data-on-color="success" data-on-text="เปิด"></td>--%>
                                                                            <td><span class="badge badgeinfo" id="valve_info_door">xx</span></td>
                                                                            <th><i class="fa fa-tags"></i>Communication Valve :</th>
                                                                            <td><span class="badge badgeinfo" id="valve_info_com">xx</span></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <th><i class="fa fa-tags"></i>PowerLineStatus :</th>
                                                                            <td><span class="badge badgeinfo" id="valve_info_power">xx</span></td>
                                                                            <th><i class="fa fa-tags"></i>Switch Status :</th>
                                                                            <td><span id="valve_info_switch">xx</span></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <th><i class="fa fa-tags"></i>Runing Mode :</th>
                                                                            <td><span class="badge badgeinfo" id="valve_info_runmode">xx</span></td>
                                                                            <th><i class="fa fa-tags"></i>Status :</th>
                                                                            <td><span id="valve_info_status">xx</span></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <th><i class="fa fa-tags"></i>Last Command Date :</th>
                                                                            <td><span id="valve_info_lastupdate">xx</span></td>
                                                                        </tr>
                                                                    </tbody>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--                  <div class="tab-pane fade" id="custom-tabs-five-overlay-dark" role="tabpanel" aria-labelledby="custom-tabs-five-overlay-dark-tab">
                    <div class="overlay-wrapper">
                      <div class="overlay dark"><i class="fas fa-3x fa-sync-alt fa-spin"></i><div class="text-bold pt-2">Loading...</div></div>
                      
                    </div>
                  </div>
                  <div class="tab-pane fade" id="custom-tabs-five-normal" role="tabpanel" aria-labelledby="custom-tabs-five-normal-tab">
                    
                  </div>--%>
                                    </div>
                                </div>
                                <!-- /.card -->
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <%--              <button type="button" class="btn btn-primary">Save changes</button>--%>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">ปิด</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <div class="modal fade" id="Modal_add_valva">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title modal_title_add_valva">xx</h4>

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="loader_content_modal"></div>
                    <div class="tab-content" id="tabs_editvalva">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="row">
                                    <h6 class="modal-title modal_subtital_add_valva"></h6>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12 col-12 col-sm-12">
                                <div class="form-row">
                                    <div class="form-group col-md-6 col-12 col-sm-12">
                                        <label>ประเภทประตูน้ำ :</label>
                                        <select id="m_dvtypeddl" class="form-control select2bs4" data-dropdown-css-class="select2-info" onchange="onchangeddldvtype(this.value)">
                                        </select>
                                    </div>
                                    <div id="_divtotlepilot" class="form-group col-md-6 col-12 col-sm-12">
                                        <label for="totlepilot">จำนวน pilot</label>
                                        <select id="m_totlepilot" class="form-control select2bs4" data-dropdown-css-class="select2-info" onchange="onChangepilotnum(this.value)">
                                            <option value="1" selected>1</option>
                                            <option value="2">2</option>
                                            <option value="3">3</option>
                                            <option value="4">4</option>
                                        </select>

                                        <%--<input value="" type="number" id="m_totlepilot" name="m_totlepilot" class="form-control" onkeypress="return isNumberKey(event)">--%>
                                    </div>

                                </div>
                                <div class="form-row" id="div_pilot_num">
                                    <hr />
                                    <div class="form-group col-md-3">
                                        <input value="" type="number" id="pilot_pressure1" name="pilot_pressure1" min="0.00" class="form-control" onkeypress="return isNumberKey(event)" placeholder="0.00">
                                    </div>
                                    <div class="form-group col-md-3">
                                        <input value="" type="number" id="pilot_pressure2" name="pilot_pressure2" min="0.00" class="form-control" onkeypress="return isNumberKey(event)" placeholder="0.00">
                                    </div>
                                    <div class="form-group col-md-3">
                                        <input value="" type="number" id="pilot_pressure3" name="pilot_pressure3" min="0.00" class="form-control" onkeypress="return isNumberKey(event)" placeholder="0.00">
                                    </div>
                                    <div class="form-group col-md-3">
                                        <input value="" type="number" id="pilot_pressure4" name="pilot_pressure4" min="0.00" class="form-control" onkeypress="return isNumberKey(event)" placeholder="0.00">
                                    </div>
                                    <hr />
                                </div>

                                <div class="form-row">
                                    <div class="form-group col-md-6 col-12 col-sm-12">
                                        <label for="m_controltype">ประเภท Control</label>
                                        <select id="m_controltype" class="form-control select2bs4" data-dropdown-css-class="select2-info">
                                        </select>
                                    </div>
                                    <div class="form-group col-md-6 col-12 col-sm-12">
                                        <label for="m_smartlogger">Smart Logger</label><br />
                                        <%--<input type="checkbox" id="m_smartlogger" style="min-width: 30px; min-height: 30px">--%>
                                        <label class="switch">
                                            <input type="checkbox" id="m_smartlogger">
                                            <span class="slider round"></span>
                                        </label>
                                    </div>
                                </div>
                                <hr />
                                <div class="form-row">
                                    <div class="form-group col-md-6 col-12 col-sm-12">
                                        <h5 for="deadband_pressure"><i class="fas fa-user-check"></i>ผู้แก้ไข</h5>
                                        <label id="m_usereditor">xxx</label>
                                    </div>
                                    <div class="form-group col-md-6 col-12 col-sm-12">
                                        <h5 for="deadband_flow"><i class="fas fa-user-clock"></i>วันที่แก้ไข</h5>
                                        <label id="m_lastupdate">xxx</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer justify-content-between">
                    <button id="save_add_valva" type="button" class="btn btn-success" onclick="add_valva_modal()">บันทึก(Save)</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">ปิด</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->


    <%--<div class="modal fade Modal_edit" id="Modal_edit_bv">
        <div class="modal-dialog modal-xl">
            <div class="modal-content">
                <div id="overlay_modal" class="overlay d-flex justify-content-center align-items-center">
                    <i class="fas fa-2x fa-sync fa-spin"></i>
                </div>
                <div class="modal-header">
                    <h4 class="modal-title modal_title_setting">xx</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="card card-primary card-outline card-outline-tabs">
                                <div class="card-header p-0 border-bottom-0">
                                    <ul class="nav nav-tabs" id="custom-tabs-five-tab" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" id="tab1_bv_normal" data-toggle="pill" href="#tab1_bv" role="tab" aria-controls="tab1_bv" aria-selected="true">Manual Command</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="tab2_bv_normal" data-toggle="pill" href="#tab2_bv" role="tab" aria-controls="tab2_bv" aria-selected="false">Automatic Command</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="tab3_bv_normal" data-toggle="pill" href="#tab3_bv" role="tab" aria-controls="tab3_bv" aria-selected="false">Real Time Data</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="tab4_bv_normal" data-toggle="pill" href="#tab4_bv" role="tab" aria-controls="tab4_bv" aria-selected="false">History Command</a>
                                        </li>
                                    </ul>
                                </div>
                                <div class="card-body">
                                    <div class="tab-content" id="tabs_bv">
                                        <div class="tab-pane fade show active" id="tab1_bv" role="tabpanel" aria-labelledby="tab1_bv">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <span>tab1</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade show" id="tab2_bv" role="tabpanel" aria-labelledby="tab2_bv">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <span>tab2</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade show" id="tab3_bv" role="tabpanel" aria-labelledby="tab3_bv">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <span>tab3</span>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade show" id="tab4_bv" role="tabpanel" aria-labelledby="tab4_bv">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <span>tab4</span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.card -->
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-primary">Save changes</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">ปิด</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->--%>


    <div class="modal fade Modal_edit" id="Modal_edit_bv">
        <div class="modal-dialog modal-dialog-scrollable modal-xl">
            <div class="modal-content">
                <%--                <div  id="overlay_modal" class="overlay d-flex justify-content-center align-items-center">
                    <i class="fas fa-2x fa-sync fa-spin"></i>
                </div>--%>
                <div class="modal-header">
                    <h4 class="modal-title modal_title_setting">xx</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12">
                            <div class="card card-primary card-outline card-outline-tabs">
                                <div class="card-header p-0 border-bottom-0">
                                    <ul class="nav nav-tabs" id="custom-tabs-five-tab-bv" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" id="tab1_bv_normal" data-toggle="pill" href="#tab1_bv" role="tab" aria-controls="tab1_bv" aria-selected="true">Manual Command</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="tab2_bv_normal" data-toggle="pill" href="#tab2_bv" role="tab" aria-controls="tab2_bv" aria-selected="false">Automatic Command</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="tab3_bv_normal" data-toggle="pill" href="#tab3_bv" role="tab" aria-controls="tab3_bv" aria-selected="false">Real Time Data</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="tab4_bv_normal" data-toggle="pill" href="#tab4_bv" role="tab" aria-controls="tab4_bv" aria-selected="false">History Command</a>
                                        </li>
                                    </ul>
                                </div>
                                <%--id="div_content_modal_load"--%>
                                <div class="card-body">
                                    <div class="loader_content_modal"></div>
                                    <div class="tab-content" id="tabs_bv">
                                        <div class="tab-pane fade show active" id="tab1_bv" role="tabpanel" aria-labelledby="tab1_bv">
                                            <div class="row">
                                                <div class="col-md-12 col-12 col-xl-12">
                                                    <h5>ตั้งค่าควบคุม Valve <%=unit_percent %></h5>
                                                    <hr />
                                                    <div id="_Manual_Bv">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade show" id="tab2_bv" role="tabpanel" aria-labelledby="tab2_bv">
                                            <div class="row">
                                                <div class="col-md-12 col-12 col-xl-12">
                                                    <h5>ตั้งค่าควบคุมอัตโนมัติ</h5>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <button type="button" id="btnAdd_bv" class="btn btn-primary btn-flat col-md-2" onclick="insertRow_bv(this.id);"><i class="fas fa-plus"></i>เพิ่ม</button>
                                                            <button type="button" id="btnDelete_bv" class="btn btn-warning btn-flat col-md-2" onclick="delRow_bv(this.id);"><i class="fas fa-trash-alt"></i>ลบ</button>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin-top: 2%;">
                                                        <div class="col-md-12">
                                                            <div style="width: 100%;">
                                                                <div id="_Automatic_Bv">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin-top: 2%;">
                                                        <div class="col-md-12">
                                                            <div class="card">
                                                                <div class="card-header bg-info">
                                                                    <h3 class="card-title">Failure Mode</h3>
                                                                    <div class="card-tools">
                                                                        <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                                                            <i class="fas fa-minus"></i>
                                                                        </button>
                                                                        <%--                                                                        <button type="button" class="btn btn-tool" data-card-widget="remove">
                                                                            <i class="fas fa-times"></i>
                                                                        </button>--%>
                                                                    </div>
                                                                </div>
                                                                <!-- /.card-header -->
                                                                <div class="card-body">
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="form-row">
                                                                                <div class="form-group col-md-6">
                                                                                    <label for="failure_mode">Mode :</label>
                                                                                    <select id="failure_mode" name="failure_mode" class="form-control">
                                                                                        <option value="0">Pressure</option>
                                                                                        <option value="1">Flow</option>
                                                                                        <option value="2">Valve</option>
                                                                                        <option value="4">SAME_Auto_normal</option>
                                                                                    </select>
                                                                                </div>
                                                                                <div class="form-group col-md-6">
                                                                                    <label for="step_control_delay">Step Valve (<%=unit_percent %>):</label>
                                                                                    <input value="" type="text" id="step_control_delay" name="step_control_delay" class="form-control" onkeypress="return isNumberKey(event)">
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-row">
                                                                                <div class="form-group col-md-6">
                                                                                    <label for="time_loop">Time Loop Min (minute) :</label>
                                                                                    <input value="" type="text" id="time_loop" name="time_loop" class="form-control" onkeypress="return isNumberKey(event)">
                                                                                </div>
                                                                                <div class="form-group col-md-6">
                                                                                    <label for="limit_min">Limit Valve Min (<%=unit_percent %>) :</label>
                                                                                    <input value="" type="text" id="limit_min" name="limit_min" class="form-control" onkeypress="return isNumberKey(event)">
                                                                                </div>
                                                                            </div>
                                                                            <div class="form-row">
                                                                                <div class="form-group col-md-6">
                                                                                    <label for="deadband_pressure">Deadband Pressure (bar) :</label>
                                                                                    <input value="" type="text" id="deadband_pressure" name="deadband_pressure" class="form-control" onkeypress="return isNumberKey(event)">
                                                                                </div>
                                                                                <div class="form-group col-md-6">
                                                                                    <label for="deadband_flow">Deadband Flow (m³/hr):</label>
                                                                                    <input value="" type="text" id="deadband_flow" name="deadband_flow" class="form-control" onkeypress="return isNumberKey(event)">
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <!-- /.card-body -->
                                                            </div>
                                                            <!-- /.card -->
                                                        </div>
                                                        <br />
                                                        <button type="button" class="btn btn-primary btn-flat col-md-2" data-toggle="modal" onclick="Popup(0,'auto')"><i class="fa fa-floppy-o"></i>บันทึก</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade show" id="tab3_bv" role="tabpanel" aria-labelledby="tab3_bv">
                                            <div class="row">
                                                <div class="col-md-12 col-12 col-xl-12">
                                                    <h5>Real Time Data</h5>
                                                    <div class="row" style="margin-top: 2%;">
                                                        <div class="col-md-12 col-12">
                                                            <%--<div style="width: 100%;">--%>
                                                            <div id="_Realtime_Bv">
                                                            </div>
                                                            <%--</div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade show" id="tab4_bv" role="tabpanel" aria-labelledby="tab4_bv">
                                            <div class="row">
                                                <div class="col-md-12 col-12 col-xl-12">
                                                    <h5>History Command</h5>
                                                    <div class="row" style="margin-top: 2%;">
                                                        <div class="col-md-12">
                                                            <div style="width: 100%;">
                                                                <div id="_History_bv">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.card -->
                            </div>
                        </div>
                    </div>
                </div>
                <%--                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-primary">Save changes</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">ปิด</button>
                </div>--%>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <div class="modal fade Modal_edit" id="Modal_edit_prv">
        <div class="modal-dialog modal-dialog-scrollable modal-xl">
            <div class="modal-content">
                <%--                <div  id="overlay_modal" class="overlay d-flex justify-content-center align-items-center">
                    <i class="fas fa-2x fa-sync fa-spin"></i>
                </div>--%>
                <div class="modal-header">
                    <h4 class="modal-title modal_title_setting">xx</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>

                <div class="modal-body">

                    <div class="row">
                        <div class="col-md-12">
                            <div class="card card-primary card-outline card-outline-tabs">
                                <div class="card-header p-0 border-bottom-0">
                                    <ul class="nav nav-tabs" id="custom-tabs-five-tab" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" id="tab1_prv_normal" data-toggle="pill" href="#tab1_prv" role="tab" aria-controls="tab1_prv" aria-selected="true">Manual Command</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="tab2_prv_normal" data-toggle="pill" href="#tab2_prv" role="tab" aria-controls="tab2_prv" aria-selected="false">Automatic Command</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="tab3_prv_normal" data-toggle="pill" href="#tab3_prv" role="tab" aria-controls="tab3_prv" aria-selected="false">Real Time Data</a>
                                        </li>
                                        <li class="nav-item">
                                            <a class="nav-link" id="tab4_prv_normal" data-toggle="pill" href="#tab4_prv" role="tab" aria-controls="tab4_prv" aria-selected="false">History Command</a>
                                        </li>
                                    </ul>
                                </div>
                                <%--id="div_content_modal_load"--%>
                                <div class="card-body">
                                    <div class="loader_content_modal"></div>
                                    <div class="tab-content" id="tabs_prv">
                                        <div class="tab-pane fade show active" id="tab1_prv" role="tabpanel" aria-labelledby="tab1_prv">
                                            <div class="row">
                                                <div class="col-md-12 col-12 col-xl-12">
                                                    <h5>ตั้งค่าควบคุม Solenoid</h5>
                                                    <hr />
                                                    <div id="_Manual_PRV">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade show" id="tab2_prv" role="tabpanel" aria-labelledby="tab2_prv">
                                            <div class="row">
                                                <div class="col-md-12 col-12 col-xl-12">
                                                    <h5>ตั้งค่าควบคุมอัตโนมัติ</h5>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <button type="button" id="btnAdd_prv" class="btn btn-primary btn-flat col-md-2" onclick="insertRow(this.id);"><i class="fas fa-plus"></i>เพิ่ม</button>
                                                            <button type="button" id="btnDelete_prv" class="btn btn-warning btn-flat col-md-2" onclick="delRow(this.id);"><i class="fas fa-trash-alt"></i>ลบ</button>
                                                        </div>
                                                    </div>
                                                    <div class="row" style="margin-top: 2%;">
                                                        <div class="col-md-12">
                                                            <div style="width: 100%;">
                                                                <div id="_Automatic_PRV">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade show" id="tab3_prv" role="tabpanel" aria-labelledby="tab3_prv">
                                            <div class="row">
                                                <div class="col-md-12 col-12 col-xl-12">
                                                    <h5>Real Time Data</h5>
                                                    <div class="row" style="margin-top: 2%;">
                                                        <div class="col-md-12 col-12">
                                                            <%--<div style="width: 100%;">--%>
                                                            <div id="_Realtime_PRV">
                                                            </div>
                                                            <%--</div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade show" id="tab4_prv" role="tabpanel" aria-labelledby="tab4_prv">
                                            <div class="row">
                                                <div class="col-md-12 col-12 col-xl-12">
                                                    <h5>History Command</h5>
                                                    <div class="row" style="margin-top: 2%;">
                                                        <div class="col-md-12">
                                                            <div style="width: 100%;">
                                                                <div id="_History_PRV">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- /.card -->
                            </div>
                        </div>
                    </div>
                </div>
                <%--                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-primary">Save changes</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">ปิด</button>
                </div>--%>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->

    <div class="modal fade" id="Modal_warning">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title"></h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <span>ไม่พบข้อมูลจุดติดตั้ง</span>
                        </div>
                    </div>
                </div>
                <div class="modal-footer justify-content-between">
                    <button type="button" class="btn btn-primary">Save changes</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">ปิด</button>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->


    <div class="modal fade" id="aboutModal_save">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title">บันทึก</h4>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="form-horizontal col-lg-12 col-md-6">
                            <div class="row">
                                <div class="list-group">
                                    <div class="list-group-item active">
                                        <i class="fa fa-hourglass-start pull-right" aria-hidden="true">
                                            <label id="counter_s" style="float: right;"></label>
                                        </i>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="row form-group">
                                            <br />
                                            <div class="col-lg-12 form-group">
                                                <div class="col-lg-1"></div>
                                                <div class="col-lg-3">
                                                    <div class="pull-right">
                                                        หมายเหตุ
                                                    </div>
                                                </div>
                                                <div class="col-lg-8 ">
                                                    <input id="save_remark" name="" class="form-control" style="width: 100%" type="text" />
                                                </div>
                                            </div>
                                            <div class="col-lg-12 form-group">
                                                <div class="col-lg-1"></div>
                                                <div class="col-lg-3">
                                                    <div class="pull-right">
                                                        ชื่อผู้บันทึก
                                                    </div>
                                                </div>
                                                <div class="col-lg-8">
                                                    <input id="save_name" name="" class="form-control" style="width: 100%" type="text" value="<%=user.UserNAME.ToString() %>" readonly />
                                                </div>
                                            </div>
                                            <div class="col-lg-12 form-group">
                                                <div class="col-lg-1"></div>
                                                <div class="col-lg-3">
                                                    <div class="pull-right">
                                                        วันเวลาบันทึก
                                                    </div>

                                                </div>
                                                <div class="col-lg-8">
                                                    <input id="save_date" name="" class="form-control" style="width: 100%" type="datetime" value="<%=DateTime.Now.ToString() %>" readonly />
                                                </div>
                                            </div>
                                            <div class="col-lg-12 form-group">
                                                <div class="col-lg-8"></div>
                                                <div class="col-lg-4">

                                                    <button type="button" id="button_save" class="btn btn-info btn-flat" onclick="Modal_save();"><i class="far fa-save"></i>บันทึก (Save)</button>

                                                    <%--<a id="button_save" role="button" class="btn btn-primary" href="#" onclick="Modal_save();">
                                                        <i class="fas fa-plus"></i>
                                                        
                                                        <div style="color: white">                                                        
                                                            บันทึก  <i class="fa fa-floppy-o"></i>
                                                        </div>
                                                    </a>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <%--                    <button type="button" class="btn btn-primary">Save changes</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">ปิด</button>--%>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->


    <!-- Bootstrap Switch -->
    <script src="plugins/bootstrap-switch/js/bootstrap-switch.min.js"></script>
    <script src="Models/DatableControl.js"></script>
    <script src="Models/saveControl.js"></script>
    <script type="text/javascript">

    </script>
</asp:Content>
