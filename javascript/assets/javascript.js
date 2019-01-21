var myData =data[theChoice];
var reslute;
var selectVal= document.getElementsByTagName('select');

function init(){
	document.body.style.backgroundImage="url('assets/image/pexels-photo-452740.jpg')";
	redirect();
	forms();
	fChoice();	
}

if(GetCookie('user_id') == null){
	//null means the cookie does not exist, first time here
	var getName=prompt('Hi! First time here, what is your name?','');
	document.write('<h2>Welcome, '+getName);
	SetCookie('user_id',getName);
			
	}
	else{
		//Returning visiton
		var getName = GetCookie("user_id");
		document.write('<h2>Welcome back '+ getName);

	}



// create the first dynamically select pull-down element.
function fChoice(){	
	
	if(myData != undefined){
	var divTag = document.createElement("div");	
	
	var htag = document.createElement("h1");
	if(ie7){
		htag.style.color= '#ff7733';
	}
	else{
		htag.setAttribute('style','color:#ff7733;');
	}
	var tnode = document.createTextNode(myData[0]);	
	htag.appendChild(tnode);
	divTag.appendChild(htag);
	
	var stag = document.createElement("select");
	stag.setAttribute('id','firstOp');
	
	if(ie7){
		stag.setAttribute('onchange',function(){nChoice(this)});
	}
	else{
		stag.setAttribute('onchange','nChoice(this)');
	}
	

	if(ie7){
		stag.style.fontSize = '18px';
		stag.style.width = '200px';
		stag.style.color = '#3d3d5c';
		stag.style.backgroundColor =  '#f0f5f5';
		
	}
	else{
		stag.setAttribute('style','font-size:18px;width:200px;color:#3d3d5c;background-color: #f0f5f5;');
	}
			
	
	for(var i = 1;i < myData.length;i++){
		var optag = document.createElement("option");
		optag.setAttribute('value',myData[i]);
		var opnode = document.createTextNode(myData[i]);
		optag.appendChild(opnode);
		stag.appendChild(optag);
	}
	
	divTag.appendChild(stag);
	document.body.appendChild(divTag);
	}
		
}



// create second dynamically select pull-down element depend upon the user's choice
function nChoice(nextSele){
	
	dChoice(nextSele);
	
//	console.log(document.getElementById("firstOp").value);
//	localStorage.setItem('first',document.getElementById('firstOp').value);
	
	theChoice=nextSele.options[nextSele.selectedIndex].value;
	
	myData = data[theChoice];
	
	if(theChoice == '-- Select -- '){
	
		document.body.style.backgroundImage="url('assets/image/pexels-photo-452740.jpg')";
	}
	else{
		if(theChoice == 'Hot'){
			document.body.style.backgroundImage="url('assets/image/pexels-photo-434213.jpg')";
		}
		else if(theChoice == 'Cold'){
			document.body.style.backgroundImage="url('assets/image/pexels-photo-1233319.jpg')";
		}
	}
	
	if(myData != undefined){
		
		var divT = document.createElement("div");
		
		
		var newHtag = document.createElement("h1");
		if(ie7){
			newHtag.style.color = '#ff7733';
		}
		else{
			newHtag.setAttribute('style','color:#ff7733;');
		}
		var newTnode = document.createTextNode(myData[0]);
		newHtag.appendChild(newTnode);
		divT.appendChild(newHtag);
	
		var newStag = document.createElement("select");
		if(ie7){
			newStag.setAttribute('onchange',function(){tChoice(this)});
		}
		else{
			newStag.setAttribute('onchange','tChoice(this)');
		}
		
		newStag.setAttribute('id','secondOp');
		
		if(ie7){
			newStag.style.fontSize = '18px';
			newStag.style.width = '200px';
			newStag.style.color = '#3d3d5c';
			newStag.style.backgroundColor= '#f0f5f5';
		}
		else{
			newStag.setAttribute('style','font-size:18px;width:200px;color:#3d3d5c;background-color: #f0f5f5;');
		}
		
		for(var i=1;i<myData.length; i++){
			var newOpt = document.createElement("option");
			newOpt.setAttribute('value',myData[i]);
			var newOPnode = document.createTextNode(myData[i]);
			newOpt.appendChild(newOPnode);
			newStag.appendChild(newOpt);
		}
		
		
		divT.appendChild(newStag);
		document.body.appendChild(divT);
		
				
	}
	
	
}


// create third dynamically select pull-down element depend upon the user's choice
function tChoice(selectN){
	dChoice(selectN);
	theChoice=selectN.options[selectN.selectedIndex].value;
	myData = data[theChoice];


	if(myData != undefined){
		//console.log(document.getElementById("secondOp").value);
		//localStorage.setItem('second',document.getElementById('secondOp').value);
		
		var divTT = document.createElement("div");
		
		
		var nHtag = document.createElement("h1");
		if(ie7){
			nHtag.style.color = '#ff7733';
		}
		else{
			nHtag.setAttribute('style','color:#ff7733;');
		}
		var nTnode = document.createTextNode(myData[0]);
		nHtag.appendChild(nTnode);
		divTT.appendChild(nHtag);
	
		var nStag = document.createElement("select");
		if(ie7){
			nStag.setAttribute('onchange',function(){tChoice(this)});
		}
		else{
			nStag.setAttribute('onchange','tChoice(this)');
		}
		
		nStag.setAttribute("id","thirdOp");
		
		if(ie7){
			nStag.style.fontSize = '18px';
			nStag.style.width = '200px';
			nStag.style.color = '#3d3d5c';
			nStag.style.backgroundColor= '#f0f5f5';
		}
		else{
			nStag.setAttribute('style','font-size:18px;width:200px;color:#3d3d5c;background-color: #f0f5f5;');
		}
		
		for(var i=1;i<myData.length; i++){
			var nOpt = document.createElement("option");
			nOpt.setAttribute('value',myData[i]);
			var nOPnode = document.createTextNode(myData[i]);
			nOpt.appendChild(nOPnode);
			nStag.appendChild(nOpt);
		}
		
		
		divTT.appendChild(nStag);
		document.body.appendChild(divTT);
		
	}
	else{
		getResult();
	}	
	
	//console.log(document.getElementById('thirdOp').value);
	//localStorage.setItem('third',document.getElementById('thirdOp').value);
}


//To remove the select pull-down based on the user's choice
function dChoice(dValue){
		while(dValue.parentNode != dValue.parentNode.parentNode.lastChild){
		dValue.parentNode.parentNode.removeChild(dValue.parentNode.parentNode.lastChild);
	}
}


function redirect(){
	if( !document.getElementById ){
			alert("This is an old browser. To use this page get a new one");
			window.document.location.href="http://outdatedbrowser.com/en";
		}
}

//create the localStorage
function storage(){

	if(localStorage){
	localStorage.setItem('first',document.getElementById('firstOp').value);
	localStorage.setItem('second',document.getElementById('secondOp').value);
	localStorage.setItem('third',document.getElementById('thirdOp').value);
	
	if( localStorage.getItem('first') || localStorage.getItem('second') || localStorage.getItem('third')) {
	console.log(localStorage.getItem('first'));
	document.getElementById('firstOp').value=localStorage.getItem('first');
	console.log(localStorage.getItem('second'));
	document.getElementById('secondOp').value=localStorage.getItem('second');
	console.log(localStorage.getItem('third'));
	document.getElementById('thirdOp').value=localStorage.getItem('third');
	}
	}
}

// Delete my localStorage
function checkClear(){
	localStorage.removeItem('first');
	localStorage.removeItem('second');
	localStorage.removeItem('third');
}

// Delete my cookies
function clearMyCookies(){
		
	DeleteCookie('user_id');
	DeleteCookie('hit_count');
	location.reload();
		
} 


//create the clear button for the localstorage and cookies
function forms(){
	var divText = document.createElement('div');
	

	if(ie7){
		divText.style.position = 'fixed';
		divText.style.bottom = '0';
	}
	else{
		divText.setAttribute('style','position:fixed;bottom:0;margin-right:10px;');
	}


	var clear = document.createElement('button');
	var bottomClear = document.createTextNode('Clear localStroage');
	clear.appendChild(bottomClear);
	clear.setAttribute('onclick','checkClear()');
	if(ie7){
		clear.style.border='2px solid black';
		clear.style.backgroundColor='#f0f5f5';
		clear.style.fontSize='18px';
		clear.style.cursor='pointer';
	}
	else{
		clear.setAttribute('style','border 2px solid black;background-color:#f0f5f5;font-size:18px;cursor: pointer;');
	}
	divText.appendChild(clear);

	var clearC = document.createElement('button');
	var bottomC = document.createTextNode('Clear Cookies');
	clearC.appendChild(bottomC);
	clearC.setAttribute('onclick','clearMyCookies()');
	if(ie7){
		clearC.style.border='2px solid black';
		clearC.style.backgroundColor='#f0f5f5';
		clearC.style.fontSize='18px';
		clearC.style.cursor='pointer';
	}
	else{
		clearC.setAttribute('style','border 2px solid black;background-color:#f0f5f5;font-size:18px;cursor: pointer;');
	}
	divText.appendChild(clearC);
	
	document.body.appendChild(divText);
	
	
}



//Print out the final result to the web page
function getResult(){	
	if(selectVal[0].value != '-- Select -- ' && selectVal[1].value != '-- Select --' && selectVal[2].value != '-- Select --'){
	var finalText=document.createElement('div');
	
	var result = document.createElement('p');
	result.setAttribute("id","context");
	if(ie7){
		result.style.fontSize='22px';
		result.style.color='#ff7733';
	}
	else{
		result.setAttribute('style','font-size: 23px;color:#ff7733;');
	}
	result.setAttribute('style','font-size: 23px;color:#ff7733;');
	
	var optionVal= document.createTextNode("Your favorite is "+selectVal[0].value+'. You choose a kind is '+selectVal[1].value+', and you select the '+selectVal[2].value+'.');
	
	result.appendChild(optionVal);
		
	var imag = document.createElement('img');
	var pic = getImage(selectVal);
	imag.setAttribute('src',pic);
	imag.setAttribute('onclick','view()');
	imag.setAttribute('onmouseover','moveLeft()');
	imag.setAttribute('onmouseout','stoptimer()');
	
	imag.setAttribute('id','imgs');
	if(ie7){
		imag.style.width='300px';
		imag.style.height='350px';
	}
	else{
		imag.setAttribute('style','width:300px;height:350px;');
	}	
	
	
	
	finalText.appendChild(result);
	finalText.appendChild(imag);
	document.body.appendChild(finalText);
	storage();
	}
	

}




//display the image in new window
function view(){
	var click = document.getElementById('imgs').src;
	viw = window.open(click,'viw','height=300,witdth=450, top=250, screenY=350,cursor=pointer');
}


function moveLeft(){
	
	var im = document.getElementById('imgs');

//	console.log(im);	
	im.style.position="relative";
	im.style.paddingLeft = "4px";
	timer = setTimeout("moveRight()",45);	
}


function moveRight(){
	var i = document.getElementById('imgs');
	i.style.paddingLeft = "0px";
	timer = setTimeout("moveLeft()",45);
	
}


function stoptimer()
{
	clearTimeout(timer)
}


//Return the image depend on the option
function getImage(img){
	if(img[0].value == 'Hot'){
		if(img[1].value == 'Coffee'){
			if(img[2].value == 'Macchiato'){
				return "assets/image/img1-1-1.png";
				
			}
			else if(img[2].value == 'Latte'){
				return "assets/image/img1-1-2.jpg";
				
			}
			else if(img[2].value == 'Mocha'){
				return "assets/image/img1-1-3.jpg";
				
			}
		}
		else if(img[1].value == 'Chocolate'){
			if(img[2].value == 'Hot Chocolate'){
				return "assets/image/img1-2-1.jpg";
				
			}
			else if(img[2].value == 'Caramel Chocolate'){
				return "assets/image/img1-2-2.jpg";
				
			}
			else if(img[2].value == 'Hot Cocoa-Nut'){
				return "assets/image/img1-2-3.jpg";
				
			}
		}
		else if(img[1].value == 'Fruit Drink'){
			if(img[2].value == 'Pumpkin Spice'){
				return "assets/image/img1-3-1.jpg";
				
			}
			else if(img[2].value == 'Steamed Apple Juice'){
				return "assets/image/img1-3-2.jpg";
				
			}
			else if(img[2].value == 'Tea and Cider'){
				return "assets/image/img1-3-3.jpg";
				
			}
			
		}
	}
	else if(img[0].value == 'Cold'){
		if(img[1].value == 'Milkshake'){
			if(img[2].value == 'Vanilla'){
				return "assets/image/img2-1-1.jpg";
				
			}
			else if(img[2].value == 'Strawberry'){
				return "assets/image/img2-1-2.jpg";
				
			}
			else if(img[2].value == 'Caramel'){
				return "assets/image/img2-1-3.jpg";
				
			}
		}
		else if(img[1].value == 'Tea'){
			if(img[2].value == 'Black Tea'){
				return "assets/image/img2-2-1.jpg";
				
			}
			else if(img[2].value == 'Green Tea'){
				return "assets/image/img2-2-2.jpg";
				
			}
			else if(img[2].value == 'Milk Tea'){
				return "assets/image/img2-2-3.jpg";
				
			}
		}
		else if(img[1].value == 'Sode'){
			if(img[2].value == 'Pepsi'){
				return "assets/image/img2-3-1.jpg";
				
			}
			else if(img[2].value == 'Sprite'){
				return "assets/image/img2-3-2.jpg";
				
			}
			else if(img[2].value == 'Fanta'){
				return "assets/image/img2-3-3.jpg";
				
			}
		}
	}	
	
}


function getCookieVal (offset) {
	var endstr = document.cookie.indexOf (";", offset);
	if (endstr == -1) { endstr = document.cookie.length; }
	return unescape(document.cookie.substring(offset, endstr));
}

function GetCookie (name) {
	var arg = name + "=";
	var alen = arg.length;
	var clen = document.cookie.length;
	var i = 0;
	while (i < clen) {
		var j = i + alen;
		if (document.cookie.substring(i, j) == arg) {
			return getCookieVal (j);
			}
		i = document.cookie.indexOf(" ", i) + 1;
		if (i == 0) break; 
		}
	return null;
}

function DeleteCookie (name,path,domain) {
	if (GetCookie(name)) {
		document.cookie = name + "=" +
		((path) ? "; path=" + path : "") +
		((domain) ? "; domain=" + domain : "") +
		"; expires=Thu, 01-Jan-70 00:00:01 GMT";
		}
}

function SetCookie (name,value,expires,path,domain,secure) {
  document.cookie = name + "=" + escape (value) +
    ((expires) ? "; expires=" + expires.toGMTString() : "") +
    ((path) ? "; path=" + path : "") +
    ((domain) ? "; domain=" + domain : "") +
    ((secure) ? "; secure" : "");
}
