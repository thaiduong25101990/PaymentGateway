/*--------------------------
CREATED BY NGUYEN THANH TUNG
----------------------------*/
//Function confirmed to delete
function confirmdel(type) {
	var st;
	st=false;
	if (parseInt(type)==1) {
		st=confirm("Bạn có chắc chắn xoá không?(Y/N)");
	} else {
		st=confirm("Are you sure to delete?(Y/N)");
	}
	return st;
}
//Function confirmed to edit
function confirmedit(type) {
	var st;
	st=false;
	if (parseInt(type)==1) {
		st=confirm("Bạn có chắc chắn muốn sửa đổi không?(Y/N)");
	} else {
		st=confirm("Are you sure to modify?(Y/N)");
	}
	return st;
}
//Function filtered textbox input
function filterinput(str) {
	var key=String.fromCharCode(event.keyCode).toLowerCase();
	if (str.indexOf(key)<0) {
		return false;
	} else {
		return true;
	}
}
//Function bounded the filed length
function masklength(textbox,len) {
	if (parseInt(textbox.value.length)>parseInt(len)) {
		return false;
	} else {
		return true;
	}
}
//Change row colour when click
var cssrow;
var tblid;
var rowid;
cssrow="";
tblid="";
rowid="";

function changerow(stblid,srowid,cssclass,tableid,mode) {
	var stbl;
	if ((tblid!="") && (rowid!="") && (cssrow!="")) {
		stbl=eval("document.getElementById('" + tblid + "')");
		stbl.rows[rowid].className=cssrow;
	}
	stbl=eval("document.getElementById('" + stblid + "')");
	tblid=stblid;
	rowid=srowid;
	cssrow=stbl.rows[srowid].className;
	stbl.rows[srowid].className=cssclass;
	if (mode==0) {
		document.getElementById('_ctl0:txtID').value=tableid;
		void(0);
	}
	if (mode==1) {
		GetCUSTID(tableid);
		void(0);
	}
	if (mode==2) {
		GetCONTRACT(tableid);
		void(0);
	}
	if (mode==3) {
		GetCDNO(tableid);
		void(0);
	}
	if (mode==4) {
		GetUserID(tableid);
		void(0);
	}
	if (mode==5) {
		GetTypeID(tableid);
		void(0);
	}
	if (mode==6) {
		GetNotice(tableid);
		void(0);
	}
	return true;
	return true;
}
/*--------------------
END CREATED
----------------------*/