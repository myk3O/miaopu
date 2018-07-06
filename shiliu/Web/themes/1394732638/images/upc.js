var AutoPlay=new Class({options:{autoplay:true,interval:3000,pauseOnHover:true},_autoInit:function(a){if(!this.options.autoplay){return}this.autofn=a||$empty;this.autoEvent().startAutoplay()},autoEvent:function(){if(this.options.pauseOnHover&&this.container){this.container.addEvents({mouseenter:this.stopAutoplay.bind(this),mouseleave:function(){this.paused=false;this.startAutoplay()}.bind(this)})}return this},startAutoplay:function(){this.autoTimer=function(){if(this.paused){return}this.autofn()}.periodical(this.options.interval,this)},stopAutoplay:function(){if(this.autoTimer){$clear(this.autoTimer);this.autoTimer=undefined}this.paused=true}});var LazyLoad=new Class({Implements:[Options,Events],options:{img:"img-lazyload",textarea:"textarea-lazyload",lazyDataType:"textarea",execScript:true,islazyload:true,lazyEventType:"beforeSwitch"},loadCustomLazyData:function(e,b){var c,f,a=this.options.textarea,d=this.options.img;if(!this.options.islazyload){return}$splat(e).each(function(g){switch(b){case"img":f=g.nodeName==="IMG"?[g]:$ES("img",g);f.each(function(h){this.loadImgSrc(h,d)},this);break;default:c=$E("textarea",g);if(c&&c.hasClass(a)){this.loadAreaData(g,c)}break}},this)},loadImgSrc:function(b,a){a=a||this.options.img;var c=b.getProperty(a);b.removeProperty(a);if(c&&b.src!=c){new Asset.image(c,{onload:function(d){b.src=c},onerror:function(){if(window.ie&&this.options.IE_show_alt){new Element("span",{"class":"error-img",text:b.alt||b.title}).inject(b,"after");b.remove()}}.bind(this)})}},loadAreaData:function(a,c){c.setStyle("display","none").className="";var b=new Element("div").inject(c,"before");b.innerHTML=this.options.execScript?this.stripScripts(c.value):c.value},isAllDone:function(){var f=this.options.lazyDataType,b=this.options[f],d,e,a,c=f==="img";if(f){d=$ES(f,this.container);for(e=0,a=d.length;e<a;e++){if(c?d[e].get(b):d[e].hasClass(b)){return false}}}return true},stripScripts:function(b,c){var a="";var d=b.replace(/<script[^>]*>([\s\S]*?)<\/script>/gi,function(){a+=arguments[1]+"\n";return""});if(!c){$exec(a)}return d},_lazyloadInit:function(a){var b=function(){var c=$type(a)=="function"?a(arguments):a;this.loadCustomLazyData(c,this.options.lazyDataType);if(this.isAllDone()){this.removeEvent(this.options.lazyEventType,arguments.callee)}};this.addEvent(this.options.lazyEventType,b.bind(this))}});var Tabs=new Class({Implements:[AutoPlay,LazyLoad],options:{onLoad:$empty,onInit:$empty,onBeforeSwitch:$empty,onSwitch:$empty,eventType:"mouse",hasTriggers:true,triggersBox:".switchable-triggerBox",triggers:".switchable-trigger",panels:".switchable-panel",content:".switchable-content",activeIndex:0,activeClass:"active",steps:1,delay:100,haslrbtn:false,prev:".prev",next:".next",autoplay:false,disableCls:null},initialize:function(a,b){this.container=$(a);if(!this.container){return}this.setOptions(b);this.activeIndex=this.options.activeIndex;this.init()},init:function(){this.fireEvent("load");this.getMarkup().triggersEvent().extendPlugins();if(this.options.hasTriggers){this.triggers[this.activeIndex].addClass(this.options.activeClass)}if(this.options.islazyload){this.fireEvent("beforeSwitch",{toIndex:this.activeIndex})}this.fireEvent("init")},extendPlugins:function(){var a=this.options;if(a.autoplay){this._autoInit(this.autofn.bind(this))}if(a.islazyload){this._lazyloadInit(this.getLazyPanel.bind(this))}Tabs.plugins.each(function(b){if(b.init){b.init.call(this)}},this)},autofn:function(){var a=this.activeIndex<this.length-1?this.activeIndex+1:0;this.switchTo(a,"FORWARD")},getMarkup:function(){var a=this.container,b=this.options;var c=$(b.triggersBox)||$E(b.triggersBox,a);this.triggers=c?c.getChildren():$ES(b.triggers,a);panels=this.panels=$ES(b.panels,a);this.content=$(b.content)||$E(b.content,a)?$E(b.content,a):panels[0].getParent();this.content=$splat(this.content);if(!panels.length&&this.content.length){this.panels=this.content[0].getChildren()}this.length=this.panels.length/b.steps;return this},triggersEvent:function(){var a=this.options,b=this.triggers;if(a.hasTriggers){b.each(function(d,c){d.addEvent("click",function(f){if(!this.triggerIsValid(c)){return}this.cancelTimer().switchTo(c)}.bind(this));if(a.eventType==="mouse"){d.addEvents({mouseenter:function(f){if(!this.triggerIsValid(c)){return}this.switchTimer=this.switchTo.delay(a.delay,this,c)}.bind(this),mouseleave:this.cancelTimer.bind(this)})}},this)}if(a.haslrbtn){this.lrbtn()}return this},lrbtn:function(){["prev","next"].each(function(a){this[a+"btn"]=$E(this.options[a],this.container).addEvent("click",function(b){if(!$(b.target).hasClass(this.options.disableCls)){this[a]()}}.bind(this))},this);this.disabledBtn()},disabledBtn:function(){var a=this.options.disableCls;if(a){this.addEvent("switch",function(d){var b=d.currentIndex,c=(b===0)?this["prevbtn"]:(b===Math.ceil(this.length)-1)?this["nextbtn"]:undefined;this["nextbtn"].removeClass(a);this["prevbtn"].removeClass(a);if(c){c.addClass(a)}}.bind(this))}},triggerIsValid:function(a){return this.activeIndex!==a},cancelTimer:function(){if(this.switchTimer){$clear(this.switchTimer);this.switchTimer=undefined}return this},switchTo:function(b,f){var i=this.options,d=this.triggers,c=this.panels,h=this.activeIndex,e=i.steps,g=h*e,a=b*e;if(!this.triggerIsValid(b)){return this}this.fireEvent("beforeSwitch",{toIndex:b});if(i.hasTriggers){this.switchTrigger(h>-1?d[h]:null,d[b])}if(f===undefined){f=b>h?"FORWARD":"BACKWARD"}this.switchView(c.slice(g,g+e),c.slice(a,a+e),b,f);this.activeIndex=b;return this.fireEvent("switch",{currentIndex:b})},switchTrigger:function(d,a,b){var c=this.options.activeClass;if(d){d.removeClass(c)}a.addClass(c)},switchView:function(d,c,a,b){d[0].setStyle("display","none");c[0].setStyle("display","")},prev:function(){var a=this.activeIndex;this.switchTo(a>0?a-1:this.length-1,"BACKWARD")},next:function(){var a=this.activeIndex;this.switchTo(a<this.length-1?a+1:0,"FORWARD")},getLazyPanel:function(b){var a=this.options.steps,d=b[0].toIndex*a,c=d+a;return this.panels.slice(d,c)}});Tabs.plugins=[];Tabs.Effects={none:function(b,a){b[0].setStyle("display","none");a[0].setStyle("display","block")},fade:function(d,a){if(d.length!==1){throw new Error("fade effect only supports steps == 1.")}var c=d[0],b=a[0];if(this.anim){this.anim.cancel()}this.anim=new Fx.Tween(c,{duration:this.options.duration,onStart:function(){b.setStyle("opacity",1)},onCancel:function(){this.element.setStyles({opacity:0});this.fireEvent("complete")},onComplete:function(){b.setStyle("zIndex",9);c.setStyle("zIndex",1);this.anim=undefined}.bind(this)}).start("opacity",1,0)},scroll:function(l,k,f,r){var j=this,d=this.options,o=this.activeIndex,g=d.effect==="scrollx",q=this.length,n=this.content[0],e=this.viewSize[g?0:1],p=d.steps,c=this.panels,b=g?"left":"top",i=-e*f,m,h,a=r!=="FORWARD";h=(a&&o===0&&f===q-1)||(!a&&o===q-1&&f===0);if(h){i=s.call(this,true)}fromp=n.getStyle(b).toInt();fromp=isNaN(fromp)?0:fromp;if(this.anim){this.anim.cancel()}this.anim=new Fx.Tween(n,{duration:this.options.duration,onComplete:function(){if(h){s.call(j)}this.anim=undefined}.bind(this)}).start(b,fromp,i);function s(v){var y=a?q-1:0,x=y*p,w=(y+1)*p,u;for(u=x;u<w;u++){var t=(a?-1:1)*e*q;c[u].setStyle("position",v?"relative":"").setStyle(b,v?t:"")}if(v){return a?e:-e*q}return n.setStyle(b,a?-e*(q-1):"")}}};Effects=Tabs.Effects;Effects.scrollx=Effects.scrolly=Effects.scroll;var Switchable=new Class({Extends:Tabs,options:{autoplay:true,effect:"none",circular:false,duration:500,direction:"FORWARD",viewSize:[],position:"absolute"},extendPlugins:function(){this.parent();this.effInit()},effInit:function(){var j=this.options,i=j.effect,d=this.panels,c=this.content[0],f=j.steps,g=this.activeIndex,b=d.length;this.viewSize=[j.viewSize[0]||d[0].getSize().x*f,j.viewSize[1]||d[0].getSize().y*f];if(i!=="none"){switch(i){case"scrollx":case"scrolly":if(j.position==="absolute"){c.setStyle("position","absolute");c.getParent().setStyle("position","relative");if(i==="scrollx"){d.setStyle("float","left");c.setStyle("width",this.viewSize[0]*(b/f))}}if(j.position==="relative"){c.setStyle("position","relative");c.getParent().setStyles({position:"relative",width:this.viewSize[0]});if(i==="scrollx"){d.setStyles({"float":"left",width:this.viewSize[0]});c.setStyle("width",this.viewSize[0]*(b/f))}}break;case"fade":var a=g*f,e=a+f-1,h;d.each(function(k,l){h=l>=a&&l<=e;k.setStyles({opacity:h?1:0,position:"absolute",zIndex:h?9:1})});break;default:break}}},switchView:function(h,a,c,g){var b=this.options,f=b.effect,e=b.circular,d=$type(f)=="function"?f:Effects[f];if(e){g=b.direction}if(d){d.call(this,h,a,c,g)}}});Switchable.autoRender=function(b,d){var c=b||".Auto_Widget",a=$(d||document.body).getElements(c);if(a.length){a.each(function(h){var g=h.get("data-widget-type"),f;if(g&&("Tabs Switchable Popup Countdown".indexOf(g)>-1)){try{f=h.get("data-widget-config")||{};if(g=="DataLazyLoad"){return new window[g](JSON.decode(f),h)}new window[g](h,JSON.decode(f))}catch(i){}}})}};window.addEvent("load",function(){Switchable.autoRender.call(Switchable)});