<%@ Page Title="استمارة طالب" Language="vb" AutoEventWireup="false" CodeBehind="Report_Student.aspx.vb" Inherits="Evening_Study.Report_Student" %>
<!DOCTYPE html>
<html lang="en"  dir="rtl">

<head id="Head1" runat ="server" >
    <title>تقديم الطلبة</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="css\style.css" rel="stylesheet" type ="text/css"  />
    <link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap.min.css"> 
    <link rel="stylesheet" type="text/css" href="fonts/font-awesome-4.7.0/css/font-awesome.min.css"> 
    <link rel="stylesheet" type="text/css" href="vendor/animate/animate.css">
    <link rel="stylesheet" type="text/css" href="vendor/select2/select2.min1000.css"> 
    <link rel="stylesheet" type="text/css" href="vendor/perfect-scrollbar/perfect-scrollbar1000.css">
    <link rel="stylesheet" type="text/css" href="css/util1000.css">
    <link rel="stylesheet" type="text/css" href="css/main1000.css">
    <link rel="stylesheet" type="text/css" href="css/main.css">
    <link rel="stylesheet" href="css/ResponsiveTable1000.css">           
    <link rel="icon" type="image/png" href="../images/icons/ico.png">
    <link rel="stylesheet" type="text/css" href="fonts/Linearicons-Free-v1.0.0/icon-font.min.css">	
    <link rel="stylesheet" type="text/css" href="vendor/css-hamburgers/hamburgers.min.css">
    <link rel="stylesheet" type="text/css" href="vendor/animsition/css/animsition.min.css">	
    <link rel="stylesheet" type="text/css" href="vendor/daterangepicker/daterangepicker.css">
    <link rel="stylesheet" href="css/Popup.css">        
    <link rel="stylesheet" href="css/Login_style-Haider.css">    
    <style type ="text/css">
        @font-face {
        font-family: "MySpecialFont";
        src:url(js/AIRSTRIPARABIC.TTF);
}
        .SpecialFont {
            font-family:"myspecialfont";
        }

    </style>
    <style>
        @media (max-width: 992px) {
            .wrap-input100 {
                height: auto!important;
            }
            #Label1 {
                font-size:22px!important;
            }
            #Header_LBL {
                font-size:20px!important;
            }
            .form {
                padding:0!important;
                height:auto!important;
                zoom: 100%!important;
            }
            img {
                visibility:hidden!important;
            }
            #specialDIV {
                height:auto!important;
            }
            body {
                
                width:100%!important;
            }
            .contact100-form {
                width: 100%!important;
                display: block!important;
                padding: 10px 20px 10px 20px!important;
            }
            input[type='submit'] {
                width:90%!important;
                margin-bottom:10px!important;
            }
            label {
                font-size:16px!important;
            }
        }
     </style>
</head>
   


<body class ="body" style="background:url(../img/logo10.png);background-size:cover;">
<form id="Form1"  runat ="server" >
    
<div class="form" style ="height:100px;width:100%;position:relative;margin-top:0px">
    <div class="forceColor"></div>
    <div class="topbar">
        <asp:Image ID ="Image1" runat ="server" ImageUrl ="~/img/logo_old.png"  Width ="100px" Height ="80%" style="position:absolute; "/>
    </div> 
    <center>        
      <h1><asp:Label ID="Label1" runat="server" style="text-shadow :2px 2px 2px white;font-family:'myspecialfont';color: black;"  CssClass ="HeaderLogin SpecialFont">إستمارة المتقدمين للدراسة المسائية</asp:Label></h1>
      <h3><asp:Label ID="Header_LBL" runat="server" style="text-shadow :2px 2px 2px black;font-family:'myspecialfont';color: red;"  CssClass ="HeaderLogin SpecialFont">الجامعة المستنصرية / مركز الحاسبة الألكترونية</asp:Label></h3>
    </center>
</div>
    
 <script src="js/jquery-1.7.2.min.js"></script>
 <script src="js/Popup.js"></script>

	<div>
        <div >
            <div style="width:  100%;padding-right: 4%;padding-left: 4%;padding-top:  20px;">
                <div>
                    <div style="padding-right: 0px;padding-left: 0px;padding-bottom: 20px; border :0">
					    <asp:TextBox ID="ErrorBox"  Font-Size ="10" Enabled ="false"  Height ="50px" style="color:Red;background-color:Transparent;border-width:2px;border-style:Solid;font-size:10pt;font-weight:bold;color: #ff0000;background-color:#fff;border-width:2px;border-style:Solid;font-size:10pt;font-weight:bold;padding-bottom :15px;resize :none;border-radius: 11px;overflow:  hidden;margin-top:  -10px;margin-bottom:  -20px;" Visible ="false"  Font-Bold ="true"  BorderStyle ="Solid" BorderWidth ="2" ForeColor ="Red" BackColor ="Transparent"    runat ="server"  MaxLength ="1000" textmode="MultiLine"  CssClass ="input100"></asp:TextBox>
				    </div>
                </div>
            </div>

            <div class="contact100-form" >				
				<div class="row">

                    <div class="col-xs-12 col-sm-6 col-md-6">
						<div class="wrap-input100 validate-input" style="background:rgba(255, 147, 10, 0.3)" data-validate="رقم الإستمارة مطلوب">
					        <label  class="label-input100" for="name" style ="color:black; text-shadow:none; font-size :14px">رقم الإستمارة؟</label>
					        <asp:TextBox ID="sCode"  runat ="server" style="direction:ltr ! important;  text-align:left;font-family:'Droid Arabic Naskh';font-size:16px" placeholder="أدخل رقم الإستمارة" MaxLength ="50" textmode="SingleLine" CssClass ="input100"></asp:TextBox>
                            
					        <span class="focus-input100"></span>
				        </div>
					</div>
                                        
                    <div class="col-xs-12 col-sm-6 col-md-6">
						<div class="wrap-input100" style="background:rgba(255, 147, 10, 0.3);height: 75px">
                            
                            <div class="container-contact100-form-btn" style="padding-top:  12px;background-color:transparent  ;border:  none">
                                <asp:Button runat ="server" ID ="Print_Btn" Enabled ="true" OnClick="Print_Btn_Click" Text ="تحميل الإستمارة" CssClass ="contact100-form-btn-save" style="font-size:20px;font-weight :bold;width :75%" />
                                <a style="width :2.5%"></a>
                                <asp:Button runat ="server" ID ="Cancel_Btn" OnClick="Cancel_Btn_Click" Text ="رجوع" CssClass ="contact100-form-btn-Seacrh" style="font-size:17px;font-weight :bold;width:15%;" />
		                    </div>

						</div>
					</div>
                                                                                                   
                </div>

	
            </div>
		</div>
	    
	<script src="vendor/jquery/jquery-3.2.1.min.js"></script>
	<script src="vendor/animsition/js/animsition.min.js"></script>
	<script src="vendor/bootstrap/js/popper.js"></script>
	<script src="vendor/bootstrap/js/bootstrap.min.js"></script>
    <script src="js/main1000.js"> </script>
	<script src="vendor/select2/select2.min.js"></script>
	<script>
	    $(".js-select2").each(function () {
	        $(this).select2({
	            minimumResultsForSearch: 20,
	            dropdownParent: $(this).next('.dropDownSelect2')
	        });
	    })
	    $(".js-select2").each(function () {
	        $(this).on('select2:open', function (e) {
	            $(this).parent().next().addClass('eff-focus-selection');
	        });
	    });
	    $(".js-select2").each(function () {
	        $(this).on('select2:close', function (e) {
	            $(this).parent().next().removeClass('eff-focus-selection');
	        });
	    });

	</script>


	<script src="vendor/daterangepicker/moment.min.js"></script>
	<script src="vendor/daterangepicker/daterangepicker.js"></script>

	<script src="vendor/countdowntime/countdowntime.js"></script>

	<script src="js/main.js"></script>
	<!-- Global site tag (gtag.js) - Google Analytics -->
	
	<script>
	    window.dataLayer = window.dataLayer || [];
	    function gtag() { dataLayer.push(arguments); }
	    gtag('js', new Date());

	    gtag('config', 'UA-23581568-13');
	</script>
</div>
</form>
</body>
</html>
