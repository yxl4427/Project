

<?php
	$path = './';
	include $path.'header.php';
?>



<h1>Comments</h1>
	<?php 
	include $path.'nav.php';
	?>
	
	<div class="box1">
	<div class="commBord">
	<form action="comments.php" method="get" class="bord">		
	Name: <input type="text" id="name" name="Name"/><br/>
	Comment: <br/>
	<textarea id="comment" rows="4" cols="40" name="Comment"/></textarea>
	<br/>
	<input type="submit" value="Submit"/>
	</form>
	
	
	
	<div>
	
	
	


<?php
  	include "../240dbConn.php";
	if ($conn) {
	  //if we are adding a new user
	  if( $_GET['Name']!='' && $_GET['Comment']!='' ){
		$stmt=$conn->prepare("insert into comment (name, Comment) values (?, ?)");
		$stmt->bind_param("ss",$_GET['Name'],$_GET['Comment']);
		$stmt->execute();
		$stmt->close();
	  }
	  //get contents of table and send back
	  $result=$conn->query('select name, comment from comment');
	  if($result){
		while($rowHolder = $result->FETCH_ASSOC()){
			$records[] = $rowHolder;
		}
	  }
	  
	  
	  foreach($records as $curr){
			echo 'Name: '.$curr['name'].'<br>'.'Comment: '.$curr['comment'].'<br>'.'<hr>';
			
	  }
	}
?>
</div>

	</div>
	</div>
		 
<?php 
	include $path.'footer.php';
?>