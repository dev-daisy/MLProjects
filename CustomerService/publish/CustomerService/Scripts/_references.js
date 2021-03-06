/// <autosync enabled="true" />
/// <reference path="esm/popper-utils.js" />
/// <reference path="esm/popper.js" />
/// <reference path="umd/popper-utils.js" />
/// <reference path="umd/popper.js" />
/// <reference path="bootstrap.js" />
/// <reference path="jquery.ui.map.js" />
/// <reference path="jquery.unobtrusive-ajax.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />
/// <reference path="jquery-3.1.0.js" />
/// <reference path="jquery-3.1.0.slim.js" />
/// <reference path="modernizr-2.8.3.js" />
/// <reference path="canvasjs.js" />
/// <reference path="jquery-ui.js" />

$(document).ready(function () {
   
    hidemodal();
    function hidemodal(){
        $("#modal").hide();
        $("#custinformation").hide();
        $("#category").hide();
        $("#graph").hide();
        $("#loader").hide();
        $("#loader1").hide();
        $("#txnsummary").hide();
        $("#myModal").hide();
    }

    $("#divsearch").show();

    $('#search').click(function () {
        pop('loader');
        var fname = $('#custfname').val();
        var lname = $('#custlname').val();
        var name = fname + " " + lname;
        //if (document.getElementById('radiosummary').checked) {
        //    getdata();
        //}
        //if (document.getElementById('radiohistory').checked) {
        //    searchcust(name);
        //}
        searchcust(name);

    });


    $('input[type="radio"]').on('click change', function (e) {
        hidemodal();

        if ($(this).val() == 'summary') {
            getdata();
        }
        else if ($(this).val() == 'inserttxn') {
            
            pop('category');
        }
        else {
            $("#msg").empty();
            $("#divsearch").show();
        }
    });

    function getdata() {
        hidemodal();

        var name = $('#custname').val();
            pop('loader');

            var kpsocount = 0;
            var kppocount = 0;
            var kprfccount = 0;
            var kprtscount = 0;
            var kpcsocount = 0;
            var kpcpocount = 0;
            var walletsocount = 0;
            var walletpocount = 0;
            var walletrfccount = 0;
            var walletrtscount = 0;
            var walletcsocount = 0;
            var walletcpocount = 0;
            var walletbpcount = 0;
            var walleteloadcount = 0;
            var walletcorppocount = 0;
            var expresssocount = 0;
            var expresspocount = 0;
            var expressrfccount = 0;
            var expressrtscount = 0;
            var expresscsocount = 0;
            var expresscpocount = 0;
            var expressbpcount = 0;
            var expresseloadcount = 0;
            var expresscorppocount = 0;
            var mlgsocount = 0;
            var mlgpocount = 0;
            var mlgrfccount = 0;
            var mlgrtscount = 0;
            var mlgcsocount = 0;
            var mlgcpocount = 0;
            var apipocount = 0;
            var apicpocount = 0;
            var bpsocount = 0;
            var bprfccount = 0;
            var bpcsocount = 0;
            var fusocount = 0;
            var fupocount = 0;
            var furfccount = 0;
            var furtscount = 0;
            var fucsocount = 0;
            var fucpocount = 0;
            var wscsocount = 0;
            var wscpocount = 0;
            var wscrfccount = 0;
            var wscrtscount = 0;
            var wsccsocount = 0;
            var wsccpocount = 0;
            var qclprendacount = 0;
            var qcllukatcount = 0;
            var qclrenewcount = 0;
            var qclreappraisecount = 0;
            var layawaycount = 0;
            var salescount = 0;
            var tradeincount = 0;
            var sblcount = 0;
            var eloadcount = 0;
            var insurancecount = 0;
            var goodscount = 0;

            $.ajax({
                type: "POST",
                url: "CustomerService/SearchCustomers",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ 'custname': name }),
                cache: false,
                dataType: "json",
                success: function (data) {
                    pop('myModal');
                    $("#msg").hide();
                    if (data == "Empty") {
                        $("#msg").show();
                        document.getElementById("msg").innerHTML = "No Record Found!";
                        hide('loader');
                    }
                    else if (data == "Error") {
                        $("#msg").show();
                        document.getElementById("msg").innerHTML = "Unable to process request. The system encountered some technical problem. Sorry for the inconvenience. Please contact admin.";
                        hide('loader');
                    }
                    else {
                        $("#customersummary").show();
                        $("#chartContainer").show();

                        var table = "<table id='customers'>";
                        table += "<thead>";
                        table += "<tr>";
                        table += "<th rowspan='3'><br/></th>";
                        table += "<th colspan='6' rowspan='2'>CUSTOMER INFORMATION</th>";
                        table += "<th colspan='30'>MONEY TRANSFER</th>";
                        table += "<th colspan='17'>CORPORATE</th>";
                        table += "<th colspan='7'>BOS</th>";
                        table += "<th colspan='4' rowspan='2'>ISPD</th>";
                        table += "</tr>";
                        table += "<tr>";
                        //money transfer
                        table += "<th colspan='6'>Kwarta Padala</th>";
                        table += "<th colspan='9'>Wallet</th>";
                        table += "<th colspan='9'>Express</th>";
                        table += "<th colspan='6'>Global</th>";
                        //corporate
                        table += "<th colspan='2'>Partner</th>";
                        table += "<th colspan='6'>File Upload</th>";
                        table += "<th colspan='6'>WSC</th>";
                        table += "<th colspan='3'>Billspay</th>";
                        //bos
                        table += "<th colspan='4'>QCL</th>";                        
                        table += "<th colspan='3'>Jewellers</th>";
                        table += "</tr>";

                        table += "<tr>";
                        table += "<th>Customer ID</th>";
                        table += "<th>Last Name</th>";
                        table += "<th>First Name</th>";
                        table += "<th>Middle Name</th>";
                        table += "<th>Birth Date</th>";
                        table += "<th>Gender</th>";

                        //kp
                        table += "<th>SO</th>";
                        table += "<th>PO</th>";
                        table += "<th>RFC</th>";
                        table += "<th>RTS</th>";
                        table += "<th>CSO</th>";
                        table += "<th>CPO</th>";
                        //wallet
                        table += "<th>SO</th>";
                        table += "<th>PO</th>";
                        table += "<th>RFC</th>";
                        table += "<th>RTS</th>";
                        table += "<th>CSO</th>";
                        table += "<th>CPO</th>";
                        table += "<th>Billspay</th>";
                        table += "<th>Eload</th>";
                        table += "<th>Corp PO</th>";
                        //express
                        table += "<th>SO</th>";
                        table += "<th>PO</th>";
                        table += "<th>RFC</th>";
                        table += "<th>RTS</th>";
                        table += "<th>CSO</th>";
                        table += "<th>CPO</th>";
                        table += "<th>Billspay</th>";
                        table += "<th>Eload</th>";
                        table += "<th>Corp PO</th>";
                        //global
                        table += "<th>SO</th>";
                        table += "<th>PO</th>";
                        table += "<th>RFC</th>";
                        table += "<th>RTS</th>";
                        table += "<th>CSO</th>";
                        table += "<th>CPO</th>";
                        //api
                        table += "<th>PO</th>";
                        table += "<th>CPO</th>";
                        //fu
                        table += "<th>SO</th>";
                        table += "<th>PO</th>";
                        table += "<th>RFC</th>";
                        table += "<th>RTS</th>";
                        table += "<th>CSO</th>";
                        table += "<th>CPO</th>";
                        //wsc
                        table += "<th>SO</th>";
                        table += "<th>PO</th>";
                        table += "<th>RFC</th>";
                        table += "<th>RTS</th>";
                        table += "<th>CSO</th>";
                        table += "<th>CPO</th>";
                        //bp
                        table += "<th>SO</th>";
                        table += "<th>RFC</th>";
                        table += "<th>CSO</th>";
                        //qcl
                        table += "<th>Prenda</th>";
                        table += "<th>Lukat</th>";
                        table += "<th>Renew</th>";
                        table += "<th>Reappraise</th>";
                        //jewellers
                        table += "<th>Lay Away</th>";
                        table += "<th>Sales</th>";
                        table += "<th>Trade In</th>";
                        //ispd
                        table += "<th>SBL</th>";
                        table += "<th>Eload</th>";
                        table += "<th>Insurance</th>";
                        table += "<th>Goods</th>";
                        
                        table += "</tr>";
                        table += "</thead>";
                        table += "<tbody>";
                        $.each(data, function (index, value) {
                            table += "<tr>";
                            table += "<td><a href='#'  id='graphid'>View</a></td>";
                            table += "<td style='text-align:left'>" + value.custID + "</td>";
                            table += "<td style='text-align:left'>" + value.lastName + "</td>";
                            table += "<td style='text-align:left'>" + value.firstName + "</td>";
                            table += "<td style='text-align:left'>" + value.middleName + "</td>";
                            table+= "<td>" + value.birthDate + "</td>";
                            table += "<td style='text-align:left'>" + value.gender + "</td>";
                            table+= "<td>" + value.kpsocount + "</td>";
                            table+= "<td>" + value.kppocount + "</td>";
                            table+= "<td>" + value.kprfccount + "</td>";
                            table+= "<td>" + value.kprtscount + "</td>";
                            table+= "<td>" + value.kpcsocount + "</td>";
                            table+= "<td>" + value.kpcpocount + "</td>";
                           
                            table+= "<td>" + value.walletsocount + "</td>";
                            table+= "<td>" + value.walletpocount + "</td>";
                            table+= "<td>" + value.walletrfccount + "</td>";
                            table+= "<td>" + value.walletrtscount + "</td>";
                            table+= "<td>" + value.walletcsocount + "</td>";
                            table+= "<td>" + value.walletcpocount + "</td>";
                            table+= "<td>" + value.walletbpcount + "</td>";
                            table+= "<td>" + value.walleteloadcount + "</td>";
                            table+= "<td>" + value.walletcorppocount + "</td>";

                            table+= "<td>" + value.expresssocount + "</td>";
                            table+= "<td>" + value.expresspocount + "</td>";
                            table+= "<td>" + value.expressrfccount + "</td>";
                            table+= "<td>" + value.expressrtscount + "</td>";
                            table+= "<td>" + value.expresscsocount + "</td>";
                            table+= "<td>" + value.expresscpocount + "</td>";
                            table+= "<td>" + value.expressbpcount + "</td>";
                            table+= "<td>" + value.expresseloadcount + "</td>";
                            table+= "<td>" + value.expresscorppocount + "</td>";

                            table+= "<td>" + value.globalsocount + "</td>";
                            table+= "<td>" + value.globalpocount + "</td>";
                            table+= "<td>" + value.globalrfccount + "</td>";
                            table+= "<td>" + value.globalrtscount + "</td>";
                            table+= "<td>" + value.globalcsocount + "</td>";
                            table+= "<td>" + value.globalcpocount + "</td>";

                            table+= "<td>" + value.apipocount + "</td>";
                            table+= "<td>" + value.apicpocount + "</td>";

                            table+= "<td>" + value.fusocount + "</td>";
                            table+= "<td>" + value.fupocount + "</td>";
                            table+= "<td>" + value.furfccount + "</td>";
                            table+= "<td>" + value.furtscount + "</td>";
                            table+= "<td>" + value.fucsocount + "</td>";
                            table+= "<td>" + value.fucpocount + "</td>";

                            table+= "<td>" + value.wscsocount + "</td>";
                            table+= "<td>" + value.wscpocount + "</td>";
                            table+= "<td>" + value.wscrfccount + "</td>";
                            table+= "<td>" + value.wscrtscount + "</td>";
                            table+= "<td>" + value.wsccsocount + "</td>";
                            table+= "<td>" + value.wsccpocount + "</td>";

                            table += "<td>" + value.bpsocount + "</td>";
                            table += "<td>" + value.bprfccount + "</td>";
                            table += "<td>" + value.bpcsocount + "</td>";

                            table += "<td>" + value.prendacount + "</td>";
                            table+= "<td>" + value.lukatcount + "</td>";
                            table+= "<td>" + value.renewcount + "</td>";
                            table+= "<td>" + value.reappraisecount + "</td>";

                            table+= "<td>" + value.layawaycount + "</td>";
                            table+= "<td>" + value.salescount + "</td>";
                            table+= "<td>" + value.tradeincount + "</td>";

                            table+= "<td>" + value.sblcount + "</td>";
                            table+= "<td>" + value.eloadcount + "</td>";
                            table+= "<td>" + value.insurancecount + "</td>";
                            table+= "<td>" + value.goodscount + "</td>";
                            table += "</tr>";

                            kpsocount = kpsocount + parseInt(value.kpsocount);
                            kppocount = kppocount + parseInt(value.kppocount);
                            kprfccount = kprfccount + parseInt(value.kprfccount);
                            kprtscount = kprtscount + parseInt(value.kprtscount);
                            kpcsocount = kpcsocount + parseInt(value.kpcsocount);
                            kpcpocount = kpcpocount + parseInt(value.kpcpocount);

                            walletsocount = walletsocount + parseInt(value.walletsocount);
                            walletpocount = walletpocount + parseInt(value.walletpocount);
                            walletrfccount = walletrfccount + parseInt(value.walletrfccount);
                            walletrtscount = walletrtscount + parseInt(value.walletrtscount);
                            walletcsocount = walletcsocount + parseInt(value.walletcsocount);
                            walletcpocount = walletcpocount + parseInt(value.walletcpocount);
                            walletbpcount = walletbpcount + parseInt(value.walletbpcount);
                            walleteloadcount = walleteloadcount + parseInt(value.walleteloadcount);
                            walletcorppocount = walletcorppocount + parseInt(value.walletcorppocount);

                            expresssocount = expresssocount + parseInt(value.expresssocount);
                            expresspocount = expresspocount + parseInt(value.expresspocount);
                            expressrfccount = expressrfccount + parseInt(value.expressrfccount);
                            expressrtscount = expressrtscount + parseInt(value.expressrtscount);
                            expresscsocount = expresscsocount + parseInt(value.expresscsocount);
                            expresscpocount = expresscpocount + parseInt(value.expresscpocount);
                            expressbpcount = expressbpcount + parseInt(value.expressbpcount);
                            expresseloadcount = expresseloadcount + parseInt(value.expresseloadcount);
                            expresscorppocount = expresscorppocount + parseInt(value.expresscorppocount);

                            mlgsocount = mlgsocount + parseInt(value.globalsocount);
                            mlgpocount = mlgpocount + parseInt(value.globalpocount);
                            mlgrfccount = mlgrfccount + parseInt(value.globalrfccount);
                            mlgrtscount = mlgrtscount + parseInt(value.globalrtscount);
                            mlgcsocount = mlgcsocount + parseInt(value.globalcsocount);
                            mlgcpocount = mlgcpocount + parseInt(value.globalcpocount);

                            apipocount = apipocount + parseInt(value.apipocount);
                            apicpocount = apicpocount + parseInt(value.apipcpocount);

                            bpsocount = bpsocount + parseInt(value.bpsocount);
                            bprfccount = bprfccount + parseInt(value.bprfccount);
                            bpcsocount = bpcsocount + parseInt(value.bpcsocount);

                            fusocount = fusocount + parseInt(value.fusocount);
                            fupocount = fupocount + parseInt(value.fupocount);
                            furfccount = furfccount + parseInt(value.furfccount);
                            furtscount = furtscount + parseInt(value.furtscount);
                            fucsocount = fucsocount + parseInt(value.fucsocount);
                            fucpocount = fucpocount + parseInt(value.fucpocount);

                            wscsocount = wscsocount + parseInt(value.wscsocount);
                            wscpocount = wscpocount + parseInt(value.wscpocount);
                            wscrfccount = wscrfccount + parseInt(value.wscrfccount);
                            wscrtscount = wscrtscount + parseInt(value.wscrtscount);
                            wsccsocount = wsccsocount + parseInt(value.wsccsocount);
                            wsccpocount = wsccpocount + parseInt(value.wsccpocount);

                            qclprendacount = qclprendacount + parseInt(value.prendacount);
                            qcllukatcount = qcllukatcount + parseInt(value.lukatcount);
                            qclrenewcount = qclrenewcount + parseInt(value.renewcount);
                            qclreappraisecount = qclreappraisecount + parseInt(value.reappraisecount);

                            layawaycount = layawaycount + parseInt(value.layawaycount);
                            salescount = salescount + parseInt(value.salescount);
                            tradeincount = tradeincount + parseInt(value.tradeincount);

                            sblcount = sblcount + parseInt(value.sblcount);
                            eloadcount = eloadcount + parseInt(value.eloadcount);
                            insurancecount = insurancecount + parseInt(value.insurancecount);
                            goodscount = goodscount + parseInt(value.goodscount);
                        });

                        table += "</tbody></table>";
                        $("#customersummary").html(table);

                        var chart = new CanvasJS.Chart("chartContainer", {
                            animationEnabled: true,
                            theme: "light2", // "light1", "light2", "dark1", "dark2"
                            title: {
                                text: "Distribution Per Service"
                            },
                            axisY: {
                                title: "Transaction Count"
                            },
                            data: [{
                                type: "column",
                                //showInLegend: true,
                                //legendMarkerColor: "grey",
                                //legendText: "MMbbl = one million barrels",
                                dataPoints: [
                                    { y: kpsocount, label: "Domestic SO" },
                                    { y: kppocount, label: "Domestic PO" },
                                    { y: kprfccount, label: "Domestic RFC" },
                                    { y: kprtscount, label: "Domestic RTS" },
                                    { y: kpcsocount, label: "Domestic CSO" },
                                    { y: kpcpocount, label: "Domestic CPO" },
                                    { y: walletsocount, label: "Wallet SO" },
                                    { y: walletpocount, label: "Wallet PO" },
                                    { y: walletrfccount, label: "Wallet RFC" },
                                    { y: walletrtscount, label: "Wallet RTS" },
                                    { y: walletcsocount, label: "Wallet CSO" },
                                    { y: walletcpocount, label: "Wallet CPO" },
                                    { y: walletbpcount, label: "Wallet Billspay" },
                                    { y: walleteloadcount, label: "Wallet Eload" },
                                    { y: walletcorppocount, label: "Wallet Corp PO" },
                                    { y: expresssocount, label: "Express SO" },
                                    { y: expresspocount, label: "Express PO" },
                                    { y: expressrfccount, label: "Express RFC" },
                                    { y: expressrtscount, label: "Express RTS" },
                                    { y: expresscsocount, label: "Express CSO" },
                                    { y: expresscpocount, label: "Express CPO" },
                                    { y: expressbpcount, label: "Express Billspay" },
                                    { y: expresseloadcount, label: "Express Eload" },
                                    { y: expresscorppocount, label: "Express Corp PO" },

                                    { y: mlgsocount, label: "Global SO" },
                                    { y: mlgpocount, label: "Global PO" },
                                    { y: mlgrfccount, label: "Global RFC" },
                                    { y: mlgrtscount, label: "Global RTS" },
                                    { y: mlgcsocount, label: "Global CSO" },
                                    { y: mlgcpocount, label: "Global CPO" },

                                    { y: apipocount, label: "API PO" },
                                    { y: apipocount, label: "API CPO" },
                                     { y: fusocount, label: "Fileupload SO" },
                                    { y: fupocount, label: "Fileupload PO" },
                                    { y: furfccount, label: "Fileupload RFC" },
                                    { y: furtscount, label: "Fileupload RTS" },
                                    { y: fucsocount, label: "Fileupload CSO" },
                                    { y: fucpocount, label: "Fileupload CPO" },
                                     { y: wscsocount, label: "WSC SO" },
                                    { y: wscpocount, label: "WSC PO" },
                                    { y: wscrfccount, label: "WSC RFC" },
                                    { y: wscrtscount, label: "WSC RTS" },
                                    { y: wsccsocount, label: "WSC CSO" },
                                    { y: wsccpocount, label: "WSC CPO" },
                                    { y: bpsocount, label: "Billspay SO" },
                                    { y: bprfccount, label: "Billspay RFC" },
                                    { y: bpcsocount, label: "Billspay CSO" },

                                    { y: qclprendacount, label: "Prenda" },
                                    { y: qcllukatcount, label: "Lukat" },
                                    { y: qclrenewcount, label: "Renew" },
                                    { y: qclreappraisecount, label: "Reappraise" },
                                ]
                            }]
                        });

                        chart.render();

                        hide('loader');
                    }
                },
                error: function (result) {
                    $("#msg").show();
                    $("#customersummary").hide();

                    //document.getElementById("msg").innerHTML = result.stringify();
                    document.getElementById("msg").innerHTML = "Unable to process request. The system encountered some technical problem. Sorry for the inconvenience. Please contact admin.";
                    hide('loader');
                }
            }); 
    }

    function searchcust(name) {
        $.ajax({
            type: "POST",
            url: "CustomerService/SearchCustomers",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ "custname": name }),
            cache: false,
            dataType: "json",
            success: function (data) {
                pop('myModal');
                $("#msg").hide();
                if (data == "Empty") {
                    $("#msg").show();
                    document.getElementById("msg").innerHTML = "No Record Found!";
                    hide('loader');
                }
                else if (data == "Error") {
                    $("#msg").show();
                    document.getElementById("msg").innerHTML = "Unable to process request. The system encountered some technical problem. Sorry for the inconvenience. Please contact admin.";
                    hide('loader');
                }
                else {
                    $("#customerinfo").show();
                    var table = "<table id='customers'>";
                    table += "<thead>";
                    table += "<tr style='font-weight:bold'>";
                    table += "<th>Information</th>";
                    table += "<th>Transaction</th>";
                    table += "<th>Customer ID</th>";
                    table += "<th>User Name</th>";
                    table += "<th>Wallet No.</th>";
                    table += "<th>Last Name</th>";
                    table += "<th>First Name</th>";
                    table += "<th>Middle Name</th>";
                    table += "<th>Birth Date</th>";
                    table += "<th>Gender</th>";
                    //table += "<th>Address</th>";
                    table += "</tr>";
                    table += "<thead>";
                    table += "<tbody>";
                    $.each(data, function (index, value) {
                        var param = "gethistory('" + value.custID.replace(" ", "_") + "','" + value.userName.replace(" ", "_") + "','" + value.walletNo.replace(" ", "_") + "','" + value.lastName.replace(" ", "_") + "','" + value.firstName.replace(" ", "_") + "','" + value.middleName.replace(" ", "_") + "')";
                        var param1 = "getcustinfo('" + value.custID.replace(" ", "_") + "','" + value.userName.replace(" ", "_") + "','" + value.walletNo.replace(" ", "_") + "','" + value.lastName.replace(" ", "_") + "','" + value.firstName.replace(" ", "_") + "','" + value.middleName.replace(" ", "_") + "')";
                        //var param2 = "getgraph('" + value.custID.replace(" ", "_") + "','" + value.userName.replace(" ", "_") + "','" + value.walletNo.replace(" ", "_") + "','" + value.lastName.replace(" ", "_") + "','" + value.firstName.replace(" ", "_") + "','" + value.middleName.replace(" ", "_") + "')";
                        table += "<tr>";
                        table += "<td><a href='#'  class='custinfo' onclick=" + param1 + ">View</a></td>";
                        table += "<td><a href='#'  class='txnsummary' onclick=" + param + ">View</a></td>";
                        //table += "<td><a href='#'  class='graph' onclick=" + param2 + ">view</a></td>";
                        table += "<td style='text-align:left'>" + value.custID + "</td>";
                        table += "<td style='text-align:left'>" + value.userName + "</td>";
                        table += "<td style='text-align:left'>" + value.walletNo + "</td>";
                        table += "<td style='text-align:left'>" + value.lastName + "</td>";
                        table += "<td style='text-align:left'>" + value.firstName + "</td>";
                        table += "<td style='text-align:left'>" + value.middleName + "</td>";
                        table+= "<td>" + value.birthDate + "</td>";
                        table += "<td style='text-align:left'>" + value.gender + "</td>";
                        //table += "<td>" + value.address + "</td>";
                        table += "</tr>";
                    });
                    table += "</tbody></table>";
                    $("#customerinfo").html(table);
                    hide('loader');
                }
            },
            error: function (result) {
                $("#msg").show();
                $("#customerinfo").hide();

                //document.getElementById("msg").innerHTML = result.stringify();
                document.getElementById("msg").innerHTML = "Unable to process request. The system encountered some technical problem. Sorry for the inconvenience. Please contact admin.";
                hide('loader');
            }
        });
    }

    $('#cth').click(function () {
        var year = $("#year").val();
        var month = $("#month").val();
        var category = $("#ttype").val();
        pop('loader1');
        $.ajax({
            type: "POST",
            url: "Admin/insertcusttxnhistory",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ 'year': year, 'month': month, 'category': category }),
            cache: false,
            dataType: "json",
            success: function (data) {
                pop('modal');
                $("#msg1").show();
                if (data == "Empty") {
                    document.getElementById("msg1").innerHTML = "No Record Found!";
                }
                else if (data == "Error") {
                    document.getElementById("msg1").innerHTML = "Unable to process request. The system encountered some technical problem. Sorry for the inconvenience. Please contact admin.";
                }
                else {
                    pop('modal');
                    document.getElementById("msg1").innerHTML = "Successfully inserted!";
                }
                hide('loader1');
            },
            error: function (result) {
                $("#msg1").show();
                //document.getElementById("msg").innerHTML = result.stringify();
                document.getElementById("msg1").innerHTML = "Unable to process request. The system encountered some technical problem. Sorry for the inconvenience. Please contact admin.";
                hide('loader1');
            }
        });
    });
    $('#cts').click(function () {
        var year = $("#year").val();
        var month = $("#month").val();
        var category = $("#ttype").val();
        pop('loader1');
        $.ajax({
            type: "POST",
            url: "Admin/insertcusttxnsummary",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ 'year': year, 'month': month, 'category': category }),
            cache: false,
            dataType: "json",
            success: function (data) {
                pop('modal');
                $("#msg1").show();
                if (data == "Empty") {
                    document.getElementById("msg1").innerHTML = "No Record Found!";
                }
                else if (data == "Error") {
                    document.getElementById("msg1").innerHTML = "Unable to process request. The system encountered some technical problem. Sorry for the inconvenience. Please contact admin.";
                }
                else {
                    pop('modal');
                    document.getElementById("msg1").innerHTML = "Successfully inserted!";
                }
                hide('loader1');
            },
            error: function (result) {
                $("#msg1").show();
                //document.getElementById("msg").innerHTML = result.stringify();
                document.getElementById("msg1").innerHTML = "Unable to process request. The system encountered some technical problem. Sorry for the inconvenience. Please contact admin.";
                hide('loader1');
            }
        });
    });

    $('#summaryreport').click(function () {
        pop('loader1');
        $('#exportsummary span').trigger('click');
        hide('loader1');
        $("#graph").show();
    });

    $('#custinforeport').click(function () {
        pop('loader1');
        $('#exportcustinfo span').trigger('click');
        hide('loader1');
    });

    $('#historyreport').click(function () {
        $("#graph").hide();

        pop('loader1');
        var custid = document.getElementById("custid").innerHTML;
        var uname = document.getElementById("uname").innerHTML;
        var fname = document.getElementById("fname").innerHTML;
        var mname = document.getElementById("mname").innerHTML;
        var lname = document.getElementById("lname").innerHTML;
        var walletno = document.getElementById("walletno").innerHTML;
        $.ajax({
            type: "POST",
            url: "CustomerService/getCustomerHistory",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ 'custid': custid, 'uname': uname, 'walletno': walletno, 'lname': lname, 'fname': fname, 'mname': mname }),
            cache: false,
            dataType: "json",
            success: function (data) {
                $("#msg1").hide();
                if (data == "Empty") {
                    $("#msg1").show();
                    document.getElementById("msg1").innerHTML = "No Record Found!";
                    hide('loader1');
                }
                else if (data == "Error") {
                    $("#msg1").show();
                    document.getElementById("msg1").innerHTML = "Unable to process request. The system encountered some technical problem. Sorry for the inconvenience. Please contact admin.";
                    hide('loader1');
                }
                else {
                    $('#exporthistory span').trigger('click');
                    $("#msg1").show();
                    document.getElementById("msg1").innerHTML = "Report Successfully Generated.";
                    hide('loader1');
                }
            },
            error: function (result) {
                $("#msg1").show();
                //document.getElementById("msg1").innerHTML = result.stringify();
                document.getElementById("msg").innerHTML = "Unable to process request. The system encountered some technical problem. Sorry for the inconvenience. Please contact admin.";
                hide('loader1');
            }
        });
    });

});

function getcustinfo(custid, uname, walletno, lname, fname, mname) {
    pop('loader1');
    document.getElementById("custid").innerHTML = custid;
    document.getElementById("uname").innerHTML = uname;
    document.getElementById("fname").innerHTML = fname;
    document.getElementById("mname").innerHTML = mname;
    document.getElementById("lname").innerHTML = lname;
    document.getElementById("walletno").innerHTML = walletno;
    $.ajax({
        type: "POST",
        url: "CustomerService/getCustomerInfo",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ 'custid': custid, 'uname': uname, 'walletno': walletno, 'lname': lname, 'fname': fname, 'mname': mname }),
        cache: false,
        dataType: "json",
        success: function (data) {
            if (data == "Empty") {
                document.getElementById("msg2").innerHTML = "No Record Found!";
            }
            else if (data == "Error") {
                document.getElementById("msg2").innerHTML = "Unable to process request. The system encountered some technical problem. Sorry for the inconvenience. Please contact admin.";
            }
            else {
                pop('custinformation');

                $("#percustomerinfo").show();
                var table = "<table id='customers'>";
                $.each(data, function (index, value) {
                    table += "<thead>";
                    table += "<th  style='text-align:center; background-color:black; color:white' colspan='3'>KYC INFORMATION</th>";
                    table += "<tr>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>ML Card Number:</th>";
                    table += "<td style='text-align:left'>" + value.mlcardno + "</td>";
                    table += "<th  style='text-align:center'>KYC PHOTO</th>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>ML Wallet Number:</th>";
                    table += "<td style='text-align:left'>" + value.walletNo + "</td>";
                    table += "<td style='width:30%; background-size: 364px 279px; background-image:url(" + value.customersimage.replace(value.path, '/Content/images/customerimages/').replace(value.path1, '/Content/images/defaultimage/') + ");background-repeat: no-repeat;'  rowspan='8' ></td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>ML Wallet Username:</th>";
                    table += "<td style='text-align:left'>" + value.userName + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>First Name:</th>";
                    table += "<td style='text-align:left'>" + value.firstName + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Middle Name:</th>";
                    table += "<td style='text-align:left'>" + value.middleName + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Last Name:</th>";
                    table += "<td style='text-align:left'>" + value.lastName + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Birth Date:</th>";
                    table += "<td style='text-align:left'>" + value.birthDate + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Place of Birth:</th>";
                    table += "<td style='text-align:left'>" + value.placeofbirth + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Gender:</th>";
                    table += "<td style='text-align:left'>" + value.gender + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Mobile Number:</th>";
                    table += "<td style='text-align:left'>" + value.mobileNo + "</td>";
                    table += "<th  style='text-align:center'>Customer's Signature</th>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Phone Number:</th>";
                    table += "<td style='text-align:left'>" + value.phoneno + "</td>";
                    table += "<td style='width:30%; background-size: 364px 254px; background-image:url(" + value.imagefree1.replace(value.path, '/Content/images/customerimages/').replace(value.path1, '/Content/images/defaultimage/') + ");background-repeat: no-repeat;'  rowspan='7' ></td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Email Address:</th>";
                    table += "<td style='text-align:left'>" + value.emailAdd + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Current Address:</th>";
                    table += "<td style='text-align:left'>" + value.address + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Permanent Address:</th>";
                    table += "<td style='text-align:left'>" + value.permanentaddress + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>City:</th>";
                    table += "<td style='text-align:left'>" + value.provincecity + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Country:</th>";
                    table += "<td style='text-align:left'>" + value.country + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Nationality:</th>";
                    table += "<td style='text-align:left' >" + value.nationality + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Nature of Work:</th>";
                    table += "<td style='text-align:left'>" + value.natureofwork + "</td>";
                    table += "<th  style='text-align:center'>Customer's Registered Biometric</th>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Profession:</th>";
                    table += "<td style='text-align:left'>" + value.businessorprofession + "</td>";
                    table += "<td style='width:30%;background-size: 364px 241px; background-image:url(" + value.fingerprint.replace(value.path, '/Content/images/customerimages/').replace(value.path1, '/Content/images/defaultimage/') + ");background-repeat: no-repeat;'  rowspan='7' ></td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Company Name:</th>";
                    table += "<td style='text-align:left'>" + value.companyoremployer + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Government Issued ID:</th>";
                    table += "<td style='text-align:left'>" + value.govtidtype + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Government ID Number:</th>";
                    table += "<td style='text-align:left'>" + value.govtidno + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Other ID Type:</th>";
                    table += "<td style='text-align:left'>" + value.idtype + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Other ID Number:</th>";
                    table += "<td style='text-align:left'>" + value.idno + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Expiry Date:</th>";
                    table += "<td style='text-align:left'>" + value.expirydate + "</td>";
                    table += "</tr>";

                    table += "<tr>";
                    table += "<th  style='text-align:center;background-color:gray;'><br/></th>";
                    table += "<th  style='text-align:center;background-color:gray;'>KYC Created:</th>";
                    table += "<th  style='text-align:center;background-color:gray;'>KYC Modified:</th>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Branch</th>";
                    table += "<td style='text-align:left'>" + value.branchcreated + "</td>";
                    table += "<td style='text-align:left'>" + value.branchmodified + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Date and Time</th>";
                    table += "<td style='text-align:left'>" + value.dtcreated + "</td>";
                    table += "<td style='text-align:left'>" + value.dtmodified + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left'>Operator</th>";
                    table += "<td style='text-align:left'>" + value.createdby + "</td>";
                    table += "<td style='text-align:left'>" + value.modifiedby + "</td>";
                    table += "</tr>"
                    table += "<tr>";
                    table += "<th colspan='3' style='text-align:center'><br/></th>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:center'>ID 1</th>";
                    table += "<th  style='text-align:center'>ID 2</th>";
                    table += "<th  style='text-align:center'>ID 3</th>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<td style='width:30%; background-size:363px 283px; background-image:url(" + value.id1.replace(value.path, '/Content/images/customerimages/').replace(value.path1, '/Content/images/defaultimage/') + ");background-repeat: no-repeat;'  ><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/></td>";
                    table += "<td style='width:30%; background-size: 363px 283px; background-image:url(" + value.id2.replace(value.path, '/Content/images/customerimages/').replace(value.path1, '/Content/images/defaultimage/') + ");background-repeat: no-repeat;'  ><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/></td>";
                    table += "<td style='width:30%; background-size:364px 283px; background-image:url(" + value.id3.replace(value.path, '/Content/images/customerimages/').replace(value.path1, '/Content/images/defaultimage/') + ");background-repeat: no-repeat;'  ><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/></td>";
                    table += "</tr>";
                    table += "</thead>";
                });
                table += "</table>";
                $("#percustomerinfo").html(table);
            }
            hide('loader1');
        },
        error: function (result) {
            $("#msg2").show();
            //document.getElementById("msg").innerHTML = result.stringify();
            document.getElementById("msg1").innerHTML = "Unable to process request. The system encountered some technical problem. Sorry for the inconvenience. Please contact admin.";
            hide('loader1');
        }
    });

   
};
function gethistory(custid, uname, walletno, lname, fname, mname) {
    pop('loader');
    document.getElementById("custid").innerHTML = custid;
    document.getElementById("uname").innerHTML = uname;
    document.getElementById("fname").innerHTML = fname;
    document.getElementById("mname").innerHTML = mname;
    document.getElementById("lname").innerHTML = lname;
    document.getElementById("walletno").innerHTML = walletno;
    $.ajax({
        type: "POST",
        url: "CustomerService/getCustomerSummary",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ 'custid': custid, 'uname': uname, 'walletno': walletno, 'lname': lname, 'fname': fname, 'mname': mname }),
        cache: false,
        dataType: "json",
        success: function (data) {
            if (data == "Empty") {
                document.getElementById("msg1").innerHTML = "No Record Found!";
            }
            else if (data == "Error") {
                document.getElementById("msg1").innerHTML = "Unable to process request. The system encountered some technical problem. Sorry for the inconvenience. Please contact admin.";
            }
            else {
                pop('txnsummary');
                $("#percustomersummary").show();
                $("#chartContainer").show();
                var table = "<table id='customers'>";
                $.each(data, function (index, value) {
                    var fullname = value.lastName + ", " + value.firstName + " " + value.middleName;
                    table += "<thead>";
                    table += "<tr>";
                    table += "<th  style='text-align:left' colspan='6'>CUSTOMER ID</th>";
                    table += "<td style='text-align:left' colspan='9'>" + value.custID + "</td>";
                    table += "<th  style='text-align:left' colspan='6'>CONTACT #</th>";
                    table += "<td style='text-align:left' colspan='9'>" + value.mobileNo + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left' colspan='6'>CUSTOMER NAME</th>";
                    table += "<td style='text-align:left' colspan='9'>" + fullname + "</td>";
                    table += "<th  style='text-align:left' colspan='6'>EMAIL ADDRESS</th>";
                    table += "<td style='text-align:left' colspan='9'>" + value.emailAdd + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th style='text-align:left' colspan='6'>ADDRESS</th>";
                    table += "<td style='text-align:left' colspan='24'>" + value.address + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th colspan='30'>MONEY TRANSFER</th>";
                    table += "</tr>";
                                       
                    table += "<tr>";
                    table += "<th colspan='6'>Kwarta Padala</th>";
                    table += "<th colspan='9'>Wallet</th>";
                    table += "<th colspan='9'>Express</th>";
                    table += "<th colspan='6'>Global</th>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th>SO</th>";
                    table += "<th>PO</th>";
                    table += "<th>RFC</th>";
                    table += "<th>RTS</th>";
                    table += "<th>CSO</th>";
                    table += "<th>CPO</th>";

                    table += "<th>SO</th>";
                    table += "<th>PO</th>";
                    table += "<th>RFC</th>";
                    table += "<th>RTS</th>";
                    table += "<th>CSO</th>";
                    table += "<th>CPO</th>";
                    table += "<th>Billspay</th>";
                    table += "<th>Eload</th>";
                    table += "<th>Corp PO</th>";

                    table += "<th>SO</th>";
                    table += "<th>PO</th>";
                    table += "<th>RFC</th>";
                    table += "<th>RTS</th>";
                    table += "<th>CSO</th>";
                    table += "<th>CPO</th>";
                    table += "<th>Billspay</th>";
                    table += "<th>Eload</th>";
                    table += "<th>Corp PO</th>";

                    table += "<th>SO</th>";
                    table += "<th>PO</th>";
                    table += "<th>RFC</th>";
                    table += "<th>RTS</th>";
                    table += "<th>CSO</th>";
                    table += "<th>CPO</th>";

                    table += "<tr>";
                    table+= "<td>" + value.kpsocount + "</td>";
                    table+= "<td>" + value.kppocount + "</td>";
                    table+= "<td>" + value.kprfccount + "</td>";
                    table+= "<td>" + value.kprtscount + "</td>";
                    table+= "<td>" + value.kpcsocount + "</td>";
                    table+= "<td>" + value.kpcpocount + "</td>";

                    table+= "<td>" + value.walletsocount + "</td>";
                    table+= "<td>" + value.walletpocount + "</td>";
                    table+= "<td>" + value.walletrfccount + "</td>";
                    table+= "<td>" + value.walletrtscount + "</td>";
                    table+= "<td>" + value.walletcsocount + "</td>";
                    table+= "<td>" + value.walletcpocount + "</td>";
                    table+= "<td>" + value.walletbpcount + "</td>";
                    table+= "<td>" + value.walleteloadcount + "</td>";
                    table+= "<td>" + value.walletcorppocount + "</td>";

                    table+= "<td>" + value.expresssocount + "</td>";
                    table+= "<td>" + value.expresspocount + "</td>";
                    table+= "<td>" + value.expressrfccount + "</td>";
                    table+= "<td>" + value.expressrtscount + "</td>";
                    table+= "<td>" + value.expresscsocount + "</td>";
                    table+= "<td>" + value.expresscpocount + "</td>";
                    table+= "<td>" + value.expressbpcount + "</td>";
                    table+= "<td>" + value.expresseloadcount + "</td>";
                    table+= "<td>" + value.expresscorppocount + "</td>";

                    table+= "<td>" + value.globalsocount + "</td>";
                    table+= "<td>" + value.globalpocount + "</td>";
                    table+= "<td>" + value.globalrfccount + "</td>";
                    table+= "<td>" + value.globalrtscount + "</td>";
                    table+= "<td>" + value.globalcsocount + "</td>";
                    table+= "<td>" + value.globalcpocount + "</td>";
                    table += "</tr>";

                    table += "<tr>";
                    table += "<th colspan='30'>CORPORATE</th>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th colspan='4'>Partner</th>";
                     table += "<th colspan='10'>File Upload</th>";
                    table += "<th colspan='10'>WSC</th>";
                    table += "<th colspan='6'>Billspay</th>";
                    table += "</tr>";

                    table += "<tr>";
                    table += "<th colspan='2'>PO</th>";
                    table += "<th colspan='2'>CPO</th>";

                    table += "<th>SO</th>";
                    table += "<th colspan='2'>PO</th>";
                    table += "<th colspan='2'>RFC</th>";
                    table += "<th colspan='2'>RTS</th>";
                    table += "<th colspan='2'>CSO</th>";
                    table += "<th>CPO</th>";

                    table += "<th>SO</th>";
                    table += "<th colspan='2'>PO</th>";
                    table += "<th colspan='2'>RFC</th>";
                    table += "<th colspan='2'>RTS</th>";
                    table += "<th colspan='2'>CSO</th>";
                    table += "<th>CPO</th>";

                    table += "<th colspan='2'>SO</th>";
                    table += "<th colspan='2'>RFC</th>";
                    table += "<th colspan='2'>CSO</th>";
                    table += "</tr>";

                    table += "<tr>";
                    table += "<td colspan='2'>" + value.apipocount + "</td>";
                    table += "<td colspan='2'>" + value.apicpocount + "</td>";

                    table+= "<td>" + value.fusocount + "</td>";
                    table += "<td colspan='2'>" + value.fupocount + "</td>";
                    table += "<td colspan='2'>" + value.furfccount + "</td>";
                    table += "<td colspan='2'>" + value.furtscount + "</td>";
                    table += "<td colspan='2'>" + value.fucsocount + "</td>";
                    table+= "<td>" + value.fucpocount + "</td>";

                    table+= "<td>" + value.wscsocount + "</td>";
                    table += "<td colspan='2'>" + value.wscpocount + "</td>";
                    table += "<td colspan='2'>" + value.wscrfccount + "</td>";
                    table += "<td colspan='2'>" + value.wscrtscount + "</td>";
                    table += "<td colspan='2'>" + value.wsccsocount + "</td>";
                    table+= "<td>" + value.wsccpocount + "</td>";

                    table += "<td colspan='2'>" + value.bpsocount + "</td>";
                    table += "<td colspan='2'>" + value.bprfccount + "</td>";
                    table += "<td colspan='2'>" + value.bpcsocount + "</td>";

                    table += "</tr>";

                    table += "<tr>";
                    table += "<th colspan='22'>BOS</th>";
                    table += "<th colspan='8' rowspan='2'>ISPD</th>";
                    table += "</tr>";

                    table += "<tr>";
                    table += "<th colspan='12'>QCL</th>";
                    table += "<th colspan='10'>Jewellers</th>";
                    table += "</tr>";

                    table += "<tr>";
                    table += "<th colspan='3'>Prenda</th>";
                    table += "<th colspan='3'>Lukat</th>";
                    table += "<th colspan='3'>Renew</th>";
                    table += "<th colspan='3'>Reappraise</th>";

                    table += "<th colspan='3'>Lay Away</th>";
                    table += "<th colspan='4'>Sales</th>";
                    table += "<th colspan='3'>Trade In</th>";

                    table += "<th colspan='2'>SBL</th>";
                    table += "<th colspan='2'>Eload</th>";
                    table += "<th colspan='2'>Insurance</th>";
                    table += "<th colspan='2'>Goods</th>";
                    table += "</tr>";

                    table += "<tr>";
                    table += "<td colspan='3'>" + value.prendacount + "</td>";
                    table += "<td colspan='3'>" + value.lukatcount + "</td>";
                    table += "<td colspan='3'>" + value.renewcount + "</td>";
                    table += "<td colspan='3'>" + value.reappraisecount + "</td>";

                    table += "<td colspan='3'>" + value.layawaycount + "</td>";
                    table += "<td colspan='4'>" + value.salescount + "</td>";
                    table += "<td colspan='3'>" + value.tradeincount + "</td>";

                    table += "<td colspan='2'>" + value.sblcount + "</td>";
                    table += "<td colspan='2'>" + value.eloadcount + "</td>";
                    table += "<td colspan='2'>" + value.insurancecount + "</td>";
                    table += "<td colspan='2'>" + value.goodscount + "</td>";
                    table += "</tr>";

                    table += "</thead>";
                    table += "<tbody>";
                    
                    table += "<tbody>";
                });
                table += "</table>";
                $("#percustomersummary").html(table);

                $.each(data, function (index, value) {
                    var chart = new CanvasJS.Chart("chartContainer", {
                        animationEnabled: true,
                        theme: "light2", // "light1", "light2", "dark1", "dark2"
                        title: {
                            text: "Distribution Per Service"
                        },
                        axisY: {
                            title: "Transaction Count"
                        },
                        data: [{
                            type: "column",
                            //showInLegend: true,
                            //legendMarkerColor: "grey",
                            //legendText: "MMbbl = one million barrels",
                            dataPoints: [
                                { y: value.kpsocount, label: "Domestic SO" },
                                { y: value.kppocount, label: "Domestic PO" },
                                { y: value.kprfccount, label: "Domestic RFC" },
                                { y: value.kprtscount, label: "Domestic RTS" },
                                { y: value.kpcsocount, label: "Domestic CSO" },
                                { y: value.kpcpocount, label: "Domestic CPO" },
                                { y: value.walletsocount, label: "Wallet SO" },
                                { y: value.walletpocount, label: "Wallet PO" },
                                { y: value.walletrfccount, label: "Wallet RFC" },
                                { y: value.walletrtscount, label: "Wallet RTS" },
                                { y: value.walletcsocount, label: "Wallet CSO" },
                                { y: value.walletcpocount, label: "Wallet CPO" },
                                { y: value.walletbpcount, label: "Wallet Billspay" },
                                { y: value.walleteloadcount, label: "Wallet Eload" },
                                { y: value.walletcorppocount, label: "Wallet Corp PO" },
                                { y: value.expresssocount, label: "Express SO" },
                                { y: value.expresspocount, label: "Express PO" },
                                { y: value.expressrfccount, label: "Express RFC" },
                                { y: value.expressrtscount, label: "Express RTS" },
                                { y: value.expresscsocount, label: "Express CSO" },
                                { y: value.expresscpocount, label: "Express CPO" },
                                { y: value.expressbpcount, label: "Express Billspay" },
                                { y: value.expresseloadcount, label: "Express Eload" },
                                { y: value.expresscorppocount, label: "Express Corp PO" },

                                { y: value.globalsocount, label: "Global SO" },
                                { y: value.globalpocount, label: "Global PO" },
                                { y: value.globalgrfccount, label: "Global RFC" },
                                { y: value.globalrtscount, label: "Global RTS" },
                                { y: value.globalcsocount, label: "Global CSO" },
                                { y: value.globalcpocount, label: "Global CPO" },

                                { y: value.apipocount, label: "API PO" },
                                { y: value.apipocount, label: "API CPO" },
                                 { y: value.fusocount, label: "Fileupload SO" },
                                { y: value.fupocount, label: "Fileupload PO" },
                                { y: value.furfccount, label: "Fileupload RFC" },
                                { y: value.furtscount, label: "Fileupload RTS" },
                                { y: value.fucsocount, label: "Fileupload CSO" },
                                { y: value.fucpocount, label: "Fileupload CPO" },
                                 { y: value.wscsocount, label: "WSC SO" },
                                { y: value.wscpocount, label: "WSC PO" },
                                { y: value.wscrfccount, label: "WSC RFC" },
                                { y: value.wscrtscount, label: "WSC RTS" },
                                { y: value.wsccsocount, label: "WSC CSO" },
                                { y: value.wsccpocount, label: "WSC CPO" },
                                { y: value.bpsocount, label: "Billspay SO" },
                                { y: value.bprfccount, label: "Billspay RFC" },
                                { y: value.bpcsocount, label: "Billspay CSO" },

                                { y: value.qclprendacount, label: "Prenda" },
                                { y: value.qcllukatcount, label: "Lukat" },
                                { y: value.qclrenewcount, label: "Renew" },
                                { y: value.qclreappraisecount, label: "Reappraise" },
                            ]
                        }]
                    });
                    chart.render();
                });
            }
            hide('loader');
        },
        error: function (result) {
            $("#msg1").show();
            //document.getElementById("msg").innerHTML = result.stringify();
            document.getElementById("msg1").innerHTML = "Unable to process request. The system encountered some technical problem. Sorry for the inconvenience. Please contact admin.";
            hide('loader');
        }
    });
};
function getgraph(custid, uname, walletno, lname, fname, mname) {
    var name = $('#custname').val();
    pop('loader');
   
    var kpsocount = 0;
    var kppocount = 0;
    var kprfccount = 0;
    var kprtscount = 0;
    var kpcsocount = 0;
    var kpcpocount = 0;
    var walletsocount = 0;
    var walletpocount = 0;
    var walletrfccount = 0;
    var walletrtscount = 0;
    var walletcsocount = 0;
    var walletcpocount = 0;
    var walletbpcount = 0;
    var walleteloadcount = 0;
    var walletcorppocount = 0;
    var expresssocount = 0;
    var expresspocount = 0;
    var expressrfccount = 0;
    var expressrtscount = 0;
    var expresscsocount = 0;
    var expresscpocount = 0;
    var expressbpcount = 0;
    var expresseloadcount = 0;
    var expresscorppocount = 0;
    var mlgsocount = 0;
    var mlgpocount = 0;
    var mlgrfccount = 0;
    var mlgrtscount = 0;
    var mlgcsocount = 0;
    var mlgcpocount = 0;
    var apipocount = 0;
    var apicpocount = 0;
    var bpsocount = 0;
    var bprfccount = 0;
    var bpcsocount = 0;
    var fusocount = 0;
    var fupocount = 0;
    var furfccount = 0;
    var furtscount = 0;
    var fucsocount = 0;
    var fucpocount = 0;
    var wscsocount = 0;
    var wscpocount = 0;
    var wscrfccount = 0;
    var wscrtscount = 0;
    var wsccsocount = 0;
    var wsccpocount = 0;
    var qclprendacount = 0;
    var qcllukatcount = 0;
    var qclrenewcount = 0;
    var qclreappraisecount = 0;
    var layawaycount = 0;
    var salescount = 0;
    var tradeincount = 0;
    var sblcount = 0;
    var eloadcount = 0;
    var insurancecount = 0;
    var goodscount = 0;
    var name = fname + " " + mname + " " + lname;

    $.ajax({
        type: "POST",
        url: "CustomerService/getCustomerSummary",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify({ 'custid': custid, 'uname': uname, 'walletno': walletno, 'lname': lname, 'fname': fname, 'mname': mname }),
        cache: false,
        dataType: "json",
        success: function (data) {

            pop('myModal');
            $("#msg").hide();
            if (data == "Empty") {
                $("#msg").show();
                document.getElementById("msg").innerHTML = "No Record Found!";
                hide('loader');
            }
            else if (data == "Error") {
                $("#msg").show();
                document.getElementById("msg").innerHTML = "Unable to process request. The system encountered some technical problem. Sorry for the inconvenience. Please contact admin.";
                hide('loader');
            }
            else {
              
                $("#customersummary").show();
                $("#chartContainer").show();
                $.each(data, function (index, value) {
                    var table = "<table id='customers'>";
                    table += "</tbody><thead>";
                    table += "<tr>";
                    table += "<th  style='text-align:left' colspan='6'>CUSTOMER ID</th>";
                    table += "<td style='text-align:left' colspan='9'>" + value.custID + "</td>";
                    table += "<th  style='text-align:left' colspan='6'>CONTACT #</th>";
                    table += "<td style='text-align:left' colspan='9'>" + value.mobileNo + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th  style='text-align:left' colspan='6'>CUSTOMER NAME</th>";
                    table += "<td style='text-align:left' colspan='9'>" + fullname + "</td>";
                    table += "<th  style='text-align:left' colspan='6'>EMAIL ADDRESS</th>";
                    table += "<td style='text-align:left' colspan='9'>" + value.emailAdd + "</td>";
                    table += "</tr>";
                    table += "<tr>";
                    table += "<th style='text-align:left' colspan='6'>ADDRESS</th>";
                    table += "<td style='text-align:left' colspan='24'>" + value.address + "</td>";
                    table += "</tr>";
                    table += "</thead>";


                    table += "</tbody></table>";
                    $("#customersummary").html(table);

                    var chart = new CanvasJS.Chart("chartContainer", {
                        animationEnabled: true,
                        theme: "light2", // "light1", "light2", "dark1", "dark2"
                        title: {
                            text: "Distribution Per Service"
                        },
                        axisY: {
                            title: "Transaction Count"
                        },
                        data: [{
                            type: "column",
                            //showInLegend: true,
                            //legendMarkerColor: "grey",
                            //legendText: "MMbbl = one million barrels",
                            dataPoints: [
                                { y: value.kpsocount, label: "Domestic SO" },
                                { y: value.kppocount, label: "Domestic PO" },
                                { y: value.kprfccount, label: "Domestic RFC" },
                                { y: value.kprtscount, label: "Domestic RTS" },
                                { y: value.kpcsocount, label: "Domestic CSO" },
                                { y: value.kpcpocount, label: "Domestic CPO" },
                                { y: value.walletsocount, label: "Wallet SO" },
                                { y: value.walletpocount, label: "Wallet PO" },
                                { y: value.walletrfccount, label: "Wallet RFC" },
                                { y: value.walletrtscount, label: "Wallet RTS" },
                                { y: value.walletcsocount, label: "Wallet CSO" },
                                { y: value.walletcpocount, label: "Wallet CPO" },
                                { y: value.walletbpcount, label: "Wallet Billspay" },
                                { y: value.walleteloadcount, label: "Wallet Eload" },
                                { y: value.walletcorppocount, label: "Wallet Corp PO" },
                                { y: value.expresssocount, label: "Express SO" },
                                { y: value.expresspocount, label: "Express PO" },
                                { y: value.expressrfccount, label: "Express RFC" },
                                { y: value.expressrtscount, label: "Express RTS" },
                                { y: value.expresscsocount, label: "Express CSO" },
                                { y: value.expresscpocount, label: "Express CPO" },
                                { y: value.expressbpcount, label: "Express Billspay" },
                                { y: value.expresseloadcount, label: "Express Eload" },
                                { y: value.expresscorppocount, label: "Express Corp PO" },

                                { y: value.apipocount, label: "API PO" },
                                { y: value.apipocount, label: "API CPO" },
                                 { y: value.fusocount, label: "Fileupload SO" },
                                { y: value.fupocount, label: "Fileupload PO" },
                                { y: value.furfccount, label: "Fileupload RFC" },
                                { y: value.furtscount, label: "Fileupload RTS" },
                                { y: value.fucsocount, label: "Fileupload CSO" },
                                { y: value.fucpocount, label: "Fileupload CPO" },
                                 { y: value.wscsocount, label: "WSC SO" },
                                { y: value.wscpocount, label: "WSC PO" },
                                { y: value.wscrfccount, label: "WSC RFC" },
                                { y: value.wscrtscount, label: "WSC RTS" },
                                { y: value.wsccsocount, label: "WSC CSO" },
                                { y: value.wsccpocount, label: "WSC CPO" },
                                { y: value.bpsocount, label: "Billspay SO" },
                                { y: value.bprfccount, label: "Billspay RFC" },
                                { y: value.bpcsocount, label: "Billspay CSO" },

                                { y: value.qclprendacount, label: "Prenda" },
                                { y: value.qcllukatcount, label: "Lukat" },
                                { y: value.qclrenewcount, label: "Renew" },
                                { y: value.qclreappraisecount, label: "Reappraise" },
                            ]
                        }]
                    });
                });
                chart.render();

                hide('loader');
            }
        },
        error: function (result) {
            $("#msg").show();
            $("#customersummary").hide();

            //document.getElementById("msg").innerHTML = result.stringify();
            document.getElementById("msg").innerHTML = "Unable to process request. The system encountered some technical problem. Sorry for the inconvenience. Please contact admin.";
            hide('loader');
        }
    });
};


//open pop
function pop(div) {
    document.getElementById(div).style.display = 'block';
};
function hide(div) {
    document.getElementById(div).style.display = 'none';
};
