$(document).ready(function(){
	
	//Plugin the text animation, the text title will move ever 0.7 second
	$('h1').funnyText({
		speed: 700,
		borderColor: 'black',
		activeColor: 'white',
		color: '#ff6600',
		direction: 'both'
		
	});
	// call a XHR - XML Http Request to get the information we need
	// Get all the about information
	myXhr('get',{path:'/about/'},'#about').done(function(json){
		var x="<h2>"+json.title+"</h2>";
		x+="<p>"+json.description+"</p>";
		x+="<q id='quote' style='color:#4d4d4d;'>"+json.quote+"</q>";
		x+="<p id='author' style='font-size:18px;'><span>-</span>"+json.quoteAuthor+"</p>";
		$("#about").html(x);
	});
	
	//Read the all minors information and the popup box will show the more details about the minor major.
	//The datas will return to #minContext
	myXhr('get',{path:'/minors/UgMinors/'},'#minor').done(function(json){
		var x='';
		

		$.each(json.UgMinors,function(i,minorItem){
		
			//	console.log($(this));
				
				x+='<div onclick="getMinor(this)" class="fourFaculty oneRow" data-name="'+minorItem.name+'">';
				x+= '<h3 style="color:#737373;padding-top:35px;text-align:center;">'+minorItem.title+'</h3></div>';
			
		});	
		$('#minContext').append(x);
		
	});
	
	
		//Read the undergraduate information and the popup box will show the more information about the undergraduate major 
	myXhr('get',{path:'/degrees/undergraduate/'},'#undergraduate').done(function(json){
			//got good data back in json
			//dump out all of the degree titles
			$.each(json.undergraduate,function(i, item){
				//console.log($(this));
				var x="";
				for (var i = 0; i < item.concentrations.length; i++) {
    						x += '<p style="line-height:0.3">'+item.concentrations[i] + "</p>";
				}
				
				if(item.degreeName =='wmc'){
					$('#wmCom').append('<h3>'+item.title+'</h3>'+'<p style="color:#595959">'+item.description+'</p>');
					$('#wmCom').click(function(){
						$.dialog({
						title: item.title+'<p style="text-align:center; color:#8c8c8c;">Concentrations</p><hr>',
						content:x,
						});
						
					});	
				}
				else if(item.degreeName =='hcc'){
					$('#hcCom').append('<h3>'+item.title+'</h3>'+'<p style="color:#595959">'+item.description+'</p>');
					$('#hcCom').click(function(){
						$.dialog({
						title: item.title + '<p style="text-align:center; color:#8c8c8c;">Concentrations</p><hr>',
						content: x,
						});
						
					});
				}
				else if(item.degreeName == 'cit'){
					$('#citCom').append('<h3>'+item.title+'</h3>'+'<p style="color:#595959">'+item.description+'</p>');
					$('#citCom').click(function(){
						$.dialog({
						title: item.title+'<p style="text-align:center; color:#8c8c8c;">Concentrations</p><hr>',
						content: x,
						});
						
					});
				}
			});
			
			
			
	});
	
	
	//Read the graduate information and the popup box will show the more information about the graduate major
	myXhr('get',{path:'/degrees/graduate/'},'#graduate').done(function(json){
		$.each(json.graduate,function(i, gradItem){
			//console.log($(this));
			
			if(gradItem.degreeName == 'ist'){
			
				var graX="";
			
				for (var u = 0; u < gradItem.concentrations.length; u++) {
    						graX += '<p style="line-height:0.3">'+gradItem.concentrations[u] + "</p>";
				}
				$('#ist').append('<h3>'+gradItem.title+'</h3>'+'<p>'+gradItem.description+'</p>');
				$('#ist').click(function(){
					$.dialog({
						title: gradItem.title+'<p style="text-align:center; color:#8c8c8c;">Concentrations</p><hr>',
						content:graX,
					});
				});
				
			}
			else if(gradItem.degreeName == 'hci'){
				var graH="";
			
				for (var u = 0; u < gradItem.concentrations.length; u++) {
    						graH += '<p style="line-height:0.3">'+gradItem.concentrations[u] + "</p>";
				}
				
				$('#hci').append('<h3>'+gradItem.title+'</h3>'+'<p>'+gradItem.description+'</p>');
				$('#hci').click(function(){
					$.dialog({
						title: gradItem.title+'<p style="text-align:center; color:#8c8c8c;">Concentrations</p><hr>',
						content:graH,
					});
				});
			}
			else if(gradItem.degreeName == 'nsa'){
				var graN="";
			
				for (var u = 0; u < gradItem.concentrations.length; u++) {
    						graN += '<p style="line-height:0.3">'+gradItem.concentrations[u] + "</p>";
				}
				$('#nsa').append('<h3>'+gradItem.title+'</h3>'+'<p>'+gradItem.description+'</p>');
				$('#nsa').click(function(){
					$.dialog({
						title: gradItem.title+'<p style="text-align:center; color:#8c8c8c;">Concentrations</p><hr>',
						content:graN,
					});
				});
			}else if(gradItem.degreeName == 'graduate advanced certificates'){	
				var cert = "";
				for (var u = 0; u < gradItem.availableCertificates.length; u++) {
    				cert += '<p style="line-height:0.3;color:#4d4d4d">'+gradItem.availableCertificates[u] + "</p>";
				}
				$('#certificates').append('<h2>Our Graduate Advanced Certificates</h2>'+cert);
			}
			
			
		});	
	});
	
	
	//Get the introduction employment information and display it to #introduction 
	myXhr('get',{path:'/employment/introduction/'},'#employment').done(function(json){
		$("#introduction").append("<h2>"+json.introduction.title+"</h2>");
		var x="";
		
		for (i in json.introduction.content) {
    		x += "<h2 style='color:#b36b00'>" + json.introduction.content[i].title + "</h2><hr>";
    		for (j in json.introduction.content[i].description) {
        		x += json.introduction.content[i].description[j];
    		}
		}		
		$("#introduction").append(x);
		
	});
	
	
	
	//Get the degree statistics from the employment
	//return the data to #degreeStat
	myXhr('get',{path:'/employment/degreeStatistics/'},'#employment').done(function(json){
		$("#degreeStat").append("<h2 style='color:#b36b00>"+json.degreeStatistics.title+"</h2><hr>");
		var x="";
		
		/*for (i in json.degreeStatistics.statistics) {
    		x +="<h2 style='color:#b36b00'>" + json.degreeStatistics.statistics[i].value + "</h2>";
    		for (j in json.degreeStatistics.statistics[i].description) {
        		x += json.degreeStatistics.statistics[i].description[j];
    		}
		}
		*/
		
		
		for (i in json.degreeStatistics.statistics) {
    		x +="<h2 style='color:#b36b00'>" + json.degreeStatistics.statistics[i].value + "</h2>"+'<p>'+json.degreeStatistics.statistics[i].description+'</p></div>';
		}
		$("#degreeStat").append(x);		

		
	});
	
	//Read employers information from the employment
	//show the information in #employers
	myXhr('get',{path:'/employment/employers/'},'#employment').done(function(json){
		$("#employers").append("<h2 style='color:#b36b00'>"+json.employers.title+"</h2><hr>");
	//	console.log(json.employers.employerNames[1]);
		var x="";
		for (i in json.employers.employerNames) {
    		x += "	-" +json.employers.employerNames[i]+"	" ;
    		
		}
		$("#employers").append(x);		

		
	});
	myXhr('get',{path:'/employment/careers/'},'#employment').done(function(json){
		$("#careers").append("<h2 style='color:#b36b00'>"+json.careers.title+"</h2><hr>");
		var x="";
		for (i in json.careers.careerNames) {
    		x += "	-" +json.careers.careerNames[i]+"	" ;
    		
		}
		$("#careers").append(x);		

		
	});
	
	
	//Get students coop history from the  /employment/coopTable/
	//Popup box would shows the all history 
	//return the information to #coopTable
	myXhr('get',{path:'/employment/coopTable/'},'#coopSect').done(function(json){
	console.log(json);
		$("#coopTable").append("<h2 style='color:#ffffff'>"+json.coopTable.title+"</h2>");		
		//	var text='<table><tr><th>Employer</th><th>Degree</th><th>City</th><th>term</th></tr><tr><th>'+coopItem.coopInformation.employer+'<th/><th>'+coopItem.coopInformation.degree+'</th><th>'+coopItem.coopInformation.city+'</th><th>'+coopItem.coopInformation.term+'</th></tr></table>';
	/*		$.each(json.coopTable, function(i,coopItem){
				$('#coopTable').append('<p>'+coopItem.coopInformation+'</p>');
			});*/
			var x="";
			
			for(i in json.coopTable.coopInformation){
				x += '<tr><td>'+json.coopTable.coopInformation[i].employer+'</td><td>'+json.coopTable.coopInformation[i].degree+'</td><td>'+json.coopTable.coopInformation[i].city+'</td><td>'+json.coopTable.coopInformation[i].term+'</td></tr>';
			}
			
		//	$('#coopSect').append(x);
		
		
		$('#coopTable').click(function(){
			ssi_modal.show({
				content:'<table><tr><th>Employer</th><th>Degree</th><th>City</th><th>term</th></tr>'+x+'</table>'
			});
		});
	});
	
	//Get enmployment history from the  /employment/employmentTable/
	//Popup box would shows the all history 
	//return the information to #emmploymentTable
	myXhr('get',{path:'/employment/employmentTable'},'#coopSect').done(function(json){
		$('#employmentTable').append("<h2 style='color:#ffffff'>"+json.employmentTable.title+"</h2>");	
		
		var x ="";
		for(i in json.employmentTable.professionalEmploymentInformation){
			x += '<tr><td>'+json.employmentTable.professionalEmploymentInformation[i].employer+'</td><td>'+json.employmentTable.professionalEmploymentInformation[i].degree+'</td><td>'+json.employmentTable.professionalEmploymentInformation[i].city+'</td><td>'+json.employmentTable.professionalEmploymentInformation[i].startDate+'</td></tr>';
		}
		
		$('#employmentTable').click(function(){
			ssi_modal.show({
				content:'<table><tr><th>Employer</th><th>Degree</th><th>City</th><th>Date</th></tr>'+x+'</table>'
			});
		});
	});	
	
	//Get the faculty and staff information
	//To click to get more detail
	//They will display in #faculty and #staff
	myXhr('get',{path:'/people/'},'#people').done(function(json){
		$('#peopleTitle').append("<h2>"+json.title+'</h2><p style="text-align:center;color:#4d4d4d">'+json.subTitle+'</p>');
		var x='';
		
		$.each(json.faculty,function(i,facultyItem){
		
			//	console.log($(this));
				
				x+='<div onclick="getFac(this)" class="fourFaculty falStyle" data-username="'+facultyItem.username+'">';
				x+= '<p>'+facultyItem.name+'</p></div>';
			
		});
	
		$('#faculty').append('<h2>Faculty</h2>'+x);
		
		
		var y='';
		
		$.each(json.staff,function(i,staffItem){
			y += '<div onclick="getStaff(this)" class="fourFaculty falStyle" data-username="'+staffItem.username+'">';
			y += '<p>'+staffItem.name+'</p></div>';
		});
		
		$('#staff').append('<h2>Staff</h2>'+y);
		
	});
	myXhr('get',{path:'/research/byInterestArea/'},'#interestResearch').done(function(json){
		
	//	console.log(json);
	
		var x="";
		
		$.each(json.byInterestArea,function(i, areaItem){
			//	console.log($(this));
				x += '<div onclick="getArea(this)" class="fourFaculty areaStyle" data-area="'+areaItem.areaName+'">';
				x += '<p>'+areaItem.areaName+'</p></div>';
		});
		
			$('#byInterestArea').append(x);	
	});
	
	//display the image who is interacte in research
	//the information will return to #facultyArea
	myXhr('get',{path:'/research/byFaculty/'},'#interestResearch').done(function(json){
		var x="";
		$.each(json.byFaculty,function(i, facuAreaItem){
			x += '<div style="cursor: pointer;" onclick="getFacArea(this)" class="fourFaculty" data-facuArea="'+facuAreaItem.username+'">';
			x += '<img width="120px" height="120px" src=https://ist.rit.edu/assets/img/people/'+facuAreaItem.username+'.jpg></div>';
			
		});
		$('#facultyArea').append(x);
	});
	
	
	//Read the resources from the /resources/
	//There have mutil boxes to show each information
	myXhr('get',{path:'/resources/'},'#resources').done(function(json){
		$("#resourceTitle").append('<h2>'+json.title+'</h2><p style="text-align:center;color:#4d4d4d">'+json.subTitle+'</p>');
			// 	Read the student resources information
			var x="";
			var place="";
			x += '<div style="cursor: pointer;" class="fourFaculty resStyle" data-sa="'+json.studyAbroad.title+'">';
			x += '<p>'+json.studyAbroad.title+'</p></div>';	
			$.each(json.studyAbroad.places, function(i, placeItem){
				place +='<h2 style="color: #47476b">'+placeItem.nameOfPlace+'</h2><p style="color:#595959">'+placeItem.description+'</p>';
			});
			var descr = '<p>'+json.studyAbroad.description+'</p>';
			
			//Pop up window of the study abroad when user to click
			$('#sa').click(function(){
				ssi_modal.show({
					title:json.studyAbroad.title,
					content: descr+place,
				});
			});
			
			$('#sa').append(x);
			
			//Read the Student Services information
			var sst='';
			var aa="";
			var faq = '';
			var pa="";
			var paNa="";
			var fa="";
			var istM="";
			var mai="";
			
						
			sst += '<div style="cursor: pointer;"  class="fourFaculty resStyle" data-ss="'+json.studentServices.title+'">';
			sst += '<p>'+json.studentServices.title+'</p></div>';
			
			//The information of Academic Advisors 
			faq += '<h3 style="color:#862d2d">'+json.studentServices.academicAdvisors.faq.title+'</h3><p><a href="'+json.studentServices.academicAdvisors.faq.contentHref+'">http://ist.rit.edu/assets/includes/resources/calls.php?area=advising</a></p>';
			aa += '<h2 style="color:#862d2d">'+json.studentServices.academicAdvisors.title+'</h2><p>'+json.studentServices.academicAdvisors.description+'</p>'+faq;
			
			//The information of Professonal Advisors
			$.each(json.studentServices.professonalAdvisors.advisorInformation, function(i,paItem){
				paNa +='<h3 style="color:#862d2d">'+paItem.name+'</h3><p style="color:#595959">'+paItem.department+'</p><p style="color:#595959">'+paItem.email+'</p>';
			});
			pa += '<h2 style="color:#862d2d">'+json.studentServices.professonalAdvisors.title+'</h2>'+paNa;
			
			
			//The information of Faculty Advisors
			fa += '<h2 style="color:#862d2d">'+json.studentServices.facultyAdvisors.title+'</h2><p>'+json.studentServices.facultyAdvisors.description+'</p>';
			
			
			//The information of IST Minor Advising
			$.each(json.studentServices.istMinorAdvising.minorAdvisorInformation, function(i,imItem){
				mai +='<p style="color:#595959">'+imItem.title+'</p><p style="color:#595959">'+imItem.advisor+'</p><p style="color:#595959">'+imItem.email+'</p><hr>';
			});
			istM += '<h2 style="color:#862d2d">'+json.studentServices.istMinorAdvising.title+'</h2>' +mai ;
						
			//Pop up the window when the user to click	
			$('#ss').click(function(){
				ssi_modal.show({
					title:json.studentServices.title,
					content: aa +pa + fa + istM
				});
			});
			
			$('#ss').append(sst);
			
			
			
			//The information of Tutors / Lab Information
			var ta="";
			var taW="";
			ta += '<div style="cursor: pointer;"  class="fourFaculty resStyle" data-ss="'+json.tutorsAndLabInformation.title+'">';
			ta += '<p>'+json.tutorsAndLabInformation.title+'</p></div>';
			taW += '<h2 style="color:#862d2d">'+json.tutorsAndLabInformation.title+'</h2><p>'+json.tutorsAndLabInformation.description+'</p><a href="'+json.tutorsAndLabInformation.tutoringLabHoursLink+'">http://www.istlabs.rit.edu</a></p>';
			$('#ta').click(function(){
				ssi_modal.show({
					title: json.tutorsAndLabInformation.title,
					content: taW
				});
			});	
			
			$('#ta').append(ta);
			
			
			//The information of Student Ambassador Information & Application
			var sam="";
			var samW ="";
			var samWW="";
			sam += '<div style="cursor: pointer;"  class="fourFaculty resStyle" data-ss="'+json.studentAmbassadors.title+'">';
			sam += '<p>'+json.studentAmbassadors.title+'</p></div>';
			
			$.each(json.studentAmbassadors.subSectionContent, function(i,samItem){
				samW +='<h2 style="color:#595959">'+samItem.title+'</h2><p style="color:#595959">'+samItem.description+'</p><hr>';
			});
			
			samWW += '<h2 style="color:#862d2d">'+json.studentAmbassadors.title+'</h2><p><a href="'+json.studentAmbassadors.ambassadorsImageSource+'">http://ist.rit.edu/assets/img/resources/Ambassadors.jpg</a></p>' + samW +'<p><a href="'+json.studentAmbassadors.applicationFormLink+'">http://goo.gl/forms/PIL1T1JGdm</a></p><p>'+json.studentAmbassadors.note+'</p>';
			
			$('#sam').click(function(){
				ssi_modal.show({
					title: json.studentAmbassadors.title,
					content: samWW
				});
			});
			
			$('#sam').append(sam);
			
			
			//The information of form
			var graForm= "";
			var undeForm="";
			var form="";
			form += '<div style="cursor: pointer;"  class="fourFaculty resStyle"><p>Form</p></div>' ;
			
			// graduate forms
			$.each(json.forms.graduateForms, function(i,grItem){
				console.log(grItem.href);
				graForm +='<p><a href="'+grItem.href+'"download>'+grItem.formName+'</a></p><hr>';
			});
			var formTitle ='<h3 style="color:#595959">Graduate</h3>';
			
			// undergraduate Forms
			$.each(json.forms.undergraduateForms, function(i,unItem){
				
				undeForm +='<p><a href="'+unItem.href+'"download>'+unItem.formName+'</a></p><hr>';
			});
			var formT = '<h3 style="color:#595959">undergraduate</h3>';
		
			
			$('#forms').click(function(){
				ssi_modal.show({
					title: 'From',
					content: formTitle+graForm+formT+undeForm
				});
			});
			
			$('#forms').append(form);
			
			
			
			//The information of coopEnrollment
			var coop="";
			var enrol="";
			var en="";
			coop += '<div style="cursor: pointer;"  class="fourFaculty resStyle" data-ss="'+json.studentAmbassadors.title+'">';
			coop += '<p>'+json.coopEnrollment.title+'</p></div>';
			
			$.each(json.coopEnrollment.enrollmentInformationContent, function(i,coItem){
				enrol +='<h2 style="color:#595959">'+coItem.title+'</h2><p style="color:#595959">'+coItem.description+'</p><hr>';
			});
			
			en += '<h3 style="color:#862d2d">'+json.coopEnrollment.title+'</h3>'+enrol+'<p><a href="'+json.coopEnrollment.RITJobZoneGuidelink+'">http://www.rit.edu/emcs/oce/student/stu_alum_pdfs/RITJobZoneGuide.pdf</a></p>';
			
			$('#ce').click(function(){
				ssi_modal.show({
					title: json.coopEnrollment.title,
					content: en
				});
			});
			
			$('#ce').append(coop);
			
			//console.log(json);
	});
	
	myXhr('get',{path:'/footer/'},'#foot').done(function(json){
		$("#byFoot1").append("<h2 style='color: #cc6600'>"+json.social.title+"</h2>");
		$('#byFoot1').append('<p style="text-align:center">'+json.social.tweet+'</p><p style="text-align:center">'+json.social.by+'</p><p style="text-align:center;"><a href="'+json.social.twitter+'"><img src="assets/image/Twitter-High-Quality-PNG.png" width="50" height="50"></a><a href="'+json.social.facebook+'"><img src="assets/image/facebook_circle-512.png" width="50" height="50" style="margin-left: 20px"></a></p>');
		
		var x="";
		
		$.each(json.quickLinks, function(i,oItem){
    		x += "<a href='" + oItem.href+ "'>"+oItem.title +"</a> | ";
		});	
		$("#byFoot2").append('<p style="text-align:center">'+x+'<a href="'+json.copyright.news+'">News</a>');
		
		
		$("#byFoot2").append('<p style="text-align:center">'+json.copyright.title+'</p>'+json.copyright.html);
		
	});
	
	
	
			
});


//To determine the name of UgMinors 
//Show the information which click by the user
function getMinor(minr){
	myXhr('get',{path:'/minors/UgMinors/name='+$(minr).attr('data-name')},null).done(function(json){
		console.log(json);
		var course = '';
		
		for (var i = 0; i < json.courses.length; i++) {
    			course+= '<p style="line-height:0.3;text-align:center; color: #ffa64d">'+json.courses[i] + "</p>";
		}
		var txtContent='<p>'+json.description+'</p>'+course+'<p>'+json.note+'</p>';
		
		ssi_modal.show({
			title:json.title,
			content: txtContent,
		});
	});
}

//To determine the username of faculty 
//Show the information which click by the user
function getFac(dom){
		myXhr('get',{path:'/people/faculty/username='+$(dom).attr('data-username')},null).done(function(json){
			console.log(json);

			
			var img = '<p><img src="'+json.imagePath+'" style="float:left; margin-right:15px"/>';
			var text = '<p>Office: '+json.office+'</p><p>Phone: '+json.phone+'</p><p>Email: '+json.email+'</p><p>Tagline: '+json.tagline+'</p><p>Interest Area: '+json.interestArea+'</p><p>Website: '+json.website+'</p><p>Twitter: '+json.twitter+'</p><p>Facebook: '+json.facebook+'<p>';
			$.dialog({
					title:json.name+", "+json.title+'<hr>',
					content: img +text,
									
				});
		});
}

//To determine the username of staff 
//Show the information which click by the user
function getStaff(dom){
		myXhr('get',{path:'/people/staff/username='+$(dom).attr('data-username')},null).done(function(json){
			console.log(json);

			
			var img = '<p><img src="'+json.imagePath+'" style="float:left; margin-right:15px"/>';
			var text = '<p>Office: '+json.office+'</p><p>Phone: '+json.phone+'</p><p>Email: '+json.email+'</p><p>Tagline: '+json.tagline+'</p><p>Interest Area: '+json.interestArea+'</p><p>Website: '+json.website+'</p><p>Twitter: '+json.twitter+'</p><p>Facebook: '+json.facebook+'<p>';
			$.dialog({
					title:json.name+", "+json.title+'<hr>',
					content: img +text,
									
				});
		});
}
	
	
//To determine the areaname of byInterestArea
//popup the information which click by the user
function getArea(inter){
		myXhr('get',{path:'/research/byInterestArea/areaName='+$(inter).attr('data-area')},null).done(function(json){
		
		//console.log(json);
		
		var x="";
		for(i in json.citations){
			x += '<p>'+json.citations[i]+'</p>';
		}
		
		ssi_modal.show({
			title: json.areaName,
			content: x,
		});
		
	});
	
}	

//To determine the username of byFaculty
//popup the information which click by the user
function getFacArea(faArea){
	myXhr('get',{path:'/research/byFaculty/username='+$(faArea).attr('data-facuArea')},null).done(function(json){
		var x="";

		for(i in json.citations){
			x += '<p>'+json.citations[i]+'</p>';
		}
		
		ssi_modal.show({
			title:json.facultyName,
			content: x,
		});
	});	
}	

//(getOrPost, data, idForSpinner)
function myXhr( t, d, id ){
	return $.ajax({
		type:t,
		url:'proxy.php',
		dataType:'json',
		data:d,
		cache:false,
		async:true
	}).fail(function(){
		// handle failure
	});
	
} 