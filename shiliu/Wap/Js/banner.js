// JavaScript Document



$(function () {
    $.banner01();
    $.banner02();
}); 

$.banner01 = (function () {
    var num = 0;
    num = $("#slider01 li").length;
    var html = "";
    html = "<a href='javascript:void(0);' {c}>{i}</a>";
    var lasthtml,
    thtml;
    lasthtml = "";

    for (var i = 0; i < num; i++) {
        thtml = "";
        if (i == 0) {
            thtml = html.replace("{c}", "class='active' ");

        } else {
            thtml = html.replace("{c}", "");

        }
        thtml = thtml.replace("{i}", i);

        lasthtml += thtml;

    };
    $(".pagenavi01").html(lasthtml);
    var active = 0,
    as = $('.pagenavi01 a');

    for (var i = 0; i < as.length; i++) {
        (function () {
            var j = i;
            as[i].onclick = function () {
                t2.slide(j);
                return false;
            }

        })();

    }

    var t2 = new TouchSlider({
        id: 'slider01',
        speed: 600,
        timeout: 6000,
        before: function (index) {
            as[active].className = '';
            active = index;
            as[active].className = 'active';

        }
    });
    setTimeout(function () { t2.resize(); }, 100);


});

$.banner02 = (function () {
    var num = 0;
    num = $("#slider02 li").length;
    var html = "";
    html = "<a href='javascript:void(0);' {c}>{i}</a>";
    var lasthtml,
    thtml;
    lasthtml = "";

    for (var i = 0; i < num; i++) {
        thtml = "";
        if (i == 0) {
            thtml = html.replace("{c}", "class='active' ");

        } else {
            thtml = html.replace("{c}", "");

        }
        thtml = thtml.replace("{i}", i);

        lasthtml += thtml;

    };
    $(".pagenavi02").html(lasthtml);
    var active = 0,
    as = $('.pagenavi02 a');

    for (var i = 0; i < as.length; i++) {
        (function () {
            var j = i;
            as[i].onclick = function () {
                t2.slide(j);
                return false;
            }

        })();

    }

    var t2 = new TouchSlider({
        id: 'slider02',
        speed: 600,
        timeout: 6000,
        before: function (index) {
            as[active].className = '';
            active = index;
            as[active].className = 'active';

        }
    });
    setTimeout(function () { t2.resize(); }, 100);


});
