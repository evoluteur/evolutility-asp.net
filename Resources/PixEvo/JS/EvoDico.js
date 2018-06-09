//   EvoDico Library by Evolutility - Last update 03/30/2009 
//   www.evolutility.org - (c) 2009 Olivier Giulieri 

//	Copyright (c) 2003-2009 Olivier Giulieri
//  email: evoluteur at evolutility dot org 

//	This file is part of Evolutility CRUD Framework.
//	Source link <http://www.evolutility.org/download/download.aspx>

//	Evolutility is free software: you can redistribute it and/or modify
//	it under the terms of the GNU Affero General Public License as published by
//	the Free Software Foundation, either version 3 of the License, or
//	(at your option) any later version.

//	Evolutility is distributed in the hope that it will be useful,
//	but WITHOUT ANY WARRANTY; without even the implied warranty of
//	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//	GNU Affero General Public License for more details.

//	You should have received a copy of the GNU Affero General Public License
//	along with Evolutility. If not, see <http://www.gnu.org/licenses/>.


var EvoDico={

    version:"2.5.3",
    
    edit:function(to,mU,label){
        var ws=wo=null;
        switch(to){
        case 'fld':
            ws='HEIGHT=440,WIDTH=480';
            wo='Field';
            break;
        case 'pnl':
            ws='HEIGHT=360,WIDTH=400';
            wo="Panel";
            break;
        case 'frm':
            ws='HEIGHT=440,WIDTH=560';
            wo='Form';
            break;
        case 'src':
            ws='HEIGHT=560,WIDTH=460';
            wo='Form searches';
            break;
        }
        top.f_dialogOpen(mU,['<span class="des-',to,'"></span>&nbsp;',wo].join(''),ws);         
    },

    insert:function(to,mU,label){ 
		debugger;
        ws='top=210,HEIGHT=380,WIDTH=400';
        wo='Field';
        divwin=dhtmlwindow.open('divbox','iframe',mU+'&MODE=new',wo+' : '+label,ws+'left=200px,resize=1,scrolling=1');
    } 

//    del:function(){ 
//		debugger;
//    },
//     
//    editLabel:function(that,fieldID){ 
//        that.ondblclick=null;
//        var w=200,c=that.innerHTML,i=c.indexOf("<span");
//        if(i>0)
//            c=c.substr(0,i);
//        else
//            w=that.offsetWidth*1.6;
//        that.innerHTML='<input id="'+fieldID+'" value="'+c+'" class="FieldLabelEdit" style="Width:'+w+'"></input>';
//    } 
       
}

// Drag & drop from softcomplex.com
var N_BASEZINDEX=0;
var RE_PARAM=/^\s*(\w+)\s*\=\s*(.*)\s*$/;

// this function makes the document numb to the mouse events by placing the transparent layer over it
function f_putScreen(b_show){ 
	//Evol.fadeScreen();
	if(b_show==null&&!window.b_screenOn)
		return;
	if(b_show==false){
		window.b_screenOn=false;
		if(e_screen)
			e_screen.style.display='none';
		return;
	}
	// create the layer if doesn't exist
	if(window.e_screen==null){
		window.e_screen=document.createElement("div");
		e_screen.innerHTML="&nbsp;";
		document.body.appendChild(e_screen);
		e_screen.style.position='absolute';
		e_screen.id='eScreen';		
		// attach event
		if(document.addEventListener){
			document.addEventListener('mousemove',f_dragProgress,false);
			window.addEventListener('resize',f_putScreen,false);
			window.addEventListener('scroll',f_putScreen,false);
		}
		if(window.attachEvent){
			document.attachEvent('onmousemove',f_dragProgress);
			window.attachEvent('onresize',f_putScreen);
			window.attachEvent('onscroll',f_putScreen);
		}else{
			document.onmousemove=f_dragProgress;
			window.onresize=f_putScreen;
			window.onscroll=f_putScreen;
		}
	}
	// set properties
	var a_docSize=f_documentSize();
	var ss=e_screen.style;
	ss.left=a_docSize[2]+'px';
	ss.top=a_docSize[3]+'px';
	ss.width=a_docSize[0]+'px';
	ss.height=a_docSize[1]+'px';
	ss.zIndex=N_BASEZINDEX+a_windows.length*2-1;
	//ss.zIndex=5000;
	ss.display='block';
}

// returns the size of the document
function f_documentSize(){
	var scrollX=0,scrollY=0,dde=document.documentElement;
	if(typeof(window.pageYOffset)=='number'){
		scrollX=window.pageXOffset;
		scrollY=window.pageYOffset;
	}
	else if(document.body && (document.body.scrollLeft || document.body.scrollTop)){
		scrollX=document.body.scrollLeft;
		scrollY=document.body.scrollTop;
	}
	else {
		if(dde && (dde.scrollLeft || dde.scrollTop)){
			scrollX=dde.scrollLeft;
			scrollY=dde.scrollTop;
		}
	}
	if(typeof(window.innerWidth)=='number')
		return [window.innerWidth,window.innerHeight,scrollX,scrollY];
	if(dde && (dde.clientWidth || dde.clientHeight))
		return [dde.clientWidth,dde.clientHeight,scrollX,scrollY];
	if(document.body &&(document.body.clientWidth || document.body.clientHeight))
		return [document.body.clientWidth,document.body.clientHeight,scrollX,scrollY];
	return [0, 0];
}

function f_dialogOpen(url,title,features){
	if(!window.a_windows)
		window.a_windows=[];
	// parse parameters
	var featuresS=features.split(',');
	var a_features=[];
	for(var i=0;i<featuresS.length;i++)
		if(featuresS[i].match(RE_PARAM))
			a_features[String(RegExp.$1).toLowerCase()]=RegExp.$2;
	// create element for window
	var n_nesting=a_windows.length;
	var ew=document.createElement("div");
	ew.style.position='absolute';
	var n_width=a_features.width?parseInt(a_features.width):300;
	var n_height=a_features.height?parseInt(a_features.height):200;
	var a_docSize=f_documentSize();
	var ews=ew.style;
	ews.left=(a_features.left?parseInt(a_features.left):((a_docSize[0]-n_width)/2)+a_docSize[2])+'px';
	ews.top =(a_features.top?parseInt(a_features.top):((a_docSize[1]-n_height)/2)+a_docSize[3])+'px';
	ews.zIndex=N_BASEZINDEX+a_windows.length*2+2;
	ew.innerHTML=[
		'<table border="2" class="',
		(a_features.css?a_features.css:'dialogWindow'),
		'"><tr><th onmousedown="f_dragStart(',n_nesting,', event)" onmouseup="f_dragEnd()" onmousemove="f_dragProgress(event)" onselectstart="return false"><span style="float:left"">',
		(title?title:'Evolutility designer'),
		'</span><div onclick="top.f_dialogClose();" onmousedown="return false;" style="float:right" class="close"></th></tr><tr><td><iframe width="',n_width,
		'" height="',n_height,
		'" src="',url,'"></iframe></td></tr></table>'].join('');
	document.body.appendChild(ew);
	a_windows[n_nesting]=ew;
	// put the screen
	f_putScreen(true);
}

function f_dialogClose(){
	var n_nesting=a_windows.length-1;
	// destroy element
	if(a_windows[n_nesting].removeNode)
		a_windows[n_nesting].removeNode(true);
	else if(document.body.removeChild)
		document.body.removeChild(a_windows[n_nesting]);
	a_windows[n_nesting]=null;
	a_windows.length=n_nesting;
	// move the screen
	f_putScreen(n_nesting?true:false);
}

// drag'n'drop functions
function f_dragStart(s_name,e_event){
	if(!e_event && window.event) 
		e_event=window.event;
	// save mouse coordinates
	window.n_mouseX=e_event.clientX;
	window.n_mouseY=e_event.clientY;
	window.e_draggedWindow=window.a_windows[s_name];
	return false;
}
function f_dragProgress(e_event){
	if(!e_event && window.event) 
		e_event=window.event;
	if(!e_event||window.e_draggedWindow==null) 
		return;
	var mouseX=e_event.clientX;
	var mouseY=e_event.clientY;
	var dws=window.e_draggedWindow.style;	
	dws.left=(parseInt(dws.left)-window.n_mouseX+mouseX)+'px';
	dws.top =(parseInt(dws.top)-window.n_mouseY+mouseY)+'px';	
	window.n_mouseX=mouseX;
	window.n_mouseY=mouseY;
}

function f_dragEnd(){
	window.e_draggedWindow=null;
}

