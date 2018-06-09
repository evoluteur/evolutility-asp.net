/**
Original DatePicker from Julian Robichaux -- http://www.nsftools.com
*/

var datePickerDivID = "datepicker";
var iFrameDivID = "datepickeriframe";
 
// date formatting variables 
var defaultDateSep = "/";
var dateSep = defaultDateSep;
var dateFormat = defaultDateFormat;

function ShowDatePicker(fldName, belowThis, dtFormat, dtSep)
{
  var targetDateField = document.getElementsByName(fldName).item(0);
  if (!belowThis)
    belowThis = targetDateField;
  if (dtSep)
    dateSep = dtSep;
  else
    dateSep = defaultDateSep;
  if (dtFormat)
    dateFormat = dtFormat;
  else
    dateFormat = defaultDateFormat;
  var x = belowThis.offsetLeft;
  var y = belowThis.offsetTop + belowThis.offsetHeight ;
  var parent = belowThis;
  while (parent.offsetParent) {
    parent = parent.offsetParent;
    x += parent.offsetLeft;
    y += parent.offsetTop ;
  } 
  drawDatePicker(targetDateField, x, y);
}

function drawDatePicker(targetDateField, x, y)
{
  var dt = string2Date(targetDateField.value );
  if (!document.getElementById(datePickerDivID)) {
    var newNode = document.createElement("div");
    newNode.setAttribute("id", datePickerDivID);
    newNode.setAttribute("class", "dpDiv");
    newNode.setAttribute("style", "visibility: hidden;");
    document.body.appendChild(newNode);
  } 
  // move the datepicker div to the proper x,y coordinate and toggle the visiblity
  var pickerDiv = document.getElementById(datePickerDivID);
  if(pickerDiv!=null){
	  var s=pickerDiv.style;
	  s.position = "absolute";
	  s.left = x+"px";
	  s.top = y+"px";
	  s.visibility = (s.visibility=="visible"?"hidden":"visible");
	  s.display = (s.display=="block"?"none":"block");
	  s.zIndex = 10000; 
  }
  // draw the datepicker table
  refreshDatePicker(targetDateField.name, dt.getFullYear(), dt.getMonth(), dt.getDate());
}

function refreshDatePicker(fldName, year, month, day)
{
  // if no arguments are passed, use today's date;
  var thisDay = new Date(); 
  if ((month >= 0) && (year > 0)) {
    thisDay = new Date(year, month, 1);
  } else {
    day = thisDay.getDate();
    thisDay.setDate(1);
  }
  var crlf = "\r\n"; 
  var TR = "<tr>";
  var TR_days = "<tr class='dpDayTR'>";
  var TR_todaybutton = "<tr class='dpTodayButtonTR'>";
  var xTR = "</tr>"  ;
  var TD = "<td class='dpTD' onMouseOut='this.className=\"dpTD\";' onMouseOver='this.className=\"dpTDHover\";' ";    // leave this tag open, because we'll be adding an onClick event
  var TD_title = "<td colspan=5>";
  var TD_buttons = "<td >";
  var TD_todaybutton = "<td colspan=7 class='dpTodayButtonTD'>";
  var TD_days = "<td class='dpDayTD'>";
  var TD_selected = "<td class='dpDayHighlightTD' onMouseOut='this.className=\"dpDayHighlightTD\";' onMouseOver='this.className=\"dpTDHover\";' ";    // leave this tag open, because we'll be adding an onClick event
  var xTD = "</td>";
  var DIV_title = "<div class='PanelLabel'>";
  var DIV_selected = "<div class='dpDayHighlight'>";
  var xDIV = "</div>";
 
  var html=["<table cols=7 class='panel'>",
	// title bar
	TR,TD_buttons,getButtonCode(fldName,thisDay,-1,"&lt;"),xTD,
	TD_title,"<nobr>",monthArrLong[thisDay.getMonth()]," ",thisDay.getFullYear(),"</nobr>",xTD,
	TD_buttons,getButtonCode(fldName,thisDay,1,"&gt;"),xTD,xTR,
	// row that indicates which day of the week we're on
	TR_days,TD_days,dayArrShort.join(xTD+TD_days),xTD,xTR,
	// populating the table with days of the month
	TR];
  // first, the leading blanks
  for (i = weekBegin; i < thisDay.getDay(); i++)
    html.push("<td>&nbsp;</td>");
  html=html.join(''); 
  // now, the days of the month
  do {
    dayNum = thisDay.getDate();
    TD_onclick = [" onclick=\"updateDateField('",fldName,"','",date2String(thisDay),"');\">"].join("");    
    if (dayNum == day)
      html += [TD_selected,TD_onclick,DIV_selected,dayNum,xDIV,xTD].join("");
    else
      html += [TD,TD_onclick,dayNum,xTD].join("");    
    // if this is a Saturday/Dimanche, start a new row
    if (thisDay.getDay() == weekEnd)
      html += xTR + TR;    
    // increment the day
    thisDay.setDate(thisDay.getDate() + 1);
  } while (thisDay.getDate() > 1) 
  // fill in any trailing blanks
  if (thisDay.getDay() > 0) {
    for (i = 6; i > thisDay.getDay(); i--)
      html += "<td>&nbsp;</td>"; 
  }
  html += xTR;
  // button
  var today = new Date();
  var todayString = [todayIs,dayArrMed[today.getDay()],", ",monthArrMed[today.getMonth()]," ",today.getDate()].join("");
  html += [TR_todaybutton,TD_todaybutton,
  "<nobr><button class='Button2' onClick='refreshDatePicker(\"",fldName,"\");'>",thisMonth,"</button> ",
  "<button class='Button' onClick='updateDateField(\"",fldName,"\");'>",EvolLang.close,"</button></nobr>",
  xTD,xTR,"</table>"].join(''); 
  document.getElementById(datePickerDivID).innerHTML = html;
  // add an "iFrame shim" to allow the datepicker to display above selection lists
  adjustiFrame();
}

function getButtonCode(fldName, dateVal, adjust, label)
{
  var newMonth = (dateVal.getMonth () + adjust) % 12;
  var newYear = dateVal.getFullYear() + parseInt((dateVal.getMonth() + adjust) / 12);
  if (newMonth < 0) {
    newMonth += 12;
    newYear += -1;
  } 
  return ["<button class='Button' onClick='refreshDatePicker(\"",fldName,"\", ",newYear,", ",newMonth,");'>",label,"</button>"].join("");
}

function date2String(dateVal)
{
  var dayStr = "00" + dateVal.getDate();
  var monthStr = "00" + (dateVal.getMonth()+1);
  dayStr = dayStr.substring(dayStr.length - 2);
  monthStr = monthStr.substring(monthStr.length - 2);
 
  switch (dateFormat) {
    case "dmy" :
      return [dayStr,monthStr,dateVal.getFullYear()].join(dateSep);
    case "ymd" :
      return [dateVal.getFullYear(),monthStr,dayStr].join(dateSep);
    case "mdy" :
    default :
      return [monthStr,dayStr,dateVal.getFullYear()].join(dateSep);
  }
}

function string2Date(dateString)
{
  var dateVal,dArray,d,m,y; 
  try {
    dArray = splitDateString(dateString);
    if (dArray) {
      switch (dateFormat) {
        case "dmy" :
          d = parseInt(dArray[0], 10);
          m = parseInt(dArray[1], 10) - 1;
          y = parseInt(dArray[2], 10);
          break;
        case "ymd" :
          d = parseInt(dArray[2], 10);
          m = parseInt(dArray[1], 10) - 1;
          y = parseInt(dArray[0], 10);
          break;
        case "mdy" :
        default :
          d = parseInt(dArray[1], 10);
          m = parseInt(dArray[0], 10) - 1;
          y = parseInt(dArray[2], 10);
          break;
      }
      dateVal = new Date(y, m, d);
    } else if (dateString) {
      dateVal = new Date(dateString);
    } else {
      dateVal = new Date();
    }
  } catch(e) {
    dateVal = new Date();
  } 
  return dateVal;
}

function splitDateString(dateString)
{
  var dArray;
  if (dateString.indexOf("/") >= 0)
    dArray = dateString.split("/");
  else if (dateString.indexOf(".") >= 0)
    dArray = dateString.split(".");
  else if (dateString.indexOf("-") >= 0)
    dArray = dateString.split("-");
  else if (dateString.indexOf("\\") >= 0)
    dArray = dateString.split("\\");
  else
    dArray = false;
  return dArray;
}

function updateDateField(fldName, dateString)
{
  var targetDateField = document.getElementsByName (fldName).item(0);
  if (dateString)
    targetDateField.value = dateString;
 
  var pickerDiv = document.getElementById(datePickerDivID);
  if(pickerDiv!=null){
	pickerDiv.style.visibility = "hidden";
	pickerDiv.style.display = "none";
    adjustiFrame();
  }
  try{targetDateField.focus()}
		catch(err){} 
 
  // after the datepicker has closed, optionally run a user-defined function called
  // datePickerClosed, passing the field that was just updated as a parameter
  // (note that this will only run if the user actually selected a date from the datepicker)
  if ((dateString) && (typeof(datePickerClosed) == "function"))
    datePickerClosed(targetDateField);
}

function adjustiFrame(pickerDiv, iFrameDiv)
{
  var is_opera = (navigator.userAgent.toLowerCase().indexOf("opera") != -1);
  if (is_opera)
    return;  
  try {
    if (!document.getElementById(iFrameDivID)) {
      var newNode = document.createElement("iFrame");
      newNode.setAttribute("id", iFrameDivID);
      newNode.setAttribute("src", "javascript:false;");
      newNode.setAttribute("scrolling", "no");
      newNode.setAttribute ("frameborder", "0");
      document.body.appendChild(newNode);
    }
    
    if (!pickerDiv)
      pickerDiv = document.getElementById(datePickerDivID);
    if (!iFrameDiv)
      iFrameDiv = document.getElementById(iFrameDivID);
    
    try {
      var ifs=iFrameDiv.style;
      ifs.position = "absolute";
      ifs.width = pickerDiv.offsetWidth;
      ifs.height = pickerDiv.offsetHeight ;
      var dps=pickerDiv.style
      ifs.top = dps.top;
      ifs.left = dps.left;
      ifs.zIndex = dps.zIndex - 1;
      ifs.visibility = dps.visibility ;
      ifs.display = dps.display;
    } catch(e) {
    }
   } catch (ee) {
  } 
}

