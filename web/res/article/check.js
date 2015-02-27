function selectAll(){
 var checklist = document.getElementsByName ("selected");
   if(document.getElementById("controlAll").checked)
   {
   for(var i=0;i<checklist.length;i++)
   {
      checklist[i].checked = 1;
   } 
   }else{
     for(var j=0;j<checklist.length;j++)
     {
       checklist[j].checked = 0;
     }
   }
}

$(this).ready(function(){
  
  $("input[type='checkbox']").click(function(){
    var str="//crm/article/batch/";
    var str1="//crm/article/upload/";
      var r=document.getElementsByName("selected"); 
      for(var i=0;i<r.length;i++){
           if(r[i].checked){
          str+=r[i].value+",";
          str1+=r[i].value+",";
         }
      }   
    var string=str.substring(1);
    var string1=str1.substring(1);
    $("#articles").attr("href",string);
    $("#articles1").attr("href",string1);
  });
});