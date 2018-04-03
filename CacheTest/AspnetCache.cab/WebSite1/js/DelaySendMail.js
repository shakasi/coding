
// 用户输入的调整记录的原因
var g_reason = null;

$(function(){
	$("#btnMoveUp").click( function() { MoveRec(-1); } );
	$("#btnMoveDown").click( function() { MoveRec(1); } );
});

function MoveRec(direction){
	if( ~~($("#spanSequence").text()) + direction < 0 ){
		alert("已经不能上移了。");
		return;
	}
	if( g_reason == null ){
		g_reason = prompt("请输入调整记录顺序的原因：", "由于什么什么原因，我要调整...");
		if( g_reason == null )
			return;
	}
	
	$.ajax({
		url: "/AjaxDelaySendMail/MoveRec.fish",
		data: { RowGuid: $("#spanRowGuid").text(), 
				Direction: direction,
				Reason: g_reason
		},
		type: "POST", dataType: "text",
		success: function(responseText){
			$("#spanSequence").text(responseText);
		}
	});
}

