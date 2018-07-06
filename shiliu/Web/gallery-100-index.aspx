<%@ Page Language="C#" AutoEventWireup="true" CodeFile="gallery-100-index.aspx.cs"
    Inherits="Web_gallery_100_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Web/footer.ascx" TagName="footer" TagPrefix="ft" %>
<%@ Register Src="~/Web/TopHead.ascx" TagName="header" TagPrefix="hd" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>马赛特酒庄</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta name="keywords" content="马赛特酒庄">
    <meta name="description" content="马赛特酒庄共找到2个商品">
    <meta name="generator" content="ShopEx 4.8.5">
    <link rel="icon" href="../image/bitbug_favicon.ico" type="image/x-icon" />
    <link rel="bookmark" href="../image/bitbug_favicon.ico" type="image/x-icon" />
    <link rel="stylesheet" href="statics/style.css" type="text/css" />
    <script type="text/javascript" src="statics/script/tools.js"></script>
    <script type="text/javascript" src="statics/script/goodscupcake.js"></script>
    <link href="themes/1394732638/images/css.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="themes/1394732638/images/upc.js"></script>
</head>
<body>
    <hd:header ID="Header1" runat="server" />
    <div class="main innerbox">
        <div class="nav">
            <div class="Navigation">
                您当前的位置： <span><a href="index.aspx" alt="" title="">首页</a></span> <span>&raquo;</span>
                <%=urlJump %>
            </div>
        </div>
                <script type="text/javascript">

                    //添加购物车商品
                    function add_cart(nID, a) {

                        var loginUser = $('#LoginUserID').val();
                        if (loginUser == null || loginUser == "") {
                            //MyChangePage("Person/Individual.html");
                            alert('请先登录');
                            window.location.href = "passport-login.aspx";
                            return false;
                        }

                        if (Number($(a).attr('kucun')) < 1) {//如果库存小于1
                            alert('产品已售完');
                            return false;
                        }

                        $.ajax({
                            type: "post",
                            async: false,
                            cache: false,
                            url: "../Wap/Handler.ashx",
                            dataType: "json",
                            data: {
                                "Method": "InsertCart",
                                "UserID": loginUser,
                                "ProID": nID,
                                "ProCount": 1
                            },
                            success: function (data) {
                                if (data) {
                                    alert("添加成功");
                                    //window.location.reload();
                                } else {
                                    alert("添加失败");
                                }
                            },
                            error: function () {
                                alert('连接超时');
                            }
                        });
                    }

                    //添加商品
                    function drop_cart_item(rec_id, a) {
                        add_cart(rec_id, a);
                    }                                 


        </script>
        <input value="" id="LoginUserID" type="hidden" runat="server" /><%--session  id--%>
        <div class="leftbar">
            <div class="l_hotsale">
                <div class="border3" id="">
                    <h3>
                        热销排行</h3>
                    <div class="l_contentbox">
                        <style>
                            .goodslist li
                            {
                                float: left;
                            }
                            .goodslist
                            {
                                overflow: hidden;
                            }
                        </style>
                        <ul class="goodslist">
                            <%=hotProduct %>
                        </ul>
                    </div>
                </div>
            </div>
<%--            <div class="l_hst">
                <div class="border3" id="">
                    <h3>
                        浏览过的商品</h3>
                    <div class="l_contentbox">
                        <div class="GoodsBrowsed" id="box_406">
                        </div>
                        <script>
                            withBroswerStore(function (broswerStore) {
                                var box = $('box_406'); ;
                                broswerStore.get('history', function (v) {
                                    v = JSON.decode(v);
                                    if (!v || !v.length) return;
                                    var html = '';
                                    var template = '<div class="hstgoodslist clearfix">';
                                    template += '<div class="span-2 goodpic">';
                                    template += '<a href="product-{goodsId}-index.html" target="_blank" title="{goodsName}" inner_img="{goodsImg}" gid="{goodsId}">';
                                    template += '</a>';
                                    template += '</div><div class="prepend-2 goodsName">';
                                    template += '<div class="view-time">{viewTime}</div>';
                                    template += '<a class="goods_name" href="product-{goodsId}-index.html" target="_blank" title="{goodsName}">{goodsName}</a></div></div>';

                                    var max = Math.min(v.length, 3);
                                    if (v.length > 1)
                                        v.reverse();

                                    v.each(function (goods, index) {
                                        var vt = ($time() - goods['viewTime']);
                                        vt = Math.round(vt / (60 * 1000)) + '分钟前浏览过:';
                                        if (vt.toInt() >= 60) {
                                            vt = Math.round(vt.toInt() / 60) + '小时前浏览过:';
                                            if (vt.toInt() > 23) {
                                                vt = Math.round(vt.toInt() / 24) + '天前浏览过:';
                                                if (vt.toInt() > 3) {
                                                    vt = new Date(goods['viewTime']).toLocaleString() + '浏览过:';
                                                }
                                            }
                                        };
                                        if (!!!vt.toInt()) { vt = '刚才浏览了:' }
                                        goods['viewTime'] = vt;
                                        if (index < max)
                                            html += template.substitute(goods);
                                    });

                                    $('box_406').set('html', html);

                                    $ES('.goodpic', box).each(function (i) {
                                        var imga = $E('a', i);
                                        var imgsrc = imga.get('inner_img');
                                        if (!imgsrc) {
                                            imgsrc = "http://www.马赛特624.com/images/default/default_thumbnail_pic.gif";
                                        }
                                        imga.setText('loading...');
                                        new Asset.image(imgsrc, { onload: function () {
                                            var img = $(this);
                                            if (img.$e) return;
                                            img.zoomImg(70, 70);
                                            img.inject(imga.empty());
                                            img.$e = true;
                                        }, onerror: function () {

                                            imga.setText('update...');
                                            var gid = imga.get('gid');
                                            new Request.JSON({ method: 'get', url: "product-picsJson.html", onComplete: function (data) {
                                                new Asset.image(data[0]['thumbnail_pic'], { onload: function () {
                                                    var img = $(this);
                                                    if (img.$e) return;
                                                    img.zoomImg(70, 70);
                                                    img.inject(imga.empty());
                                                    img.$e = true;
                                                }, onerror: function () {
                                                    imga.remove();
                                                }
                                                });

                                                v.map(function (goods, index) {
                                                    if (index < max && goods['goodsId'] == gid) {
                                                        return goods['goodsImg'] = '';
                                                    } else {
                                                        return goods['goodsImg'] = data[0]['thumbnail_pic'];
                                                    }
                                                });
                                                broswerStore.set('history', v);
                                            }
                                            }).get($H({ 'gids': gid }));


                                        }
                                        });
                                    });

                                });


                            });
                        </script>
                        <div class="hst_btn">
                            <a class="lnk clearAll" onclick="if(broswerStore){broswerStore.remove('history');$('box_406').empty()}">
                                清除列表</a> | <a class="lnk viewAll" href="tools-history.html">查看所有</a><span></span>
                        </div>
                    </div>
                </div>
            </div>--%>
        </div>
        <div class="rightbar">
            <div class="g_focus">
                <div class="AdvBanner">
                    <a href="" target="_blank">
                        <img src='../Wap/img/banner01.jpg' width="783" style="" />
                    </a>
                </div>
            </div>
            <div class="clear">
            </div>
            <div class="GoodsSearchWrap">
                <div class="search_total">
                    总共找到<font color='red'><%=proCount %></font>款商品
                </div>
                <div class="title" id='gallerybar'>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tbody>
                            <tr>
                                <td>
                                </td>
                                <td>
                                    <div class="listmode">
                                          <%=ListTypeUrl %>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="ItemsWarp clearfix">
                   <%=productClass %>
               <%--     <div class="items-list " product="121" id="pdt-121">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="goodpic" valign="middle" style='width: 220px;'>
                                    <a target="_blank" style='width: 220px; height: 220px;' href="product-117.html">
                                        <img src="http://www.马赛特624.com/images/goods/20140414/e4718366095522b6.jpg" alt="红酒" />
                                    </a>
                                </td>
                                <td width='10px;'>
                                    &nbsp;
                                </td>
                                <td class="goodinfo">
                                    <h6>
                                        <a href="product-117.html" title="红酒" class="entry-title" target="_blank">红酒</a></h6>
                                </td>
                                <td class="price_button" width="250">
                                    <ul>
                                        <li><span class="price1">￥358.00</span><span class="mktprice1">市场价: ￥558.00</span></li>
                                        <li style="padding-left: 4px; padding-top: 8px;"><span class="saveprice1">节省：￥200.00</span></li>
                                        <!--<li class="intro rank-3">Rank 3</li>-->
                                    </ul>
                                    <ul class="button">
                                        <li class="addcart"><a href="cart-index.html" type="g" buy="121" class="listact"
                                            target="_dialog_minicart" title="加入购物车" rel="nofollow">加入购物车</a> </li>
                                        </li>
                                        <li class="vdetail zoom"><a title="红酒" href="product-117.html" pic='http://www.马赛特624.com/images/goods/20140414/4360f34f2e3413df.jpg'
                                            target="_blank" class="listact">查看详细</a></li>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="items-list " product="113" id="pdt-113">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="goodpic" valign="middle" style='width: 220px;'>
                                    <a target="_blank" style='width: 220px; height: 220px;' href="product-117.html">
                                        <img src="http://www.马赛特624.com/images/goods/20140606/dca83cdd2de7c1aa.jpg" alt="红酒" />
                                    </a>
                                </td>
                                <td width='10px;'>
                                    &nbsp;
                                </td>
                                <td class="goodinfo">
                                    <h6>
                                        <a href="product-117.html" title="红酒" class="entry-title" target="_blank">红酒</a></h6>
                                </td>
                                <td class="price_button" width="250">
                                    <ul>
                                        <li><span class="price1">￥998.00</span><span class="mktprice1">市场价: ￥558.00</span></li>
                                        <!--<li class="intro rank-3">Rank 3</li>-->
                                    </ul>
                                    <ul class="button">
                                        <li class="addcart"><a href="cart-index.html" type="g" buy="113" class="listact"
                                            target="_dialog_minicart" title="加入购物车" rel="nofollow">加入购物车</a> </li>
                                        </li>
                                        <li class="vdetail zoom"><a title="红酒" href="product-117.html" pic='http://www.马赛特624.com/images/goods/20140606/212884ec7b93e2d8.jpg'
                                            target="_blank" class="listact">查看详细</a></li>
                                    </ul>
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                </div>
            </div>
            <script>
                window.addEvent('domready', function () {

                    /*关键字高亮*/
                    try {
                        (function (replace_str) {
                            var replace = replace_str.split("+");

                            if (replace.length) {
                                $ES('.entry-title').each(function (r) {
                                    for (i = 0; i < replace.length; i++) {
                                        if (replace[i]) {
                                            if (replace[i] === '1') {
                                                r.setText(r.get('text').replace(/[^{]1/, function () {
                                                    return "{0}" + arguments[0] + "{1}";
                                                }));
                                                continue;
                                            } else if (replace[i] === '0') {
                                                r.setText(r.get('text').replace(/[^{]0/, function () {
                                                    return "{0}" + arguments[0] + "{1}";
                                                }));
                                                continue;
                                            };
                                            var reg = new RegExp("(" + replace[i].escapeRegExp() + ")", "gi");
                                            r.setText(r.get('text').replace(reg, function () {
                                                return "{0}" + arguments[0] + "{1}";
                                            }));
                                        }
                                    }
                                    r.set('html', r.get('text').format("<font color=red>", "</font>"));
                                });
                            }
                        })('');
                    } catch (e) { }

                    if (window.ie6) return;

                    // div id='gallerybar' 布局和排序区域dom定位
                    var gallerybar = $('gallerybar'),
    gallerybarSize = gallerybar.getSize(),
    gallerybarPos = gallerybar.getPosition(),
    fixedStart = gallerybarSize.y + gallerybarPos.y;

                    var fixGalleryBar = function () {
                        if (fixedStart < this.getScrollTop()) {
                            gallerybar.addClass('fixed').setStyles({ 'width': gallerybarSize.x });
                        } else {
                            gallerybar.removeClass('fixed').setStyles({ 'width': 'auto' });
                        }
                    };

                    window.addEvents({
                        'resize': fixGalleryBar,
                        'scroll': fixGalleryBar
                    });

                });

            </script>
        </div>
        <div class="clearfix">
        </div>
    </div>
    <ft:footer ID="Footer1" runat="server" />
</body>
</html>
