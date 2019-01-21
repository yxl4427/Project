//side bar menu
function change(){ 
	var secondDiv = this.parentNode.getElementsByTagName("ul")[0]; 

	if(secondDiv.className == "myHide") 
		secondDiv.className = "myShow"; 
	else 
		secondDiv.className = "myHide"; 
} 


window.onload = function(){ 
	var row = document.getElementById("content"); 
	var col = row.childNodes;
	var a; 
		for(var i=0;i<col.length;i++){ 
			if(col[i].tagName == "LI" && col[i].getElementsByTagName("ul").length){ 
				a = col[i].firstChild;
				a.onclick = change;
			} 
	} 
}
 
 
//screen size of click to open the navigation menu 
function openNav() {
    document.getElementById("mySidenav").style.width = "100%";
}

//close the side navigation menu
function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
}

//image become large
function bigImg(x) {
    x.style.width = "400px";
    x.style.height = "350px";

}

//image become small
function normalImg(x) {
    x.style.width = "350px";
    x.style.height = "250px";

}