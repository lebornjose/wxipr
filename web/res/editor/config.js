CKEDITOR.editorConfig = function( config )
{
	config.language = 'zh-cn';
	config.disableNativeSpellChecker=false;
	config.font_names = '宋体;楷体_GB2312;新宋体;黑体;隶书;幼圆;微软雅黑;Arial;Comic Sans MS;Courier New;Tahoma;Times New Roman;Verdana;';
	config.filebrowserImageUploadUrl = '/base/home/ckupload/jpg/jabinfo';
	config.filebrowserFlashUploadUrl = '/base/home/ckupload/swf/jabinfo';
	config.toolbar_Full =
	[
		['Source','Preview'],
		['Cut','Copy','Paste','PasteText','PasteFromWord'],
		['Undo','Redo','-','Find','Replace','SelectAll','RemoveFormat','CreateDiv'],['Styles','Format','Font','FontSize'],
		'/',
		['Bold','Italic','Underline','Strike','Subscript','Superscript'],
		['NumberedList','BulletedList','Outdent','Indent','Blockquote'],
		['JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock'],
		['Link','Unlink','Anchor'],
		['Image','Flash','Table','HorizontalRule','SpecialChar'],['TextColor','BGColor'],['Maximize', 'ShowBlocks','Templates']
	];
};