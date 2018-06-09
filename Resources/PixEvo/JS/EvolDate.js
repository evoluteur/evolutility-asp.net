/**
Original DatePicker from Julian Robichaux -- http://www.nsftools.com
*/

var datePickerDivID="datepicker";
var iFrameDivID="datepickeriframe";
var defaultDateSep="/";
var dateSep=defaultDateSep;
var dateFormat=defaultDateFormat;

function ShowDatePicker(fldName, belowThis, dtFormat, dtSep){
	var tDateFld=document.getElementsByName(fldName).item(0);
	if (!belowThis)
		belowThis=tDateFld;
	if (dtSep)
		dateSep=dtSep;
	else
		dateSep=defaultDateSep;
	if (dtFormat)
		dateFormat=dtFormat;
	else
		dateFormat=defaultDateFormat;
	var x=belowThis.offsetLeft;
	var y=belowThis.offsetTop+belowThis.offsetHeight;
	var parent=belowThis;
	while (parent.offsetParent){
		parent=parent.offsetParent;
		x+=parent.offsetLeft;
		y+=parent.offsetTop;
	} 
	drawDatePicker(tDateFld, x, y);
}

function drawDatePicker(tDateFld, x, y){
	var dt=string2Date(tDateFld.value );
	if (!document.getElementById(datePickerDivID)) {
		var newNode=document.createElement("div");
		newNode.setAttribute("id", datePickerDivID);
		newNode.setAttribute("class", "dpDiv");
		newNode.setAttribute("style", "visibility: hidden;");
		document.body.appendChild(newNode);
	} 
	// move the datepicker div to the proper x,y coordinate and toggle the visiblity
	var pickerDiv=document.getElementById(datePickerDivID);
	if(pickerDiv!=null){
		var s=pickerDiv.style;
		s.position="absolute";
		s.left=x+"px";
		s.top=y+"px";
		s.visibility=(s.visibility=="visible"?"hidden":"visible");
		s.display=(s.display=="block"?"none":"block");
		s.zIndex=10000; 
	}
	// draw the datepicker table
	refreshDatePicker(tDateFld.name, dt.getFullYear(), dt.getMonth(), dt.getDate());
}

function refreshDatePicker(fldName, year, month, day){
	// if no arguments are passed, use today's date;
	var thisDay=new Date(); 
	if ((month >= 0) && (year > 0)){
		thisDay=new Date(year, month, 1);
	}else{
		day=thisDay.getDate();
		thisDay.setDate(1);
	}
	var crlf="\r\n"; 
	var TR="<tr>";
	var TR_days="<tr class='dpDayTR'>";
	var TR_todaybutton="<tr class='dpTodayButtonTR'>";
	var xTR="</tr>"  ;
	var TD="<td class='dpTD' onMouseOut='this.className=\"dpTD\";' onMouseOver='this.className=\"dpTDHover\";' ";    // leave this tag open for onClick event
	var TD_title="<td colspan=5>";
	var TD_buttons="<td  colspan=7>";
	var TD_todaybutton="<td colspan=7 class='dpTodayButtonTD'>";
	var TD_days="<td class='dpDayTD'>";
	var TD_selected="<td class='dpDayHighlightTD' onMouseOut='this.className=\"dpDayHighlightTD\";' onMouseOver='this.className=\"dpTDHover\";' ";    // leave this tag open for onClick event
	var xTD="</td>";
	var DIV_title="<div class='PanelLabel'>";
	var DIV_selected="<div class='dpDayHighlight'>";
	var xDIV="</div>";

	var html=["<table cols=7 class='PanelCalendar'>",
	// title bar
	TR,TD_buttons,getButtonCode(fldName,thisDay,-1,"&lt;","margin-right:10px;"),
	"<nobr>",monthArrLong[thisDay.getMonth()]," ",thisDay.getFullYear(),"</nobr>",
	getButtonCode(fldName,thisDay,1,"&gt;","float:right;"),xTD,xTR,
	// row that indicates which day of the week we're on
	TR_days,TD_days,dayArrShort.join(xTD+TD_days),xTD,xTR,
	// populating the table with days of the month
	TR];
	// first, the leading blanks
	for (i=weekBegin;i<thisDay.getDay();i++)
		html.push("<td>&nbsp;</td>");
	html=html.join(''); 
	// now, the days of the month
	do {
		dayNum=thisDay.getDate();
		TD_onclick=[" onclick=\"updateDateField('",fldName,"','",date2String(thisDay),"');\">"].join("");    
		if (dayNum == day)
			html+=[TD_selected,TD_onclick,DIV_selected,dayNum,xDIV,xTD].join("");
		else
			html+=[TD,TD_onclick,dayNum,xTD].join("");    
		// if this is a Saturday/Dimanche, start a new row
		if (thisDay.getDay()==weekEnd)
			html+=xTR+TR;    
		// increment the day
		thisDay.setDate(thisDay.getDate()+1);
	} while (thisDay.getDate()>1) 
	// fill in any trailing blanks
	if (thisDay.getDay()>0){
	for (i=6;i>thisDay.getDay();i--)
		html+="<td>&nbsp;</td>"; 
	}
	html+=xTR;
	// button
	var today=new Date();
	var todayString=[todayIs,dayArrMed[today.getDay()],", ",monthArrMed[today.getMonth()]," ",today.getDate()].join("");
	html+=[TR_todaybutton,TD_todaybutton,
	"<nobr><button class='Button2' onClick='refreshDatePicker(\"",fldName,"\");'>",thisMonth,"</button> ",
	"<button class='Button' onClick='updateDateField(\"",fldName,"\");'>",EvolLang.close,"</button></nobr>",
	xTD,xTR,"</table>"].join(''); 
	document.getElementById(datePickerDivID).innerHTML=html;
	// add an "iFrame shim" to allow the datepicker to display above selection lists
	adjustiFrame();
}

function getButtonCode(fldName, dateVal, adjust, label, style){
	var newMonth=(dateVal.getMonth()+adjust)%12;
	var newYear=dateVal.getFullYear()+parseInt((dateVal.getMonth()+adjust)/12);
	if (newMonth<0){
		newMonth+=12;
		newYear+=-1;
	}
	return ["<button class='Button' onClick='refreshDatePicker(\"",fldName,"\", ",newYear,", ",newMonth,");' style='",style,"'>",label,"</button>"].join("");
};

function date2String(dateVal){
	var dayStr="00"+dateVal.getDate();
	var monthStr="00"+(dateVal.getMonth()+1);
	dayStr=dayStr.substring(dayStr.length-2);
	monthStr=monthStr.substring(monthStr.length-2);
	switch (dateFormat){
	case "dmy":
		return [dayStr,monthStr,dateVal.getFullYear()].join(dateSep);
	case "ymd":
		return [dateVal.getFullYear(),monthStr,dayStr].join(dateSep);
	case "mdy":
	default:
		return [monthStr,dayStr,dateVal.getFullYear()].join(dateSep);
	}
}

function string2Date(dateStr){
	var dateVal,dateArr,d,m,y;
	var dI,mI,yI;
	try {
		dateArr=splitdateStr(dateStr);
		if (dateArr){
			switch (dateFormat){
			case "dmy":
				dI=0;mI=1;yI=2;
				break;
			case "ymd":
				dI=2;mI=1;yI=0;
				break;
			case "mdy":
			default:
				dI=1;mI=0;yI=2;
				break;
			}
			d=parseInt(dateArr[dI], 10);
			m=parseInt(dateArr[mI], 10)-1;
			y=parseInt(dateArr[yI], 10);
			dateVal=new Date(y, m, d);
		}else if(dateStr)
			dateVal=new Date(dateStr);
		else
			dateVal=new Date();
	}catch(e){
		dateVal=new Date();
	} 
	return dateVal;
}

function splitdateStr(dateStr){
	var dateArr;
	if (dateStr.indexOf("/") >= 0)
		dateArr=dateStr.split("/");
	else if (dateStr.indexOf(".") >= 0)
		dateArr=dateStr.split(".");
	else if (dateStr.indexOf("-") >= 0)
		dateArr=dateStr.split("-");
	else if (dateStr.indexOf("\\") >= 0)
		dateArr=dateStr.split("\\");
	else
		dateArr=false;
	return dateArr;
}

function updateDateField(fldName, dateStr){
  var tDateFld=document.getElementsByName(fldName).item(0);
  if(dateStr)
    tDateFld.value=dateStr; 
  var pickerDiv=document.getElementById(datePickerDivID);
  if(pickerDiv!=null){
	pickerDiv.style.visibility="hidden";
	pickerDiv.style.display="none";
    adjustiFrame();
  }
  try{tDateFld.focus()}
		catch(err){} 
 
  // after the datepicker has closed, optionally run a user-defined function called
  // datePickerClosed, passing the field that was just updated as a parameter
  // (note that this will only run if the user actually selected a date from the datepicker)
  if ((dateStr) && (typeof(datePickerClosed) == "function"))
    datePickerClosed(tDateFld);
}

function adjustiFrame(pickerDiv, iFrameDiv){
	var is_opera=(navigator.userAgent.toLowerCase().indexOf("opera")!=-1);
	if(is_opera)
		return;
	try{
	if(!document.getElementById(iFrameDivID)){
		var newNode=document.createElement("iFrame");
		newNode.setAttribute("id", iFrameDivID);
		newNode.setAttribute("src", "javascript:false;");
		newNode.setAttribute("scrolling", "no");
		newNode.setAttribute ("frameborder", "0");
		document.body.appendChild(newNode);
	}
	if(!pickerDiv)
		pickerDiv=document.getElementById(datePickerDivID);
	if(!iFrameDiv)
		iFrameDiv=document.getElementById(iFrameDivID);    
	try{
		var ifs=iFrameDiv.style;
		ifs.position="absolute";
		ifs.width=pickerDiv.offsetWidth;
		ifs.height=pickerDiv.offsetHeight ;
		var dps=pickerDiv.style
		ifs.top=dps.top;
		ifs.left=dps.left;
		ifs.zIndex=dps.zIndex-1;
		ifs.visibility=dps.visibility ;
		ifs.display=dps.display;
	}catch(e){}
	}catch(ee){} 
}

// Date Validation Javascript
// copyright 30th October 2004, 31st December 2009 by Stephen Chapman
// http://javascript.about.com

function stripBlanks(fld) {var result="";var c=0;for (i=0; i<fld.length; i++) {
if (fld.charAt(i) != " " || c > 0) {result += fld.charAt(i);
if (fld.charAt(i) != " ") c=result.length;}}return result.substr(0,c);}
var numb='0123456789';
function isValid(parm,val) {if (parm == "") return true;
for (i=0; i<parm.length; i++) {if (val.indexOf(parm.charAt(i),0) == -1)
return false;}return true;}
function isNumber(parm) {return isValid(parm,numb);}
var mth=new Array(' ','january','february','march','april','may','june','july','august','september','october','november','december');
var day=new Array(31,28,31,30,31,30,31,31,30,31,30,31);
function isDate(fld) {
	var dd, mm, yy;var today=new Date;var t=new Date;fld=stripBlanks(fld);
	if(fld == '') return false;var d1=fld.split('\/');
	if(d1.length != 3) d1=fld.split(' ');
	if(d1.length != 3) return false;
	if(defaultDateFormat == 'mdy') {
	  dd=d1[1]; mm=d1[0]; yy=d1[2];}
	else if(defaultDateFormat == 'dmy'){
	  dd=d1[0]; mm=d1[1]; yy=d1[2];}
	else if(defaultDateFormat == 'ymd'){
	  dd=d1[2]; mm=d1[1]; yy=d1[0];}
	else return false;
	var n=dd.lastIndexOf('st');
	if (n > -1) dd=dd.substr(0,n);
	n=dd.lastIndexOf('nd');
	if (n > -1) dd=dd.substr(0,n);
	n=dd.lastIndexOf('rd');
	if (n > -1) dd=dd.substr(0,n);
	n=dd.lastIndexOf('th');
	if (n > -1) dd=dd.substr(0,n);
	n=dd.lastIndexOf(',');
	if (n > -1) dd=dd.substr(0,n);
	n=mm.lastIndexOf(',');
	if (n > -1) mm=mm.substr(0,n);
	if (!isNumber(dd)) return false;
	if (!isNumber(yy)) return false;
	if (!isNumber(mm)) {
	  var nn=mm.toLowerCase();
	  for (var i=1; i < 13; i++) {
		if (nn == mth[i] ||
			nn == mth[i].substr(0,3)) {mm=i; i=13;}
	  }
	}
	if (!isNumber(mm)) return false;
	dd=parseFloat(dd); mm=parseFloat(mm); yy=parseFloat(yy);
	if (yy < 100) yy += 2000;
	if (yy < 1582 || yy > 4881) return false;
	if (mm == 2 && (yy%400 == 0 || (yy%4 == 0 && yy%100 != 0))) day[1]=29;else day[1]=28;
	if (mm < 1 || mm > 12) return false;
	if (dd < 1 || dd > day[mm-1]) return false; 
	return true;
}

