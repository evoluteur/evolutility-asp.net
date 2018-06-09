//      www.evolutility.org 
//      (c) 2011 Olivier Giulieri 
var h;
function tProd(){ 
SetH('Overview, Features, Possibilities...',400);
//'What is it?';
}
function tDemo(){
SetH('Things you do with Evolutility...',532);
//h.innerHTML='What is it for?';
}
function tDown(){ 
SetH('Get it now !',654);
//h.innerHTML='Can I have one?';
}
function tDico(){ 
SetH('No more XML!!!',620);
//h.innerHTML='Use the database instead of XML';
}
function tMore(){ 
SetH('Documentation, News, Contributions...',720) 
}
function tO(){
h.style.display='none';
}
function SetH(t,m){
if(h==null){
	h=document.getElementById('TabHelp');
	if(h==null){
		var hc=document.createElement('div');
		hc.className='TabH';
		hc.innerHTML='<div id="TabHelp"></div>';
		document.body.appendChild(hc);
		h=hc.firstChild;
	}
}
h.innerHTML=t;
if(m!=null)
	h.style.left=m+"px";
h.style.display='block';
}

//google analytics
var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
try {
var pageTracker = _gat._getTracker("UA-6608267-1");
pageTracker._trackPageview();
} catch(err) {}
