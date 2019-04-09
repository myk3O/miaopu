<%@ Page Title="" Language="C#" MasterPageFile="~/mp/MasterPage.master" AutoEventWireup="true" CodeFile="productshow.aspx.cs" Inherits="mp_productshow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>烟台红富士_响应式绿化花木果苗类网站模板(自适应手机端)
    </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="met-position  pattern-show">
        <div class="container">
            <div class="row">
                <ol class="breadcrumb">
                    <li>
                        <a href='/'>主页
                        </a>
                        >
                            <a href='/a/chanpinzhongxin/'>产品中心
                            </a>
                        >
                            <a href='/a/chanpinzhongxin/guoshumiao/'>果树苗
                            </a>
                        >
                    </li>
                </ol>
            </div>
        </div>
    </div>
    <div class="page met-showproduct pagetype1 animsition">
        <div class="met-showproduct-head">
            <div class="container">
                <div class="row">
                    <div class="col-md-7">
                        <div class='met-showproduct-list fngallery text-center ' id="met-imgs-carousel">
                            <div class='slick-slide lg-item-box' data-src="picture/1-1P104093602.jpg"
                                data-exthumbimage="picture/1-1P104093602.jpg">
                                <span>
                                    <img src="picture/1-1p104093602.jpg" data-src='picture/1-1P104093602.jpg'
                                        class="img-responsive" alt="" />
                                </span>
                            </div>
                            <div class='slick-slide lg-item-box' data-src="picture/1-1P104093603.jpg"
                                data-exthumbimage="picture/1-1P104093603.jpg">
                                <span>
                                    <img src="picture/1-1p104093603.jpg" data-src='picture/1-1P104093603.jpg'
                                        class="img-responsive" alt="" />
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5 product-intro">
                        <h1 class='font-weight-300'>烟台红富士
                        </h1>
                        <p class="description">
                            十月下旬成熟，果形高桩，单果重265克左右。果实底色绿白，全面着条纹红色。果肉淡黄色，汁多甜脆，固形物含量14.9%左右。
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <div class="met-showproduct-body">
            <div class="container">
                <div class="row no-space">
                    <div class="col-md-9 product-content-body">
                        <div class="row">
                            <div class="panel product-detail">
                                <div class="panel-body">
                                    <ul class="nav nav-tabs nav-tabs-line met-showproduct-navtabs affix-nav">
                                        <li class="active">
                                            <a data-toggle="tab" href="#product-details" data-get="product-details">详细信息
                                            </a>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div class="tab-pane met-editor lazyload clearfix animation-fade active"
                                            id="product-details">
                                            <div>
                                                <img alt="" src="picture/1-1p104093525e1.jpg" />
                                                <br />
                                                果实个大、果形正，色泽鲜艳红润，外表光滑细腻，口味酸甜适口，咬一口细脆津纯，清香蜜味，且果肉硬度大，纤维少，质地细，果汁含量在89%以上，糖份含量高，总糖量16.4%，含有铁、锌、锰、钙等人体有益的微量元素，氨基酸含量较高。经常食用，可起到帮助消化、养颜润肤的独特作用。烟台苹果有&ldquo;水果皇后&rdquo;美誉，受到许多人的喜爱
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--右侧开始-->
                    <div class="col-md-3">
                        <div class="row">
                            <div class="panel product-hot">
                                <div class="panel-body">
                                    <h2 class="margin-bottom-15 font-size-16 font-weight-300">热门推荐
                                    </h2>
                                    <ul class="blocks-2 blocks-sm-3 mob-masonry" data-scale='1'>
                                        <!--string(7) "product" -->
                                        <li>
                                            <a href="productshow.html" class="img"
                                                title="西府海棠">
                                                <img data-original="picture/1-1P10410032K18.jpg" class="cover-image"
                                                    style='height: 200px;' alt="西府海棠">
                                            </a>
                                            <a href="productshow.html" class="txt"
                                                title="西府海棠">西府海棠
                                            </a>
                                        </li>
                                        <li>
                                            <a href="productshow.html" class="img"
                                                title="小叶黄杨">
                                                <img data-original="picture/1-1P1041002250-L.jpg" class="cover-image"
                                                    style='height: 200px;' alt="小叶黄杨">
                                            </a>
                                            <a href="productshow.html" class="txt"
                                                title="小叶黄杨">小叶黄杨
                                            </a>
                                        </li>
                                        <li>
                                            <a href="productshow.html" class="img"
                                                title="云杉">
                                                <img data-original="picture/1-1P1040945350-L.jpg" class="cover-image"
                                                    style='height: 200px;' alt="云杉">
                                            </a>
                                            <a href="productshow.html" class="txt"
                                                title="云杉">云杉
                                            </a>
                                        </li>
                                        <li>
                                            <a href="productshow.html" class="img"
                                                title="黄金蜜糖李">
                                                <img data-original="picture/1-1P1040940220-L.jpg" class="cover-image"
                                                    style='height: 200px;' alt="黄金蜜糖李">
                                            </a>
                                            <a href="productshow.html" class="txt"
                                                title="黄金蜜糖李">黄金蜜糖李
                                            </a>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--右侧结束-->
                </div>
            </div>
        </div>
    </div>
</asp:Content>

