<%@ Page Title="" Language="C#" MasterPageFile="~/mp/MasterPage.master" AutoEventWireup="true" CodeFile="message.aspx.cs" Inherits="mp_message" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
            <title>
            关于我们 > 在线留言_响应式绿化花木果苗类网站模板(自适应手机端)
        </title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
            <div class="met-banner banner-ny-h" data-height='' style=''>
            <div class="slick-slide">
                <img class="cover-image" src="picture/banner1.jpg" sizes="(max-width: 767px) 767px" alt="在线留言">
            </div>
        </div>
        <div class="met-column-nav ">
            <div class="container">
                <div class="row">
                    <div class="sidebar-tile">
                        <ul class="met-column-nav-ul invisible-xs">
                            <li>
                                <a href="about.html"​ class="link ">
                                    公司简介
                                </a>
                            </li>
                            <li>
                                <a href='message.html' class='link active'>
                                    在线留言
                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
        <section class="met-message animsition">
            <div class="container">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="met-message-submit">
                                <form action="/plus/diy.php" class="met-form met-form-validation" enctype="multipart/form-data"
                                method="post">
                                    <input type="hidden" name="required" value="name,tel" />
                                    <input type="hidden" name="action" value="post" />
                                    <input type="hidden" name="diyid" value="1" />
                                    <input type="hidden" name="do" value="2" />
                                    <div class="form-group">
                                        <div>
                                            <input name='name' class='form-control' type='text' placeholder='姓名* '
                                            />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div>
                                            <input name='tel' class='form-control' type='text' placeholder='电话* '
                                            />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div>
                                            <input name='email' class='form-control' type='text' placeholder='邮箱 '
                                            />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div>
                                            <input name='adr' class='form-control' type='text' placeholder='联系地址 '
                                            />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div>
                                            <textarea name='content' class='form-control' placeholder='留言内容 ' rows='5'>
                                            </textarea>
                                        </div>
                                    </div>
                                    <input type="hidden" name="dede_fields" value="name,text;tel,text;email,text;adr,text;content,multitext"
                                    />
                                    <input type="hidden" name="dede_fieldshash" value="7df5890cb749dae8e7b9d0d5361bf468"
                                    />
                                    <div class="form-group margin-bottom-0">
                                        <button type="submit" class="btn btn-primary btn-block btn-squared">
                                            提交
                                        </button>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>
</asp:Content>

