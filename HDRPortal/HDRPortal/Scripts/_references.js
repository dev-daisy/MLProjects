/// <autosync enabled="true" />
/// <reference path="bootstrap.js" />
/// <reference path="jquery-1.10.2.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery.validate.unobtrusive.js" />
/// <reference path="modernizr-2.6.2.js" />
/// <reference path="respond.js" />




$(document).ready(function () {
    document.getElementById("mlmap").hidden = false;
    document.getElementById("mltrend").hidden = false;
    document.getElementById("year").hidden = true;
    document.getElementById("offlinemap").hidden = true;
    document.getElementById("customerservice").hidden = true;
    var user = $('#user').text();
    var role = $('#role').text();
    if (user == "MARLYN  LARIOSA") {
        document.getElementById("mlmap").hidden = false;
        document.getElementById("mltrend").hidden = false;
        document.getElementById("mlcustomerstat").hidden = false;
        document.getElementById("offlinemap").hidden = false;
        document.getElementById("customerservice").hidden = false;
    }
    else if (user == "admin") {
        document.getElementById("mlmap").hidden = false;
        document.getElementById("mltrend").hidden = false;
        document.getElementById("mlcustomerstat").hidden = false;
        document.getElementById("offlinemap").hidden = false;
        document.getElementById("customerservice").hidden = false;
    } else {
        document.getElementById("mlmap").hidden = true;
        document.getElementById("mltrend").hidden = true;
        document.getElementById("mlcustomerstat").hidden = true;
        document.getElementById("customerservice").hidden = true;
        if (role == "KP-GMO") {
            document.getElementById("offlinemap").hidden = false;
        }
        else {
            document.getElementById("offlinemap").hidden = true;
        }
    }

    $(function () {
        var mappath = $('#mappath').text();
        var trendpath = $('#trendpath').text();
        var customerpath = $('#customerpath').text();
        var offlinepath = $('#offlinemonitoringpath').text();
        var custservicepath = $('#customerservicepath').text();
        $("#mlmap").click(function () {
            if (user == "MARLYN  LARIOSA") {
                window.open(mappath);
            }
        });
        $("#mltrend").click(function () {
            if (user == "MARLYN  LARIOSA") {
                window.open(trendpath);
            }
        });
        $("#mlcustomerstat").click(function () {
            if (user == "MARLYN  LARIOSA") {
                window.open(customerpath);
            }
        });
        $("#offlinemap").click(function () {
            if (role == "KP-GMO" || user == "MARLYN  LARIOSA") {
                window.open(offlinepath);
            }
        });
        $("#customerservice").click(function () {
            if (user == "MARLYN  LARIOSA" || user=="admin") {
                window.open(custservicepath);
            }
        });
    });
});


function checkuser() {
    var user = $("#user").val();
    if (/^[a-zA-Z0-9- ]*$/.test(user) == false) {
        document.getElementById("user").value = user.replace(/[^a-zA-Z0-9 ]/g, "");
    }
}

    $(function () {
        $("#generate").click(function () {
            var _category = $('#dCategory').val();
            var _rcode = $('#dRegion').val();
            var _acode = $('#dArea').val();
            var _bcode = $('#dBranch').val();
            if (_category == "behaviorscore") {
                pop('pop2');
                    var chart = new CanvasJS.Chart("chartContainer", {
                        theme: "theme3",
                        zoomEnabled: true,
                        animationEnabled: true,
                        title: {
                            text: "MLHUILLIER TREND"
                        },
                        axisY: {
                            title: "COUNT"
                        },
                        data: [
                    {
                        type: "line",
                        //showInLegend: true,
                        //legendText: "2016",
                        //legendMarkerType: "square",
                        dataPoints: [
                            { label: "Jan", x: 0, y: 0 },
                            { label: "Feb", x: 10, y: 0 },
                            { label: "Mar", x: 20, y: 0 },
                            { label: "Apr", x: 30, y: 0 },
                            { label: "May", x: 40, y: 0 },
                            { label: "Jun", x: 50, y: 0 },
                            { label: "Jul", x: 60, y: 0 },
                            { label: "Aug", x: 70, y: 0 },
                            { label: "Sep", x: 80, y: 0 },
                            { label: "Oct", x: 90, y: 0 },
                            { label: "Nov", x: 100, y: 0 },
                            { label: "Dec", x: 110, y: 0 },
                        ],
                    }
                        ]
                    });
                    chart.render();
            } else if (_category != "" && _rcode != "") {
                var _rname = $('#dRegion').find("option:selected").text().trim();
                var _aname = $('#dArea').find("option:selected").text().trim();
                var _bname = $('#dBranch').find("option:selected").text().trim();
                $("body").addClass("loading");
                $.ajax({
                    type: "GET",
                    url: "Home/getUsers",
                    contentType: "application/json; charset=utf-8",
                    data: { category: _category, rcode: _rcode, rname: _rname, acode: _acode, aname: _aname, bcode: _bcode, bname: _bname },
                    cache: false,
                    dataType: "json",
                    success: function (result) {
                        $("body").removeClass("loading");
                        if (_category == "branchprofile") {
                            if ((result.startsWith("2, ")) == true) { alert(result); }
                            else{
                                var link = document.createElement("a");
                                link.download = result;
                                link.href = "Reports/Export/" + result;
                                link.click();
                            }
                            //window.prompt('Report Successfully Generated. Please open this path : \n', link.href);
                        } else {
                            if (result == "Successfull") {
                                $('#myReport span').trigger('click');
                                //alert("Please Wait for Report file to generate,Thankyou!");
                            }
                            else { alert("No Records Found!"); }
                        }
                    },
                    error: function (result) {
                        $("body").removeClass("loading");
                        alert("Unable to get data. Please contact admin support!");
                    }
                });
            } else { alert("Please Select Category And Region!"); }
        });
    });


    $(function () {
        $("#dCategory").click(function () {
            var _category = $('#dCategory').val();
            document.getElementById("dRegion").hidden = false;
            document.getElementById("dArea").hidden = false;
            document.getElementById("dBranch").hidden = false;
            document.getElementById("year").hidden = true;
            if (_category == "activeusers") {
                $('#dArea option:eq(0)').prop('selected', true).change();
                document.getElementById("dArea").hidden = true;
                document.getElementById("dBranch").hidden = true;
                document.getElementById("dArea").disabled = true;
                document.getElementById("dBranch").disabled = true;
            } else if (_category == "behaviorscore") {
                document.getElementById("dRegion").hidden = true;
                document.getElementById("dArea").hidden = true;
                document.getElementById("dBranch").hidden = true;
                document.getElementById("year").hidden = false;
            } else {
                document.getElementById("dArea").disabled = false;
                document.getElementById("dBranch").disabled = false;
            }
        });
    });

    //get areanname
    $(function () {
        $("#dRegion").change(function () {
            var _code = $(this).val();
            var _name = $(this).find("option:selected").text().trim();
            var area = $("#dArea");
            area.empty();

            var branch = $("#dBranch");
            branch.empty();

            $.ajax({
                type: "GET",
                url: "Home/getCode",
                contentType: "application/json; charset=utf-8",
                data: { code: _code, name: _name, flag: "Area" },
                cache: false,
                dataType: "json",
                success: function (result) {
                    //var res = JSON.stringify(result);
                    for (var i = 0; i < result.length; i++) {
                        area.append('<option value="' + result[i].Value.trim() + '">' + result[i].Text.trim() + '</option>');
                    }
                },
                error: function (result) {
                    //var msg = $("#msg"); msg.empty(); pop('pop1');
                    alert(JSON.stringify(result));
                    alert('Unable To Get Area! Please Contact Admin Support!');
                    //alert(JSON.stringify(result)); 
                }
            });
        });
    });

    //get branchname
    $(function () {
        $("#dArea").change(function () {
            //$(".modal").hide();
            //$(".popup").show();
            var _code = $("#dRegion").val();
            var _name = $(this).find("option:selected").text().trim();
            var branch = $("#dBranch");
            branch.empty();
            //branch.append('<option value="">- Please Select Branch -</option>');
            $.ajax({
                type: "GET",
                url: "Home/getCode",
                contentType: "application/json; charset=utf-8",
                data: { code: _code, name: _name, flag: "Branch" },
                cache: false,
                dataType: "json",
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        branch.append('<option value="' + result[i].Value.trim() + '">' + result[i].Text.trim() + '</option>');
                    }
                },
                error: function (result) {
                    alert("Unable To Get Branch! Please Contact Admin Support!");
                }
            });
        });
    });

    $(function () {
        $("#stc").change(function () {
            $("body").addClass("loading");

            if ($("#stc").is(":checked")) {
                $.ajax({
                    type: "GET",
                    url: "Home/insert_stat",
                    contentType: "application/json; charset=utf-8",
                    data: { stat: "1" },
                    cache: false,
                    dataType: "json",
                    success: function (result) {
                        $("body").removeClass("loading");
                        $("#stat").text("Status: On")
                    },
                    error: function (result) {
                        $("body").removeClass("loading");
                        $("#stat").text("Please Call admin support! Process not valid")
                    }
                });

            }
            else {
                $.ajax({
                    type: "GET",
                    url: "Home/insert_stat",
                    contentType: "application/json; charset=utf-8",
                    data: { stat: "0" },
                    cache: false,
                    dataType: "json",
                    success: function (result) {
                        $("body").removeClass("loading");
                        $("#stat").text("Status: Off")
                    },
                    error: function (result) {
                        $("body").removeClass("loading");
                        $("#stat").text("Please Call admin support! Process not valid")
                    }
                });
            }


        })

    });


    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });



    /* Set the width of the side navigation to 250px */
    //function openNav() {
    //document.getElementById("mySidenav").style.width = "250px";
    //document.getElementById("main").style.marginLeft = "250px";
    //}

    /* Set the width of the side navigation to 0 */
    //function closeNav() {
    //    document.getElementById("mySidenav").style.width = "0";
    //document.getElementById("main").style.marginLeft = "0px";
    //}

    


function pop(div) {
    //closeNav();
    document.getElementById(div).style.display = 'block';

}
function hide(div) {
    document.getElementById(div).style.display = 'none';
}
function status() {
    $("body").addClass("loading")
    $.ajax({
        type: "GET",
        url: "Home/Getstat",
        contentType: "application/json; charset=utf-8",
        //data: { stat: "1" },
        cache: false,
        dataType: "json",
        success: function (result) {
            $("body").removeClass("loading");
            $.each(result, function (index, value) {
                if (value.status == "1") {
                    $("#stc").prop('checked', true);
                    $("#stat").text("Status: On")
                }
                else {
                    $("#stc").prop('checked', false);
                    $("#stat").text("Status: off")
                }
            });
        },
        error: function (result) {
            // $("#stat").text("Please Call admin support! Process not valid")
            alert(JSON.stringify(result));
        }
    });
}