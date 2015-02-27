$(document).ready(function(){
	/*鼠标移过，左右按钮显示*/
	$(".index-anmation").hover(function(){
		$(this).find(".prev,.next").fadeTo("show",0.1);
	},function(){
		$(this).find(".prev,.next").hide();
	})
	/*鼠标移过某个按钮 高亮显示*/
	$(".prev,.next").hover(function(){
		$(this).fadeTo("show",0.7);
	},function(){
		$(this).fadeTo("show",0.1);
	})
	$(".index-anmation").slide({ titCell:".num ul" , mainCell:".pic" , effect:"fold", autoPlay:true, delayTime:700 , autoPage:true });	
})
