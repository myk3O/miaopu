// JavaScript Document
$(function(){
	
		$(":input").focus(function(){
			  $(this).addClass("focus");
			  if($(this).val() ==this.defaultValue){  
                  $(this).val("");           
			  } 
		}).blur(function(){
			 $(this).removeClass("focus");
			 if ($(this).val() == '') {
                $(this).val(this.defaultValue);
             }
		});	

	  	//隔行换色	
		$(".news_list_a li:not(li.none):odd").addClass("hasddB");  /* （除去tr 的class为none）奇数行添加样式*/
		$(".news_list_a li:not(li.none):even").addClass("hasddA"); /* （除去tr 的class为none）偶数行添加样式*/

		$(".jituan").hover(function () {
			$(this).find("ul").slideDown();
		},function () {
			$(this).find("ul").slideUp();
		});
		
		//底部微信
		$(".wx_img").hover(function() {
			$(this).find('.wx_box').css("display","block");
			},function(){
			$(this).find('.wx_box').css("display","none");
		});	
		  
		
		//返回顶部
		var $backToTopTxt = "", $backToTopEle = $('<div class="backToTop"></div>').appendTo($("body"))
		.text($backToTopTxt).attr("title", $backToTopTxt).click(function() {
			$("html, body").animate({ scrollTop: 0 }, 120);
		}), $backToTopFun = function() {
		var st = $(document).scrollTop(), winh = $(window).height();
		(st > 0)? $backToTopEle.show(): $backToTopEle.hide();	
		//IE6下的定位
		if (!window.XMLHttpRequest) {
			$backToTopEle.css("top", st + winh - 166);	
		}
	};
	$backToTopFun();
	$(window).bind("scroll", $backToTopFun);
})	
	
	
function settab(name, cursel, num) {
	for ( i = 1; i <= num; i++) {
		var menu = document.getElementById(name + i);
		var con = document.getElementById(name + "_con_" + i);
		menu.className = i == cursel ? "hover" : "";
		con.style.display = i == cursel ? "block" : "none";
	}
}	

function cli(obj) {
    $(obj).next('ul').slideToggle(0);
}	

// 设置为主页 
function SetHome(obj,vrl){ 
try{ 
obj.style.behavior='url(#default#homepage)';obj.setHomePage(vrl); 
} 
catch(e){ 
if(window.netscape) { 
try { 
netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect"); 
} 
catch (e) { 
alert("此操作被浏览器拒绝！\n请在浏览器地址栏输入'about:config'并回车\n然后将 [signed.applets.codebase_principal_support]的值设置为'true',双击即可。"); 
} 
var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch); 
prefs.setCharPref('browser.startup.homepage',vrl); 
}else{ 
alert("您的浏览器不支持，请按照下面步骤操作：1.打开浏览器设置。2.点击设置网页。3.输入："+vrl+"点击确定。"); 
} 
} 
} 
// 加入收藏 兼容360和IE6 
function shoucang(sTitle,sURL) 
{ 
try 
{ 
window.external.addFavorite(sURL, sTitle); 
} 
catch (e) 
{ 
try 
{ 
window.sidebar.addPanel(sTitle, sURL, ""); 
} 
catch (e) 
{ 
alert("加入收藏失败，请使用Ctrl+D进行添加"); 
} 
} 
} 