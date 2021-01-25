<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="controldma.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link rel="icon" href="images/logo_large.png">
    <title>PWA Control</title>
    <!--===============================================================================================-->
	    <link rel="stylesheet" type="text/css" href="dist/css/adminlte.min.css">
    <!--===============================================================================================-->
	    <link rel="stylesheet" type="text/css" href="plugins/fontawesome-free/css/fontawesome.min.css">
    <!--===============================================================================================-->
	    <link rel="stylesheet" type="text/css" href="plugins/fonts/iconic/css/material-design-iconic-font.min.css">
    <!--===============================================================================================-->
	    <link rel="stylesheet" type="text/css" href="plugins/login/animate/animate.css">
    <!--===============================================================================================-->	
	    <link rel="stylesheet" type="text/css" href="plugins/login/css-hamburgers/hamburgers.min.css">
    <!--===============================================================================================-->
	    <link rel="stylesheet" type="text/css" href="plugins/login/animsition/css/animsition.min.css">
    <!--===============================================================================================-->
<%--	    <link rel="stylesheet" type="text/css" href="vendor/select2/select2.min.css">--%>
    <!--===============================================================================================-->	
	    <link rel="stylesheet" type="text/css" href="plugins/login/daterangepicker/daterangepicker.css">
    <!--===============================================================================================-->
	    <link rel="stylesheet" type="text/css" href="plugins/login/css/util.css">
	    <link rel="stylesheet" type="text/css" href="plugins/login/css/main.css">
    <!--===============================================================================================-->
        <link rel="stylesheet" href="plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css" />
    <!--===============================================================================================-->
        <link rel="stylesheet" href="plugins/toastr/toastr.min.css" />
    <!--===============================================================================================-->
</head>
<body>
    <div class="limiter">
		<div class="container-login100" style="background-image: url('images/bg.jpg');">
			<div class="wrap-login100">
				<form class="login100-form validate-form" runat="server">
					<span class="login100-form-logo" style="height: inherit;">
						<%--<i class="zmdi zmdi-landscape"></i>--%>
                        <img src="images/logo_large.png" />
					</span>
					<span class="login100-form-title p-b-34 p-t-27">
						DMA Control
                        <p style="color:#ffffff;">งานพัฒนาเทคโนโลยีสารสนเทศลดน้ำสูญเสีย</p>
					</span>
                    <p></p>              
					<div class="wrap-input100 validate-input" data-validate = "Enter username">
						<input class="input100" type="text" id="l_username" name="l_username" runat="server" placeholder="Username">
						<span class="focus-input100" data-placeholder="&#xf207;"></span>
					</div>

					<div class="wrap-input100 validate-input" data-validate="Enter password">
						<input class="input100" type="password" id="l_pass" name="l_pass" runat="server" placeholder="Password">
						<span class="focus-input100" data-placeholder="&#xf191;"></span>
					</div>

					<div class="contact100-form-checkbox">
						<input class="input-checkbox100" id="ckb1" type="checkbox" name="remember-me">
						<label class="label-checkbox100" for="ckb1">
							Remember me
						</label>
					</div>
                    
					<div class="container-login100-form-btn">
						<button type="button" class="login100-form-btn" id="btnLogin">
							เข้าสู่ระบบ
						</button>
                        <asp:Button runat="server" ID="btn_Login" OnClick="btn_Login_Click" Style="display: none;" />       
					</div>
<%--					<div class="text-center p-t-90">
						<a class="txt1" href="#">
							Forgot Password?
						</a>
					</div>--%>
				</form>
			</div>
		</div>
	</div>
	<div id="dropDownSelect1"></div>
    
    
    <script src="plugins/jquery/jquery.min.js"></script>
    <script src="plugins/jquery-validation/jquery.validate.min.js"></script>
    <script src="plugins/login/animsition/js/animsition.min.js"></script>
    <script src="plugins/popper/popper.min.js"></script>
    <script src="plugins/login/daterangepicker/moment.min.js"></script>
    <script src="plugins/login/daterangepicker/daterangepicker.js"></script>
    <script src="plugins/login/js/main.js"></script>
    <script src="plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="plugins/sweetalert2/sweetalert2.min.js"></script>
    <script src="plugins/toastr/toastr.min.js"></script>
    <script src="Models/SweetAlert2.js"></script>
    <script type="text/javascript">
        $(document).ready(() => {
            $('#btnLogin').click(function () {
                $('#btn_Login').click();
            })
        })
    </script>
    <asp:Literal ID="litMsg" runat="server"></asp:Literal>
 </body>
</html>
