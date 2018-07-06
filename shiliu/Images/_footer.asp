<div class="footer">
	<div class="f1">
    <p><span>上海峰幂影视文化传播有限公司</span></p>
    <p>COPYRIGHT 2013 <span>FOONMEE MEDIA</span> ALL RIGHTS RESERVED.</p>
    <p>地址：上海市徐汇区漕溪北路737弄汇翠花园1号楼2901室    公司座机：021-34605639   传真：021-34605639   公司邮箱：foonmee@163.com</p>
    </div>
  
</div>

<div id="qq" class="pane" style="position:absolute; z-index:999; top:50px;">
<h3><img src="images/qheader.jpg" width="107" height="31" /></h3>
	<div>
    	<ul class="qqlist">
        	<li>  <a href="tencent://Message/?Uin=2021238862&websiteName=q-zone.qq.com&Menu=yes" style="background:none; padding-left:10px;"><img src="images/qq.gif" width="15" height="18" align="absmiddle" />在线客服</a></li>
            <li style="text-align:center"><img src="images/erwei.jpg"></li>
                    </ul>
    </div>

 <!--   <p style="text-align:center; padding-top:10px;"><img src="images/close.jpg" alt="" class="delete" style="cursor:pointer;"></p>
    <script type="text/javascript">
$(document).ready(function(){
						   
	$(".pane .delete").click(function(){
		$(this).parents(".pane").animate({ opacity: 'hide' }, "slow");
	});

});
</script>-->
</div>

<style>
#qq{ background:url(images/qbg.jpg) no-repeat bottom; width:107px;min-height:140px; height:auto !important; height:140px; overflow:visible;}
.qqlist{ background:url(images/qqbg.jpg) repeat-y;}
.qqlist li{ padding:6px 0;  }
.qqlist li a{ color:#333;}
</style>

<SCRIPT type=text/javascript>
var verticalpos="frombottom"
function JSFX_FloatTopDiv()
{
var startX =0,
startY = 459;
var ns = (navigator.appName.indexOf("Netscape") != -1);
var d = document;
function ml(id)
{
  var el=d.getElementById?d.getElementById(id):d.all?d.all[id]:d.divs[id];
  if(d.divs)el.style=el;
  el.sP=function(x,y){this.style.right=x;this.style.top=y;};
  el.x = startX;
  if (verticalpos=="fromtop")
  el.y = startY;
  else{
  el.y = ns ? pageYOffset + innerHeight : document.documentElement.scrollTop + 
document.documentElement.clientHeight;
  el.y -= startY;
  }
  return el;
}
window.stayTopright=function()
{
  if (verticalpos=="fromtop"){
  var pY = ns ? pageYOffset : document.documentElement.scrollTop;
  ftlObj.y += (pY + startY - ftlObj.y)/8;
  }
  else{
  var pY = ns ? pageYOffset + innerHeight : document.documentElement.scrollTop + 
document.documentElement.clientHeight;
  ftlObj.y += (pY - startY - ftlObj.y)/8;
  }
  ftlObj.sP(ftlObj.x, ftlObj.y);
  setTimeout("stayTopright()", 10);
}
ftlObj = ml("qq");
stayTopright();
}
JSFX_FloatTopDiv();


</SCRIPT>
</body>
</html>