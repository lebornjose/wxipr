
$(document).ready(function() {
  var changed, code, container, converter, create, current, defaultValue, editor, init, list, listHandle, listHide, markdown, menu, page, play, preview, save, session, update;
  editor = ace.edit("J_code");
  converter = new Markdown.Converter();
  code = $("#J_code");
  preview = $("#J_preview");
  container = $("#J_container");
  menu = $("#J_menu");
  listHandle = $("#J_listHandle");
  markdown = {};
  page = 0;
  defaultValue = "the new text here";
  changed = false;
  session = editor.getSession();
  markdown.count = 0;
  editor.setBehavioursEnabled(true);
  update = function(e) {
    var source;
    source = $.trim(session.getValue());
    preview.html(converter.makeHtml(source));
    return changed = true;
  };
  save = function() {
    var patt, result;
    if (!changed) {
      return false;
    }
    changed = false;
    markdown.content = session.getValue();
    if (markdown.content.length === markdown.count || markdown.content === defaultValue) {
      return;
    }
    patt = /(.+)\n?/;
    result = patt.exec(markdown.content);
    markdown.title = result[1].replace(/\#|\+/g, "");
    return $.ajax({
      "url": "/zento/markdown/save",
      "type": "post",
      "data": markdown,
      "success": function(id) {
        markdown.markdown_id = id;
        return window.localStorage["current"] = JSON.stringify(markdown);
      }
    });
  };
  create = function(e) {
    var target, val;
    target = $(e.currentTarget);
    markdown.markdown_id = "";
    if (target.hasClass("doc")) {
      return session.setValue("");
    }
    val = "# 这里填写标题\n\n===\n\n## 第一章\n\n---\n\n## 第一节\n+ 第一节内容\n\n---\n\n##第二节\n+ 第二节内容\n\n===\n\n## 第二章\n\n---\n\n## 第二章\n>   1. 测试看看\n>   2. 你好\n>   3. 第二页";
    return session.setValue(val);
  };
  list = function() {
    if (listHandle.display) {
      return listHide();
    }
    listHandle.display = true;
    listHandle.show();
    return $.ajax({
      "url": "/zento/markdown/list",
      "type": "post",
      "dataType": "JSON",
      "data": "page=" + page,
      "success": function(reback) {
        var current, li, _i, _len, _results;
        $("ul", listHandle).html("");
        _results = [];
        for (_i = 0, _len = reback.length; _i < _len; _i++) {
          current = reback[_i];
          listHandle.data(current.markdown_id, current);
          li = $('<li>' + current.title + '</li>');
          li.data("markdown", current);
          _results.push($("ul", listHandle).append(li));
        }
        return _results;
      }
    });
  };
  listHide = function() {
    if (listHandle.display) {
      listHandle.display = false;
      return listHandle.hide();
    }
  };
  init = function() {
    var view_h, view_w;
    view_h = $(window).height();
    view_w = $(window).width();
    container.height(view_h - 33);
    code.width(parseInt(view_w / 2) + 10);
    return preview.parent().width(parseInt(view_w / 2) - 80);
  };
  $(".doc", menu).click(create);
  $(".ppt", menu).click(create);
  $(".list", menu).click(list);
  listHandle.delegate("li", "click", function() {
    var play;
    markdown = $(this).data("markdown");
    session.setValue(markdown.content);
    play = $(".play", menu);
    return $("a", play).attr("href", "/zento/markdown/play/" + markdown.markdown_id);
  });
  setTimeout(init, 50);
  $(window).bind("unload", save);
  $(window).resize(init);
  setInterval(save, 3000);
  editor.setShowPrintMargin(true);
  session.setMode("ace/mode/markdown");
  editor.setTheme("ace/theme/monokai");
  session.setTabSize(4);
  session.setUseSoftTabs(true);
  session.setUseWrapMode(true);
  session.on("change", update);
  current = window.localStorage["current"];
  if (!!current) {
    markdown = JSON.parse(current);
    session.setValue(markdown.content);
    play = $(".play", menu);
    return $("a", play).attr("href", "/zento/markdown/play/" + markdown.markdown_id);
  } else {
    return session.setValue(defaultValue);
  }
});
