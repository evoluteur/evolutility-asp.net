//   Evolutility Library 
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


// ############  Evolutility.JS  #################################################################
/*
	Evol: Core features
	EvoVal: Validation
	EvoHelp: User help
	EvoGrid: Edit grid
	EvoExport: Export forms
	EvoUI: UI helpers
	
	EvoGen: generated model w/ fields info and Grid def for edit
*/

// ############  Evol #################################################################

var Evol={

	version:'3.0',
	
	prefix:'EVOLU_',
	sep:'~!',
	modeIDs:{'search':3,'searchp':4},

	setup:function(){
		// Toolbar
		var tb=e$('Toolbar');
		if(!EvoGen.isSet){
			var ie=EvoUI.isIE();
			EvoGen.mode=e$('EVOL_Mode').value; 
			EvoGen.cacheForms={search:null,searchp:null,sep:null};
			EvoGen.tbBttn={};
			if(tb!=null){
				// hack to fix labels on top of icons with DOCTYPE XHTML Transitional or Strict 
				var v,dfc=document.firstChild;
				var sp5=null;
				if(dfc!=null){
					if(EvoUI.isIE()){
						v=dfc.nodeValue;
						if(v!=null && v.substring(0,5)!='CTYPE')
							v=null;
					}
					else
						v=dfc.publicId;
					if(v!=null&&v.length>12){
						var iX=v.indexOf('XHTML');
						if(iX>-1 && (v.indexOf('Transitional',iX)>0 || v.indexOf('Strict',iX)>0))
							sp5='&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;';
					}
				}
				// setup toolbar events
				var tb=tb.firstChild;
				var c=tb.getElementsByTagName('a');
				for(var i=0;i<c.length;i++){
					if(sp5!=null)
						c[i].innerHTML=[sp5,c[i].innerHTML].join('');
					if(c[i].href.substring(0,4).toLowerCase()!='java'){
						var url,cn=c[i].className;
						var cnA=cn.indexOf(' act'); // assumes last of several classnames
						if(cnA>0){
							cn=cn.substring(0,cnA);
							switch(cn){
							case 'del':
								url='Evol.deleteItem()';
								break;
							case 'print':
								url='print()';
								break;
							case 'help':
								url='EvoHelp.setHelpContent()';
								break;
							case 'search':	// 3
							case 'searchp': // 4
								cn='search';
							case 'sel': // 60
								url=['Evol.showForm(\'',cn,'\')'].join('');
								break;
							default:
								var toDo={view:0,edit:1,'new':12,all:110,logout:49};
								url=['EvPost(',toDo[cn],')'].join('');
								break;
							}
						}else
							url='void(0)';
						c[i].href='javascript:'+url;
						EvoGen.tbBttn[cn]=c[i];
					}
				}
			}
			EvoGen.isSet=true;
		}
		if(EvoGen.mode<2){
			// Tabs
			var th=e$('evoTabs');
			if(th!=null){
				var c=th.getElementsByTagName('a');
				var lB="javascript:Evol.selTab('z','";
				for(var i=0;i<c.length;i++){
					c[i].id='zTabB'+i;
					var l=[lB,i,"')"].join('');
					c[i].href=l;
				}
			}
		}
		switch(EvoGen.mode){
			case '1': // edit view
				// FCKeditor - Rich Text Editor
				var FCKbad=false;
				var fds=EvoGen.fields;
				for(var i in fds){
					var fd=fds[i];
					if(fd.t=='html'){
						try{ 
							var oFCKeditor=new FCKeditor(Evol.prefix+fd.id);
							oFCKeditor.BasePath=EvoGen.path+"FCKeditor/";
							oFCKeditor.Config["AutoDetectLanguage"]=false;
					 		oFCKeditor.Config["DefaultLanguage"]=EvoGen.lang.toLowerCase();
							oFCKeditor.ReplaceTextarea();
						}catch(err){
							FCKbad=true;
						}
					}
				}
				if(FCKbad)
					alert('Error: Cannot find widget FCKeditor.')
				// Details
				if(EvoUI.isNN(EvoGen.details)){
					for(var i in EvoGen.details.lst)
						EvoGrid.setup(i);
				}
				// Dependent dropdowns
				var fds=EvoGen.fields;
				for(var i in fds){
					var fd=fds[i];
					if(EvoUI.isNN(fd.dep)){
						var f=e$(Evol.prefix+fd.id);
						if(f!=null){
							Evol.setLovContent(fd.id,fd.dep,ie);
							EvoUI.setOnClick(f,['Evol.setLovContent("',fd.id,'","',fd.dep,'")'].join(''),ie);
						}
					}
				}
				break;
			//case '3': //search view
			case '4': //adv search view
				var t=e$('GridSearch')
				if(t!=null){
					var rs=t.rows,rn=rs.length;
					if(rn>0)
						for(var i=0;i<rn;i++){
							var fc=rs[i].childNodes[1].firstChild;
							if(fc!=null&&fc.tagName=='SELECT')
								EvoUI.setOnClick(fc,'Evol.newOp(this)',ie);						
						}
				}
				break;
		}
	},

	// Generic. used for Search and Adv Search for now.
	showForm:function(m){
		isSearch= m=='search';
		if(isSearch){
			m1=e$('EVOL_Mode').value;
			if(m1==3)
				m='searchp';
		}
		if((isSearch || m=='sel') && EvoGen.cacheForms[m]!=null){
			Evol.setForm(EvoGen.cacheForms[m],m);
			return;
		}
		var prm=['action=getform&formID=',EvoGen.formid,'&form=',m].join('');
			var u=location.href; 
			var u1=u.lastIndexOf('\/'); 
			var pn=u.substring(u1+1);
			pn+=(pn.indexOf("?")>0)?"&":"?";
			EvoUI.AJAX(pn, prm, function(f){
				if(f.length>0){ 
					if(EvoGen.cacheForms[m]==null)
						EvoGen.cacheForms[m]=f;
					Evol.setForm(f,m);
				}
			});
	},	
	 
	setForm:function(f,mode){
		function disableTbButton(b,css){
			if(b!=null&&b.className!=css){
				b.className=css;
				b.href='javascript:void(0)';
			}
		}

		var c=e$(EvoGen.id+'_Content');
		var ft=f.indexOf('@!#@'); // hack
		if(ft>0){
			Evol.SetFormTitle(f.substring(0,ft));
			f=f.substring(ft+4);
		}
		c.innerHTML=f;
		var m=e$('EVOL_Mode');
		EvoGen.mode=Evol.modeIDs[mode];
		m.value=EvoGen.mode;
		m=e$('EVOL_ItemID');
		m.value=0;
		var allB=EvoGen.tbBttn;
		if(mode=='search'||mode=='searchp'||mode=='sel'){
			var b=allB.search;
			if(b!=null)
				b.className=(mode=='search')?'searchp act':'search act';
			disableTbButton(allB.del,'delZ');
			disableTbButton(allB.edit,'editZ');	
			disableTbButton(allB.view,'editZ');
			b=e$('EvoRecPage');
			if(b!=null)
				b.style.display='none';	
		}
		var tmsg=e$('Msg');
		if(tmsg!=null)
			tmsg.style.display='none';
		Evol.setup();
	},  
	SetFormTitle:function(t){
		var te=e$("EVOL_Title")
		if(te!=null)
			te.innerHTML=t;
	},
	
	addFldLabel:function(f,many){
		var fh=EvoUI.getOrCreateHidden(f.name+'_lbl',f.parentElement);
		if(many){
			var txt=[];
			var maxLoop=f.options.length
			for (var i=0;i <maxLoop;i++){
				var o=f.options[i];
				if (o.selected){
					txt.push((document.all)?o.innerText:o.textContent); 
				}
			}
			fh.value=txt.join(', ');
		}else{
			var o=f.options[f.selectedIndex];
			fh.value=(document.all)?o.innerText:o.textContent;
		}
	},
	newOp:function(f){
		var v=f.value, d=!(v=='null'||v=='nn');
		f.parentElement.parentElement.childNodes[2].style.display=(d)?'':'none';
	},	

	// Comments
	commentsForm:function(){
		var frm=['<hr><div class="FieldLabel" onmouseover="javascript:EvoUI.showResize(\'EVOLComPost\',-1,this)"><label for="EVOLComPost">',
			EvolLang.comments,'</label></div>',
			EvoUI.inputTextM('EVOLComPost','',2000,4),
			'<br/><input type="button" onclick="EvPost(\'0\')" class="button" name="puc" value=" ',
			EvolLang.post,' "><br/>&nbsp;'];
		var f=e$('evoCOMcfz');
		f.innerHTML=frm.join('');
		Evol.showMore('evoCOMcfz',1);
	},

	// Upload UI
	docM:function(fn){
		e$(fn+'_dp').value='1';
	},

	pixM:function(fn){
		Evol.docM(fn);
		e$(fn+'img').src=EvoGen.path+'imgdelete.gif';
	},

	deleteItem:function(){
		Evol.showLB(EvolLang.del.replace('{0}',EvoGen.entity)+'<br/>&nbsp;',null,'EvPost(\'10\')','del');
	},

	// Dependent drop-downs 
	setLovContent:function(fm,fs){ 
		var f=e$(Evol.prefix+fm),id=f.value; //id=f.options[f.selectedIndex].value;
		var prm=["action=getlov&formID=",EvoGen.formID,"&id=",id,"&fm=",fm,"&fs=",fs].join('');
		EvoUI.AJAX("evolutility.aspx?", prm, function(r){
			Evol.setLov(r,fs);
		});
 	},

	setLov:function(r,fs){
		if(r!=null){
			var f=e$(Evol.prefix+fs),err=0;
			if(f!=null){
				if(!f.disabled){
					EvoUI.setEnabledF(f,false);
					var pV=f.value;
					try{
						eval("var flds="+r+";");
					}catch(err){err=1}
					if(err<1){
						var fcs=f.childNodes,ml=flds.length,mlo=fcs.length;
						for(var i=0;i<mlo;i++){ 
							var c=fcs[i];
							if(i<ml){
								c.text=flds[i].v;
								c.value=flds[i].id;
							}else
								f.remove(ml);
						}
						for(var i=mlo;i<ml;i++){
							var c=document.createElement("option");
							c.text=flds[i].v;
							c.value=flds[i].id; 
							f.options.add(c);
						}
						f.value=pV;
						if(f.value=='')
							f.selectedIndex=0;
					}
					EvoUI.setEnabledF(f,true);
				}
			}
		}
 	},

	// Lightbox - original code from http://www.emanueleferonato.com/
	showLB:function(txt,ff,fn,ico){
		var dfc=window.document.firstChild;
		var ss=EvoUI.getOrCreate('fadeEVOL').style;
		Evol.fadeScreen(ss);
		var lb=EvoUI.getOrCreate('lightEVOL');
		//position:absolute;top:25%;left:25%;width:50%;padding:16px;
		var msg=['<table class="LB_sep" style="background-color:white;"><tr valign="top">'];
		if(ico!=null){
			msg.push(['<td width="20px"><span class="ico msg',ico,'">&nbsp;</span></td><td>&nbsp;'].join(''));
		}else{
			msg.push('<td>');
		}
		msg.push(txt);
		msg.push('</td></tr><tr><td>');
		if(ico!=null)
			msg.push('</td><td>');
		msg.push('<p align="right"><a href="javascript:');
		if(fn!=null){
			msg.push([fn,'">',EvolLang.ok,'</a>&nbsp;&nbsp;&nbsp;<a href="javascript:Evol.closeLB();">',EvolLang.cancel].join(''));
		}else{
			msg.push('Evol.closeLB();');
			if(ff!=null){
				msg.push(['EvoUI.focus(\'',ff,'\')">',EvolLang.close].join(''));
			}
		}
		msg.push('</a>&nbsp;</p>');
		msg.push('</td></tr></table>');	
		lb.innerHTML=msg.join("");
		var lbs=lb.style;
		lbs.position='absolute';
		lbs.pixelTop=ss.pixelTop+parseInt(dfc.offsetHeight/4);
		lbs.display='block';
		document.body.style.overflow='hidden';
	},

	fadeScreen:function(ss){
		if(ss==null) 
			var ss=EvoUI.getOrCreate('fadeEVOL').style;
		ss.position='absolute';
		ss.pixelTop=(document.all)?document.body.scrollTop:window.pageYOffset;
		ss.pixelLeft=(document.all)?document.body.scrollLeft:window.pageXOffset;
		var dfc=window.document.firstChild;
		ss.height=dfc.offsetHeight;
		ss.width=dfc.offsetWidth;
		ss.display='block';	 
	},

	closeLB:function(){ 
		document.body.style.overflow='auto';
		EvoUI.setDisplay('lightEVOL','none');
		EvoUI.setDisplay('fadeEVOL','none'); 
	},

	// Panels & Tabs 
	togglePanel:function(pID,b){
		var c,p=e$(pID)
		if(p==null)
			return false;
		var ps=p.style,d=(ps.display=='none');
		ps.overflow='hidden';
		if(d){
			c='close';
			ps.height=0;
			ps.display='';
		}else{
			c='open';
			p.mh=p.offsetHeight;
			ps.height=p.mh;
		}
		EvoUI.slide(p,d);
		if(b){
			var l=e$(pID+'link');
			if(l!=null)
				l.className='ico panel'+c;
		}
	},

	showMore:function(pID,b){ 
		this.togglePanel(pID,0);
		if(b)
			this.togglePanel(pID+'link',0);
	},

	selTab:function(t,n){
		var tn=t+'Tab';
		EvoUI.setDisplay(tn+n,'');
		e$(tn+'B'+n).className='tabselected';
		if(TABevo!=n){
			EvoUI.setDisplay(tn+TABevo,'none');
			e$(tn+'B'+TABevo).className='tab';
			TABevo=e$('EvoActTab').value=n;
		}
	}

}

// ############ EvoVal #################################################################

var EvoVal={ // Validation
	
	checkMaxLen:function(F,maxL){
		if(F.value.length>maxL)
			F.value=F.value.substring(0,maxL-1)
	},

	checkNum:function(F,t){
		var nv,fv=F.value;
		if(t.substring(0,1)=='i')
			nv=parseInt(fv)
		else{
			var ln=EvolLang.LOCALE;
			if(ln=='FR'||ln=='DA')
				fv=fv.replace(",",".");
			nv=parseFloat(fv);
		}
		if(isNaN(nv))
			F.value=''
		else if(fv!=nv)
			F.value=nv;
	},

	validForm:function(z){
		EvoGrid.disableRow(null);
		if(EvoVal.checkFields(EvoGen.fields))
			EvPost((z==9)?'25':'24')
		else
			return false
	},

	checkFields:function(fds){
		var evoRegEx={
			email:/^[\w\.\-]+@[\w\.\-]+\.[\w\.\-]+$/,
			integer:/^-?\d+$/,
			decimalEN:/^\d+(\.\d+)?$/,
			decimalFR:/^\d+(\,\d+)?$/,
			decimalDA:/^\d+(\,\d+)?$/
		};
		var msgs=[],ff=null;
		for(var i in fds){
			var fd=fds[i], f=e$(Evol.prefix+fd.id);
			if(f!=null){
				var ner=true,nm=msgs.length;
				// Check empty & type
				if(fd.r>0){ 
					if(isEmpty(f)){
						labMsg(EvolLang.empty);
						ner=false;
					}else
						typeCheck();
				}else
					typeCheck();
				// Check regexp
				if(fd.rg!=null){
					var rg=new RegExp(fd.rg);
					if(!f.value.match(rg))
						labMsg(EvolLang.reg,fd.rg);
				}
				// Check custom
				if(fd.jsv!=null){
					p=eval([fd.jsv,'("',Evol.prefix,fd.id,'","',fd.l,'")'].join(''));
					if(p!=null&&p.length>0)
						labMsg(p);
				}
				// Check min & max
				if(ner){
					var fv=f.value.trim();
					if(fv!=''){
						if(fd.max!=null&&parseFloat(fv)>fd.max)
							labMsg(EvolLang.max,fd.max);
						if(fd.min!=null&&parseFloat(fv)<fd.min)
							labMsg(EvolLang.min,fd.min);
					}
				}
				flagValid(f,nm==msgs.length);
			}
		}
		if(msgs.length>0)
			Evol.showLB([EvolLang.intro,"<ul><li>",msgs.join("<li>"),"</li></ul>"].join(""),ff.id,null,"warn");
		else
			return true;

		function typeCheck(){
			var fv=f.value.trim();
			if(fv!='')
				switch(fd.t){
					case "integer":
					case "email":
						if(!evoRegEx[fd.t].test(fv))
							labMsg(EvolLang[fd.t]);
						break;
					case "decimal":
						if(!evoRegEx[fd.t+EvolLang.LOCALE].test(fv))
							labMsg(EvolLang[fd.t]);
						break;
					case "datetime":
					case "date":
						if((fv!='')&&(EvolLang.LOCALE=='EN')&&(!Date.parse(fv)))
							labMsg(EvolLang[fd.t]);
						break;
				}
		}
		function isEmpty(f){
			if(f.tagName=="SELECT"&&f.selectedIndex>-1)
				return(f.options[f.selectedIndex].value=="0");
			else
				return(f.value.trim()=="");
		}
		function labMsg(msg,r2){
			var m=msg.replace('{0}',fd.l);
			if(r2!=null)
				m=m.replace('{1}',r2);
			msgs.push(m);
			if(ff==null)
				ff=f;
		}
		function flagValid(f,v){
			var cfi="FieldInvalid";
			var cn=f.className.split(" ");
			if(v){
				for(var i in cn)
					if(cn[i]==cfi)
						delete cn[i];
			}else if(!(cfi in cn))
				cn.push(cfi);
			f.className=cn.join(" ").trim();
		}
		
	}
	
}

// ############ EvoHelp #################################################################

var EvoHelp={
	
	cState:0,
	
	setHelp:function(fds){
		if(EvoHelp.cState==0){
			for(var i in fds){
				var fd=fds[i], f=e$(Evol.prefix+fd.id);
				if(f!=null && EvoUI.isNN(fd.help)){
					var p=f.parentNode, e=EvoUI.createElem('span','tip'+fd.id);
					e.innerHTML=['<br/><span class="HelpTip">',fd.help,'</span>'].join('');
					p.appendChild(e);
				}
			}
			EvoHelp.cState=2;
		}else{
			var s=(EvoHelp.cState==1);
			for(var i in fds){
				var fd=fds[i];
				if(EvoUI.isNN(fd.help))
					EvoUI.showOrHide('tip'+fd.id,s);
			}
			EvoHelp.cState=s?2:1;
		}
	},

	setHelpContent:function(){
		var prm="action=gethelp&formID="+EvoGen.formid;
		EvoUI.AJAX("evolutility.aspx?", prm, function(r){
			eval("var flds="+r+";");
			if(flds.length>0)
				EvoHelp.setHelp(flds);
		});
 	}
 	
}

// ############ EvoExport #################################################################

var EvoExport={

	cFormat:'CSV',

	showFormatOpts:function(xFormat){
		var E=Evol.prefix;
		function showXPT(e,s1,s2){
			EvoUI.setDisplay(e+'csv2',s1);
			EvoUI.setDisplay(e+'tab2',s2);
		};
		switch(xFormat){
			case 'TAB':
				xFormat='CSV';
				showXPT(E,'none','');
				break;
			case 'CSV':
				showXPT(E,'','none');
				break;
			case 'HTML':
			case 'XML':
			case 'SQL':
				var p=e$(E+xFormat);
				if(p!=null&&p.innerHTML=='')
					p.innerHTML=EvoExport['form'+xFormat]();
				break;
		}
		var cFormat=EvoExport.cFormat;
		if(cFormat=='TAB')
			cFormat='CSV';
		EvoUI.setDisplay(E+cFormat,'none');
		EvoUI.setDisplay(E+xFormat,'');
		EvoExport.cFormat=xFormat;
	},

	formHTML:function(){
		function ColorBox(fID,lbl,fV){
			fID='evoColRC'+fID;
			return ['<tr><td colspan="3">',EvoUI.fieldLabel(fID,lbl),'</td></tr><tr><td valign="top"><input class="Field" type="text" style="width:120;" maxlength="20"',
			' onKeyUp="EvoExport.color(\'',fID,'\')" name="',fID,'" id="',fID,'" value="',fV,'"></td><td>&nbsp;</td><td ID="',fID,'COL"><div class="ColorBox" style="background:',fV,
			'"> </div></td></tr>'].join('');
		};
		var ls=EvolLang.xpColors.split('-');
		return ['<p><table border="0" class="holder" style="width:100">',
			ColorBox("T",ls[0],'#D5D5D5'),ColorBox("O",ls[1],'#EDEDED'),ColorBox("E",ls[2],'#F3F3F3'),
			"</table></p>"].join('');
	},

	formXML:function(){
		var b1='evoRoot',b2='evoxpC2X';
		return [EvoUI.fieldLabel(b1,EvolLang.xpXMLroot),
			EvoUI.inputText(b1,EvoGen.entities,30),'<br/><br/>',
			EvoUI.fieldLabel(b2,EvolLang.xpColMap),
			EvoUI.inputRadio(b2,"2",EvolLang.xpXMLAttr,true,"EVOLxr"),'<br/>',
			EvoUI.inputRadio(b2,"1",EvolLang.xpXMLElem,false,"LOVExr")].join('');
	},

	formSQL:function(){
		return ['<p>',
			EvoUI.fieldLabel('',EvolLang.xpSQL),
			EvoUI.inputCheckbox('evoxpTRS1','1'),EvoUI.fieldLabelSmall('evoxpTRS1',EvolLang.xpSQLTrans),'<br/>',
			EvoUI.inputCheckbox('evoxpTRS2','1'),EvoUI.fieldLabelSmall('evoxpTRS2',EvolLang.xpSQLId),
			'</p>'].join('');
	},

	color:function(e){
		var c=e$(e).value,cc=e$(e+'COL');
		if(c&&cc)
			cc.innerHTML=['<div class="ColorBox" style="background:',c,'"> </div>'].join('');
	}
	
}

// ############ EvoGrid #################################################################

var EvoGrid={
	
	setup:function(gID){
		var ds=EvoGen.details, gd=ds.lst[gID], g=e$('EvoEditGrid'+gID);
		gd.grid=g;
		gd.cRowID=-1;
		gd.gIDc=null;
		if(g!=null){
			var rs=g.childNodes[1].rows,ml=rs.length;
			var sfn=["EvoGrid.editRow('",gID,"',"].join('');
			var ie=EvoUI.isIE();
			for(var i=0;i<ml;i++){
				rs[i].id='r'+(i+1);
				EvoUI.setOnClick(rs[i],[sfn,i+1,')'].join(''),ie)
			}
		}
	},
	
	setSelected:function(f,s){
		f.style.backgroundColor=(s)?'#F5F5DC':'';
	},
	
	setRowCellContent:function(r,i,v){
		if(document.all)
			r.cells(i).innerText=v;
		else
			r.cells.item(i).textContent=v;
	},
	getRowCellContent:function(r,i){
		if(document.all)
			return r.cells(i).innerText;
		else
			return r.cells.item(i).textContent;
	},

	editRow:function(gID,rIX){
		var ds=EvoGen.details, gd=ds.lst[gID], fds=gd.flds, g=gd.grid;
		var gIDo=ds.gIDc;
		ds.gIDc=gID;
		if(gd.cRowID==rIX && gID==gIDo)
			return;
		var rw=g.rows.item(rIX);
		if(gIDo!=null){
			var cRowID=ds.lst[gIDo].cRowID;
			if(cRowID>0 && ((gIDo==gID && cRowID!=rIX) || gIDo!=gID))
				EvoGrid.disableRow(gIDo);
		}
		if(gd.cRowID!=rIX){
			EvoGrid.setSelected(rw,true);
			gd.cRow=rw;
			gd.cRowID=rIX;
			var rVs=[EvoGrid.getRowCellContent(gd.cRow,0),Evol.sep];
			for(var i in fds){
				var fd=fds[i];
				if(fd.t=='lov'){
					var c=rw.cells.item(fd.i);
					var vl=c.innerHTML, buf=c.intnum;
					c.innerHTML=EvoUI.inputLOV('evolGFE'+fd.i,buf,vl,fd.lov);
					rVs.push(buf);
				}else
					rVs.push(EvoGrid.setCell(rw,fd));
				rVs.push(Evol.sep);	
			}
			EvoGrid.setRowInfo(gID,gd.cRowID,rVs.join(''));
			if(gd.cRowID>-1)
				EvoUI.focus('evolGFE1');
		}
	},

	disableRow:function(gID){
		var ds=EvoGen.details;
		if(gID==null){
			if(ds!=null && ds.gIDc!=null)
				 EvoGrid.disableRow(ds.gIDc);
			return;
		}
		var cssRow='';
		var gd=ds.lst[gID], fds=gd.flds, g=gd.grid;
		if(gd.cRowID>0){
			EvoGrid.setSelected(gd.cRow,0);
			var rVs=[EvoGrid.getRowCellContent(gd.cRow,0),Evol.sep];
			for(var i in fds){
				var fd=fds[i];
				switch(fd.t){
					case 'boolean':
						rVs.push(EvoGrid.disableCellCheck(gd.cRow,fd.i,fd.pix));
						break;
					case 'date':
					case 'datetime':
					//case 'time':
						updateDateField('evolGFE'+fd.i);
						rVs.push(EvoGrid.disableCell(gd.cRow,fd.i));
						break;
					case 'lov':
						rVs.push(EvoGrid.disableCellLOV(gd.cRow,fd.i));
						break;
					default:
						rVs.push(EvoGrid.disableCell(gd.cRow,fd.i));
						break;
				} 
				if(fd.r>0 && rVs[rVs.length-1]=='')
					cssRow='RowBadVal';
				rVs.push(Evol.sep);
			}
			var vs=rVs.join('');
			if(cssRow=='' && (vs!=e$(['evoRO',gID,'-C',gd.cRowID].join('')).value))
				cssRow='RowNewVal';
			e$(['evoRO',gID,'-',gd.cRowID].join('')).value=vs;
			gd.cRow.cells.item(0).className=cssRow;
			gd.cRowID=-1;
			EvoGrid.setGridFlag(gID);			
		}
		return true;
	},

	disableCell:function(r,i){
		var cf=e$('evolGFE'+i),v=null;
		if(cf!=null){
			v=cf.value;
			EvoGrid.setRowCellContent(r,i,v);
		}
		return v;
	},
	disableCellCheck:function(r,i,pix){
		var v='';
		if(e$('evolGFE'+i).checked){
			r.cells.item(i).innerHTML=['&nbsp;<img src="',EvoGen.path,pix,'" alt="Checked"/>'].join(''); 
			v='1';
		}else{
			EvoGrid.setRowCellContent(r,i,'');
			v='0';
		}
		return v;
	},
	disableCellLOV:function(r,i){
		var vList=e$('evolGFE'+i);
		var cR=r.cells.item(i);
		cR.intnum=vList.value;
		cR.innerHTML=vList.options[vList.selectedIndex].text;
		return cR.intnum;
	},
	
	setCell:function(r,fd){
		var i=fd.i,t=fd.t;
		var c=r.cells.item(i);
		if(c!=null){
			if(document.all)
				var v=c.innerText;
			else
				var v=c.textContent;
			var fn='evolGFE'+i;
			switch(t){
				case 'boolean':
					v=(c.innerHTML.length>6)?'1':'0';
					c.innerHTML=EvoUI.inputCheckbox(fn,v);
					break;
				case 'date':
				case 'datetime':
					c.innerHTML=EvoUI.inputDate(fn,v);
					break; 
				case 'textmultiline':
					var ml=(fd.ml>0)?fd.ml:0;
					c.innerHTML=EvoUI.inputTextM(fn,v,ml,3);
					break;
				case 'integer':
				case 'decimal':
					c.innerHTML=EvoUI.inputTextInt(fn,v);
					break;
				default:
					var ml=(fd.ml>0)?fd.ml:0;
					c.innerHTML=EvoUI.inputText(fn,v,ml);
					break;
			}
		}else
			return null;
		return v;
	},

	addRowCells:function(tr,n){
		tr.className=(tr.rowIndex%2==1)?'RowOdd':'RowEven';
		EvoGrid.setSelected(tr,true);
		tr.insertCell(0).innerHTML='&nbsp;';
		for(var i=1;i<n;i++){
			tr.insertCell(i);
		}
	},

	setRowInfo:function(gID,i,v){
		var n=['evoRO',gID,'-C',i].join(''), f=e$(n);
		if(f==null){
			EvoGrid.addRowInfo(gID,i);
			f=e$(n);
		}
		if(f.value=='')
			f.value=v;
	},

	addRowInfo:function(gID,i){
		var fn=['evoRO',gID,'-'].join(''), f=e$(fn+'new');
		if (f==null){
			Evol.error.push('');
			return;
		}
		f.innerHTML=[f.innerHTML,EvoUI.inputHidden(fn+i,''),EvoUI.inputHidden(fn+'C'+i,'')].join('');
	},
	
	delRow:function(gID){
		var ds=EvoGen.details, gd=ds.lst[gID], g=gd.grid; 
		if(gd.cRowID>-1){
			var r=e$(['evoRO',gID,'-',gd.cRowID].join(''))
			if(document.all){
				r.value=gd.cRow.cells(0).innerText+'~!DEL';
				gd.cRow.innerText='';
			}else{
				r.value=gd.cRow.cells.item(0).textContent+'~!DEL';
				gd.cRow.textContent='';
			}
			gd.cRow.style.display='none';
			gd.cRowID=-1;
			EvoGrid.setGridFlag(gID);
		}
	},
	
	addRow:function(gID){
		var tt=EvoGen.details.lst[gID].grid;
		var ie=EvoUI.isIE();
		if(ie)
			tt=tt.tBodies(0);
		var nf=tt.rows[0].cells.length-1;
		var i=tt.rows.length;
		if(i<100){
			tr=tt.insertRow(i);
			if(ie)
				i++;
			tr.setAttribute('id','r'+i);
			EvoGrid.addRowCells(tr,nf+1);
			EvoGrid.addRowInfo(gID,i);
			EvoUI.setOnClick(tr,['EvoGrid.editRow(\'',gID,'\',',i,')'].join(''),ie);
			EvoGrid.editRow(gID,i)
		}
	},
	
	setGridFlag:function(gID){
		var fn='evoUDtls';
		//if(EvoGen.details)
		e$(fn).value='1';
		e$(fn+gID).value='1'; 
	}

}

// ############  EvoUI #################################################################

var EvoUI={

	fieldLabel:function(fID,fLbl){
		return ['<div class="FieldLabel"><label for="',fID,'">',fLbl,'</label></div>'].join('');
	},
	fieldLabelSmall:function(fID,fLbl){
		return ['<label for="',fID,'"><small>',fLbl,'</small></label>'].join('');
	},

	inputText:function(fID,fV,ml){
		var fh=['<input type="text" name="',fID,'" id="',fID,'" value="',fV];
		if(ml>0){
			fh.push('" maxlength="');
			fh.push(ml);
		}	
		fh.push('" class="Field">');
		return fh.join('');
	},
	inputTextInt:function(fID,fV,fT,max,min){
		return ['<input type="text" name="',fID,'" id="',fID,'" value="',fV,
			'" onKeyUp="EvoVal.checkNum(this,\'',fT,'\')" class="Field" maxlength="12">'].join('');
	},
	inputTextM:function(fID,fV,ml,h){
		var fh=['<textarea name="',fID,'" id="',fID,
			'" class="Field" style="height:64" rows="',h,'" cols="52'];
		if(ml>0)
			fh.push(['" onKeyUp="EvoVal.checkMaxLen(this,',ml,')'].join(''));
		fh.push('">');	
		fh.push(fV);
		fh.push('</textarea>');
		return fh.join('');
	},
	inputDate:function(fID,fV){
		return ['<nobr><input type="text" id="',fID,'" name="',fID,'" value="',fV,'" class="Field Field80" size="15" maxlength="22">',
			'&nbsp;<a href="javascript:ShowDatePicker(\'',fID,'\');" class="ico Calendar"></a></nobr>'].join('');
	},
	inputCheckbox:function(fID,fV){
		var fh=['<input type="checkbox" id="',fID,'"'];
		if(fV!=null&&fV!=''&&fV!='0')
			fh.push(' checked');
		fh.push(' value="1">');
		return fh.join("");
	},
	inputRadio:function(fN,fV,fLbl,sel,fID){
		var fh=['<label for="',fID,'"><input ID="',fID,'" name="',fN,'" type="radio" value="',fV,'"'];
		if(sel)
			fh.push(' checked="checked"');
		fh.push('"><small>');
		fh.push(fLbl);
		fh.push("</small></label>&nbsp;");
		return fh.join("");
	},
	inputLOV:function(fID,fV,fVLabel,fLOV){
		var fh=['<select class="Field" id="',fID,'"><option value="',fV,'" selected>',fVLabel,'</option>'];
		var rVs=[];
		for(var i in fLOV){
			var lv=fLOV[i];
			rVs.push(EvoUI.inputOption(lv.id,lv.v));
		}
		fh.push(rVs.join(''));	
		fh.push('</select>');		
		return fh.join('');
	},
	inputHidden:function(fID,fV){
		return ['<input type="hidden" name="',fID,'" id="',fID,'" value="',fV,'"/>'].join('');
	},
	inputOption:function(fID,fV){
		return ['<option value="',fID,'"/>',fV,'</option>'].join('');
	},
	
	setDisplay:function(fID,s){
		EvoUI.setDisplayF(e$(fID),s);
	},
	setDisplayF:function(f,s){
		if(f!=null)
			f.style.display=s;
	},
	setEnabledF:function(f,s){
		if(f!=null)
			f.disabled=!s;
	},	
	showOrHide:function(fID,s){
		var f=e$(fID);
		if(f!=null)
			f.style.display=s?'':'none';
	},
	
	setOnClick:function(obj,fn,ie){
		if(ie)
			obj['onclick']=new Function(fn);
		else
			obj.setAttribute('onclick',fn);
	},
	
	createElem:function(fTg,fID){
		e=document.createElement(fTg);
		e.id=fID;
		return e;
	},
	createElemHidden:function(fID){ 
		e=EvoUI.createElem('input',fID);
		e.type="hidden";
		e.name=fID;
		return e;
	},
	getOrCreate:function(fID){
		var e=e$(fID);
		if(e==null){
			e=EvoUI.createElem('div',fID);
			document.body.appendChild(e);
		}
		return e;	
	},
	getOrCreateHidden:function(fID,p){
		var e=e$(fID);
		if(e==null){
			e=EvoUI.createElemHidden(fID);
			if(p!=null)
				p.appendChild(e);
			else
				document.body.appendChild(e);
		}
		return e;	
	},
	
	focus:function(fID){ 
		try{e$(fID).focus()}
		catch(err){}
	},

	// Fields 
	showResize:function(fID,b,fl){ 
		if(fl!=null)
			fl.onmouseover=null;
		p=e$('gr'+fID);
		if(p==null){
			var f=e$(fID);
			if(f!=null){
				var h=parseInt(f.offsetHeight);
				var nD=EvoUI.createElem('span','gr'+fID);
				nD.innerHTML=[this.HTML4Resize(fID,1,h<100),this.HTML4Resize(fID,-1,h>800)].join("");
				fl.appendChild(nD);
			}
		}
		if(b)
			Evol.togglePanel(fID+'link',0);
	},
	
	HTML4Resize:function(fID,d,v){
		var h=['<a href="javascript:EvoUI.sizeML(\'',fID,'\',',d,')" class="ico ',
			(d==1)?'MLbigger" id="rp_':'MLsmaller" id="rm_',fID];
		if(v==false)
			h.push('" style="display:none');	
		h.push('"></a>');
		return h.join('');
	},

	// Resize multiline fields
	sizeML:function(fID,d){ 
		var f=e$(fID),h=parseInt(f.offsetHeight);
		var r=true,b1=true,b2=true;
		if(d>0){
			if(h>800)
				r=false;
			if(h>720)
				b1=false;
		}else{
			if(h<100)
				r=false; 
			if(h<180)
				b2=false;
		}
		if(r)
			f.style.height=h+80*d;
		EvoUI.showOrHide('rp_'+fID,b1);
		EvoUI.showOrHide('rm_'+fID,b2);
	},
	
	slide:function(p,d){
		var ps=p.style,h=parseInt(ps.height),iu=false;
		var cb=function(){EvoUI.slide(p,d)};
		if(d){
			if(h<p.mh){
				ps.height=h+10+' px';
				p.runtimer=window.setTimeout(cb,10);
				iu=true;
			}else{
				p.runtimer=null;
				if(p.mh!=null)
				ps.height=p.mh;
				ps.display=ps.height='';
			}
		}else{
			if(h>11){
				ps.height=h-10+' px';
				p.runtimer=window.setTimeout(cb,10)
			}else{
				p.runtimer=null;
				ps.display='none';
				ps.height='';
				iu=true;
			}
		}
		return iu;
	},

	isNN:function(e){
		return (typeof e!='undefined'&&e!=null);
	},
	
	isIE:function(){
		return document.all?true:false;
	},
		
	AJAX:function(url,vars,cb){
		var rq=new XMLHttpRequest();
		rq.open('POST',url,true);
		rq.setRequestHeader('Content-Type','application/x-www-form-urlencoded'); 
		rq.onreadystatechange=function(){
			var done=4,ok=200;
			if(rq.readyState==done && rq.status==ok){
				if(rq.responseText)
					cb(rq.responseText);
			}
		}
		rq.send(vars);
	}
}

e$=function(e){
	return document.getElementById(e);
}

String.prototype.trim=function(){
	return this.replace(/^\s+|\s+$/g,"");
}



