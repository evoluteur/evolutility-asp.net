//   Evolutility Library - Last update 7/24/2008
//   (c) 2003-2008 Olivier Giulieri - www.evolutility.org
/*
This program is open source software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

var Evol={

	version:"2.2",
	
//Panels & Tabs & Toolbar  
	togglePanel:function(pID,b){
		var c,p=$(pID)
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
		this.slide(p,d);
		if(b)
			var l=$(pID+'link');
			if(l!=null)
				l.className='ico panel'+c;
		},

	togglePanelOnce:function(pID,b){ 
		this.togglePanel(pID,0);
		if(b)
			this.togglePanel(pID+'link',0);
	},

	slide:function(p,d){
		var ps=p.style,h=parseInt(ps.height),iu=false;
		if(d){
			if(h<p.mh){
				ps.height=h+10+" px";
				p.runtimer=window.setTimeout(function(){Evol.slide(p,d)},10);
				iu=true;
			}else{
				p.runtimer=null;
				if(p.mh!=null)
				ps.height=p.mh;
				ps.display=ps.height='';
			}
		}else{
			if(h>11){
				ps.height=h-10+" px";
				p.runtimer=window.setTimeout(function(){Evol.slide(p,d)},10)
			}else{
				p.runtimer=null;
				ps.display='none';
				ps.height='';
				iu=true;
			}
		}
		return iu;
	},

	selTab:function(t,n){
		var tn=t+'Tab';
		Evol.setDisplay(tn+n,'');
		$(tn+'B'+n).className='tabselected'; 
		if(TABevo!=n){
			Evol.setDisplay(tn+TABevo,'none');
			$(tn+'B'+TABevo).className='tab';
			TABevo=$('EvoActTab').value=n;
		}
	},
	
	focus:function(fID){ 
		try{$(fID).focus()}
		catch(err){}
	},
	 
//Fields 
	showResize:function(fID,b,fl){ 
		if(fl!=null)
			fl.onmouseover=null;
		p=$('gr'+fID);
		if(p==null){
			var f=$(fID);
			if(f!=null){
				var h=parseInt(f.offsetHeight);
				var ht=[];
				ht.push(this.HTML4Resize(fID,1,h<100))
				ht.push(this.HTML4Resize(fID,-1,h>800))
				var nD=document.createElement('span');
				nD.setAttribute('id','gr'+fID);		
				nD.innerHTML=ht.join("");
				fl.appendChild(nD);
			}
		}
		if(b)
			this.togglePanel(fID+'link',0);
	},	
	
	showSort:function(td,ct){
		if(td.label==null){ 
			td.onmouseout=new Function("Evol.hideSort(this)"); 
			td.label=td.innerHTML;
			td.innerHTML=td.label+this.HTML4Sort(ct); 
		}
	},	
	hideSort:function(td){ 
		if(td.label!=null){
			var e = window.event;
			var relTarg = e.relatedTarget || e.toElement; 
			if (relTarg==null||relTarg.nodeName == 'A') 
				return;
			td.innerHTML=td.label;
			td.label=null;
		}			 
	},	
	HTML4Resize:function(fID,d,v){
		var h=['<a href="javascript:Evol.sizeML(\'',fID,'\',',d,')" class="ico '];
		if(d==1)
			h.push('MLbigger" id="rp_');
		else
			h.push('MLsmaller" id="rm_');
		h.push(fID);
		if(!v)
			h.push('" style="display:none');
		h.push('"></a>');	
			
		return h.join('');
	},
	HTML4Sort:function(fID){
		return ['<a href="javascript:EvPost(\'a:',fID,'\',1)" class="ico arrUp"></a><a href="javascript:EvPost(\'d:',fID,'\',-1)" class="ico arrDown"></a>'].join('');
	},

//Lightbox - courtesy of http://www.emanueleferonato.com/
	showLB:function(txt,ff){
		var lbs=$('fadeEVOL').style;
		lbs.height=window.document.firstChild.offsetHeight; 
		lbs.width=window.document.firstChild.offsetWidth;
		lbs.display='block';  
		$('lightEVOL').innerHTML=[txt,'<p align=right><a href="javascript:Evol.closeLB();Evol.focus(\'',ff,'\')">',Evol.msgVal[EvolLANG].close,'</a></p>'].join("");
		Evol.setDisplay('lightEVOL','block');
		document.body.style.overflow="hidden";
	},

	closeLB:function(){ 
		document.body.style.overflow="auto";
		Evol.setDisplay('lightEVOL','none');
		Evol.setDisplay('fadeEVOL','none'); 
	},

// Print
	print:function(){
		print();
	},
	
// Resize multiline fields
	sizeML:function(fID,d){ 
		var f=$(fID),h=parseInt(f.offsetHeight);
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
		this.showOrHide("rp_"+fID,b1);
		this.showOrHide("rm_"+fID,b2);
	},

// Export
	exportFields:function(E,o){
		function showDetails(e,s1,s2){
			Evol.setDisplay(e+'csv2',s1);
			Evol.setDisplay(e+'tab2',s2);
		};
		if(o=='TAB'){
			o='CSV';
			showDetails(E,'none','')
		}else if(o=='CSV') 
			showDetails(E,'','none')
		if(evoloX=='TAB')
			evoloX='CSV';
		Evol.setDisplay(E+evoloX,'none');
		Evol.setDisplay(E+o,'');
		evoloX=o;
	},

	color:function(e){
		var c=$(e).value,cc=$(e+'COL');
		if(c&&cc)
			cc.innerHTML='<div class="ColorBox" style="background:'+c+'"> </div>'    
	},

// Upload UI
	docM:function(fn){
		$(fn+'_dp').value='1'
	},

	pixM:function(fn){
		Evol.docM(fn);
		$(fn+'img').src=EvolPATH+'imgdelete.gif'
	},

//Validation
	msgVal:{
		EN:{    // English
			intro:'You are not finished yet:',
			empty:'"{0}" must have a value.',
			email:'"{0}" must be a valid email.',
			integer:'"{0}" must only use numbers.',
			decimal:'"{0}" must be a valid decimal numbers.',
			date:'"{0}" must be a valid date, format must be "MM/DD/YYYY" like "12/24/2005".',
			datetime:'"{0}" must be a valid date/time, format must be "MM/DD/YYYY hh:mm am/pm" like "12/24/2005 10:30 am".',
			time:'"{0}" must be a valid date/time, format must be "hh:mm am/pm" like "10:30 am".',
			max:'"{0}" must be smaller or equal to {1}.',
			min:'"{0}" must be greater or equal to {1}.',
			close:'Close'
		},
		FR:{    // French 
			intro:'Vous n\'avez pas encore termin\351:',  
			empty:'Vous devez entrer une valeur pour le champs {0}.',
			email:'"{0}" doit avoir la forme "identifiant@domaine.com".',
			integer:'"{0}" doit \351tre un nombre entier.', 
			decimal:'"{0}" doit \351tre un nombre d\351cimal.',
			date:'"{0}" doit s\'\351crire sous la forme Jour/Mois/Ann\351e, par exemple: 24/12/2005.',
			datetime:'"{0}" doit s\'\351crire sous la forme Jour/Mois/Ann\351e heure:minutes am/pm, par exemple : 12/24/2005 10:30 am.', 
			time:'"{0}" doit s\'\351crire sous la forme heure:minutes am/pm, par exemple : 10:30 am.',
			max:'"{0}" doit pas \352tre inf\351rieur ou \351gal \340 {1}.',
			min:'"{0}" doit \352tre sup\351rieur ou \351gal \340 {1}.',
			close:'Fermer'
		}
	},

	checkMaxLen:function(F,maxL){
		if(F.value.length>maxL)
			F.value=F.value.substring(0,maxL-1)
	},

	checkNum:function(F,t){ 
		var nv,fv=F.value;
		if(EvolLANG=='FR')
			fv=fv.replace(",",".");
		if(t=='i')
			nv=parseInt(fv)
		else
			nv=parseFloat(fv);
		if(isNaN(nv))
			F.value=''
		else if(fv!=nv)
			F.value=nv;
	},

	checkAllFields:function(fds){
		var evoRegEx={
			email:/^[\w\.\-]+@[\w\.\-]+\.[\w\.\-]+$/,
			integer:/^-?\d+$/,
			decimalEN:/^\d+(\.\d+)?$/,
			decimalFR:/^\d+(\,\d+)?$/
		};
		var msgs=[],ff=null; 
		for(var i in fds){
			var fd=fds[i], f=$(fd.id);
			if(f!=null){
				var ner=true,nm=msgs.length;
				if(fd.r>0){ 
					if(isEmpty(f)){
						labMsg(Evol.msgVal[EvolLANG].empty);
						ner=false;
					}else
						typeCheck();          
				}else
					typeCheck();
				if(ner){
					var fv=f.value.trim();
					if(fv!=''){
						if(fd.max!=null&&parseFloat(fv)>fd.max)
							labMsg(Evol.msgVal[EvolLANG].max,fd.max);
						if(fd.min!=null&&parseFloat(fv)<fd.min)
							labMsg(Evol.msgVal[EvolLANG].min,fd.min);
					}                    
				}
				flagValid(f,nm==msgs.length);                                
			}
		}
		if(msgs.length>0)
			Evol.showLB([Evol.msgVal[EvolLANG].intro,"<ul><li>",msgs.join("<li>"),"</li></ul>"].join(""),ff.id);
		else
			return true;

		function typeCheck(){
			var fv=f.value.trim();
			if(fv!='')
				switch(fd.t){
				case "integer":
				case "email":
					if(!evoRegEx[fd.t].test(fv))
						labMsg(Evol.msgVal[EvolLANG][fd.t]);
					break;
				case "decimal":             
					if(!evoRegEx[fd.t+EvolLANG].test(fv))
						labMsg(Evol.msgVal[EvolLANG][fd.t]);
					break;               
				case "datetime":
				case "date":
					if((fv!='')&&(EvolLANG=='EN')&&(!Date.parse(fv)))
						labMsg(Evol.msgVal[EvolLANG][fd.t]);
					break;
				}
		} 
		function isEmpty(f){
			if(f.tagName=="SELECT")
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
	},
	
	deleteItem:function(n){
	//to do : use lightbox
		if(confirm('Delete this {0}?'.replace("{0}",n)))
			EvPost('10')
	},
    
//Edit Grid
	setSelected:function(f,s){
		f.style.backgroundColor=(s)?'#F5F5DC':'';
	},
		
	bLOV:'<select class="Field" id="evolGFE',
	bText:'<input type="text" class="Field" id="evolGFE',
	bBool:'<input type="checkbox" id="evolGFE',
	bDate:'<input type="text" class="Field Field80" size="15" maxlength="22" id="evolGFE',
	bCalendar:function(n){
		return ['&nbsp;<a href="javascript:ShowDatePicker(\'',n,'\');" class="ico Calendar"></a>'].join("");
	},	
	
	addRowCells:function(tr,n){
		tr.className=(tr.rowIndex%2==1)?"RowOdd":"RowEven";
		this.setSelected(tr,true);
		tr.insertCell(0).innerHTML='&nbsp;';
		for(var i=1;i<n;i++){
			tr.insertCell(i);
		}
	},
	
	setRowInfo:function(i,v){
		var n='evoROC'+i, f=$(n);
		if(f==null)
			this.addRowInfo(i);
		f=$(n);
		if(f.value=='')
			f.value=v;
	},
	
	addRowInfo:function(i){
		var tp='<input type="hidden" name="evoROz" id="evoROz"><input type="hidden" name="evoROCz" id="evoROCz">';
		$('evoROz').innerHTML+=tp.replace(/z/g, i);
	},
	
//Misc.
	setDisplay:function(fID,s){
		$(fID).style.display=s;
	},	
	showOrHide:function(fID,s){
		var f=$(fID);
		if(f!=null)
			f.style.display=s?"":"none";
	}	
}

var evoloX='CSV'; 

// -----------  Misc UI -----------
$=function(e){
	return document.getElementById(e);
}
String.prototype.trim=function() {
	return this.replace(/^\s+|\s+$/g,"");
}


// Menu - courtesy of http://javascript-array.com 
var timeout=500,closetimer=0,ddmenuitem=0;
function mopen(id){
	mcancelclosetime();
	if(ddmenuitem) ddmenuitem.style.visibility = 'hidden';
	ddmenuitem = document.getElementById(id);
	ddmenuitem.style.visibility = 'visible';
}
function mclose(){
	if(ddmenuitem) ddmenuitem.style.visibility = 'hidden';
}
function mclosetime(){
	closetimer = window.setTimeout(mclose, timeout);
}
function mcancelclosetime(){
	if(closetimer){
		window.clearTimeout(closetimer);
		closetimer = null;
	}
}
document.onclick = mclose; 



 